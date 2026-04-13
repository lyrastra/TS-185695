using System;
using System.Security.Cryptography;
using System.Text;
using Moedelo.Common.Enums.Enums.ExternalPartner;

namespace Moedelo.CommonV2.Auth.Wsse.WebApi
{
    public sealed class WsseAuthenticationSha256Attribute: WsseAuthenticationAttribute
    {
        public WsseAuthenticationSha256Attribute(params ExternalPartnerRule[] rules) : base(rules)
        {
        }

        protected override string ComputeHash(string input)
        {
            using (SHA256Managed shHash = new SHA256Managed())
            {
                return Convert.ToBase64String(shHash.ComputeHash(Encoding.UTF8.GetBytes(input)));
            }
        }
    }
}
