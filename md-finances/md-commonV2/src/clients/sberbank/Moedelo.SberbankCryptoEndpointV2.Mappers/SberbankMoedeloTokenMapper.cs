using Moedelo.BankIntegrations.Models.Sberbank.SberbankMoedeloToken;
using Moedelo.SberbankCryptoEndpointV2.Dto.SberbankMoedeloToken;

namespace Moedelo.BankIntegrations.Mappers.Sberbank
{
    public static class SberbankMoedeloTokenMapper
    {
        public static SberbankMoedeloToken Map(this SberbankMoedeloTokenDto dto)
        {
            return new SberbankMoedeloToken
            {
                AccessToken = dto.AccessToken,
                RefreshToken = dto.RefreshToken,
                SessionLastDate = dto.SessionLastDate,
                ClientId = dto.ClientId,
                OrganizationIdHash = dto.OrganizationIdHash
            };
        }

        public static SberbankMoedeloTokenDto Map(this SberbankMoedeloToken model)
        {
            return new SberbankMoedeloTokenDto
            {
                AccessToken = model.AccessToken,
                RefreshToken = model.RefreshToken,
                SessionLastDate = model.SessionLastDate,
                ClientId = model.ClientId,
                OrganizationIdHash = model.OrganizationIdHash
            };
        }

        public static SberbankMoedeloTokenSaveResponseDto Map(this SberbankMoedeloTokenSaveResult result)
        {
            return new SberbankMoedeloTokenSaveResponseDto
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message
            };
        }

        public static SberbankMoedeloTokenSaveResult Map(this SberbankMoedeloTokenSaveResponseDto result)
        {
            return new SberbankMoedeloTokenSaveResult
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message
            };
        }
    }
}