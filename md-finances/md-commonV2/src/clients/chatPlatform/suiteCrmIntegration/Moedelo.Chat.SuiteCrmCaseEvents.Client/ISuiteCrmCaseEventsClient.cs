using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Chat.SuiteCrmCaseEvents.Client
{
    public interface ISuiteCrmCaseEventsClient: IDI
    {
        Task<bool> AddAllMessagesToCaseAsync(string caseId, Guid requestId);

        Task<bool> AddMessagesToCaseAsync(string caseId, IReadOnlyCollection<Guid> messagesIds);

        Task<bool> AddMessageAttachmentToCaseAsync(string caseId, Guid messageAttachmentId);
    }
}
