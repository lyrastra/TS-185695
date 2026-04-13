using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Kontragents;

namespace Moedelo.AccountingV2.Dto.Requsites
{
    public class BankKontragentRequisites
    {
        public string KontragentName { get; set; }

        public KontragentForm? KontragentForm { get; set; }

        public string KontragentINN { get; set; }

        public string KontragentKPP { get; set; }

        public string KontragentOKATO { get; set; }

        public string KontragentOKTMO { get; set; }

        public string KontragentSettlementAccount { get; set; }

        public IList<KontragentSettlementAccountDto> KontragentSettlementAccounts { get; set; }

        public string KontragentBankCorrespondentAccount { get; set; }

        public string KontragentBankBIK { get; set; }

        public string KontragentBankName { get; set; }
    }
}