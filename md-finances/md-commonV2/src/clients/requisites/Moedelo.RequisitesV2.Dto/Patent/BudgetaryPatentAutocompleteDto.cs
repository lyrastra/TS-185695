using System;

namespace Moedelo.RequisitesV2.Dto.Patent
{
    public class BudgetaryPatentAutocompleteDto
    {
        public long Id { get; set; }

        public int FirmId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string ShortName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

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

        public string TaxRecipientName { get; set; }

        public string TaxInn { get; set; }

        public string TaxKpp { get; set; }

        public string TaxSettlement { get; set; }

        public string TaxBankName { get; set; }

        public string TaxBankBik { get; set; }

        public string TaxBankId { get; set; }
    }
}