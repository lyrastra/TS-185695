using System;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings.Dto;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other;

[InjectAsSingleton(typeof(OtherIncomingAccPostingGetter))]
internal class OtherIncomingAccPostingGetter
{
    private readonly IAccountingPostingsClient accountingPostingsClient;
    private readonly IExecutionInfoContextAccessor contextAccessor;
    private readonly ISubcontoClient subcontoClient;
    private readonly ISettlementAccountsReader settlementAccountsReader;

    public OtherIncomingAccPostingGetter(
        IAccountingPostingsClient accountingPostingsClient,
        IExecutionInfoContextAccessor contextAccessor,
        ISubcontoClient subcontoClient,
        ISettlementAccountsReader settlementAccountsReader)
    {
        this.accountingPostingsClient = accountingPostingsClient;
        this.contextAccessor = contextAccessor;
        this.subcontoClient = subcontoClient;
        this.settlementAccountsReader = settlementAccountsReader;
    }

    public async Task<OtherIncomingCustomAccPosting> GetTargetIncomeAsync(OtherIncomingSaveRequest request)
    {
        if (request is not { ProvideInAccounting: true, IsTargetIncome: true })
        {
            return null;
        }

        var settlementAccountTask = settlementAccountsReader.GetByIdAsync(request.SettlementAccountId);
        var targetIncomeSubcontoTask = subcontoClient.GetOrCreateTextSubcontoAsync(
            contextAccessor.ExecutionInfoContext.FirmId,
            contextAccessor.ExecutionInfoContext.UserId,
            SubcontoType.AppointmentOfTrustFunds,
            "Целевое поступление");

        await Task.WhenAll(settlementAccountTask, targetIncomeSubcontoTask);

        return new OtherIncomingCustomAccPosting
        {
            Date = request.Date,
            Sum = request.Sum,
            // DebitCode = SyntheticAccountCode._51_01,
            DebitSubconto = settlementAccountTask.Result.SubcontoId.GetValueOrDefault(),
            CreditCode = (int)SyntheticAccountCode._860100,
            CreditSubconto = new[]
            {
                new Subconto
                {
                    Id = targetIncomeSubcontoTask.Result.Id,
                    Name = targetIncomeSubcontoTask.Result.Name,
                }
            },
            Description = request.Description
        };
    }

    public async Task<OtherIncomingCustomAccPosting> GetExistentAsync(OtherIncomingResponse existent)
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
        return new OtherIncomingCustomAccPosting
        {
            Date = dto.Date,
            Description = dto.Description,
            Sum = dto.Sum,
            CreditCode = (int)dto.CreditCode,
            CreditSubconto = dto.CreditSubcontos?.Select(id => new Subconto { Id = id }).ToArray() ?? Array.Empty<Subconto>(),
            // если нет субконто по 51 счету, то лучше упасть, чем в остановить очередь БУ, положив невалидные данные
            DebitSubconto = dto.DebitSubcontos!.First() 
        };
    }

    public async Task<OtherIncomingCustomAccPosting> GetOutsourcePostingAsync(OtherIncomingSaveRequest request)
    {
        if (request is not { ProvideInAccounting: true, IsTargetIncome: false, IsOutsourceImportRuleApplied: true })
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

        return new OtherIncomingCustomAccPosting
        {
            Date = request.Date,
            Sum = request.Sum,
            // DebitCode = SyntheticAccountCode._51_01,
            DebitSubconto = settlementAccountTask.Result.SubcontoId.GetValueOrDefault(),
            CreditCode = (int)SyntheticAccountCode._55_03,
            CreditSubconto = new[]
            {
                new Subconto
                {
                    Id = specialSettlementAccountSubcontoTask.Result.Id,
                    Name = specialSettlementAccountSubcontoTask.Result.Name,
                }
            },
            Description = request.Description
        };
    }
}