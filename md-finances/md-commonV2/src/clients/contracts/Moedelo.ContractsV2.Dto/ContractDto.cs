using System;
using Moedelo.Common.Enums.Enums.Contract;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.ContractsV2.Dto
{
    public class ContractDto
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string ContractStatus { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public decimal Sum { get; set; }

        public bool IsArchived { get; set; }

        /// <summary> readonly </summary>
        public bool IsMainContract { get; set; }

        public int KontragentId { get; set; }

        public int? TemplateId { get; set; }

        public long? SubcontoId { get; set; }

        public string SubcontoName { get; set; }

        public long DocumentBaseId { get; set; }

        public int? ProjectId { get; set; }

        public ContractStatus Status { get; set; }

        public ContractKind ContractKind { get; set; }

        public decimal? InterestSum { get; set; }

        public decimal? PercentRate { get; set; }

        public PrimaryDocumentsTransferDirection Direction { get; set; }

        public DateTime? PaymentStartDate { get; set; }
    }
}
