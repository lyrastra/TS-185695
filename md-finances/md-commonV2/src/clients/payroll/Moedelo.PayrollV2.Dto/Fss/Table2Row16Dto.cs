using System;

namespace Moedelo.PayrollV2.Dto.Fss
{
    public class Table2Row16Dto
    {
        public decimal PeriodStartSum { get; set; }
        public decimal LastThreeMonthsSum { get; set; }
        public string Month1PaymentNumber { get; set; }
        public DateTime Month1PaymentDate { get; set; }
        public decimal Month1Sum { get; set; }
        public string Month2PaymentNumber { get; set; }
        public DateTime Month2PaymentDate { get; set; }
        public decimal Month2Sum { get; set; }
        public string Month3PaymentNumber { get; set; }
        public DateTime Month3PaymentDate { get; set; }
        public decimal Month3Sum { get; set; }
        public decimal PeriodSum { get; set; }
    }
}