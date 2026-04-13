using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models;
using System;
using Moedelo.Money.Domain.Outsource;

namespace Moedelo.Money.Api.Mappers.PaymentOrders;

public static class OutsourceMapper
{
    public static OutsourceApproveResponseDto[] Map(IReadOnlyCollection<OutsourceApproveResponse> models)
    {
        return models.Select(Map).ToArray();
    }

    private static OutsourceApproveResponseDto Map(OutsourceApproveResponse dto)
    {
        return new OutsourceApproveResponseDto
        {
            DocumentBaseId = dto.DocumentBaseId,
            IsApproved = dto.IsApproved,
        };
    }

    public static OutsourceConfirmResultDto ToDto(this OutsourceConfirmResult model)
    {
        return new OutsourceConfirmResultDto
        {
            DocumentBaseId = model.DocumentBaseId,
            Status = model.Status,
        };
    }

    public static OutsourceDeleteResultDto ToDto(this OutsourceDeleteResult model)
    {
        return new OutsourceDeleteResultDto
        {
            DocumentBaseId = model.DocumentBaseId,
            Status = model.Status
        };
    }

    public static AllOperationsApprovedRequest Map(OutsourceAllOperationsApprovedRequestDto dto)
    {
        return new AllOperationsApprovedRequest
        {
            FirmIds = dto.FirmIds,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsOnlyPaid = dto.IsOnlyPaid,
        };
    }
}