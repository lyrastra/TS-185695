namespace Moedelo.OfficeV2.Dto.File
{
    public class FileResponseDto
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}
