using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.PayrollV2.Dto.SalaryPayments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.PayrollV2.Client.Payments
{
    public interface IPaymentsApiClient : IDI
    {
        Task<bool> HasAnyCalendarPaymentEventsAsync(int firmId, int userId);

        [Obsolete("Use IsDeductionExistAsync")]
        Task<bool> IsCustomChargeExistAsync(int firmId, int userId, string kontragentName, decimal sum);
        
        Task<bool> IsDeductionExistAsync(int firmId, int userId, string kontragentName);

        Task SetSalaryChargingStartDate(int firmId, int userId, DateTime date);

        Task<decimal> GetTripPaymentSumAsync(int firmId, int userId, long tripId);

        Task<List<SickKudirDto>> GetKudirSicklistPaymentsAsync(int firmId, int userId, int year);
    }
}
