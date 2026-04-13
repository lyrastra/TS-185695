using System.Collections.Generic;
using Moedelo.BankIntegrations.Enums.Sberbank;

namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.ClientInfo
{
    public class ClientInfoResponseDto
    {
        public string ErrorMessage { get; set; }
        public int FirmId { get; set; }
        /// <summary> Cчета организации клиента, доступные партнеру </summary>
        public List<ClientInfoSettlementAccountDto> ClientInfoSettlementAccounts { get; set; }
        public SberClientInfoResponseStatus ResponseStatus { get; set; }
    }
}