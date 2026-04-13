using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier;

[InjectAsSingleton]
internal sealed class PaymentToSupplierOutsourceProcessor : PaymentOrderOutsourceProcessor<PaymentToSupplierSaveRequest>, IPaymentToSupplierOutsourceProcessor
{
    private readonly IPaymentToSupplierValidator validator;
    private readonly IPaymentToSupplierReader reader;
    private readonly IPaymentToSupplierUpdater updater;

    public PaymentToSupplierOutsourceProcessor(
        IPaymentToSupplierValidator validator,
        IPaymentToSupplierReader reader,
        IPaymentToSupplierUpdater updater,
        ILogger<PaymentToSupplierOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(PaymentToSupplierSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<PaymentToSupplierSaveRequest> MapToExistentAsync(PaymentToSupplierSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static PaymentToSupplierSaveRequest MapToExistent(
        PaymentToSupplierResponse existent,
        PaymentToSupplierSaveRequest newValues)
    {
        var result = PaymentToSupplierMapper.MapToSaveRequest(existent);

        result.TaxPostings = newValues.TaxPostings;
        result.Kontragent = newValues.Kontragent;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        
        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = null;
            result.DocumentLinks = Array.Empty<DocumentLinkSaveRequest>();
            result.InvoiceLinks = Array.Empty<InvoiceLinkSaveRequest>();
            result.IsMainContractor = true;
        }

        return result;
    }

    protected override Task UpdateAsync(PaymentToSupplierSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(PaymentToSupplierSaveRequest request) => validator.ValidateAsync(request);
}