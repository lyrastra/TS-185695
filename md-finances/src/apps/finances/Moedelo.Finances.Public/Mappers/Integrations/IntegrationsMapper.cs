using System.Linq;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.Finances.Public.ClientData.Integrations;

namespace Moedelo.Finances.Public.Mappers.Integrations
{
    public static class IntegrationsMapper
    {
        public static SendPaymentOrdersResponseClientData MapSendPaymentOrdersResponseToClient(SendPaymentOrdersResponse response)
        {
            return new SendPaymentOrdersResponseClientData
            {
                StatusCode = response.StatusCode,
                ErrorCode = response.ErrorCode,
                Message = response.Message,
                PhoneMask = response.PhoneMask,
                List = response.List
                    .Select(MapSendPaymentOrderResponseToClient)
                    .ToList()
            };
        }
        
        public static SendBankInvoiceRequest Map(this SendBankInvoiceRequestClientData clientData)
        {
            return new SendBankInvoiceRequest
            {
                OperationId = clientData.OperationId,
                SourceType = clientData.SourceType,
                BackUrl = clientData.BackUrl
            };
        }

        private static SendPaymentOrderResponseClientData MapSendPaymentOrderResponseToClient(SendPaymentOrderResponse response)
        {
            return new SendPaymentOrderResponseClientData
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                ExternalId = response.ExternalId,
                IsSuccess = string.IsNullOrEmpty(response.Error),
                Message = response.Error,
            };
        }
    }
}