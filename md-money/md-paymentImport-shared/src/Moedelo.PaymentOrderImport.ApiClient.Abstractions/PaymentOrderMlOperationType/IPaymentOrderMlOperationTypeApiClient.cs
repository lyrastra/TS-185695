using System.Threading;
using System.Threading.Tasks;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;
using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.PaymentOrderMlOperationType
{
    public interface IPaymentOrderMlOperationTypeApiClient
    {
        /// <summary>
        /// Верификация типа операции (с записью результата в бд)
        /// </summary>
        Task<PaymentImportOperationType?> VerifyAsync(
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
