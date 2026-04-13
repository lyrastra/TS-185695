namespace Moedelo.PayrollV2.Dto.Balance
{
    public class WorkerBalancesDto
    {
        public string Inn { get; set; }
        
        public string TableNumber { get; set; }

        public decimal StaffDebtSum { get; set; }

        public decimal GpdDebtSum { get; set; }

        public decimal DividendsDebtSum { get; set; }

        public decimal StaffOverdraftSum { get; set; }

        public decimal GpdOverdraftSum { get; set; }

        public decimal DividendsOverdraftSum { get; set; }
    }
}
