using Moedelo.BankIntegrations.Dto;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Payments
{
    public class PaymentDetailsResponseDto : BaseResponseDto
    {
        /// <summary>
        /// Номер платежа
        /// </summary>
        public string PaymentNumber { get; set; }
        
        /// <summary>
        /// Счет отправителя
        /// </summary>
        public string PayerSettlementNumber { get; set; }
        
        /// <summary>
        /// Счет получателя
        /// </summary>
        public string PayeeSettlementNumber  { get; set; }
        
        /// <summary>
        /// ИНН получателя
        /// </summary>
        public string PayeeInn  { get; set; }
        
        /// <summary>
        /// Cумма платежа
        /// </summary>
        public decimal Amount { get; set; }
    }
}