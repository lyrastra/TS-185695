using System.Collections.Generic;

namespace Moedelo.RptV2.Dto.DownloadReport
{
    public class BillRequisitesDto
    {
        public int RegionalPartnerInfoId { get; set; }

        public string FirmName { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Ogrn { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string PhoneFederal { get; set; }

        public string SettlementAccount { get; set; }

        public string BankName { get; set; }

        public string BankBik { get; set; }

        public string BankAccount { get; set; }

        public string Director { get; set; }

        public bool IsWorkingWithNds { get; set; }
        
        public IReadOnlyCollection<BillSignerDto> Signers { get; set; }
    }
}