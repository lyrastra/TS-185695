namespace Moedelo.Money.Common.Domain.Models.Reports
{
    public class ReportFile
    {
        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }
    }
}
