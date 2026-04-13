using System;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models
{
    public class IpOsnoTaxPosting : ITaxPosting
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public TaxPostingDirection Direction { get; set; }
    }
}