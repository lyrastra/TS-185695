using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.Finances.Public.ClientData.Integrations;

namespace Moedelo.Finances.Public.Mappers.Integrations;

public static class IntegrationErrorMapper
{
    public static IntegrationErrorClientData Map(this IntegrationErrorResponse error)
    {
        return new IntegrationErrorClientData
        {
            ErrorIds = error.ErrorIds,
            Message = error.Message,
            IntegrationPartnerId = (int)error.IntegrationPartnerId,
            ErrorType = (int)error.ErrorType,
            IntegrationNotificationErrorType = error.IntegrationNotificationErrorType.ToString().ToLower(),
            NeedLink = error.NeedLink
        };
    }
}