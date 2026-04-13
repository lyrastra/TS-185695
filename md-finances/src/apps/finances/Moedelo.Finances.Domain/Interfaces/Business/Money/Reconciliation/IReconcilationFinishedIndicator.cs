
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation
{
    /// <summary>
    /// Индикатор завершения сверки с банком
    /// </summary>
    public interface IReconcilationFinishedIndicator : IDI
    {
        /// <summary>
        /// Включение индикатора
        /// </summary>
        Task IdicatorOnAsync(int firmId, ReconcilationFinishedIndicatorData data);

        /// <summary>
        /// Однократный показ индикатора по недавно пройденой сверке с банком
        /// </summary>
        Task<ReconcilationFinishedIndicatorData> LetSeeAsync(int firmId);
    }
}
