namespace Moedelo.AccountingV2.Dto.AdvanceStatement
{
    public class AdvanceStatementInfoDto
    {
        public long Id { get; set; }

        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public string Date { get; set; }

        public int WorkerId { get; set; }

        public string WorkerName { get; set; }

        public decimal Sum { get; set; }

        public bool ProvideInAccounting { get; set; }
    }
}
