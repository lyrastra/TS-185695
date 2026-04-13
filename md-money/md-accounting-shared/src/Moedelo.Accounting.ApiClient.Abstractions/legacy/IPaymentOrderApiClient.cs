using Moedelo.Common.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.PaymentOrder;
using Moedelo.Accounting.Enums.PaymentOrder;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface IPaymentOrderApiClient
    {
        Task ProvideAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        Task DeleteAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        Task<decimal> FindNextNumberByYearAsync(FirmId firmId, UserId userId, DateTime dateTime,
            int? settlementAccountId);

        Task<byte[]> GetFileAsync(FirmId firmId, UserId userId, GetFileRequestDto request);

        Task<DocFileInfoDto> GetFileByBaseIdAsync(FirmId firmId, UserId userId, long id, FileFormat format);
    }
}