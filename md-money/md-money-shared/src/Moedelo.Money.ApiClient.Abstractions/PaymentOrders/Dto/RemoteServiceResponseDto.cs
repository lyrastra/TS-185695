using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
{
    public class RemoteServiceResponseDto<T>
    {
        /// <summary>
        /// Значение
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Статус ответа
        /// Ok - 0
        /// Ошибка = 1
        /// </summary>
        public RemoteServiceResponseStatus Status { get; set; }
    }
}