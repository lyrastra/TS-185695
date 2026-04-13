using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Dto.Gateway.Payments
{
    /// <summary>
    /// Критерии для получения истории платежей фирмы
    /// </summary>
    public class BillingGatewaySuccessPaymentsCriteriaDto
    {
        /// <summary>
        /// Типы методов оплат. Опционально
        /// Объединяются в общий список методов платежей с параметром PaymentMethods
        /// </summary>
        public PaymentMethodType[] PaymentMethodTypes { get; set; }

        /// <summary>
        /// Методы оплат. Опционально
        /// Объединяются в общий список методов платежей с параметром PaymentMethodTypes
        /// </summary>
        public string[] PaymentMethods { get; set; }

        /// <summary>
        /// Коды продуктов. Опционально
        /// </summary>
        public string[] ProductCodes { get; set; }

        /// <summary>
        /// Исключенные методы оплаты. Опционально
        /// Объединяются в общий список методов платежей с параметром ExcludedPaymentMethodTypes
        /// </summary>
        public string[] ExcludedPaymentMethods { get; set; }

        /// <summary>
        /// Исключенные типы методов оплат. Опционально
        /// Объединяются в общий список методов платежей с параметром ExcludedPaymentMethods
        /// </summary>
        public PaymentMethodType[] ExcludedPaymentMethodTypes { get; set; }

        /// <summary>
        /// Флаг, указывающий на включение в выборку разовых платежей
        /// </summary>
        public bool IncludeOneTimeServices { get; set; } = true;

        /// <summary>
        /// Флаг, указывающий на включение в выборку платежей с возвратом
        /// </summary>
        public bool IncludeRefundedPayments { get; set; } = false;
    }
}
