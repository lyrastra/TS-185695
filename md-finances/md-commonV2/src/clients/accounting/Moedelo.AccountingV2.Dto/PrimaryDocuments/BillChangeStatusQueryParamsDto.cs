namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class BillChangeStatusQueryParamsDto
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public bool ForBizImport { get; set; }
    }
}
