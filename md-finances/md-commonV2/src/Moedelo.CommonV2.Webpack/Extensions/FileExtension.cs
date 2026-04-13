using Moedelo.CommonV2.Webpack.Enums;

namespace Moedelo.CommonV2.Webpack.Extensions
{
    internal static class FileExtension
    {
        internal static string CheckFileExtension(this string value, RenderType type)
        {
            return type == RenderType.Js ? GetJsFile(value) : GetCssFile(value);
        }

        private static string GetJsFile(string value)
        {
            return value.EndsWith(".js")
                ? value
                : $"{value}.js";
        }

        private static string GetCssFile(string value)
        {
            return value.EndsWith(".css")
                ? value
                : $"{value}.css";
        }
    }
}