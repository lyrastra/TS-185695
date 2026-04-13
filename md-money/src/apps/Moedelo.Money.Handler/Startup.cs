using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moedelo.Common.Audit.Middleware;
using Moedelo.Common.Consul.AspNetCore.Extensions;
using Moedelo.Common.Consul.ServiceDiscovery.Extensions;
using Moedelo.Common.Kafka.Monitoring.Extensions;
using Moedelo.Infrastructure.AspNetCore.Extensions;
using Moedelo.Infrastructure.DependencyInjection;
using Moedelo.Infrastructure.DependencyInjection.Warmup;
using Moedelo.Money.Handler.HostedServices;
using Moedelo.Money.Handler.HostedServices.AccountingStatements;
using Moedelo.Money.Handler.HostedServices.CashOrders;
using Moedelo.Money.Handler.HostedServices.PaymentOrders;
using Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming;
using Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing;
using Moedelo.Money.Handler.HostedServices.PurseOperations;

namespace Moedelo.Money.Handler
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterByDIAttribute("Moedelo.*");
            services.AddMoedeloServiceDiscovery();
            services.AddMoedeloKafkaConsumersMonitoring();

            // банк, поступления
            services
                .AddHostedService<IncomingCurrencyPurchaseCommandsHostedService>()
                .AddHostedService<IncomingCurrencySaleCommandsHostedService>()
                .AddHostedService<CurrencyPaymentFromCustomerHostedCommandsService>()
                .AddHostedService<CurrencyTransferFromAccountCommandsHostedService>()
                .AddHostedService<ContributionOfOwnFundsCommandsHostedService>()
                .AddHostedService<ContributionToAuthorizedCapitalCommandsHostedService>()
                .AddHostedService<FinancialAssistanceCommandsHostedService>()
                .AddHostedService<LoanObtainingCommandsHostedService>()
                .AddHostedService<LoanReturnCommandsHostedService>()
                .AddHostedService<RetailRevenueCommandsHostedService>()
                .AddHostedService<PaymentFromCustomerCommandsHostedService>()
                .AddHostedService<TransferFromAccountCommandsHostedService>()
                .AddHostedService<TransferFromCashCommandsHostedService>()
                .AddHostedService<TransferFromPurseCommandsHostedService>()
                .AddHostedService<AccrualOfInterestCommandsHostedService>()
                .AddHostedService<IncomeFromCommissionAgentCommandsHostedService>()
                .AddHostedService<IncomingPaymentOrderOtherCommandsHostedService>()
                .AddHostedService<RefundToSettlementAccountCommandsHostedService>()
                .AddHostedService<RefundFromAccountablePersonCommandsHostedService>()
                .AddHostedService<MediationFeeCommandsHostedService>();

            // банк, списания
            services
                .AddHostedService<OutgoingCurrencyPurchaseCommandsHostedService>()
                .AddHostedService<OutgoingCurrencySaleCommandsHostedService>()
                .AddHostedService<CurrencyPaymentToSupplierCommandsHostedService>()
                .AddHostedService<CurrencyBankFeeCommandsHostedService>()
                .AddHostedService<CurrencyTransferToAccountCommandsHostedService>()
                .AddHostedService<BankFeeCommandsHostedService>()
                .AddHostedService<BudgetaryPaymentCommandsHostedService>()
                .AddHostedService<RefundToCustomerCommandsHostedService>()
                .AddHostedService<LoanIssueCommandsHostedService>()
                .AddHostedService<LoanRepaymentCommandsHostedService>()
                .AddHostedService<PaymentToAccountablePersonCommandsHostedService>()
                .AddHostedService<PaymentToNaturalPersonsCommandsHostedService>()
                .AddHostedService<PaymentToSupplierCommandsHostedService>()
                .AddHostedService<TransferToAccountCommandsHostedService>()
                .AddHostedService<WithdrawalFromAccountCommandsHostedService>()
                .AddHostedService<WithdrawalOfProfitCommandsHostedService>()
                .AddHostedService<OutgoingPaymentOrderOtherCommandsHostedService>()
                .AddHostedService<OutgoingPaymentOrderDeductionCommandsHostedService>()
                .AddHostedService<AgencyContractCommandsHostedService>()
                .AddHostedService<UnifiedBudgetaryPaymentCommandsHostedService>()
                .AddHostedService<RentPaymentCommandsHostedService>();

            // банк, разное
            services
                .AddHostedService<PaymentOrdersBatchProvideCommandHostedService>()
                .AddHostedService<PaymentOrderOutgoingCommandHostedService>()
                .AddHostedService<PaymentOrderSetMissingEmployeeCommandHostedService>();

            // кошельки
            services
                .AddHostedService<PurseOperationCommandHostedService>();

            // массовая смена СНО
            services
                .AddHostedService<ChangeTaxationSystemCommandHostedService>()
                .AddHostedService<TaxationSystemChangedEventHostedService>()
                .AddHostedService<PaymentOrderChangeTaxationSystemCommandHostedService>()
                .AddHostedService<CashOrderChangeTaxationSystemCommandHostedService>()
                .AddHostedService<PurseOperationChangeTaxationSystemCommandHostedService>();

            // счета (не актуально)
            //services.AddHostedService<BillAndPaymentChangeLinkEventsReadHostedService>();

            // первичка
            services
                .AddHostedService<ReceiptStatementAndPaymentChangeLinkEventReadHostedService>()
                .AddHostedService<PurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventReadHostedService>();

            // бух. справки
            services
                .AddHostedService<AcquiringCommissionAccountingStatementEventHostedService>()
                .AddHostedService<TradingFeeAccountingStatementEventHostedService>();

            services.AddWarmup();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/MoneyHandler");
            app.LogMoedeloConsulLoadingErrors();
            app.UsePing();
            app.UseAuditApiHandlerTrace();
        }
    }
}
