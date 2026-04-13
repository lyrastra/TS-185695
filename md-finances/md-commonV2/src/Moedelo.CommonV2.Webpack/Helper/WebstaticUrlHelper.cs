using System;
using System.Text.RegularExpressions;

namespace Moedelo.CommonV2.Webpack.Helper
{
    public static class WebstaticUrlHelper
    {
        private const string DomainPattern = "{domain}";
        private const string UrlPattern = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        private const string BoxPattern = @"(\w*)box(-)?\d+";

        public static string GetUrl(string template, string host)
        {
            Validate(template, host);

            if (!template.Contains(DomainPattern))
            {
                return template;
            }
            
            var box = GetBoxName(host);
            return Regex.Replace(template, DomainPattern, box);

        }

        private static void Validate(string template, string host)
        {
            if (string.IsNullOrEmpty(template))
            {
                throw new ArgumentException("Шаблон URL не может быть пустой строкой");
            }

            if (!Regex.IsMatch(template, UrlPattern) && !template.Contains(DomainPattern) && !template.Contains("localhost"))
            {
                throw new ArgumentException($"Строка {template} не является шаблоном URL");
            }

            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("Host не может быть пустой строкой");
            }
        }

        private static string GetBoxName(string host)
        {
            var regex = new Regex(BoxPattern);
            var match = regex.Match(host);
            return match.Value;
        }
    }
}