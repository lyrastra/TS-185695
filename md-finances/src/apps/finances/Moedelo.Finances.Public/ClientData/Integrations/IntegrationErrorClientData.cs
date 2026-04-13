using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Integrations
{
    public class IntegrationErrorClientData
    {
        public string Message { get; set; }

        public List<int> ErrorIds { get; set; }
        
        public int IntegrationPartnerId { get; set; }

        public int ErrorType { get; set; }
        
        public string IntegrationNotificationErrorType { get; set; }
        
        public bool NeedLink { get; set; }
    }
}