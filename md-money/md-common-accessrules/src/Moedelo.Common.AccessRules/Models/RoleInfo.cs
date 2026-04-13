using Moedelo.Common.AccessRules.Abstractions;
using System.Collections.Generic;

namespace Moedelo.Common.AccessRules.Models
{
    internal sealed class RoleInfo
    {
        public int Id { get; set; }

        public string RoleCode { get; set; }

        public string Name { get; set; }

        public HashSet<AccessRule> AccessRules { get; set; }
    }
}
