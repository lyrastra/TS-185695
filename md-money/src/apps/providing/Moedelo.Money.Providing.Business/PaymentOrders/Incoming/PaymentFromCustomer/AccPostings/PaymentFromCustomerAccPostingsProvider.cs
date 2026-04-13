using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.AccountingPostings;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.SettlementAccounts;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.AccPostings
{
    [InjectAsSingleton(typeof(PaymentFromCustomerAccPostingsProvider))]
    internal class PaymentFromCustomerAccPostingsProvider
    {
        private readonly AccountingPostingsSaver accountingPostingsSaver;
        private readonly AccountingPostingsRemover accountingPostingsRemover;
        private readonly SettlementAccountReader settlementAccountReader;

        public PaymentFromCustomerAccPostingsProvider(
            AccountingPostingsSaver accountingPostingsSaver,
            AccountingPostingsRemover accountingPostingsRemover,
            SettlementAccountReader settlementAccountReader)
        {
            this.accountingPostingsSaver = accountingPostingsSaver;
            this.accountingPostingsRemover = accountingPostingsRemover;
            this.settlementAccountReader = settlementAccountReader;
        }

        public async Task ProvideAsync(PaymentFromCustomerAccPostingsProvideRequest request)
        {
            await accountingPostingsRemover.DeleteAsync(request.DocumentBaseId);

            if (request.IsBadOperationState || request.ProvideInAccounting == false)
            {
                return;
            }

            var settlementAccount = await settlementAccountReader.GetByIdAsync(request.SettlementAccountId);

            var accPostingGenerateRequest = new PaymentFromCustomerAccPostingGenerateRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Sum = request.Sum,
                IsMediation = request.IsMediation,
                SettlementAccount = settlementAccount,
                Kontragent = request.Kontragent,
                Contract = request.Contract,
                Documents = request.BaseDocuments,
                IsMainKontragent = request.IsMainKontragent,
            };
            var accPosting = PaymentFromCustomerAccPostingsGenerator.Generate(accPostingGenerateRequest);
            await accountingPostingsSaver.OverwriteAsync(request.DocumentBaseId, new[] { accPosting });
        }
    }
}
