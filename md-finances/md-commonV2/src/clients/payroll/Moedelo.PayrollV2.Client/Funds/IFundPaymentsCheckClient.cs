using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.Funds;

namespace Moedelo.PayrollV2.Client.Funds
{
    public interface IFundPaymentsCheckClient : IDI
    {
        Task<bool> HasNonPayedFundPaymentsAsync(int firmId, int userId, FundsRegistryRequestDto requestDto);
    }
}