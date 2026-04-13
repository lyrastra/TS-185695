using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.PayrollV2.Dto.Postings
{
    public class SalarySubcontoDto
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public SubcontoType SubcontoType { get; set; }
    }
}