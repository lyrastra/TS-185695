using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Kontragents
{
    [InjectAsSingleton(typeof(IKontragentsReader))]
    internal sealed class KontragentsReader : IKontragentsReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IKontragentsApiClient kontragentApiClient;
        private readonly IKontragentKppsApiClient kontragentKppsApiClient;
        private readonly IBankApiClient bankApiClient;

        public KontragentsReader(
            IExecutionInfoContextAccessor contextAccessor,
            IKontragentsApiClient kontragentApiClient,
            IKontragentKppsApiClient kontragentKppsApiClient,
            IBankApiClient bankApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.kontragentApiClient = kontragentApiClient;
            this.kontragentKppsApiClient = kontragentKppsApiClient;
            this.bankApiClient = bankApiClient;
        }

        public async Task<Kontragent> GetByIdAsync(int kontragentId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var dtos = await kontragentApiClient.GetBasicInfoByIdsAsync(context.FirmId, context.UserId, new[] { kontragentId });
            var dto = dtos.FirstOrDefault();
            return dto != null
               ? Map(dto)
               : null;
        }

        public async Task<IReadOnlyCollection<Kontragent>> GetByIdsAsync(IReadOnlyCollection<int> kontragentIds)
        {
            if ((kontragentIds?.Count ?? 0) == 0)
            {
                return Array.Empty<Kontragent>();
            }
            var context = contextAccessor.ExecutionInfoContext;
            var dtos = await kontragentApiClient.GetBasicInfoByIdsAsync(context.FirmId, context.UserId, kontragentIds);
            return dtos.Select(Map).ToArray();
        }

        public async Task<IReadOnlyCollection<KontragentWithRequisites>> GetWithRequisitesByIdsAsync(DateTime date, IReadOnlyCollection<int> kontragentIds)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var kontragents = await kontragentApiClient.GetByIdsAsync(context.FirmId, context.UserId, kontragentIds);

            var bankIds = kontragents
                .Select(k => k.GetSettlementAccount())
                .Where(sa => sa != null && sa.BankId != null)
                .Select(sa => sa.BankId.Value)
                .ToArray();
            var banksTask = bankApiClient.GetByIdsAsync(bankIds);
            var kppsTask = kontragentKppsApiClient.GetByKontragentIdsAsync(
                context.FirmId,
                context.UserId,
                new KontragentKppsRequestDto { KontragentIds = kontragentIds, ActualOnDate = date }
            );

            await Task.WhenAll(banksTask, kppsTask);

            return kontragents
                .Select(k => MapWithRequisites(k, banksTask.Result, kppsTask.Result))
                .ToArray();
        }

        private static Kontragent Map(BasicKontragentInfoDto dto)
        {
            return new Kontragent
            {
                Id = dto.Id,
                Form = dto.Form,
                SubcontoId = dto.SubcontoId
            };
        }

        private static KontragentWithRequisites MapWithRequisites(
            KontragentDto dto,
            IReadOnlyCollection<BankDto> banks,
            IReadOnlyCollection<KontragentKppDto> kpps)
        {
            var settlementAccount = dto?.GetSettlementAccount();
            var bank = banks.FirstOrDefault(x => x.Id == settlementAccount?.BankId);
            var kpp = kpps.FirstOrDefault(x => x.KontragentId == dto.Id);

            return new KontragentWithRequisites
            {
                Id = dto.Id,
                Name = dto.ShortName,
                Inn = dto.Inn,
                Kpp = kpp?.Number,
                Form = (int)dto.Form,
                SettlementAccount = settlementAccount?.Number,
                BankBik = bank?.Bik,
                BankName = bank?.FullName,
                IsArchive = dto.IsArchived,
            };
        }
    }
}
