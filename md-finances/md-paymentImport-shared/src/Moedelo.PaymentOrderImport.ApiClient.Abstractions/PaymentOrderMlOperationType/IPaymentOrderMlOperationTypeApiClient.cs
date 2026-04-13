using System.Threading;
using System.Threading.Tasks;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.PaymentOrderMlOperationType
{
    public interface IPaymentOrderMlOperationTypeApiClient
    {
        /// <summary>
        /// Верификация типа операции (с записью результата в бд)
        /// </summary>
        Task<VerifyOperationTypeResponseDto> VerifyAsync(
            VerifyOperationTypeDto request,
            CancellationToken ct);

        /// <summary>
        /// Сохранение признака пропуска с массовой страницы
        /// </summary>
        Task SetSkipStatus(
            SetMlSkipStatusDto request,
            CancellationToken ct);
    }
}
