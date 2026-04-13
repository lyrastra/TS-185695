using System.Linq;

namespace Moedelo.Docs.Enums.Extensions
{
    public static class FileNameExtensions
    {
        private static readonly char[] ForbiddenPrintCharacters =
        {
            '<',
            '>',
            ':',
            '"',
            '/',
            '\\',
            '|',
            '?',
            '*'
        };
        
        /// <summary>
        /// Удаляет символы, запрещенные в названиях файлов
        /// </summary>
        public static string RemoveInvalidCharacters(this string fileName)
        {
            // https://stackoverflow.com/questions/1976007/what-characters-are-forbidden-in-windows-and-linux-directory-names
            return new string(fileName.Where(c =>
                !char.IsControl(c) &&
                !ForbiddenPrintCharacters.Contains(c)
            ).ToArray());
        }
    }
}