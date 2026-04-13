using System;

namespace Moedelo.RptV2.Dto.File
{
    public class FileTransferDto: BaseDto
    {
        public static FileTransferDto FromFileResult(FileResultDto dto)
        {
            return new FileTransferDto
            {
                Name = dto.Name,
                MimeType = dto.MimeType,
                Content = Convert.ToBase64String(dto.Content)
            };
        }

        public FileResultDto ToFileResult()
        {
            return new FileResultDto
            {
                Name = Name,
                MimeType = MimeType,
                Content = Convert.FromBase64String(Content)
            };
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public string MimeType { get; set; }
    }
}
