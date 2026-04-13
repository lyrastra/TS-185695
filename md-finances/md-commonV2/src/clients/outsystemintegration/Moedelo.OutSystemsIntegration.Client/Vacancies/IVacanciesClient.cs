using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto.Vacancies;

namespace Moedelo.OutSystemsIntegrationV2.Client.Vacancies
{
    public interface IVacanciesClient : IDI
    {
        Task<VacanciesDto> GetVacanciesByInnAsync(string inn, int? limit, int? offset, TimeSpan? timeout = null);

        Task<VacanciesDto> GetVacancyAsync(Guid id, string companyCode);
    }
}