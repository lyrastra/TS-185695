namespace Moedelo.Finances.Domain.Models.Reports
{
    public class Report
    {
        public byte[] Content { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
    }
}
