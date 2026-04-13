using System.Collections.Generic;
using Moedelo.CommonV2.Webpack.Enums;

namespace Moedelo.CommonV2.Webpack.Services
{
    public interface IWebpackService
    {
        string Render(string app, string bundle, RenderType type);

        string RenderWlCss(string app);

        string GetStaticHost();

        void ClearCache();

        IDictionary<string, string> GetVersions();
    }
}