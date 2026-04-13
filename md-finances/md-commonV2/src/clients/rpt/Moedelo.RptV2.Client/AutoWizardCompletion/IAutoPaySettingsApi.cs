using System.Threading.Tasks;
using System.Collections.Generic;
using Moedelo.RptV2.Dto.AutoWizardCompletion;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.RptV2.Client.AutoWizardCompletion
{
    public interface IAutoPaySettingsApi : IDI
    {
        Task<IList<AutoWizardCompletionParamsDto>> GetAutoWizardCompletionParamsAsync();
        Task<IList<AutoWizardCompletionParamsDto>> GetAutoWizardCompletionParamsForPeriodAsync(int forLastDays);
    }
}
