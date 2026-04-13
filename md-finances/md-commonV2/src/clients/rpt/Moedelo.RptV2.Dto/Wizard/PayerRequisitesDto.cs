using System;

namespace Moedelo.RptV2.Dto.Wizard
{
    public class PayerRequisitesDto
    {
        public DateTime BudgetTaxDocDate { get; set; }
        public string Narrative { get; set; }        
        public string PayerName { get; set; }
        public string PayerInn { get; set; }        
        public string PaymentOkato { get; set; }
        public string PaymentKbk { get; set; }
        public int PeriodNumber { get; set; }
        public int PeriodYear { get; set; }
        public decimal Sum { get; set; }
    }
}