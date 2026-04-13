namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.File
{
    public class FileResultDto
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }

        public string MimeType { get; set; }
    }
}