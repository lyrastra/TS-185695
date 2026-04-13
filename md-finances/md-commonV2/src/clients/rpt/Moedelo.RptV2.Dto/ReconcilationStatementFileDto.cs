using System;


namespace Moedelo.RptV2.Dto
{
    public class ReconcilationStatementFileDto
    {
     
        public byte[] Content { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        public int KontragentId { get; set; }

        public DateTime DocDate { get; set; }
    }
}
