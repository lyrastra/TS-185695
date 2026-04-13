using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentMetadataReader))]
    internal sealed class BudgetaryPaymentMetadataReader : IBudgetaryPaymentMetadataReader
    {
        private static readonly DateTime reasonChangeDate = new(2021, 10, 1);
        private static readonly string newFreeDebtRepaymentDescription = "погашение задолженности, по истекшим налоговым, расчетным (отчетным) периодам, в том числе добровольное";
        private static readonly IReadOnlyCollection<BudgetaryPaymentBase> generalReasonCodes = new[]
        {
            BudgetaryPaymentBase.Tr,
            BudgetaryPaymentBase.Pr,
            BudgetaryPaymentBase.Ap,
            BudgetaryPaymentBase.Ar
        };

        private readonly BudgetaryPaymentApiClient apiClient;
        private readonly BudgetaryPaymentAccountsReader accountsReader;
        private readonly IBudgetaryStatusOfPayerApiClient budgetaryStatusOfPayerApiClient;

        public BudgetaryPaymentMetadataReader(
            BudgetaryPaymentApiClient apiClient,
            BudgetaryPaymentAccountsReader accountsReader,
            IBudgetaryStatusOfPayerApiClient budgetaryStatusOfPayerApiClient)
        {
            this.apiClient = apiClient;
            this.accountsReader = accountsReader;
            this.budgetaryStatusOfPayerApiClient = budgetaryStatusOfPayerApiClient;
        }

        public async Task<BudgetaryPaymentMetadata> GetAsync(DateTime paymentDate)
        {
            var getAccountsTask = accountsReader.GetAsync(paymentDate);
            var getPaymentReasonsTask = apiClient.GetPaymentReasonsAsync();
            var getStatusOfPayerTask = budgetaryStatusOfPayerApiClient.GetListAsync();

            await Task.WhenAll(getAccountsTask, getPaymentReasonsTask, getStatusOfPayerTask);
            var allReasons = getPaymentReasonsTask.Result;

            var result = new BudgetaryPaymentMetadata
            {
                Accounts = getAccountsTask.Result,
                StatusOfPayers = MapToBudgetaryStatusOfPayer(getStatusOfPayerTask.Result)
            };

            if (paymentDate >= reasonChangeDate)
            {
                result.PaymentReasons = GetGeneralReasons(allReasons);
                result.PaymentSubReasons = GetSubReasons(allReasons);
            }
            else
            {
                result.PaymentReasons = allReasons;
                result.PaymentSubReasons = Array.Empty<BudgetaryPaymentReason>();
            }

            return result;
        }

        private static IReadOnlyCollection<BudgetaryPaymentReason> GetGeneralReasons(IReadOnlyCollection<BudgetaryPaymentReason> reasons)
        {
            var result = reasons.Where(r => !generalReasonCodes.Contains(r.Code)).ToArray();
            var freeDebtRepayment = result.FirstOrDefault(r => r.Code == BudgetaryPaymentBase.FreeDebtRepayment);
            if (freeDebtRepayment != null)
            {
                freeDebtRepayment.Description = newFreeDebtRepaymentDescription;
            }
            return result;
        }

        private static IReadOnlyCollection<BudgetaryPaymentReason> GetSubReasons(IReadOnlyCollection<BudgetaryPaymentReason> reasons)
        {
            var result = new List<BudgetaryPaymentReason>
            {
                new BudgetaryPaymentReason
                {
                    Code = BudgetaryPaymentBase.None,
                    Description = string.Empty,
                    Designation = string.Empty
                }
            };
            result.AddRange(reasons.Where(r => generalReasonCodes.Contains(r.Code)));
            return result;
        }

        private static IReadOnlyCollection<BudgetaryStatusOfPayer> MapToBudgetaryStatusOfPayer(IReadOnlyCollection<BudgetaryStatusOfPayerDto> dto)
        {
            return dto.Select(x =>
                new BudgetaryStatusOfPayer
                {
                    Code = (BudgetaryPayerStatus)x.Code,
                    Description = x.Description
                }).ToArray();
        }
    }
}
