using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Domain.Outsource;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outsource;

namespace Moedelo.Money.Business.PaymentOrders.Outsource
{
    [InjectAsSingleton(typeof(IOutsourceApproveService))]
    internal sealed class OutsourceApproveService : IOutsourceApproveService
    {
        private static readonly DateTime initialDate = new(2023, 06, 01);

        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly OutsourceApproveApiClient apiClient;
        private readonly IProfOutsourceClient profOutsourceClient;

        public OutsourceApproveService(
            IExecutionInfoContextAccessor contextAccessor,
            OutsourceApproveApiClient apiClient,
            IProfOutsourceClient profOutsourceClient)
        {
            this.contextAccessor = contextAccessor;
            this.apiClient = apiClient;
            this.profOutsourceClient = profOutsourceClient;
        }

        public async Task<DateTime> GetInitialDateAsync()
        {
            await CheckIsOutsourseAsync();

            return initialDate;
        }

        public async Task<bool> GetIsApprovedAsync(long documentBaseId)
        {
            await CheckIsOutsourseAsync();

            return await apiClient.GetIsApprovedAsync(documentBaseId, initialDate);
        }

        public async Task<OutsourceApproveResponse[]> GetIsApprovedAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            await CheckIsOutsourseAsync();

            var response = await apiClient.GetIsApprovedAsync(documentBaseIds, initialDate);

            return response.Select(Map).ToArray();
        }

        public Task<IReadOnlyDictionary<int, bool>> GetIsAllOperationsApprovedAsync(
            AllOperationsApprovedRequest request, CancellationToken ct)
        {
            return apiClient.GetIsAllOperationsApprovedAsync(new OutsourceAllOperationsApprovedRequestDto
            {
                FirmIds = request.FirmIds,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsOnlyPaid = request.IsOnlyPaid,
                InitialDate = initialDate
            }, ct);
        }

        public async Task SetIsApprovedAsync(long documentBaseId, bool isApproved)
        {
            await CheckIsOutsourseAsync();

            await apiClient.SetIsApprovedAsync(documentBaseId, isApproved, initialDate);
        }

        public async Task SetIsApprovedAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            await CheckIsOutsourseAsync();

            await apiClient.SetIsApprovedAsync(documentBaseIds, initialDate);
        }

        private async Task CheckIsOutsourseAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var outsourceContext = await profOutsourceClient.GetOutsourceContext((int)context.FirmId, (int)context.UserId);

            if (outsourceContext.IsUserOutsourcer == false)
            {
                throw new NotOutsourceException();
            }
        }

        private static OutsourceApproveResponse Map(OutsourceApproveResponseDto dto)
        {
            return new OutsourceApproveResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                IsApproved = dto.IsApproved,
            };
        }
    }
}
