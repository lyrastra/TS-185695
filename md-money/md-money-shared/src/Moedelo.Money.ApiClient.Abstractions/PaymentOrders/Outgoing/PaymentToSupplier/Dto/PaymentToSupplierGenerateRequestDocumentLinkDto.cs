namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
{
    public class PaymentToSupplierGenerateRequestDocumentLinkDto
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