using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Executory
{
    public class ExecutoryMessageResponseDto
    {
        public string BailiffName { get; set; }

        public DateTime? DebtorBirthDate { get; set; }

        public string DebtorName { get; set; }

        public string DebtorInn { get; set; }

        public string DebtorAddress { get; set; }

        public string DepartmentBailiffsAddress { get; set; }

        public DateTime? ExecutiveProductionDate { get; set; }

        public string ExecutiveProductionNumber { get; set; }

        public string ExecutiveSubject { get; set; }

        public double? ExecutorySum { get; set; }

        public bool? IsFinished { get; set; }

        public DateTime? StartDocumentDate { get; set; }

        public string StartDocumentNumber { get; set; }

        public string StartDocumentType { get; set; }

        public string ExecutionObject { get; set; }

        public string ExecutoryDocumentNumber { get; set; }

        public string ExecutoryDocumentObject { get; set; }
    }
}
