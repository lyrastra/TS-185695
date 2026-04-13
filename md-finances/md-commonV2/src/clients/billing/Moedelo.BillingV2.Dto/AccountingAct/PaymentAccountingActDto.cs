namespace Moedelo.BillingV2.Dto.AccountingAct
{
    public class PaymentAccountingActDto
    {
        /// <summary>
        /// название услуги, по которой выставлен акт
        /// </summary>
        public string ServiceName { get; set; }
        
        public decimal Sum { get; set; }

        public bool HasNds { get; set; }
    }
}