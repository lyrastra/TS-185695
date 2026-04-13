using Newtonsoft.Json;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SovcomBankWl
{
    public class ClientInfoResponseDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("taxation_system")]
        public TaxationSystemResponseDto TaxationSystem { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("fio")]
        public string Fio { get; set; }

        /// <summary>
        /// Номер мобильного телефона
        /// </summary>
        [JsonProperty("nmt")]
        public string Nmt { get; set; }

        /// <summary>
        /// Расчетные счета
        /// </summary>
        [JsonProperty("accounts")]
        public List<AccountResponseDto> Accounts { get; set; }
    }
}