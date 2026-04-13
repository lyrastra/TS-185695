using System.IO;
using System.Text;

namespace Moedelo.CommonV2.Extensions.System
{
    public static class StreamEx
    {
        /// <summary>
        /// Считывание потока в массив байт
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToArray(this Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return memoryStream.ToArray();
            }

            using (var ms = new MemoryStream())
            {
                var buffer = new byte[1024];

                int bytes;
                while ((bytes = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, bytes);
                }

                return ms.ToArray();
            }
        }

        public static Encoding GetEncoding(this Stream stream)
        {
            // Read the BOM
            var bom = new byte[4];
            stream.Read(bom, 0, 4);
            stream.Seek(0, SeekOrigin.Begin);

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;

            return Encoding.GetEncoding(1251);
        }

        public static char GetCsvSeparator(this Stream stream)
        {
            const char comma = ',';
            var commaCount = 0;
            const char semicolon = ';';
            var semicolonCount = 0;

            int val;
            while ((val = stream.ReadByte()) != -1)
            {
                switch ((char)val)
                {
                    case comma:
                        commaCount++;
                        break;
                    case semicolon:
                        semicolonCount++;
                        break;
                }
            }
            stream.Seek(0, SeekOrigin.Begin);

            return commaCount > semicolonCount
                ? comma : semicolon;
        }

        public static string ReplaceWrongSymbolsForExport(this string value)
        {
            return value?
                .Replace("–", "-")
                .Replace("«", "\"")
                .Replace("»", "\"")
                .Replace(char.ConvertFromUtf32(160), char.ConvertFromUtf32(32));
        }
    }
}