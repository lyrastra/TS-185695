using System;
using Moedelo.Contracts.Enums;

namespace Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos
{
    public class ContractSearchResponseDto
    {
        public int Id { get; set; }

        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int KontragentId { get; set; }

        public decimal Sum { get; set; }

        public ContractKind ContractKind { get; set; }

        public long SubcontoId { get; set; }

        public decimal? PercentRate { get; set; }

        public DateTime? PaymentStartDate { get; set; }

        public bool IsLongTermContract { get; set; }
    }
}