using Moedelo.AstralV3.Client.Types;
using Moedelo.CommonV2.Extensions.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Moedelo.AstralV3.Client.Helpers
{
    public static class AstralFileObjectBuilder
    {
        public static FileObject GetFileObject(string name, Stream data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var array = data.ToArray();

            return GetFileObject(name, array);
        }

        public static FileObject GetFileObject(string name, byte[] array)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (array == null) throw new ArgumentNullException(nameof(array));

            return new FileObject()
            {
                Name = GetAnsiNameFromUtf(name),
                Content = array
            };
        }

        public static List<FileObject> GetFileObjects(Dictionary<string, Stream> inputValues)
        {
            if (inputValues == null) throw new ArgumentNullException(nameof(inputValues));

            return inputValues.Select(kvp => GetFileObject(kvp.Key, kvp.Value)).ToList();
        }

        public static List<FileObject> GetFileObjects(Dictionary<string, byte[]> inputValues)
        {
            if (inputValues == null) throw new ArgumentNullException(nameof(inputValues));

            return inputValues.Select(kvp => GetFileObject(kvp.Key, kvp.Value)).ToList();
        }

        private static string GetAnsiNameFromUtf(string utf)
        {
            byte[] utfName = Encoding.UTF8.GetBytes(utf);
            byte[] ansiName = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(1251), utfName);
            return Encoding.GetEncoding(1251).GetString(ansiName);
        }

    }
}
