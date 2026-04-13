using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Contracts;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount;

[InjectAsSingleton(typeof(RefundToSettlementAccountAccPostingGetter))]
internal class RefundToSettlementAccountAccPostingGetter
{
    private readonly IExecutionInfoContextAccessor contextAccessor;
    private readonly ISettlementAccountsReader settlementAccountsReader;
    private readonly IContractsReader contractsReader;
    private readonly IKontragentsReader kontragentsReader;
    private readonly ISubcontoClient subcontoClient;

    public RefundToSettlementAccountAccPostingGetter(
        IExecutionInfoContextAccessor contextAccessor,
        ISettlementAccountsReader settlementAccountsReader,
        IContractsReader contractsReader,
        IKontragentsReader kontragentsReader,
        ISubcontoClient subcontoClient)
    {
        this.contextAccessor = contextAccessor;
        this.settlementAccountsReader = settlementAccountsReader;
        this.contractsReader = contractsReader;
        this.kontragentsReader = kontragentsReader;
        this.subcontoClient = subcontoClient;
    }

    public async Task<RefundToSettlementAccountCustomAccPosting> GetDefaultAsync(RefundToSettlementAccountSaveRequest saveRequest)
    {
        var settlementAccountTask = settlementAccountsReader.GetByIdAsync(saveRequest.SettlementAccountId);
        var creditSubcontoTask = GetDefaultCreditSubcontoAsync(saveRequest);

        await Task.WhenAll(settlementAccountTask, creditSubcontoTask);
            
        return new RefundToSettlementAccountCustomAccPosting
        {
            DebitSubconto = settlementAccountTask.Result.SubcontoId!.Value,
            Date = saveRequest.Date,
            Description = "Возврат денег на счет",
            Sum = saveRequest.Sum,
            CreditCode = (int)SyntheticAccountCode._60_02,
            CreditSubconto = creditSubcontoTask.Result
        };
    }

    private async Task<List<Subconto>> GetDefaultCreditSubcontoAsync(RefundToSettlementAccountSaveRequest saveRequest)
    {
        var kontragent = saveRequest.Contractor?.Id > 0
            ? await kontragentsReader.GetByIdAsync(saveRequest.Contractor.Id)
            : null;

        if (kontragent == null)
        {
            // если контрагент не найден, то и договор искать бессмысленно
            return new List<Subconto>();
        }
        
        var contract = saveRequest.ContractBaseId.HasValue
            ? await contractsReader.GetByBaseIdAsync(saveRequest.ContractBaseId.Value)
            : null;
        contract ??= await contractsReader.GetMainContractAsync(saveRequest.Contractor.Id);

        var subcontoIds = new[] { kontragent.SubcontoId ?? 0, contract.SubcontoId ?? 0 }
            .Where(id => id > 0)
            .ToArray();

        var context = contextAccessor.ExecutionInfoContext;
        var subcontos = await subcontoClient.GetByIdsAsync(context.FirmId, context.UserId, subcontoIds);
        return subcontos
            ?.Select(subconto => new Subconto { Id = subconto.Id, Name = subconto.Name })
            .ToList() ?? new List<Subconto>();
    }
}