using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Blancbank
{
    public class IntegrationDataDto
    {
        public int FirmId { get; set; }
        /// <summary>Список счетов из банка</summary>
        public List<AccountDto> Accounts { get; set; }

        /// <summary>Время последнего обновления</summary>
        public DateTime UpdateDt { get; set; }
        public string ConsentId { get; set; }
    }
}
