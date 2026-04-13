using System;

namespace Moedelo.Pdf417V2.Dto
{
    public sealed class FileInfoDto
    {
        public long Id { get; set; }

        public string PrinterId { get; set; }

        public string XmlData { get; set; }

        public DateTime CreateDate { get; set; }

        public byte[] BinaryData { get; set; }

        public PdfPrintStatus Status { get; set; }

        public string Extension { get; set; }
    }
}
