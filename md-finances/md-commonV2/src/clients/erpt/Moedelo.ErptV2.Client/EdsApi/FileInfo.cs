namespace Moedelo.ErptV2.Client.EdsApi
{
    public class FileInfo
    {
        public string FileName { get; }
        public string FileExtention { get; }
        public byte[] Content { get; }

        public FileInfo(string fileName, string fileExtention, byte[] content)
        {
            FileName = fileName;
            FileExtention = fileExtention;
            Content = content;
        }
    }
}