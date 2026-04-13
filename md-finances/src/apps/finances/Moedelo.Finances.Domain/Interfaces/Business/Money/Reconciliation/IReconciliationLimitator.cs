using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation
{
    public interface IReconciliationLimitator : IDI
    {
        Task<ReconciliationLimits> SetLimitsAsync(ReconciliationForUserRequest request);

        Task DeleteLimitsAsync(ReconciliationForUserRequest request);

        Task<(bool?, ReconciliationLimits)> CheckLimitsAsync(ReconciliationForUserRequest request);

        Task<ReconciliationLimits> GetLimitsAsync(ReconciliationForUserRequest request);

        int GetMaxCycleNumber();
    }





    public class ReconciliationLimits
    {
        public ReconciliationLimits()
        {
            ReconciledDate = DateTime.Today.AddYears(1);
            CurrentCycleNumber = 0;
        }
        /// <summary> Уже сверенная дата(позже нее уже запрашивать нельзя) </summary>
        public DateTime ReconciledDate { get; set; }

        /// <summary> Количество циклов текущей сверки </summary>
        public int CurrentCycleNumber { get; set; }
    }
}
