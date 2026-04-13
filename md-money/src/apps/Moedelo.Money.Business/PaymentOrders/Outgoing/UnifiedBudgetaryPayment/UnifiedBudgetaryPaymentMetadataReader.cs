using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentMetadataReader))]
    internal sealed class UnifiedBudgetaryPaymentMetadataReader : IUnifiedBudgetaryPaymentMetadataReader
    {
        private readonly BudgetaryPaymentApiClient apiClient;
        private readonly IUnifiedBudgetaryPaymentAccountsReader accountsReader;
        private readonly IBudgetaryStatusOfPayerApiClient budgetaryStatusOfPayerApiClient;

        private static readonly BudgetaryPaymentBase[] generalReasonCodes = new[]
        {
            BudgetaryPaymentBase.Other
        };
        private static readonly BudgetaryPayerStatus[] statusCodes = new[]
        {
            BudgetaryPayerStatus.Company,
            //BudgetaryPayerStatus.OtherTaxPayer
        };

        public UnifiedBudgetaryPaymentMetadataReader(
            BudgetaryPaymentApiClient apiClient,
            IUnifiedBudgetaryPaymentAccountsReader accountsReader,
            IBudgetaryStatusOfPayerApiClient budgetaryStatusOfPayerApiClient)
        {
            this.apiClient = apiClient;
            this.accountsReader = accountsReader;
            this.budgetaryStatusOfPayerApiClient = budgetaryStatusOfPayerApiClient;
        }

        public async Task<BudgetaryPaymentMetadata> GetAsync()
        {
            var getAccountsTask = accountsReader.GetAsync();
            var getPaymentReasonsTask = apiClient.GetPaymentReasonsAsync();
            var getStatusOfPayerTask = budgetaryStatusOfPayerApiClient.GetListAsync();

            await Task.WhenAll(getAccountsTask, getPaymentReasonsTask, getStatusOfPayerTask);
            var allReasons = getPaymentReasonsTask.Result;

            var result = new BudgetaryPaymentMetadata
            {
                Accounts = getAccountsTask.Result,
                StatusOfPayers = MapToBudgetaryStatusOfPayer(getStatusOfPayerTask.Result),
                PaymentReasons = GetGeneralReasons(allReasons),
                PaymentSubReasons = Array.Empty<BudgetaryPaymentReason>()
            };

            return result;
        }

        private static BudgetaryPaymentReason[] GetGeneralReasons(BudgetaryPaymentReason[] reasons)
        {
            return reasons.Where(r => generalReasonCodes.Contains(r.Code)).ToArray();
        }

        private static BudgetaryStatusOfPayer[] MapToBudgetaryStatusOfPayer(IReadOnlyCollection<BudgetaryStatusOfPayerDto> dto)
        {
            return dto
                .Where(x => statusCodes.Contains((BudgetaryPayerStatus)x.Code))
                .Select(x =>
                    new BudgetaryStatusOfPayer
                    {
                        Code = (BudgetaryPayerStatus)x.Code,
                        Description = x.Description
                    }).ToArray();
        }
    }
}
