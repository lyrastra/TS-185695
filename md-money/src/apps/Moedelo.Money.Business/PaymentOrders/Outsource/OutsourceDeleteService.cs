using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Enums.Outsource;

namespace Moedelo.Money.Business.PaymentOrders.Outsource;

[InjectAsSingleton(typeof(IOutsourceDeleteService))]
internal sealed class OutsourceDeleteService : IOutsourceDeleteService
{
    private readonly IPaymentOrderRemover paymentOrderRemover;
    private readonly ILogger logger;

    public OutsourceDeleteService(
        IPaymentOrderRemover paymentOrderRemover,
        ILogger<OutsourceDeleteService> logger)
    {
        this.paymentOrderRemover = paymentOrderRemover;
        this.logger = logger;
    }

    public async Task<OutsourceDeleteResult> DeleteAsync(long documentBaseId)
    {
        try
        {
            await paymentOrderRemover.DeleteAsync(documentBaseId);
        }
        catch (BusinessValidationException vex)
        {
            if (vex.Reason == ValidationFailedReason.ClosedPeriod)
            {
                return new OutsourceDeleteResult
                {
                    DocumentBaseId = documentBaseId,
                    Status = OutsourceDeletePaymentStatus.ClosedPeriod
                };
            }

            throw;
        }
        catch (OperationNotFoundException)
        {
            return new OutsourceDeleteResult
            {
                DocumentBaseId = documentBaseId,
                Status = OutsourceDeletePaymentStatus.NotFound
            };
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Ошибка при удалении п/п DocumentBaseId = {documentBaseId}");
                
            return new OutsourceDeleteResult
            {
                DocumentBaseId = documentBaseId,
                Status = OutsourceDeletePaymentStatus.Error
            };
        }
        
        return new OutsourceDeleteResult
        {
            DocumentBaseId = documentBaseId,
            Status = OutsourceDeletePaymentStatus.Ok
        };
    }
}