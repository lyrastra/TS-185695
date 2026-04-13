using System;
using System.Collections.Generic;
using Moedelo.Money.Registry.Domain;
using Moedelo.Money.Registry.Domain.Models;
using System.Threading.Tasks;
using Moedelo.Money.Registry.Domain.Models.SelfCostPayments;

namespace Moedelo.Money.Registry.DataAccess.Abstractions
{
    public interface IRegistryDao
    {
        Task<ListWithCount<MoneyOperation>> GetAsync(int firmId, RegistryQuery query);

        /// <summary>
        /// Возвращает объединение исходящих платежей по кассе и рассчетным счетам для Виждетов НДС и Налога на прибыль 
        /// </summary>
        Task<List<MoneyOperation>> GetOutgoingPaymentsForTaxWidgetsAsync(int firmId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Возвращает П/П (банк), участвующие в расчете себетоимости 
        /// </summary>
        Task<IReadOnlyList<SelfCostPayment>> GetBankSelfCostPaymentsAsync(int firmId, SelfCostPaymentRequest request);
        
        /// <summary>
        /// Возвращает РКО (касса), участвующие в расчете себетоимости 
        /// </summary>
        Task<IReadOnlyList<SelfCostPayment>> GetCashSelfCostPaymentsAsync(int firmId, SelfCostPaymentRequest request);
    }
}
