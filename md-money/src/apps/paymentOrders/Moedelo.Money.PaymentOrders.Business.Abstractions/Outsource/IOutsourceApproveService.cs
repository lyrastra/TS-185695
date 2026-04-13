using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outsource
{
    public interface IOutsourceApproveService
    {
        Task<bool> GetIsApprovedAsync(long documentBaseId, DateTime initialDate);
        Task<IReadOnlyList<IsApprovedResponse>> GetIsApprovedAsync(
            IReadOnlyCollection<long> documentBaseIds, DateTime initialDate);
        Task<IReadOnlyDictionary<int, bool>> GetIsAllOperationsApprovedAsync(
            AllOperationsApprovedRequest request, CancellationToken cancellationToken);
        Task SetIsApprovedAsync(long documentBaseId, bool isApproved, DateTime initialDate);
        Task SetIsApprovedAsync(IReadOnlyCollection<long> documentBaseIds);
    }
}