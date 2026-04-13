namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class DocumentsSumsDto
    {
        public decimal TotalBillsSum { get; set; }
        public decimal UnpaidBillsSum { get; set; }

        public decimal TotalStatementsSum { get; set; }
        public decimal UnsignedStatementsSum { get; set; }

        public decimal TotalWaybillsSum { get; set; }
        public decimal UnsignedWaybillsSum { get; set; }
    }
}
