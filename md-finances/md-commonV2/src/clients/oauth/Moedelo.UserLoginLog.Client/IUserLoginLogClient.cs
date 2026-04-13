using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Auth;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.UserLoginLog.Dto;

namespace Moedelo.UserLoginLog.Client
{
    public interface IUserLoginLogClient : IDI
    {
        /// <summary>
        /// Get last login date by user id's
        /// </summary>
        /// <param name="requestDto">Include list of users ids and service type (from enum ServiceType)</param>
        /// <returns>Dates of last login or null if user has not logged in</returns>
        Task<List<UserLastLoginDateDto>> GetLastLoginDateByUserIdsAsync(LastLoginDatesRequestDto requestDto);

        Task<IReadOnlyCollection<UserLoginSummaryResponseDto>> GetLoginSummaryByPlatformsAndPeriod(UserLoginSummaryRequestDto request);
    }
}