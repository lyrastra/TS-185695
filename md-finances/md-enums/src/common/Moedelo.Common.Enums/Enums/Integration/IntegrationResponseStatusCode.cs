using System;

namespace Moedelo.Common.Enums.Enums.Integration
{
    //[Obsolete ("Use Moedelo.BankIntegrations.Enums.IntegrationResponseStatusCode from md-bankIntegrations-shared")]
    public enum IntegrationResponseStatusCode
    {
        Ok = 0,
        NeedSms = 5,
        Error = 6,
        BadRequest = 8,
        TimeoutOccurred = 9,
        /// <summary> Недостаточно прав </summary>
        AccessDenied = 10
    }
}