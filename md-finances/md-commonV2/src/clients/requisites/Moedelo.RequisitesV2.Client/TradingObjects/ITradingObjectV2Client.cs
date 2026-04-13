using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.TradingObjects;

namespace Moedelo.RequisitesV2.Client.TradingObjects
{
    public interface ITradingObjectV2Client : IDI
    {
        Task<TradingObjectV2Dto> GetByIdAsync(int firmId, int userId, int id);

        Task<List<TradingObjectV2Dto>> GetByCriteriaAsync(int firmId, int userId, TradingObjectCriteriaDto criteria);

        Task<List<TradingObjectV2Dto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> tradingObjectIds);

        Task<List<TradingObjectV2ShortDto>> GetShortAsync(int firmId, int userId);

        Task<List<TradingObjectV2WizardDto>> GetForWizardAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
        
        Task<TradingObjectV2EventDto> GetTradingObjectEventAsync(int firmId, int userId, int calendarId);
        
        Task<Dictionary<int, TradingObjectV2EventDto>> GetTradingObjectEventsAsync(int firmId, int userId, IReadOnlyCollection<int> calendarIds);

        Task SetTradingObjectFnsNotifiedAsync(int firmId, int id, bool fnsNotified);
        
        Task SetTradingObjectHistoryFnsNotifiedAsync(int firmId, int historyId, bool fnsNotified);
        
        Task<decimal> GetTaxSumAsync(TradingTaxCalculationV2Dto calculationDto);
    }
}