using System;
using Moedelo.Contracts.Enums;

namespace Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos
{
    public class ContractDto
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string ContractStatus { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public bool IsArchived { get; set; }

        /// <summary> Является ли договор основным (readonly) </summary>
        public bool IsMainContract { get; set; }

        /// <summary> Апи V1 отдает это поле как nullable </summary>
        public int? KontragentId { get; set; }

        public int? TemplateId { get; set; }

        public long? SubcontoId { get; set; }

        public string SubcontoName { get; set; }

        public long DocumentBaseId { get; set; }

        public int? ProjectId { get; set; }

        public ContractStatus Status { get; set; }

        public ContractKind ContractKind { get; set; }

        public decimal? InterestSum { get; set; }

        public ContractDirection Direction { get; set; }

        public bool IsLongTermContract { get; set; }
        
        public MediationType? MediationType { get; set; }
    }
}