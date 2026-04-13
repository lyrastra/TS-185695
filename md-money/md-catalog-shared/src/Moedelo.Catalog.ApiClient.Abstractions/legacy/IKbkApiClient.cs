using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Catalog.ApiClient.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    public interface IKbkApiClient
    {
        Task<KbkDto[]> GetAsync();

        Task<KbkDto> GetAsync(int id);

        Task<KbkDto[]> GetByIdsAsync(IReadOnlyCollection<int> ids);

        Task<KbkDto> GetAsync(string kbkNumber, DateTime date);

        Task<KbkDto> GetByTypeAsync(KbkType kbkType, KbkPaymentType kbkPaymentType = KbkPaymentType.Payment);
    }
}