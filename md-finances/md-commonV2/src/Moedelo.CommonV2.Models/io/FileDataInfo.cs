using System.IO;

namespace Moedelo.CommonV2.Models.io
{
    public class FileDataInfo
    {
        public FileDataInfo(string name, string extension, Stream data)
        {
            Name = name;
            Extension = extension;
            Data = data;
        }

        public Stream Data { get; }

        public string Extension { get; }

        public string FullName => $"{Name}.{Extension}";

        public string Name { get; set; }
    }
}
