using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.TaxRemains.Client.Dto;

namespace Moedelo.TaxRemains.Client.Client
{
    public interface IProfitDeclarationRemainsClient : IDI
    {
        Task<ProfitDeclarationRemainsDto> GetAsync(int firmId);
    }
}
