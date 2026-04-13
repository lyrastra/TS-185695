using Moedelo.Common.Enums.Enums.Billing;
using Moedelo.Common.Enums.Enums.Email;
using Moedelo.Common.Enums.Enums.Products;

namespace Moedelo.CommonV2.EventBus.Registration
{
    public class SendRegistrationMailCommand
    {
        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public UnionEmailMarker Marker { get; set; }

        public Tariff Tariff { get; set; }
    }
}