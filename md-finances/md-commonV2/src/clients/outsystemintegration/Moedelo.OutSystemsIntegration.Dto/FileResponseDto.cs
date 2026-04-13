using System.IO;

namespace Moedelo.OutSystemsIntegrationV2.Dto
{
    public class FileResponseDto
    {
        public string FileName { get; set; }

        public string DocumentContentType { get; set; }

        public Stream DocumentStream { get; set; } 
    }
}