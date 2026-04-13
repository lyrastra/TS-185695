using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto.WizardAutoCompletion;

namespace Moedelo.RptV2.Client.AutoWizardCompletion
{
    public interface IWizardAutoCompleteApiClient : IDI
    {
        Task<AutoUsnResponse> CompleteUsnAdvanceWizardAsync(int firmId, int userId, int year, int period);
        
        Task<AutoPfrResponse> CompletePfrWizardAsync(int firmId, int userId, int period, int year);

        Task<AutoCompleteWizardResponseDto> CompleteSzvmWizardAsync(int firmId, int userId, AutoCompleteWizardRequestDto request);
        
        Task<AutoCompleteWizardResponseDto> CompleteRsvWizardAsync(int firmId, int userId, AutoCompleteWizardRequestDto request);

        Task<bool> CheckUserForUsnAsync(int firmId, int userId, int year);
    }
}