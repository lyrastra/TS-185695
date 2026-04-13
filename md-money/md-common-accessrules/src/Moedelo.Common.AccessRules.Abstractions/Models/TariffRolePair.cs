using System;
using System.Collections.Generic;

namespace Moedelo.Common.AccessRules.Abstractions.Models
{
    public class TariffRolePair
    {
        public int TariffId { get; set; }

        public List<int> AdditionalTariffIds { get; set; }

        public int RoleId { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
