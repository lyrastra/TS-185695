namespace Moedelo.PayrollV2.Client.SalaryProjects.DTO
{
    public class SalaryProjectDto
    {
        public int BankId { get; set; }
        public string BankInn { get; set; }
        public string BankKpp { get; set; }
        public string SettlementAccountNumber { get; set; }
        public int SenderSettlementAccountId { get; set; }
    }
}