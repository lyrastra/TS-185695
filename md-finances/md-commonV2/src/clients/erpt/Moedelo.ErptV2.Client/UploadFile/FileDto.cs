namespace Moedelo.ErptV2.Client.UploadFile
{
    public class FileDto
    {
        public int LinkedEntityId { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}