using Newtonsoft.Json;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SovcomBankWl
{
    public class IntegrationDataDto
    {
        /// <summary>
        /// Расчетные счета
        /// </summary>
        [JsonProperty("accounts")]
        public List<AccountResponseDto> Accounts { get; set; }

        /// <summary>
        /// State для проверки AuthCallBack
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }
    }
}
