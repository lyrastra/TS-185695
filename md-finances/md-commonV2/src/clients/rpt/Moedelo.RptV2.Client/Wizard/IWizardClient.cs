using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Reports;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto.Wizard;

namespace Moedelo.RptV2.Client.Wizard
{
    public interface IWizardClient : IDI
    {
        Task<List<ProfitDeclarationDto>> GetCompletedProfitDeclarations(int firmId, int userId);
        Task<PayerRequisitesDto> GetPayerRequisites(int firmId, int userId, int wizardId, int paymentIndex);
        Task<WizardStatusResultDto> IncompleteTradingTaxPaymentFromRequisitesAsync(int firmId, int userId, int wizardStateId);
        Task<WizardStatusResultDto> InCompleteByCalendarEventIdAsync(int firmId, int userId, int eventId, WizardTypeClientString type);
        Task<WizardStatusResultDto> CompleteByCalendarEventIdAsync(int firmId, int userId, int eventId, WizardTypeClientString type);
        Task<bool> CheckWizardIsCompleteAsync(int firmId, int userId, long wizardId);
        Task RemoveWizardAsync(int firmId, int userId, long wizardId);
        Task<int> GetWizardYearAsync(int firmId, int userId, long wizardId);
        Task<IReadOnlyCollection<WizardStateDto>> FindWizardStateByYearAndTypeAsync(
            FindWizardStateByYearAndTypeRequestDto request);
    }
}
