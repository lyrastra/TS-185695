using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.SqlDataAccess.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Interfaces;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.DataAccess.Abstractions;
using Moedelo.Money.PaymentOrders.DataAccess.Extensions;
using Moedelo.Money.PaymentOrders.Domain.Models;

namespace Moedelo.Money.PaymentOrders.DataAccess.PaymentOrders
{
    [InjectAsSingleton(typeof(ICurrencyPaymentOrderDao))]
    internal sealed class CurrencyPaymentOrderDao : MoedeloSqlDbExecutorBase, ICurrencyPaymentOrderDao
    {
        private readonly OperationType[] currencyOperationsTypes =
        {
            OperationType.PaymentOrderIncomingCurrencyPurchase, // Вх. Поступление от покупки валюты
            OperationType.PaymentOrderOutgoingCurrencySale, // Вх. Поступление валюты после покупки
            OperationType.PaymentOrderIncomingCurrencySale, // Вх. Поступление от продажи валюты
            OperationType.PaymentOrderIncomingCurrencyOther, // Вх. Прочее валютное поступление
            OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer, // Вх. Валютная оплата от покупателя
            OperationType.PaymentOrderIncomingCurrencyTransferFromAccount, // Вх. Валютный перевод со счёта
            
            OperationType.PaymentOrderOutgoingCurrencyPurchase, // Исх. покупка валюты
            OperationType.CurrencyBankFee, // Исх. Валютная коммиссия банка
            OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier, // Исх. Валютная оплата поставщику
            OperationType.PaymentOrderOutgoingCurrencyOther, // Исх. Валютнное прочее
            OperationType.PaymentOrderOutgoingCurrencyTransferToAccount // Исх. Валютный перевод на счёт
        };

        private readonly ISqlScriptReader scriptReader;

        public CurrencyPaymentOrderDao(
            ISqlScriptReader scriptReader,
            ISqlDbExecutor dbExecutor,
            ISettingRepository settings,
            IAuditTracer auditTracer)
            : base(dbExecutor, settings.GetConnectionString(), auditTracer)
        {
            this.scriptReader = scriptReader;
        }
        
        public Task<IReadOnlyList<PaymentOrder>> ByPeriodAsync(int firmId, PeriodRequest request)
        {
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "PaymentOrders.Scripts.Select.sql"))
                .IncludeLine("CurrencyOperationsTypesFilter")
                .IncludeLine("PeriodFilter")
                .ToString();

            var param = new
            {
                firmId,
                request.StartDate,
                request.EndDate,
                OperationStateDefault = OperationState.Default,
                currencyOperationsTypes,
                badStates = HiddenOperationStates.All
            };
            var queryObject = new QueryObject(sql, param);
            return QueryAsync<PaymentOrder>(queryObject);
        }

        public Task<IReadOnlyList<CurrencyBalance>> BalanceOnDateAsync(int firmId, DateTime date, DateTime? balanceDate)
        {
            var sql = scriptReader.Get(this, "PaymentOrders.Scripts.CurrencyBalance.sql");
            var param = new
            {
                firmId,
                date,
                operationStateDefault = OperationState.Default,
                incomingDirection = MoneyDirection.Incoming,
                outgoingDirection = MoneyDirection.Outgoing,
                currencyOperationsTypes,
                badStates = HiddenOperationStates.All,
                balanceDate = balanceDate ?? SqlDateTime.MinValue.Value
            };
            var queryObject = new QueryObject(sql, param);
            return QueryAsync<CurrencyBalance>(queryObject);
        }

        public Task<IReadOnlyList<PaymentOrder>> ByBaseIdsAsync(int firmId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult<IReadOnlyList<PaymentOrder>>(Array.Empty<PaymentOrder>());
            }
            
            var sql = new SqlQueryBuilder(scriptReader.Get(this, "PaymentOrders.Scripts.Select.sql"))
                .IncludeLine("CurrencyOperationsTypesFilter")
                .IncludeLine("SelectListFilter")
                .ToString();
            
            var param = new
            {
                firmId,
                OperationStateDefault = OperationState.Default,
                currencyOperationsTypes,
                badStates = HiddenOperationStates.All
            };
            
            var tempTables = new[]
            {
                baseIds.ToTempBigIntIds("BaseIds")
            };
            
            var queryObject = new QueryObject(sql, param, temporaryTables: tempTables);
            return QueryAsync<PaymentOrder>(queryObject);
        }
    }
}