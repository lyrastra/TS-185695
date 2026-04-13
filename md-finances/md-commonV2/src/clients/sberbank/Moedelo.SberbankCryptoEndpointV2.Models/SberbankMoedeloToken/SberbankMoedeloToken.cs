using System;
namespace Moedelo.BankIntegrations.Models.Sberbank.SberbankMoedeloToken
{
    public class SberbankMoedeloToken
    {
        public string AccessToken { get; set; }
        public DateTime? SessionLastDate { get; set; }
        public string RefreshToken { get; set; }
        public string ClientId { get; set; }
        public string OrganizationIdHash { get; set; }
    }
}
