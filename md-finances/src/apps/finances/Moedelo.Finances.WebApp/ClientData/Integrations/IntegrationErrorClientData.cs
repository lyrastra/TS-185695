using System.Collections.Generic;

namespace Moedelo.Finances.WebApp.ClientData.Integrations
{
    public class IntegrationErrorClientData
    {
        public string Message { get; set; }

        public List<int> ErrorIds { get; set; }
    }
}