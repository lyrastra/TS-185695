using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums.Payroll;
using Moedelo.PayrollV2.Dto.Employees;

namespace Moedelo.PayrollV2.Dto.Postings
{
    public class SalaryPostingsModelV2Dto
    {
        public SalaryPostingsModelV2Dto()
        {
            Postings = new List<SalaryPostingDto>();
            FundPostings = new List<SalaryPostingDto>();
            TaxPostings = new List<SalaryTaxPostingsDto>();
        }

        public List<SalaryPostingDto> Postings { get; set; }

        public List<SalaryPostingDto> FundPostings { get; set; }

        public List<SalaryTaxPostingsDto> TaxPostings { get; set; }
        
        public List<WorkerAccountSettingDto> WorkerAccountSettings { get; set; }

        public List<SalaryTaxPostingsDto> GetFundTaxPostings()
        {
            return TaxPostings.Where(x => x.TaxPostingType == TaxPostingType.Fund && x.FundAccountCode != 0).ToList();
        }
    }
}