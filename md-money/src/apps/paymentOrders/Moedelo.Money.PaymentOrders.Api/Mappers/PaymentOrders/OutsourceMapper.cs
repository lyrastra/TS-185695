using Moedelo.Money.PaymentOrders.Domain.Models.Outsource;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outsource;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders
{
    static class OutsourceMapper
    {
        public static OutsourceApproveResponseDto[] Map(IReadOnlyCollection<IsApprovedResponse> items)
        {
            return items.Select(Map).ToArray();
        }

        public static OutsourceApproveResponseDto Map(IsApprovedResponse dto)
        {
            return new OutsourceApproveResponseDto
            {
                DocumentBaseId = dto.DocumentBaseId,
                IsApproved = dto.IsApproved,
            };
        }

        public static AllOperationsApprovedRequest Map(OutsourceAllOperationsApprovedRequestDto dto)
        {
            return new AllOperationsApprovedRequest
            {
                FirmIds = dto.FirmIds,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                InitialDate = dto.InitialDate,
                IsOnlyPaid = dto.IsOnlyPaid,
            };
        }
    }
}
