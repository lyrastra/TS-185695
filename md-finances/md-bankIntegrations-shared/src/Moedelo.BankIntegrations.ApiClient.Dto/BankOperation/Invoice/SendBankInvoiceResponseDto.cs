using Moedelo.BankIntegrations.Enums.Invoices;

namespace Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.Invoice
{
    public class SendBankInvoiceResponseDto
    {
        /// <summary> Успешность отправки сквозного(прямого платежа) </summary>
        public bool IsSuccess { get; set; }
        /// <summary> Сообщение для пользователя при ошибке, IsSuccess = false </summary>
        public string ErrorMessageForUser { get; set; }
        /// <summary> Статус ответа по сквозному платежу  </summary>
        public InvoiceResponseStatusCode InvoiceResponseStatusCode { get; set; }
        /// <summary> Описание статуса платежа </summary>
        public string DescriptionStatus { get;set; }
        /// <summary> Url адрес в ДБО на конкретное п/п, для оплаты клиентом </summary>
        public string InvoiceUrl { get; set; }
    }
}