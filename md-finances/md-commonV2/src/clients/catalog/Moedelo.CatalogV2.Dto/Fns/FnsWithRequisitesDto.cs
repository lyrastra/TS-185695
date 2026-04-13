namespace Moedelo.CatalogV2.Dto.Fns
{
    public class FnsWithRequisitesDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string RecipientName { get; set; }
        public string SettlementAccount { get; set; }
        public string UnifiedSettlementAccount { get; set; }
        public string Bik { get; set; }
        public string BankName { get; set; }

        public bool IsActive { get; set; }
    }
}
