using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ExternalPartner;
using Moedelo.CommonV2.Auth.Wsse.Domain.Models;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.CommonV2.Auth.Wsse.Business.Mappers
{
    public static class ExternalPartnerMapper
    {
        public static ExternalPartnerCredential Map(ExternalPartnerCredentialDbResult model)
        {
            return model == null
                ? null
                : new ExternalPartnerCredential
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    Secret = model.Secret,
                    Rules = model.CredentialData.FromJsonString<List<ExternalPartnerRule>>(),
                    Algorithm = model.Algorithm,
                    ExpiryDate = model.ExpiryDate
                };
        }
    }
}
