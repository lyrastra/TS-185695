namespace Moedelo.RequisitesV2.Dto.TaxAdministrations
{
    public class TaxAdministrationV2Dto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Code { get; set; }

        public string CodeSpro { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string WebSite { get; set; }

        public string Bik { get; set; }

        public string SettlementAccount { get; set; }

        public string UnifiedSettlementAccount { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Type { get; set; }

        public string SproU { get; set; }

        public string SproF { get; set; }

        public string ParentSpro { get; set; }

        public string Comment { get; set; }

        public string RecepientName { get; set; }

        public bool HasMunicipal { get; set; }

        public string Okato { get; set; }

        public string Oktmo { get; set; }

        public string ReorganizedTo { get; set; }

        public int? RegPaymentTaxId { get; set; }
    }
}