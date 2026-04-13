using System;
using System.Configuration;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Web;

namespace Moedelo.InfrastructureV2.Mvc;

/// <summary>
/// Реализация проверки конфигурации Razor view engine.
/// Выполняет функциональную проверку (smoke test) путём реальной компиляции .cshtml файла через BuildManager.
/// Это гарантирует, что Razor правильно сконфигурирован и может обрабатывать views.
/// </summary>
/// <remarks>
/// Проверка выполняется потокобезопасно и кэширует результат - при успешной проверке последующие вызовы не выполняют проверку повторно.
/// При ошибке конфигурации проверка будет выполняться при каждом вызове до тех пор, пока проблема не будет исправлена.
/// </remarks>
[InjectAsSingleton(typeof(IRazorEngineConfigCheck), typeof(IWebAppConfigCheck))]
internal sealed class RazorEngineConfigCheck : IRazorEngineConfigCheck
{
    private const string Tag = nameof(RazorEngineConfigCheck);
    
    private readonly object checkingLock = new();
    private bool isChecked;
    private readonly ILogger logger;

    public RazorEngineConfigCheck(ILogger logger)
    {
        this.logger = logger;
    }

    /// <inheritdoc />
    public void Check()
    {
        if (isChecked) return;

        try
        {
            CheckRazorEngineConfiguredCorrectly();

            // если не было исключений - всё хорошо
            isChecked = true;
        }
        catch (ConfigurationErrorsException ex)
        {
            throw new InvalidOperationException(
                $"Razor view engine configuration error: {ex.Message}. Views cannot be processed!", ex);
        }
        catch (System.IO.FileLoadException ex) when (ex.Message.Contains("System.Web.WebPages") ||
                                                     ex.Message.Contains("System.Web.Mvc") ||
                                                     ex.Message.Contains("System.Web.Razor"))
        {
            throw new InvalidOperationException(
                $"Failed to load Razor assemblies: {ex.Message}. Views cannot be processed!", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("System.Web.WebPages") ||
                                              ex.Message.Contains("System.Web.Mvc"))
        {
            throw new InvalidOperationException(
                $"Assembly version conflict in Razor: {ex.Message}. Views cannot be processed!", ex);
        }
        catch (System.Web.HttpException ex) when (ex.Message.Contains("System.Web.WebPages") ||
                                                  ex.Message.Contains("System.Web.Mvc"))
        {
            throw new InvalidOperationException(
                $"Failed to compile view: {ex.Message}. Views cannot be processed!", ex);
        }
    }

    private void CheckRazorEngineConfiguredCorrectly()
    {
        lock (checkingLock)
        {
            if (isChecked)
                return;

            // Функциональная проверка: используем BuildManager для компиляции .cshtml
            // Это вызовет полную инициализацию Razor, включая чтение Views\Web.config

            var viewsPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Views");
            if (!System.IO.Directory.Exists(viewsPath))
            {
                logger.Debug(Tag, "Views directory not found. Skipping Razor configuration check.");
                return;
            }

            // Ищем любой .cshtml файл
            var cshtmlFiles =
                System.IO.Directory.GetFiles(viewsPath, "*.cshtml", System.IO.SearchOption.AllDirectories);
            if (cshtmlFiles.Length == 0)
            {
                logger.Debug(Tag, "No .cshtml files found in Views directory. Skipping Razor configuration check.");
                return;
            }

            // Берём первый найденный view
            var firstView = cshtmlFiles[0];
            var relativePath = firstView
                .Substring(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.Length)
                .Replace("\\", "/");
            var virtualPath = "~/" + relativePath.TrimStart('/');

            // Используем BuildManager для компиляции view - это вызовет чтение Views\Web.config!
            _ = System.Web.Compilation.BuildManager.GetCompiledType(virtualPath);

            // Успешно скомпилировали - Razor работает!
            logger.Info(Tag, "Razor view engine is configured correctly.");
        }
    }
}
