using System.Linq;
using Moedelo.Finances.ApiClient.Abstractions.Legacy.Integration.Dto;
using Moedelo.Finances.Domain.Models.Integrations;

namespace Moedelo.Finances.Api.Mappers
{
    public static class IntegrationsPaymentOrderMapper
    {
        public static SendPaymentOrdersResponseDto Map(this SendPaymentOrdersResponse model)
        {
            return new SendPaymentOrdersResponseDto
            {
                StatusCode = (int) model.StatusCode,
                ErrorCode = (int?) model.ErrorCode,
                PhoneMask = model.PhoneMask,
                Message = model.Message,
                List = model.List
                    .Select(Map)
                    .ToArray()
            };
        }

        private static SendPaymentOrderResponseDto Map(SendPaymentOrderResponse model)
        {
            return new SendPaymentOrderResponseDto
            {
                DocumentBaseId = model.DocumentBaseId,
                IsSuccess = string.IsNullOrEmpty(model.Error),
                ExternalId = model.ExternalId,
                Message = model.Error,
                Date = model.Date,
                Number = model.Number,
                Sum = model.Sum,
            };
        }
    }
}