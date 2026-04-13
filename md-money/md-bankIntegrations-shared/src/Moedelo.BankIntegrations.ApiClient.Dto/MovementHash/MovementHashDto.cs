using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.MovementHashService
{
    /// <summary>Модель операции для получения хеш суммы</summary>
    public class MovementHashDto
    {
        public int FirmId { get; set; }

        public int PartnerId { get; set; }

        public string NumberDoc { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public string SettlementNumber { get; set; }

        public int? IntegrationCallType { get; set; }
    }
}
