using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Funds;
using Moedelo.Payroll.Enums.Funds;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IFundPaymentApiClient
    {
        Task<decimal> GetSfrPaymentAsync(int firmId, int userId, int year, int month);
        
        Task<bool> HasNonPayedFundPaymentsAsync(int firmId, int userId, FundsRegistryRequestDto requestDto);
        
        Task<decimal> GetSfrInjuredPaymentAsync(int firmId, int userId, int year, int month);

        Task<IReadOnlyCollection<(FundChargeType fundType, decimal sum)>> GetPaymentsAsync(int firmId, int userId,
            int year, int month);

        Task<SfrPaymentDataDto> GetSfrDataAsync(FirmId firmId, UserId userId, int year, int month);
    }
}