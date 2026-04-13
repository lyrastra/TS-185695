using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.Operations
{
    public interface IChangeTaxationSystemSender
    {
        Task SendCommandAsync(IReadOnlyCollection<long> documentBaseIds, TaxationSystemType taxationSystemType);

        Task DistributeCommandsAsync(IReadOnlyCollection<long> documentBaseIds, TaxationSystemType taxationSystemType, Guid guid);
    }
}
