using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Kbk;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.KbkNumbers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.Kbk
{
    public interface IKbkApiClient : IDI
    {
        Task<List<KbkNumberDto>> GetAllAsync();

        Task<List<KbkDto>> GetKbkNumberAndTypeByIdListAsync(IReadOnlyCollection<int> kbkIds);

        Task<string> GetActualKbkForSpecialDateAsync(string typeCode, DateTime endDate);

        Task<KbkType> GetKbkTypeByNumberAsync(string kbkNumber);

        Task<KbkNumberDto> GetKbkNumber(KbkNumberRequestDto request);

        Task<List<KbkNumberDto>> GetKbkNumbersAsync(KbkNumbersRequestDto request);

        Task<List<KbkDto>> GetKbkByNumbersAsync(IReadOnlyCollection<string> kbkNumbers);

        Task<List<KbkNumberDto>> GetKbkNumbersByTypesAsync(KbkNumberType kbkType, KbkPaymentType kbkPaymentType);
    }
}