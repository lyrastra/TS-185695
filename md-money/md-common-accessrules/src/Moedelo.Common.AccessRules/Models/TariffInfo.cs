using Moedelo.Common.AccessRules.Abstractions;
using System.Collections.Generic;

namespace Moedelo.Common.AccessRules.Models
{
    internal sealed class TariffInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductPlatform { get; set; }

        public string ProductGroup { get; set; }

        public HashSet<AccessRule> AccessRules { get; set; }
    }
}
