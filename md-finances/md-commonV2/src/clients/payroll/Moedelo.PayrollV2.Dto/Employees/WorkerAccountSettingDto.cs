using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.PayrollV2.Dto.Employees
{
    public class WorkerAccountSettingDto
    {
        public int WorkerId { get; set; }
        
        public SyntheticAccountCode AccountCode { get; set; }
        
        public NomenclatureGroupCode? NomenclatureGroupCode { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}