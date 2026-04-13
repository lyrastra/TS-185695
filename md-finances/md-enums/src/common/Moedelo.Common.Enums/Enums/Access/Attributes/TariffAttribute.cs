using System;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.Common.Enums.Enums.Access.Attributes
{
    public class TariffAttribute : Attribute
    {
        private readonly Tariff[] tariffs;

        public TariffAttribute(params Tariff[] tariffs)
        {
            this.tariffs = tariffs;
        }

        public Tariff[] Tariffs
        {
            get { return tariffs; }
        }
    }
}