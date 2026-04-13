namespace Moedelo.AccountingV2.Client.BizPostings
{

    public class BizPostingReportDto
    {
        public string Date { get; set; }

        public decimal Sum { get; set; }

        public string Debit { get; set; }

        public string DebitSubcontoName { get; set; }

        public string Credit { get; set; }

        public string CreditSubcontoName { get; set; }

        public string Description { get; set; }
    }
}
