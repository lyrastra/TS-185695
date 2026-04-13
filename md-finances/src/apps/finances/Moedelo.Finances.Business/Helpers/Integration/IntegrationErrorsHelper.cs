using System.Collections.Generic;
using System.Linq;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.Finances.Domain.Models.Integrations;
using IntegrationErrorType = Moedelo.BankIntegrations.Enums.IntegrationErrorType;

namespace Moedelo.Finances.Business.Helpers.Integration
{
    public static class IntegrationErrorsHelper
    {
        private static readonly HashSet<IntegrationErrorType> LinkNeedSet = new HashSet<IntegrationErrorType>()
        {
            IntegrationErrorType.SberbankOfferNotSigning,
            IntegrationErrorType.SberbankSettlementUnavailableError,
            IntegrationErrorType.SberbankSettlementNotExistsInClientInfo,
            IntegrationErrorType.AlfaConsentReSigning,
            IntegrationErrorType.PointConsentReSigning
        };
       
        public static List<IntegrationErrorResponse> GetIntegrationErrorResponses(this List<IntegrationErrorDto> dto)
        {
            var integrationErrors = dto
                .Where(ied => !string.IsNullOrEmpty(ied.Message))
                .Select(ied =>
                    new IntegrationErrorResponse
                    {
                        Message = ied.Message,
                        ErrorIds = new List<int> {ied.Id},
                        ErrorType = ied.ErrorType,
                        IntegrationPartnerId = ied.IntegrationPartnerId,
                        IntegrationNotificationErrorType = GetIntegrationNotificationErrorType(ied.ErrorType),
                        NeedLink = LinkNeedSet.Contains(ied.ErrorType)
                    }
                )
                .OrderByDescending(x => x.NeedLink)
                .ToList();

            return integrationErrors;
        }

        private static IntegrationNotificationErrorType GetIntegrationNotificationErrorType(IntegrationErrorType errorType)
        {
            return  IntegrationErrorType.AlfaConsentExpired == errorType ? IntegrationNotificationErrorType.Error : IntegrationNotificationErrorType.Warning;
        }
    }
}