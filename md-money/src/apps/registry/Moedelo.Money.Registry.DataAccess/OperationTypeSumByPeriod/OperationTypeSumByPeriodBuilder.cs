using System.Linq;
using System.Collections.Generic;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Extensions;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;
using Moedelo.Infrastructure.SqlDataAccess.Abstractions.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Registry.Domain.Models.OperationTypeSumByPeriod;

namespace Moedelo.Money.Registry.DataAccess.OperationTypeSumByPeriod;

static class OperationTypeSumByPeriodBuilder
{
    internal static QueryObject Get(string sqlTemplate, int firmId, OperationTypeSumByPeriodRequest request)
    {
        var isNeedTurnFilterByAcquiringCommissionDate = IsNeedTurnFilterByAcquiringCommissionDate(request);

        var builder = new SqlQueryBuilder(sqlTemplate)
            .IncludeLineIf("OperationDateFilter", !isNeedTurnFilterByAcquiringCommissionDate)
            .IncludeLineIf("OperationOrAcquiringCommissionDateFilter", isNeedTurnFilterByAcquiringCommissionDate);

        var param = new
        {
            firmId = firmId,
            startDate = request.StartDate,
            endDate = request.EndDate,

            //ниже указаны константы
            OperationTypePaymentOrderIncomingCurrencyPurchase = OperationType.PaymentOrderIncomingCurrencyPurchase,
            OperationTypePaymentOrderOutgoingCurrencySale = OperationType.PaymentOrderOutgoingCurrencySale,
            OperationTypePaymentOrderIncomingCurrencyOther = OperationType.PaymentOrderIncomingCurrencyOther,
            OperationTypeCurrencyBankFee = OperationType.CurrencyBankFee,
            OperationTypePaymentOrderOutgoingCurrencyPaymentToSupplier = OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier,
            OperationTypePaymentOrderOutgoingCurrencyOther = OperationType.PaymentOrderOutgoingCurrencyOther,
            OperationTypePaymentOrderIncomingCurrencyPaymentFromCustomer = OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer,

            OperationStateDefault = OperationState.Default,
            PayedStatus = PaymentStatus.Payed,
        };

        var tempTableList = new List<TemporaryTable>()
        {
            OperationStateExtensions.BadOperationStates.Cast<int>().ToTempIntIds("BadStates")
        };

        //TODO фильтр по типам пока не делаем
        //tempTableList.Add(request.OperationTypes.Cast<int>().ToTempIntIds("OperationTypes"));

        return new QueryObject(builder.ToString(), param, null, tempTableList);
    }

    /// <summary>
    /// нужно ли включить фильтр по дате комиссии (для операции эквайринга)?
    /// </summary>
    private static bool IsNeedTurnFilterByAcquiringCommissionDate(OperationTypeSumByPeriodRequest model)
    {
        if (model.OperationTypes == null)
        {
            return true;
        }

        return model.OperationTypes.Length == 0 || model.OperationTypes.Contains(OperationType.MemorialWarrantRetailRevenue);
    }
}
