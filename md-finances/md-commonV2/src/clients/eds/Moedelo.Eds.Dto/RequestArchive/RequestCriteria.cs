using System;

namespace Moedelo.Eds.Dto.RequestArchive
{
    public class RequestCriteria
    {
        public string Login { get; set; }
        public string FirmInn { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string SortBy { get; set; }
        public bool OrderByDesc { get; set; }
        public DateTime? CertificateCreateDateStartDate { get; set; }
        public DateTime? CertificateCreateDateEndDate { get; set; }
        public DateTime? CertificateExpirationDateStartDate { get; set; }
        public DateTime? CertificateExpirationDateEndDate { get; set; }
        public bool? IsDocumentsReceived { get; set; }
        public bool? IsCommentExist { get; set; }
        public bool IsTestItemsIncluded { get; set; }
    }
}