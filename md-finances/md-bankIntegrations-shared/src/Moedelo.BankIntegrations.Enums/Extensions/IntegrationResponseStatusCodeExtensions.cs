using System.Collections.Generic;

namespace Moedelo.BankIntegrations.Enums.Extensions
{
    public static class IntegrationResponseStatusCodeExtensions
    {
        public static bool IsGoodStatus(this IntegrationResponseStatusCode statusCode)
        {
            var goodStatusCodes = new List<IntegrationResponseStatusCode>
            {
                IntegrationResponseStatusCode.Ok,
                IntegrationResponseStatusCode.NeedSms
            };

            return goodStatusCodes.Contains(statusCode);
        }
    }
}
