using System;
using System.Linq;

namespace Moedelo.Billing.Abstractions.Dto.Bills;

public class RenewalCalculationResultDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? StartDate { get; set; }
    public string PromoCode { get; set; }
    public Position[] Positions { get; set; }
    public decimal Total => Positions?.Sum(position => position?.Sum) ?? 0;
    public bool IsProlongationAvailable { get; set; }

    public class Position
    {
        public string Name { get; set; }
        public decimal Sum { get; set; }
        public int MonthCount { get; set; }
    }
}