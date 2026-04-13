using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.Outsource;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource
{
    public interface IOutsourceApproveService
    {
        Task<DateTime> GetInitialDateAsync();

        Task<bool> GetIsApprovedAsync(long documentBaseId);

        Task<OutsourceApproveResponse[]> GetIsApprovedAsync(IReadOnlyCollection<long> documentBaseIds);

        Task<IReadOnlyDictionary<int, bool>> GetIsAllOperationsApprovedAsync(AllOperationsApprovedRequest request, CancellationToken cancellationToken);

        Task SetIsApprovedAsync(long documentBaseId, bool isApproved);

        Task SetIsApprovedAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}