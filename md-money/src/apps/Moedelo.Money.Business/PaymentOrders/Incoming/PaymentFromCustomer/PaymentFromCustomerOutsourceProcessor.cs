using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer;

[InjectAsSingleton(typeof(IPaymentFromCustomerOutsourceProcessor))]
internal sealed class PaymentFromCustomerOutsourceProcessor : PaymentOrderOutsourceProcessor<PaymentFromCustomerSaveRequest>, IPaymentFromCustomerOutsourceProcessor
{
    private readonly IPaymentFromCustomerValidator validator;
    private readonly IPaymentFromCustomerReader reader;
    private readonly IPaymentFromCustomerUpdater updater;

    public PaymentFromCustomerOutsourceProcessor(
        IPaymentFromCustomerValidator validator,
        IPaymentFromCustomerReader reader,
        IPaymentFromCustomerUpdater updater,
        ILogger<PaymentFromCustomerOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(PaymentFromCustomerSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<PaymentFromCustomerSaveRequest> MapToExistentAsync(PaymentFromCustomerSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    protected override Task UpdateAsync(PaymentFromCustomerSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(PaymentFromCustomerSaveRequest request) => validator.ValidateAsync(request);

    private static PaymentFromCustomerSaveRequest MapToExistent(
        PaymentFromCustomerResponse existent,
        PaymentFromCustomerSaveRequest newValues)
    {
        var result = PaymentFromCustomerMapper.MapToSaveRequest(existent);

        result.TaxPostings ??= new TaxPostingsData { ProvidePostingType = ProvidePostingType.Auto };
        result.Kontragent = newValues.Kontragent;
        result.IsMediation = newValues.IsMediation;
        result.MediationCommissionSum = newValues.MediationCommissionSum;
        result.TaxationSystemType = newValues.TaxationSystemType;
        result.PatentId = newValues.PatentId;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;
        result.MediationNdsType = newValues.MediationNdsType;
        result.MediationNdsSum = newValues.MediationNdsSum;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        
        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = null;
            result.BillLinks = Array.Empty<BillLinkSaveRequest>();
            result.DocumentLinks = Array.Empty<DocumentLinkSaveRequest>();
            result.InvoiceLinks = Array.Empty<InvoiceLinkSaveRequest>();
            result.IsMainContractor = true;
        }

        return result;
    }
}