using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings.Dto;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.Abstractions.SyntheticAccounts;
using Moedelo.Money.Business.PaymentOrders.Outsource;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other;

[InjectAsSingleton]
internal sealed class OtherOutgoingOutsourceProcessor : PaymentOrderOutsourceProcessor<OtherOutgoingSaveRequest>, IOtherOutgoingOutsourceProcessor
{
    private readonly IOtherOutgoingValidator validator;
    private readonly IOtherOutgoingReader reader;
    private readonly IOtherOutgoingUpdater updater;
    private readonly IExecutionInfoContextAccessor contextAccessor;
    private readonly IAccountingPostingsClient accountingPostingsClient;
    private readonly ISyntheticAccountReader syntheticAccountReader;
    private readonly ISubcontoClient subcontoClient;
    private readonly ISettlementAccountsReader settlementAccountsReader;

    public OtherOutgoingOutsourceProcessor(
        IOtherOutgoingValidator validator,
        IOtherOutgoingReader reader,
        IOtherOutgoingUpdater updater,
        ILogger<OtherOutgoingOutsourceProcessor> logger,
        IAccountingPostingsClient accountingPostingsClient,
        IExecutionInfoContextAccessor contextAccessor,
        ISyntheticAccountReader syntheticAccountReader,
        ISubcontoClient subcontoClient,
        ISettlementAccountsReader settlementAccountsReader)
        : base(logger)
    {
        this.validator = validator;
        this.reader = reader;
        this.updater = updater;
        this.accountingPostingsClient = accountingPostingsClient;
        this.contextAccessor = contextAccessor;
        this.syntheticAccountReader = syntheticAccountReader;
        this.subcontoClient = subcontoClient;
        this.settlementAccountsReader = settlementAccountsReader;
    }

    public new async Task<OutsourceConfirmResult> ConfirmAsync(OtherOutgoingSaveRequest request)
    {
        // Сгенерируем БУ проводку если было применено правило импорта в массовых операциях
        request.AccPosting = await GetOutsourcePostingAsync(request);

        return await base.ConfirmAsync(request);
    }

    protected override async Task<OtherOutgoingSaveRequest> MapToExistentAsync(OtherOutgoingSaveRequest request)
    {
        var existent = await reader.GetByBaseIdAsync(request.DocumentBaseId);
        var accPosting = request.AccPosting ?? await GetExistentAccPostingAsync(existent);

        request.ProvideInAccounting = existent?.ProvideInAccounting ?? request.ProvideInAccounting;
        var contractorChanged = existent == null || existent.Contractor?.Id != request.Contractor?.Id;

        var result = MapToExistent(existent, request);
        if (contractorChanged)
        {
            // поменялся контрагент:
            // 1. установить связанные поля в значения по умолчанию
            result.ContractBaseId = null;
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

    private async Task<OtherOutgoingCustomAccPosting> GetOutsourcePostingAsync(OtherOutgoingSaveRequest request)
    {
        if (request is not { ProvideInAccounting: true, IsOutsourceImportRuleApplied: true })
        {
            return null;
        }

        if (string.IsNullOrEmpty(request?.Contractor?.SettlementAccount))
        {
            return null;
        }

        var settlementAccountTask = settlementAccountsReader.GetByIdAsync(request.SettlementAccountId);
        var specialSettlementAccountSubcontoTask = subcontoClient.GetOrCreateTextSubcontoAsync(
            contextAccessor.ExecutionInfoContext.FirmId,
            contextAccessor.ExecutionInfoContext.UserId,
            SubcontoType.SpecialSettlementAccount,
            request?.Contractor?.SettlementAccount);

        await Task.WhenAll(settlementAccountTask, specialSettlementAccountSubcontoTask);

        return new OtherOutgoingCustomAccPosting
        {
            Date = request.Date,
            Sum = request.Sum,
            DebitCode = (int)SyntheticAccountCode._55_03,
            DebitSubconto = new[]
            {
                new Subconto
                {
                    Id = specialSettlementAccountSubcontoTask.Result.Id,
                    Name = specialSettlementAccountSubcontoTask.Result.Name,
                }
            },
            //CreditCode = (int)SyntheticAccountCode._51_01,
            CreditSubconto = settlementAccountTask.Result.SubcontoId.GetValueOrDefault(),
            Description = request.Description
        };
    }

    private async Task<OtherOutgoingCustomAccPosting> OmitKontragentAndContractAsync(OtherOutgoingCustomAccPosting existentPosting)
    {
        if (existentPosting?.DebitSubconto == null)
        {
            return existentPosting;
        }
        
        var rules = await syntheticAccountReader
            .GetSubcontoRulesAsync((SyntheticAccountCode)existentPosting.DebitCode);

        existentPosting.DebitSubconto = existentPosting.DebitSubconto
            .OmitTypesByRules(rules)
            ?.ToArray();

        return existentPosting;
    }

    private static OtherOutgoingSaveRequest MapToExistent(OtherOutgoingResponse existent, OtherOutgoingSaveRequest newValues)
    {
        var result = OtherOutgoingMapper.MapToSaveRequest(existent);

        result.Contractor = newValues.Contractor;
        result.IncludeNds = newValues.IncludeNds;
        result.NdsType = newValues.NdsType;
        result.NdsSum = newValues.NdsSum;
        result.OperationState = newValues.OperationState;
        result.OutsourceState = newValues.OutsourceState;
        result.TaxPostings = newValues.TaxPostings;
        result.ProvideInAccounting = newValues.ProvideInAccounting;
        result.AccPosting = newValues.AccPosting;

        return result;
    }

    private async Task<OtherOutgoingCustomAccPosting> GetExistentAccPostingAsync(OtherOutgoingResponse existent)
    {
        if (existent is not { ProvideInAccounting: true })
        {
            return null;
        }
        
        var response = await accountingPostingsClient.GetByAsync(
            contextAccessor.ExecutionInfoContext.FirmId,
            contextAccessor.ExecutionInfoContext.UserId,
            new AccountingPostingsSearchCriteriaDto
            {
                DocumentBaseIds = new[] { existent.DocumentBaseId },
                IsFromReadOnlyDb = true
            });

        if (response?.Length != 1)
        {
            return null;
        }

        var dto = response[0];
        return new OtherOutgoingCustomAccPosting
        {
            Date = dto.Date,
            Description = dto.Description,
            Sum = dto.Sum,
            DebitCode = (int)dto.DebitCode,
            DebitSubconto = dto.DebitSubcontos?.Select(id => new Subconto { Id = id }).ToArray() ?? Array.Empty<Subconto>(),
            // если нет субконто по 51 счету, то лучше упасть, чем в остановить очередь БУ, положив невалидные данные
            CreditSubconto = dto.CreditSubcontos!.First()
        };
    }

    protected override Task ValidateAsync(OtherOutgoingSaveRequest request) => validator.ValidateAsync(request);
    protected override Task UpdateAsync(OtherOutgoingSaveRequest request) => updater.UpdateAsync(request);
}