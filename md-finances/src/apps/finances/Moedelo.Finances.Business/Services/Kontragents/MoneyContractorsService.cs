using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.KontragentsV2.Client.Kontragents;
using Moedelo.KontragentsV2.Dto;
using Moedelo.PayrollV2.Client.Employees;
using Moedelo.PayrollV2.Dto.Employees;

namespace Moedelo.Finances.Business.Services.Kontragents
{
    [InjectAsSingleton(typeof(IMoneyContractorsService))]
    public class MoneyContractorsService(
        IKontragentsClient kontragentsApi,
        IEmployeesApiClient employeesApi,
        IMoneyContractorDao contractorDao) : IMoneyContractorsService
    {
        const int AutocompleteApiCount = 30;

        public async Task<List<Contractor>> GetAsync(int firmId, int userId, string query, int count,
            MoneyContractorType type, CancellationToken cancellationToken)
        {
            var moneyKontragents = await contractorDao
                .GetAsync(firmId, type, cancellationToken)
                .ConfigureAwait(false);
            var kontragentAutocompleteItems = string.IsNullOrEmpty(query)
                ? GetNonQueryItemsAsync(firmId, userId, type, moneyKontragents, cancellationToken)
                : GetQueryItemsAsync(firmId, userId, query, type, moneyKontragents, cancellationToken);

            var filteredItems = (await kontragentAutocompleteItems.ConfigureAwait(false)).Where(x => x.Rating > 0);
            var orderedItems = filteredItems.OrderByDescending(x => x.Rating);
            var resultItems = orderedItems.Take(count);
            return resultItems.ToList();
        }

        public async Task<Contractor> GetByIdAsync(int firmId, int userId, int id, MoneyContractorType type)
        {
            switch (type)
            {
                case MoneyContractorType.Kontragent:
                    return Map(await kontragentsApi.GetByIdAsync(firmId, userId, id).ConfigureAwait(false));
                case MoneyContractorType.Worker:
                    return Map(await employeesApi.GetWorkerAsync(firmId, userId, id).ConfigureAwait(false));
                default:
                    return Map(await kontragentsApi.GetByIdAsync(firmId, userId, id).ConfigureAwait(false)) ??
                           Map(await employeesApi.GetWorkerAsync(firmId, userId, id).ConfigureAwait(false));
            }
        }

        private async Task<List<Contractor>> GetQueryItemsAsync(int firmId, int userId, string query,
            MoneyContractorType type, IReadOnlyCollection<Contractor> contractors, CancellationToken cancellationToken)
        {
            var kontragentAutocompleteItems = new List<Contractor>();

            if (type != MoneyContractorType.Worker)
            {
                var kontragents = await kontragentsApi
                    .KontragentsAutocompleteAsync(firmId, userId, query, AutocompleteApiCount, cancellationToken)
                    .ConfigureAwait(false);
                kontragentAutocompleteItems.AddRange(kontragents.Select(Map));
            }

            if (type != MoneyContractorType.Kontragent)
            {
                var workers = await employeesApi
                    .GetAutocompleteAsync(firmId, userId, query, AutocompleteApiCount)
                    .ConfigureAwait(false);
                kontragentAutocompleteItems.AddRange(workers.Select(Map));
            }

            foreach (var item in kontragentAutocompleteItems)
            {
                item.Rating = contractors.FirstOrDefault(x => x.Id == item.Id)?.Rating ?? 0;
            }

            return kontragentAutocompleteItems;
        }

        private async Task<List<Contractor>> GetNonQueryItemsAsync(int firmId, int userId, MoneyContractorType type,
            IReadOnlyCollection<Contractor> contractors, CancellationToken cancellationToken)
        {
            var result = new List<Contractor>();

            if (type != MoneyContractorType.Worker)
            {
                var kontragentIds = contractors.Where(x => x.Type == MoneyContractorType.Kontragent)
                    .Select(x => x.Id).ToList();
                if (kontragentIds.Any())
                {
                    var kontragents = await kontragentsApi
                        .GetByIdsAsync(firmId, userId, kontragentIds, cancellationToken)
                        .ConfigureAwait(false);
                    var kontragentAutocompleteItems = kontragents.Select(Map);
                    result.AddRange(kontragentAutocompleteItems);
                }
            }

            if (type != MoneyContractorType.Kontragent)
            {
                var workerIds = contractors.Where(x => x.Type == MoneyContractorType.Worker)
                    .Select(x => x.Id).ToList();
                if (workerIds.Any())
                {
                    var workers = await employeesApi
                        .GetWorkersAsync(firmId, userId, workerIds, cancellationToken)
                        .ConfigureAwait(false);
                    var kontragentAutocompleteItems = workers.Select(Map);
                    result.AddRange(kontragentAutocompleteItems);
                }
            }

            foreach (var item in result)
            {
                item.Rating = contractors.FirstOrDefault(m => m.Id == item.Id)?.Rating ?? 0;
            }

            return result;
        }

        private static Contractor Map(KontragentDto kontragent)
        {
            if (kontragent == null)
            {
                return null;
            }
            return new Contractor
            {
                Id = kontragent.Id,
                Name = kontragent.GetName(),
                Type = MoneyContractorType.Kontragent
            };
        }

        private static Contractor Map(KontragentShortDto kontragent)
        {
            if (kontragent == null)
            {
                return null;
            }
            return new Contractor
            {
                Id = kontragent.Id,
                Name = kontragent.Name,
                Type = MoneyContractorType.Kontragent
            };
        }

        private static Contractor Map(WorkerDto worker)
        {
            if (worker == null)
            {
                return null;
            }
            return new Contractor
            {
                Id = worker.Id,
                Name = worker.FullName,
                Type = MoneyContractorType.Worker
            };
        }

        private static Contractor Map(WorkerAutocompleteDto worker)
        {
            if (worker == null)
            {
                return null;
            }
            return new Contractor
            {
                Id = worker.Id,
                Name = worker.Name,
                Type = MoneyContractorType.Worker
            };
        }
    }
}