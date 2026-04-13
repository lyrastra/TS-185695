using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions
{
    public interface IOutsourceApproveDao
    {
        Task<bool> GetIsApprovedAsync(int firmId, long documentBaseId, DateTime initialDate);
        Task<IReadOnlyList<IsApprovedResponse>> GetIsApprovedAsync(
            int firmId,
            IReadOnlyCollection<long> documentBaseId,
            DateTime initialDates);

        Task<IReadOnlyDictionary<int, bool>> GetIsAllOperationsApprovedAsync(
            AllOperationsApprovedRequest request, CancellationToken cancellationToken);

        Task TrySetIsApprovedAsync(
            IReadOnlyCollection<long> documentBaseIds);

        Task TryUnsetIsApprovedAsync(int firmId, long documentBaseId, DateTime initialDate);
    }
}