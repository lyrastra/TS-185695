namespace Moedelo.RequisitesV2.Dto.Patent
{
    public class PatentInMoneyOperationV2Dto
    {
        public int Id { get; set; }

        public long PatentId { get; set; }

        public int MoneyTransferOperationId { get; set; }

        public decimal Sum { get; set; }
    }
}