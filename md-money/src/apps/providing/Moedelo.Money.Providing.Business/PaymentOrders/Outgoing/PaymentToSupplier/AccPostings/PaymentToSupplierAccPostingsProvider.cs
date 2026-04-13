using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.AccountingPostings;
using Moedelo.Money.Providing.Business.AccountingPostings.PaymentOrders.Outgoing;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.StockVisibility;
using Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.Subcontos;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models;
using Moedelo.Money.Providing.Business.SettlementAccounts;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier
{
    [InjectAsSingleton(typeof(PaymentToSupplierAccPostingsProvider))]
    class PaymentToSupplierAccPostingsProvider
    {
        private readonly AccountingPostingsSaver accountingPostingsSaver;
        private readonly AccountingPostingsRemover accountingPostingsRemover;
        private readonly SettlementAccountReader settlementAccountReader;
        private readonly PaymentToSupplierSubcontoReader subcontoReader;
        private readonly StockVisibilityInspector stockVisibilityInspector;

        public PaymentToSupplierAccPostingsProvider(
            AccountingPostingsSaver accountingPostingsSaver,
            AccountingPostingsRemover accountingPostingsRemover,
            SettlementAccountReader settlementAccountReader,
            PaymentToSupplierSubcontoReader subcontoReader,
            StockVisibilityInspector stockVisibilityInspector)
        {
            this.accountingPostingsSaver = accountingPostingsSaver;
            this.accountingPostingsRemover = accountingPostingsRemover;
            this.settlementAccountReader = settlementAccountReader;
            this.subcontoReader = subcontoReader;
            this.stockVisibilityInspector = stockVisibilityInspector;
        }

        public async Task ProvideAsync(PaymentToSupplierAccPostingsProvideRequest request)
        {
            await accountingPostingsRemover.DeleteAsync(request.DocumentBaseId);

            if (request.IsBadOperationState || request.ProvideInAccounting == false)
            {
                return;
            }

            var settlementAccount = await settlementAccountReader.GetByIdAsync(request.SettlementAccountId);

            var accPostingGenerateRequest = new PaymentToSupplierAccPostingGenerateRequest
            {
                PaymentBaseId = request.DocumentBaseId,
                PaymentDate = request.Date,
                PaymentSum = request.Sum,
                IsMainKontragent = request.IsMainKontragent,
                SettlementAccount = settlementAccount,
                Kontragent = request.Kontragent,
                Contract = request.Contract,
                BaseDocuments = request.BaseDocuments,
                IsStockInvisible = await stockVisibilityInspector.IsStockInVisible(request.Date.Year)
            };
            await FillSubcontoAsync(accPostingGenerateRequest); 
            var accPostings = PaymentToSupplierAccPostingsGenerator.Generate(accPostingGenerateRequest);
            await accountingPostingsSaver.OverwriteAsync(request.DocumentBaseId, accPostings);
        }

        private async Task FillSubcontoAsync(PaymentToSupplierAccPostingGenerateRequest request)
        {
            if (request.IsStockInvisible)
            {
                var costItemsSubcontoTask = subcontoReader.GetCostItemsSubcontoAsync();
                var divisionSubcontoTask = subcontoReader.GetDivisionSubcontoAsync();

                await Task.WhenAll(costItemsSubcontoTask, divisionSubcontoTask);
                request.CostItemsSubconto = costItemsSubcontoTask.Result;
                request.DivisionSubconto = divisionSubcontoTask.Result;
            }
        }
    }
}
