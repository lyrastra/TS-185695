using System;
using Moedelo.Payroll.Enums.Residues;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Ndfl6ReportInitialData
{
    public class Ndfl6ReportInitialResidueNoticeDto
    {
        public int WorkerId { get; set; }
        public int Code { get; set; }
        public string NoticeNumber { get; set; }
        public DateTime NoticeDate { get; set; }
        public string NoticeFnsCode { get; set; }
        public ResidueTypeCode ResidueType { get; set; }
    }
}
