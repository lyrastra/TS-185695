using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.AccPostings.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.Abstractions.SyntheticAccounts;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other;

[InjectAsSingleton]
internal sealed class OtherIncomingOutsourceProcessor : PaymentOrderOutsourceProcessor<OtherIncomingSaveRequest>, IOtherIncomingOutsourceProcessor
{
    private readonly IOtherIncomingValidator validator;
    private readonly IOtherIncomingReader reader;
    private readonly IOtherIncomingUpdater updater;
    private readonly ISyntheticAccountReader syntheticAccountReader;
    private readonly OtherIncomingAccPostingGetter accPostingGetter;
    private readonly IFirmRequisitesReader requisitesReader;

    public OtherIncomingOutsourceProcessor(
        IOtherIncomingValidator validator,
        IOtherIncomingReader reader,
        IOtherIncomingUpdater updater,
        ILogger<OtherIncomingOutsourceProcessor> logger,
        ISyntheticAccountReader syntheticAccountReader,
        OtherIncomingAccPostingGetter accPostingGetter,
        IFirmRequisitesReader requisitesReader) 
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.syntheticAccountReader = syntheticAccountReader;
        this.accPostingGetter = accPostingGetter;
        this.requisitesReader = requisitesReader;
    }

    public new async Task<OutsourceConfirmResult> ConfirmAsync(OtherIncomingSaveRequest request)
    {
        // Сгенерируем БУ проводку если было применено правило импорта в массовых операциях
        request.AccPosting = await accPostingGetter.GetOutsourcePostingAsync(request);

        return await base.ConfirmAsync(request);
    }

    protected override async Task<OtherIncomingSaveRequest> MapToExistentAsync(OtherIncomingSaveRequest request)
    {
        if (request.IsTargetIncome && await requisitesReader.IsOooAsync())
        {
            // На 12.12.2023 "Целевое поступление" реализовано только для ИП. Для ООО отрезается валидацией.
            // Генерируем нужный БУ и выключаем признак, иначе в ЛК нельзя будет пересохранить платеж.
            request.AccPosting = await accPostingGetter.GetTargetIncomeAsync(request);
            request.IsTargetIncome = false;
        }

        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        var accPosting = request.AccPosting ?? await accPostingGetter.GetExistentAsync(existent);

        request.ProvideInAccounting = existent?.ProvideInAccounting ?? request.ProvideInAccounting;
        var contractorChanged = existent == null || existent.Contractor?.Id != request.Contractor?.Id;
        var result = MapToExistent(existent, request);

        if (contractorChanged)
        {
            // поменялся контрагент:
            // 1. установить связанные поля в значения по умолчанию
            result.ContractBaseId = null;
            result.BillLinks = Array.Empty<BillLinkSaveRequest>();
            // 2. в БУ сбросить субконто типов Контрагент, Договор по КТ
            result.AccPosting = await OmitKontragentAndContractAsync(accPosting);
        }
        else
        {
            result.ProvideInAccounting = existent.ProvideInAccounting;
            result.AccPosting = accPosting;
            // в таком случае НУ не должен обрабатываться => останется старая проводка, если была
            result.TaxPostings.ProvidePostingType = ProvidePostingType.Auto;
        }

        return result;
    }

    private async Task<OtherIncomingCustomAccPosting> OmitKontragentAndContractAsync(OtherIncomingCustomAccPosting existentPosting)
    {
        if (existentPosting?.CreditSubconto == null)
        {
            return existentPosting;
        }
        
        var rules = await syntheticAccountReader
            .GetSubcontoRulesAsync((SyntheticAccountCode)existentPosting.CreditCode);

        existentPosting.CreditSubconto = existentPosting.CreditSubconto
            .OmitTypesByRules(rules)
            ?.ToArray();

        return existentPosting;
    }

    private static OtherIncomingSaveRequest MapToExistent(OtherIncomingResponse existent,
        OtherIncomingSaveRequest newValues)
    {
        var result = OtherIncomingMapper.MapToSaveRequest(existent);

        result.Contractor = newValues.Contractor;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;
        result.TaxPostings = newValues.TaxPostings;
        result.AccPosting = newValues.AccPosting;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.IsTargetIncome = newValues.IsTargetIncome;

        return result;
    }

    protected override Task ValidateAsync(OtherIncomingSaveRequest request) => validator.ValidateAsync(request);

    protected override Task UpdateAsync(OtherIncomingSaveRequest request) => updater.UpdateAsync(request);
}