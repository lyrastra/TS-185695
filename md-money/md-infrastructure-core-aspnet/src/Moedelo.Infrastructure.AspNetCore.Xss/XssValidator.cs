using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Moedelo.Infrastructure.AspNetCore.Xss
{
    public static class XssValidator
    {
        // потенциально опасные теги
        private const string DangerousTagsPattern = @"<\s*(body|embed|frame|script|frameset|html|iframe|img|style|layer|link|ilayer|meta|object|input)\b";
        // потенциально опасные атрибуты
        private const string DangerousAttributesPattern = @"\s+((src|href|background|style|content|data)|on\S+?)=";
        // массив предвычисленных строковых представлений наиболее популярных индексов
        private static readonly string[] IndexNames = new string[100];

        static XssValidator()
        {
            for (var i = 0; i < IndexNames.Length; ++i)
            {
                IndexNames[i] = $"[{i}]";
            }
        }

        public static void Validate(object obj)
        {
            Validate(obj, new Stack<string>());
        }

        private static void ValidateString(string value, Stack<string> path)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }
            // декодируем строку
            var decodedValue = Decode(value);
            if (string.IsNullOrEmpty(decodedValue))
            {
                return;
            }
            // проверяем вхождение опасных тегов
            if (Regex.IsMatch(decodedValue, DangerousTagsPattern, RegexOptions.IgnoreCase))
            {
                var pathAsString = string.Join(".", path.ToArray().Reverse()).Replace(".[", "[");
                throw new XssValidationException("Detected potentially dangerous content", pathAsString, value);
            }
            // проверяем вхождение опасных атрибутов
            if (Regex.IsMatch(decodedValue, DangerousAttributesPattern, RegexOptions.IgnoreCase))
            {
                var pathAsString = string.Join(".", path.ToArray().Reverse()).Replace(".[", "[");
                throw new XssValidationException("Detected potentially dangerous content", pathAsString, value);
            }
        }

        private static void ValidateNamed(object obj, Stack<string> path, string fieldName)
        {
            path.Push(fieldName);
            Validate(obj, path);
            path.Pop();
        }

        private static void Validate(object obj, Stack<string> path)
        {
            if (obj == null)
            {
                return;
            }

            var type = obj.GetType();
            if (!type.IsClass)
            {
                return;
            }
            // если строка, валидируем сразу
            if (type == typeof(string))
            {
                ValidateString((string)obj, path);
                return;
            }
            // специальная обработка словарей: валидируем значения по ключам
            if (obj is System.Collections.IDictionary dictionary)
            {
                foreach (System.Collections.DictionaryEntry entry in dictionary)
                {
                    var keyName = entry.Key?.ToString() ?? "<null>";
                    ValidateNamed(entry.Value, path, keyName);
                }
                return;
            }
            // перечисляемый тип - особый случай
            if (obj is System.Collections.IEnumerable enumerable)
            {
                var index = 0;
                foreach (var item in enumerable)
                {
                    var indexAsString = index < IndexNames.Length ? IndexNames[index] : $"[{index}]";
                    ValidateNamed(item, path, indexAsString);
                    ++index;
                }
                return;
            }

            // перебираем публичные поля
            var properties = obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead &&
                x.GetGetMethod(true).IsPublic && x.PropertyType.IsClass);
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(obj);
                var name = propertyInfo.Name;
                ValidateNamed(value, path, name);
            }
        }

        private static string Decode(string value)
        {
            // детектим/декодируем base64, если требуется
            value = DecodeBase64(value);
            // декодируем url
            var urlDecodedValue = WebUtility.UrlDecode(value);
            // декодируем html
            return WebUtility.HtmlDecode(urlDecodedValue);
        }

        private static string DecodeBase64(string value)
        {
            try
            {
                // декодируем из base64
                var data = Convert.FromBase64String(value);
                // если не упало, то проверяем выравнивание строки
                if (value.Replace(" ", "").Length % 4 == 0)
                {
                    // кодируем в utf8
                    return Encoding.UTF8.GetString(data);
                }
            }
            catch { }
            // ничего не изменилось
            return value;
        }
    }

}
