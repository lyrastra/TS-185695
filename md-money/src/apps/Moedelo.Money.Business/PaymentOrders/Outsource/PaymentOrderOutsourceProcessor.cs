using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Enums.Outsource;

namespace Moedelo.Money.Business.PaymentOrders.Outsource;

internal abstract class PaymentOrderOutsourceProcessor<T> where T : class, IPaymentOrderOutsourceSaveRequest
{
    private readonly ILogger logger;

    protected PaymentOrderOutsourceProcessor(ILogger logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Алгоритм действий при подтверждении п/п любого типа
    /// </summary>
    protected async Task<OutsourceConfirmResult> ConfirmAsync(T request)
    {
        var saveRequest = await GetAsync(request);
        if (saveRequest == null)
        {
            return new OutsourceConfirmResult
            {
                DocumentBaseId = request.DocumentBaseId,
                Status = OutsourceConfirmPaymentStatus.NotFound
            };
        }

        try
        {
            await ValidateAsync(saveRequest);
        }
        catch (BusinessValidationException vex)
        {
            if (vex.Reason == ValidationFailedReason.ClosedPeriod) // в закрытом периоде (изменения запрещены)
            {
                return new OutsourceConfirmResult
                {
                    DocumentBaseId = request.DocumentBaseId,
                    Status = OutsourceConfirmPaymentStatus.ClosedPeriod
                };
            }

            if (vex.Reason == ValidationFailedReason.OperationTypeNotAllowed)
            {
                return new OutsourceConfirmResult
                {
                    DocumentBaseId = request.DocumentBaseId,
                    Status = OutsourceConfirmPaymentStatus.Error
                };
            }

            // пытаемся сохранить невалидную операцию в "красную таблицу"
            saveRequest.OperationState = OperationState.OutsourceProcessingValidationFailed;

            logger.LogDebug(vex, $"Ошибка валидации {typeof(T)}.{vex.Name}");
        }

        if (saveRequest.OperationState.IsBadOperationState())
        {
            saveRequest.OutsourceState = OutsourceState.ConfirmInvalid;
        }

        await UpdateAsync(saveRequest);

        return new OutsourceConfirmResult
        {
            DocumentBaseId = request.DocumentBaseId,
            Status = saveRequest.OperationState.IsBadOperationState()
                ? OutsourceConfirmPaymentStatus.BadState
                : OutsourceConfirmPaymentStatus.Ok
        };
    }

    private async Task<T> GetAsync(T request)
    {
        try
        {
            // чтобы не потерять связи и прочие неизменяемые поля, пытаемся обновить сущуствующую операцию
            return await MapToExistentAsync(request);
        }
        // операции нет в случаях:
        // 1. изначально другого типа (предполагаем, что так и есть)
        catch (OperationMismatchTypeExcepton)
        {
            return await OnChangeTypeAsync(request);
        }
        // 2. удалена (надеемся, будет обработано ниже по стеку)
        catch (OperationNotFoundException)
        {
            return null;
        }
    }

    /// <summary>
    /// Начитать существующую операцию и смапить в нее новые данные 
    /// </summary>
    protected abstract Task<T> MapToExistentAsync(T request);
    /// <summary>
    /// Доп. действия в случае смены операции
    /// </summary>
    protected virtual Task<T> OnChangeTypeAsync(T request) => Task.FromResult(request);
    protected abstract Task UpdateAsync(T request);
    protected abstract Task ValidateAsync(T request);
}