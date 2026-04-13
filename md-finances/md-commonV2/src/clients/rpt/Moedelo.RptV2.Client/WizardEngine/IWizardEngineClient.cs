using Moedelo.Common.Enums.Enums.Calendar;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto.WizardEngine;
using System.Threading.Tasks;

namespace Moedelo.RptV2.Client.WizardEngine
{
    public interface IWizardEngineClient : IDI
    {
        Task<ChangeStatusResultDto> CompleteWizardAsync(int firmId, int userId, int eventId, CalendarEventType eventType);

        Task<ChangeStatusResultDto> ReopenWizardAsync(int firmId, int userId, int eventId, CalendarEventType eventType, bool confirmed);
    }
}
