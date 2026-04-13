using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Finances.Client.Money.Dtos;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.Finances.Dto.Money;

namespace Moedelo.Finances.Api.Mappers.Money
{
    public static class MoneyReconciliationMapper
    {
        public static ReconciliationForBackofficeRequest MapToDomain(ReconciliationForBackofficeRequestDto dto)
        {
            return new ReconciliationForBackofficeRequest
            {
                Email = dto.Email,
                Login = dto.Login,
                FileId = dto.FileId,
                SettlementNumber = dto.SettlementNumber,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status
            };
        }

        public static ReconciliationForUserRequest MapToDomain(ReconciliationForUserRequestDto dto)
        {
            return new ReconciliationForUserRequest
            {
                FileId = dto.FileId,
                SettlementNumber = dto.SettlementNumber,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                SessionId = dto.SessionId,
                IsManual = dto.IsManual
            };
        }

        public static ReconciliationResponseDto MapToDto(this ReconciliationResult domainModel)
        {
            return new ReconciliationResponseDto
            {
                SettlementAccountId = domainModel.SettlementAccountId,
                SessionId = domainModel.SessionId,
                CreateDate = domainModel.CreateDate,
                ReconciliationStatus = domainModel.Status
            };
        }

        private static ReconciliationResponseDto MapToDto(BalanceReconcilation domainModel)
        {
            return new ReconciliationResponseDto
            {
                CreateDate = domainModel.CreateDate,
                ReconciliationStatus = domainModel.Status,
                SettlementAccountId = domainModel.SettlementAccountId,
                SessionId = domainModel.SessionId
            };
        }

        public static ReconciliationResponseDto[] MapToDto(IReadOnlyCollection<BalanceReconcilation> domainModels)
        {
            if (domainModels == null || !domainModels.Any())
            {
                return Array.Empty<ReconciliationResponseDto>();
            }

            return domainModels.Select(MapToDto).ToArray();
        }
    }
}
