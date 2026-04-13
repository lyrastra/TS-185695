namespace Moedelo.ErptV2.Dto.FlcNetCore
{
    public class FileDetail
    {
        public string Name { get; }
        public byte[] Content { get; }

        public FileDetail(string name, byte[] content)
        {
            Name = name;
            Content = content;
        }
    }
}