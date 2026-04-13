using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer;

[InjectAsSingleton]
internal sealed class RefundToCustomerOutsourceProcessor : PaymentOrderOutsourceProcessor<RefundToCustomerSaveRequest>, IRefundToCustomerOutsourceProcessor
{
    private readonly IRefundToCustomerValidator validator;
    private readonly IRefundToCustomerReader reader;
    private readonly IRefundToCustomerUpdater updater;

    public RefundToCustomerOutsourceProcessor(
        IRefundToCustomerValidator validator,
        IRefundToCustomerReader reader,
        IRefundToCustomerUpdater updater,
        ILogger<RefundToCustomerOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(RefundToCustomerSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<RefundToCustomerSaveRequest> MapToExistentAsync(RefundToCustomerSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static RefundToCustomerSaveRequest MapToExistent(
        RefundToCustomerResponse existent,
        RefundToCustomerSaveRequest newValues)
    {
        var result = RefundToCustomerMapper.MapToSaveRequest(existent);

        result.TaxPostings = newValues.TaxPostings;
        result.Kontragent = newValues.Kontragent;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;
        result.TaxationSystemType = newValues.TaxationSystemType;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        
        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = null;
            result.IsMainContractor = true;
        }

        return result;
    }

    protected override Task UpdateAsync(RefundToCustomerSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(RefundToCustomerSaveRequest request) => validator.ValidateAsync(request);
}