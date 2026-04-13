namespace Moedelo.Common.Archive;
    public record ZipItem(string Name, byte[] Data)
    {
        public string Name { get; set; } = Name;
        public byte[] Data { get; set; } = Data;
    }
