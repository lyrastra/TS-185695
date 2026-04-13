using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto;
using Moedelo.RptV2.Dto.PfrFixes;

namespace Moedelo.RptV2.Client.PfrFixes
{
    public interface IPfrFixesApiClient : IDI
    {
        [Obsolete("Use IFixedPaymentsApiClient :: GetRegistryAsync")]
        Task<FileResponseDto> GetRegistryAsync(int userId, int firmId);

        [Obsolete("Use IFixedPaymentsApiClient :: GetCompletedWizardDataByYearAsync")]
        Task<List<PfrFixesWizardDataDto>> GetCompletedWizardDataByYearAsync(int userId, int firmId, int year);
    }
}
