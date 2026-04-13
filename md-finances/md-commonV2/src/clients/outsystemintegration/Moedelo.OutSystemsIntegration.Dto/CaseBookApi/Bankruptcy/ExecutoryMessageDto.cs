using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Bankruptcy
{
    public class ExecutoryMessageDto
    {
        public string BailiffName { get; set; }

        public DateTime? DebtorBirthDate { get; set; }

        public string DebtorName { get; set; }

        public string DepartmentBailiffsAddress { get; set; }

        public DateTime? ExecutiveProductionDate { get; set; }

        public string ExecutiveProductionNumber { get; set; }

        public string ExecutiveSubject { get; set; }

        public double? ExecutorySum { get; set; }

        public bool? IsFinished { get; set; }

        public DateTime? StartDocumentDate { get; set; }

        public string StartDocumentNumber { get; set; }

        public string StartDocumentType { get; set; }
    }
}