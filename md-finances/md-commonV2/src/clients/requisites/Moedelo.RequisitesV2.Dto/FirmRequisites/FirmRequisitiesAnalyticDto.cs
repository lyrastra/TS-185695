using System;

namespace Moedelo.RequisitesV2.Dto.FirmRequisites
{
    public class FirmRequisitiesAnalyticDto
    {
        public int FirmId { get; set; }
        public bool isIP { get; set; }
        public bool isOOO { get; set; }
        public bool TaxationSystem { get; set; }
        public int PatentsCount { get; set; }
        public bool UseTradingTax { get; set; }
        public string Inn { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public bool RequisitiesMinimalFilled { get; set; }
        public int PrimaryActivityId { get; set; }
        public string PrimaryActivity { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}