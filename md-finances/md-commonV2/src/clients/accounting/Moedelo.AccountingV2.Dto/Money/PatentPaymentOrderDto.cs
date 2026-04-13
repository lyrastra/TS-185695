using System;

namespace Moedelo.AccountingV2.Dto.Money
{
    public class PatentPaymentOrderDto
    {
        public long Id { get; set; }
        public long? PatentId { get; set; }

        public int FirmId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int BasicProfitability { get; set; }
        public int Cost { get; set; }
        public bool IsStopped { get; set; }
        public int CodeKindOfBusinessId { get; set; }
        public int? OkunId { get; set; }
        public string Territory { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int CreateUserId { get; set; }
        public string Oktmo { get; set; }
        public string Kbk { get; set; }

        public string TaxCode { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string Bik { get; set; }
        public string SettlementAccount { get; set; }
    }
}
