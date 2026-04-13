using System;
using Moedelo.CommonV2.Webpack.Enums;
using Moedelo.CommonV2.Webpack.Services;

namespace Moedelo.CommonV2.Webpack
{
    public static class WebpackInitializer
    {
        private static IWebpackService _webpackService;

        public static string StaticPath => _webpackService.GetStaticHost();

        public static void Init(Func<IWebpackService> action)
        {
            _webpackService = action.Invoke();
        }

        public static void Init(IWebpackService service)
        {
            _webpackService = service;
        }

        public static string RenderJs(string bundle, string app = "")
        {
            return _webpackService.Render(app, bundle, RenderType.Js);
        }

        public static string RenderCss(string bundle, string app = "")
        {
            return _webpackService.Render(app, bundle, RenderType.Css);
        }

        public static string RenderWlCss(string app = "")
        {
            return _webpackService.RenderWlCss(app);
        }
    }
}