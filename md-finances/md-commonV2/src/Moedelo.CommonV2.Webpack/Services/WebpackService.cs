using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Moedelo.CommonV2.Webpack.Enums;
using Moedelo.CommonV2.Webpack.Extensions;
using Moedelo.CommonV2.Webpack.Helper;
using Moedelo.CommonV2.Webpack.Services.Renderers;
using Moedelo.CommonV2.WhiteLabel.Services;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Consul;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Json;
using Moedelo.InfrastructureV2.Setting;

namespace Moedelo.CommonV2.Webpack.Services
{
    [InjectAsSingleton(typeof(IWebpackService))]
    public class WebpackService : IWebpackService
    {
        private const string Domain = "global/webCache";

        private readonly string tag;
        private readonly string appStaticName;
        private static readonly ConcurrentDictionary<string, IReadOnlyDictionary<string, string>> ManifestsDictionary =
            new ConcurrentDictionary<string, IReadOnlyDictionary<string, string>>();

        private readonly ICssRenderer cssRenderer;
        private readonly IJavaScriptRenderer javaScriptRenderer;
        private readonly ILogger logger;
        private readonly IWhiteLabelService whiteLabelService;
        private readonly SettingValue staticHostUrl;
        private readonly IConsulCatalogWatcher consulCatalogWatcher;
        private readonly ISettingsConfigurations settingsConfigurations;
        
        static WebpackService()
        {
            // включение поддержки TLS 1.2
            if ((ServicePointManager.SecurityProtocol & SecurityProtocolType.Tls12) == 0)
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
                // добавить SecurityProtocolType.Tls13, когда перейдём на 4.8 
            }
        }

        public WebpackService(
            ICssRenderer cssRenderer,
            IJavaScriptRenderer javaScriptRenderer,
            ILogger logger,
            IWhiteLabelService whiteLabelService,
            IConsulCatalogWatcher consulCatalogWatcher,
            ISettingRepository settingRepository,
            ISettingsConfigurations settingsConfigurations)
        {
            tag = GetType().Name;
            this.cssRenderer = cssRenderer;
            this.javaScriptRenderer = javaScriptRenderer;
            this.logger = logger;
            this.whiteLabelService = whiteLabelService;
            this.staticHostUrl = settingRepository.Get("WebStaticsUrl");
            this.consulCatalogWatcher = consulCatalogWatcher;
            this.settingsConfigurations = settingsConfigurations;

            appStaticName = ConfigurationManager.AppSettings["appStaticName"];

            WatchClearCache();
        }

        private void WatchClearCache()
        {
            var waitForInitTaskList = new[] { Task.CompletedTask };

            waitForInitTaskList[0] = consulCatalogWatcher.AddWatchCatalogAsync(
                $"{settingsConfigurations.Config.Environment}/{Domain}/",
                settings =>
                {
                    if (settings == null || !settings.Any()) return;

                    // Текущие версии кэша в сервисе
                    var versions = GetVersions();

                    foreach (var app in settings.Where(x => !string.IsNullOrEmpty(x.Key)))
                    {
                        // Пробегаем аппы в консуле и ищем тот который есть в нашей системе и версия из кэша не равна версии из консула
                        if (!versions.ContainsKey(app.Key) || versions[app.Key] == app.Value) continue;

                        if (ManifestsDictionary.TryRemove(app.Key, out _))
                        {
                            logger.Info(tag,
                                $"Web Cache Clear. App Name: {app.Key}. Old Version: {versions[app.Key]}. New Version: {app.Value}.");
                        }
                    }
                }, 10);

            Task.WaitAll(waitForInitTaskList);
        }

        public string Render(string app, string bundle, RenderType type)
        {
            var url = HttpContext.Current.Request.Url.ToString();
            app = string.IsNullOrEmpty(app) ? appStaticName : app;
            var resultBundle = $"{GetStaticHost()}/{app}/{GetBundleFromManifest(app, bundle.CheckFileExtension(type), GetStaticHost(), url)}";

            return RenderBundle(app, bundle, type, resultBundle);
        }

