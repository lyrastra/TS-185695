using Moedelo.BankIntegrations.Enums.Acceptance;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SberbankAcceptance
{
    public class SberbankAcceptanceDto
    {
        public int FirmId { get; set; }

        public string SberbankClientId { get; set; }

        public bool IsAllowed { get; set; }

        public string Purpose { get; set; }

        public string Inn { get; set; }

        public string SettlementNumber { get; set; }

        public string BankBik { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string PayerName { get; set; }

        /// <summary>
        /// Тип ЗДА который был вадан клиентом: Basic - старый, Lite - новый
        /// </summary>
        public AcceptanceType AcceptanceType { get; set; }
    }
}