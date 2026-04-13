namespace Moedelo.Authorities.ApiClient.Abstractions.Sfr.Dto
{
    public class SfrFirmRequisitesResponseDto
    {
        public string Number { get; set; }

        public int? DepartmentId { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Recipient { get; set; }

        public string BankBik { get; set; }

        public string SettlementAccount { get; set; }

        public string UnifiedSettlementAccount { get; set; }

        public string Oktmo { get; set; }

        public string FssNumber { get; set; }

        public string FssSubordinationCode { get; set; }
    }
}
