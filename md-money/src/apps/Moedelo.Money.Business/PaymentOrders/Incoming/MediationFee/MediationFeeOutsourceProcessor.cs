using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.MediationFee;

[InjectAsSingleton(typeof(IMediationFeeOutsourceProcessor))]
internal sealed class MediationFeeOutsourceProcessor : PaymentOrderOutsourceProcessor<MediationFeeSaveRequest>, IMediationFeeOutsourceProcessor
{
    private readonly IMediationFeeValidator validator;
    private readonly IMediationFeeReader reader;
    private readonly IMediationFeeUpdater updater;

    public MediationFeeOutsourceProcessor(
        IMediationFeeValidator validator,
        IMediationFeeReader reader,
        IMediationFeeUpdater updater,
        ILogger<MediationFeeOutsourceProcessor> logger) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
    }

    public new Task<OutsourceConfirmResult> ConfirmAsync(MediationFeeSaveRequest request)
    {
        return base.ConfirmAsync(request);
    }

    protected override async Task<MediationFeeSaveRequest> MapToExistentAsync(MediationFeeSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        return MapToExistent(existent, request);
    }

    private static MediationFeeSaveRequest MapToExistent(
        MediationFeeResponse existent,
        MediationFeeSaveRequest newValues)
    {
        if (existent.Contract?.Data is null)
        {
            existent.Contract = new RemoteServiceResponse<ContractLink> { Data = new ContractLink() };
        }

        var result = MediationFeeMapper.MapToSaveRequest(existent);

        result.Kontragent = newValues.Kontragent;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;
        result.TaxPostings = new TaxPostingsData { ProvidePostingType = ProvidePostingType.Auto };

        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;

        // поменялся контрагент: нужно установить связанные поля в значения по умолчанию
        if (existent.Kontragent?.Id != newValues.Kontragent?.Id)
        {
            result.ContractBaseId = 0L;
            result.DocumentLinks = Array.Empty<DocumentLinkSaveRequest>();
            result.BillLinks = Array.Empty<BillLinkSaveRequest>();
        }

        return result;
    }

    protected override Task UpdateAsync(MediationFeeSaveRequest request) => updater.UpdateAsync(request);

    protected override Task ValidateAsync(MediationFeeSaveRequest request) => validator.ValidateAsync(request);
}