        public string RenderWlCss(string app)
        {
            app = string.IsNullOrEmpty(app) ? appStaticName : app;
            var host = HttpContext.Current.Request.Url.Host;
            var whiteLabelName = whiteLabelService.GetNameByHost(host);

            if (string.IsNullOrEmpty(whiteLabelName))
                return null;

            var resultBundle = $"{GetStaticHost()}/{app}/{GetBundleFromManifest(app, whiteLabelName.CheckFileExtension(RenderType.Css), staticHostUrl.Value, HttpContext.Current.Request.Url.ToString())}";

            return RenderBundle(app, whiteLabelName, RenderType.Css, resultBundle);
        }

        public string GetStaticHost()
        {
            return WebstaticUrlHelper.GetUrl(staticHostUrl.Value, HttpContext.Current.Request.Url.Host);
        }

        public void ClearCache()
        {
            ManifestsDictionary.Clear();
        }

        public IDictionary<string, string> GetVersions()
        {
            if (ManifestsDictionary.IsEmpty)
            {
                return new Dictionary<string, string>();
            }

            return ManifestsDictionary.Select(
                xx => new { AppName = xx.Key, Version = xx.Value.First(xxx => xxx.Key == "_version").Value }
            ).ToDictionary(x => x.AppName, x => x.Version);
        }

        private string GetBundleFromManifest(
            string appName,
            string bundleName,
            string staticHost,
            string currentUrl = null)
        {
            try
            {
                if (ManifestsDictionary.TryGetValue(appName, out var appManifests)
                    && appManifests.TryGetValue(bundleName, out var manifest))
                {
                    return manifest.TrimStart('/');
                }

                if (GetManifestByApp(appName, staticHost).TryGetValue(bundleName, out var justLoadedManifest))
                {
                    return justLoadedManifest.TrimStart('/');
                }

                return null;
            }
            catch (Exception e)
            {
                logger.Error(tag, $"appName:{appName}, bundleName:{bundleName}, staticHost:{staticHost}, currentUrl:{currentUrl}", e);
                throw;
            }
        }

        private Dictionary<string, string> GetManifestByApp(string appName, string staticHost)
        {
            using (var wc = new WebClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var manifestUrl = $"{staticHost}/{appName}/manifest.json";
                try
                {
                    logger.Debug(tag, $"Start downloading manifest from {manifestUrl}", extraData: new { appName });
                    var json = wc.DownloadString(manifestUrl);
                    var manifest = json.FromJsonString<Dictionary<string, string>>();
                    AddOrUpdateManifest(appName, manifest);
                    logger.Debug(tag, $"Downloaded manifest from {manifestUrl}", extraData: new { appName });

                    return manifest;
                }
                catch (WebException webException)
                {
                    logger.Error(
                        tag,
                 $"Manifest downloading failed from {manifestUrl}",
                        webException, extraData: new { manifestUrl, appName, staticHost, webException });
                    throw;
                }
                catch (Exception exception)
                {
                    logger.Error(tag, $"General error in GetManifestByApp for url: {manifestUrl}",
                        exception, extraData: new { manifestUrl, appName, staticHost, exception });
                    throw;
                }
            }
        }

        private static void AddOrUpdateManifest(string appName, Dictionary<string, string> manifest)
        {
            if (ManifestsDictionary.ContainsKey(appName))
            {
                ManifestsDictionary[appName] = manifest;
            }
            else
            {
                ManifestsDictionary.TryAdd( appName, manifest );
            }
        }

        private string RenderBundle(string app, string bundleName, RenderType type, string bundleContent)
        {
            try
            {
                switch (type)
                {
                    case RenderType.Js:
                    {
                        return javaScriptRenderer.Render(bundleContent);
                    }
                    case RenderType.Css:
                    {
                        return cssRenderer.Render(bundleContent);
                    }
                    default:
                        throw new NotSupportedException();
                }
            }
            catch (Exception e)
            {
                logger.Error(tag, $"Error in Render for: {app}, {bundleName}, {type}", e);
                throw;
            }
        }
    }
}