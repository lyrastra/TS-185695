using Moedelo.Money.Domain.Enums;

namespace Moedelo.Money.Api.Models
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
        public RemoteServiceStatus Status { get; set; }
    }
}
