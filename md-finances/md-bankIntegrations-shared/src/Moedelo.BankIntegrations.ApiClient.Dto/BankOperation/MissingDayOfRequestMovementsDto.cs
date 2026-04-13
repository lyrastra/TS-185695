using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.BankOperation
{
    public class MissingDayOfRequestMovementsDto
    {
        public int FirmId;
        public string SettlementNumber;
        public DateTime MissingDate;
    }
}
