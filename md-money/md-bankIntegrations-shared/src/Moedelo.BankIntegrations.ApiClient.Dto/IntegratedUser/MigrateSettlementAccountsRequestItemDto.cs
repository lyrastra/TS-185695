namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class MigrateSettlementAccountsRequestItemDto
    {
        /// <summary> Код пользователя/юр.лица в Точка банк</summary>
        public string CustomerCode { get; set; }
        
        public string Inn { get; set; }
    }
}
