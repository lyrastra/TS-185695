namespace Moedelo.Finances.Public.ClientData.AccountBalanceWidgets
{
    public class SettlementAccountBalanceClientData
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Number { get; set; }
        
        public string BankName { get; set; }

        public decimal Balance { get; set; }
    }
}