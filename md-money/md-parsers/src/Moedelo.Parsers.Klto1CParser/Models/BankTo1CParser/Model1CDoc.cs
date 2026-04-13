using System;

namespace Moedelo.Parsers.Klto1CParser.Models.BankTo1CParser
{
    public class Model1CDoc
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Summ { get; set; }
        public DateTime IncomingDate { get; set; }
        public DateTime OutgoingDate { get; set; }
        public string Purpose { get; set; }
        public int Priority { get; set; }
        public ContactDetails Payer { get; set; }
        public ContactDetails Recipient { get; set; }
        public BudgetDetails BudgetDetails { get; set; } = new BudgetDetails();
        public string SectionName { get; set; } = "Платежное поручение";
    }
}