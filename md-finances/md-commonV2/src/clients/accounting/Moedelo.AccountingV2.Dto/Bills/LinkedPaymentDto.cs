using System;

namespace Moedelo.AccountingV2.Dto.Bills
{
    public class LinkedPaymentDto
    {
        public string Number { get; set; }
        
        public DateTime Date { get; set; }
        
        public decimal Sum { get; set; }
        
        public long Id { get; set; }
    }
}