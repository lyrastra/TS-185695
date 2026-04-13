using System;

namespace Moedelo.RptV2.Client
{
    public class FileResponse
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }

        public static FileResponse FromBase64 (FileResponseBase64 encodedFile)
        {
            return new FileResponse
            {
                Content  = Convert.FromBase64String(encodedFile.Content),
                FileName = encodedFile.FileName,
                MimeType = encodedFile.MimeType
            };
        }
    }
}
