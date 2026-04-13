using System;
using Moedelo.Common.Enums.Enums.ExternalPartner;

namespace Moedelo.CommonV2.Auth.Wsse.Domain.Models
{
    public class ExternalPartnerCredentialDbResult
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Secret { get; set; }

        public string CredentialData { get; set; }

        public SecureHashAlgorithms Algorithm { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
