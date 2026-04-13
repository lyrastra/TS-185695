using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto.FmsRegistry;

namespace Moedelo.OutSystemsIntegrationV2.Client.FmsRegistry
{
    public interface IFmsRegistryClient : IDI
    {
        Task<CheckIsBlockedPassportResponseDto> CheckIsBlockedPassportAsync(CheckIsBlockedPassportRequestDto request);
    }
}