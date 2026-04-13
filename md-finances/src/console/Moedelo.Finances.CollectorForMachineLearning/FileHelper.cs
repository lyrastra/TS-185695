using System.Collections.Generic;
using System.IO;

namespace Moedelo.Finances.CollectorForMachineLearning
{
    public static class FileHelper
    {
        public static void Save(string fileName, List<string> stringsToSave)
        {
            using (var file = new StreamWriter(fileName, true))
            {
                foreach (var stringToSave in stringsToSave)
                {
                    file.WriteLine(stringToSave);
                }
            }
        }
    }
}