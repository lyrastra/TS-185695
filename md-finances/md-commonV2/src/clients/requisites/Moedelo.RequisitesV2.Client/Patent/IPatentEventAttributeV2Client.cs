using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.Patent;

namespace Moedelo.RequisitesV2.Client.Patent
{
    public interface IPatentEventAttributeV2Client : IDI
    {
        Task SaveAsync(int firmId, int userId, PatentEventAttributeV2Dto attribute);

        Task<List<PatentEventAttributeV2Dto>> GetByPatentIdAsync(int firmId, int userId, long patentId);

        Task<PatentEventAttributeV2Dto> GetByWizardIdAsync(int firmId, int userId, long wizardId);

        Task<List<PatentEventAttributeV2Dto>> GetAllEventsByWizardIdsAsync(int firmId, int userId,
            IReadOnlyCollection<long> wizardIds);

        Task<PatentEventAttributeV2Dto> GetByEventIdAsync(int firmId, int userId, int eventId);

        Task AssignWizardForPatentEventAsync(int firmId, int userId, long attributeId, long wizardId);
    }
}