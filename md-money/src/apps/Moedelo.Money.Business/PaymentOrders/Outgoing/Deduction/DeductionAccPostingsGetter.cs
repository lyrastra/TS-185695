using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Subconto.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Contracts.ApiClient.Abstractions.legacy;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(DeductionAccPostingsGetter))]
    internal sealed class DeductionAccPostingsGetter
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISettlementAccountsReader settlementAccountsReader;
        private readonly IKontragentsReader kontragentsReader;
        private readonly IContractsApiClient contractsClient;
        private readonly ISubcontoClient subcontoClient;

        public DeductionAccPostingsGetter(
            ISettlementAccountsReader settlementAccountsReader, 
            ISubcontoClient subcontoClient, 
            IExecutionInfoContextAccessor contextAccessor, 
            IKontragentsReader kontragentsReader, 
            IContractsApiClient contractsClient)
        {
            this.settlementAccountsReader = settlementAccountsReader;
            this.subcontoClient = subcontoClient;
            this.contextAccessor = contextAccessor;
            this.kontragentsReader = kontragentsReader;
            this.contractsClient = contractsClient;
        }

        public async Task<DeductionCustomAccPosting> GetAsync(DeductionAccPostingRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var settlementAccount = await settlementAccountsReader.GetByIdAsync(request.SettlementAccountId);


            var kontragent = request.Contractor != null
                ? await kontragentsReader.GetByIdAsync(request.Contractor.Id)
                : null;
            var kontragentSubconto = kontragent?.SubcontoId == null
                ? null
                : new SubcontoDto
                {
                    Id = kontragent.SubcontoId.Value,
                    Name = request.Contractor.Name
                };

            int debitCode;
            SubcontoDto contractSubconto = null;
            if ((string.IsNullOrEmpty(request.Kbk) || request.Kbk == "0") &&
                request.PayerStatus == BudgetaryPayerStatus.DeductionFromSalary)
            {
                debitCode = 760600;
                // todo: если договор указан в операции, логичнее подставлять его, а не "Основной"
                var contract = request.Contractor != null 
                    ? await contractsClient.GetOrCreateMainContractAsync(context.FirmId, context.UserId, request.Contractor.Id)
                    : null;
                if (contract != null)
                {
                    contractSubconto = new SubcontoDto
                    {
                        Id = contract.SubcontoId!.Value,
                        Name = contract.SubcontoName
                    };
                }
            }
            else
            {
                debitCode = 764100;
                contractSubconto = await subcontoClient.GetOrCreateTextSubcontoAsync(context.FirmId, context.UserId,
                    SubcontoType.EnforcementDocuments, "Документы");
            }

            var debitSubcontos = new List<Subconto>();
            if (kontragentSubconto != null)
            {
                debitSubcontos.Add(new Subconto
                {
                    Id = kontragentSubconto.Id,
                    Name = kontragentSubconto.Name
                });
            }
        
            if (contractSubconto != null)
            {
                debitSubcontos.Add(new Subconto
                {
                    Id = contractSubconto.Id,
                    Name = contractSubconto.Name
                });
            }

            return new DeductionCustomAccPosting
            {
                CreditSubconto = settlementAccount.SubcontoId!.Value,
                Date = request.Date,
                Description = string.Empty,
                Sum = request.Sum,
                DebitCode = debitCode,
                DebitSubconto = debitSubcontos
            };
        }
    }
}