namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class BillForContractDetailsDto
    {
        public int Id { get; set; }

        public long BaseId { get; set; }

        public string Date { get; set; }

        public string Name { get; set; }

        public decimal Sum { get; set; }
    }
}