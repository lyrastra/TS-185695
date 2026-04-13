using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public class ChildCareNonReceiptCertificateExDto
    {
        public ChildCareNonReceiptCertificateType? NonReceiptCertificateType { get; set; }
        
        public string NonReceiptCertificateNumber { get; set; }
        
        public DateTime? NonReceiptCertificateDate { get; set; }
    }
}