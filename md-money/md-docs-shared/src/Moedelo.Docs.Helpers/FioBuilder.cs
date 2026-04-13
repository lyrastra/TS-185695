using System.Linq;

namespace Moedelo.Docs.Helpers
{
    // Unit-тесты есть в md-docsUpds
    public static class FioBuilder
    {
        private const string Space = "\u00A0"; // &nbsp;
        private static readonly char[] DelimiterChars = { ' ', '-' };
        private static readonly string[] IgnoreParts = { "оглы", "оглу", "кызы", "гызы", "бек", "хан", "ага", "заде", "шах" };
        private static string NameToInitial(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            
            // Если из нескольких составных частей состоит имя, то каждая его часть обозначается инициалом
            // (т.е. одной начальной буквой с точкой для каждой части составного имени)
            // Составные части имени могут писаться не только через дефис, но и раздельно.
            // В любом случае при сокращении до инициалов между ними ставится дефис
		
            var firstLetters = name
                .Split(DelimiterChars)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x) && !IgnoreParts.Contains(x))
                .Select(x => x[0])
                .ToArray();
		
            return string.Join(".-", firstLetters) + ".";
        }
	
        public static string GetShortName(string surname, string name = null, string patronymic = null)
        {
            var parts = new string[3];
		
            if (!string.IsNullOrEmpty(surname))
            {
                parts[0] = surname;
            }
		
            if (!string.IsNullOrEmpty(name))
            {
                parts[1] = NameToInitial(name);
            }
		
            if (!string.IsNullOrEmpty(patronymic))
            {
                parts[2] = NameToInitial(patronymic);
            }
            
            var filteredParts = parts
                .Where(part => !string.IsNullOrEmpty(part))
                .Select(part => part.Trim());
            
            return string.Join(Space, filteredParts);
        }
    }
}
