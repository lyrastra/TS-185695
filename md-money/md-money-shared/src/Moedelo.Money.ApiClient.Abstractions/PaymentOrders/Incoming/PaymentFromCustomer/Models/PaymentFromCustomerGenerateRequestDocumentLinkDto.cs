namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Dto
{
    public class PaymentFromCustomerGenerateRequestDocumentLinkDto
    {
        /// <summary>
        /// Идентификатор первичного документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Учитываемая сумма
        /// </summary>
        public decimal Sum { get; set; }
    }
}
