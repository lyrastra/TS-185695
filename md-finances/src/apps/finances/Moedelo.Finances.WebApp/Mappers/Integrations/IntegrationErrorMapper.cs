using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.Finances.WebApp.ClientData.Integrations;

namespace Moedelo.Finances.WebApp.Mappers.Integrations
{
    public static class IntegrationErrorMapper
    {
        public static IntegrationErrorClientData Map(this IntegrationErrorResponse error)
        {
            return new IntegrationErrorClientData
            {
                ErrorIds = error.ErrorIds,
                Message = error.Message
            };
        }
    }
}