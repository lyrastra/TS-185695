namespace Moedelo.ReceiptStatement.Dto
{
    public class ReceiptStatementDto
    {
        public int Id { get; set; }

        public long DocumentBaseId { get; set; }

        public string Number { get; set; }
    
        public string Date { get; set; }

        public long SubcontoId { get; set; }

        public int KontragentId { get; set; }

        public int ContractId { get; set; }

        public decimal SumWithNds { get; set; }

        public decimal NdsSum { get; set; }
    }
}
