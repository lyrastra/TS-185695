using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AutoCreation;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.AutoCreation
{
    public interface IAutoCreationSheduleApiClient : IDI
    {
        Task<List<int>> GetFirmsWithActiveSchedulesAsync(DateTime date);

        Task<PrimaryDocumentAutoCreationInfoDto> GetDocumentAutoCreationInfo(int firmId, int userId, long documentBaseId);

        Task<List<long>> RunAutoCreationSchedulesAsync(int firmId, int userId, DateTime untilDate);

        Task DeleteScheduleForDocumentAsync(int firmId, int userId, long documentBaseId);
    }
}