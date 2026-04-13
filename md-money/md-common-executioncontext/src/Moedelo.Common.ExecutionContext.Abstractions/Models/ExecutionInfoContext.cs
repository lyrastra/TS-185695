using System.Collections.Generic;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.Types;

namespace Moedelo.Common.ExecutionContext.Abstractions.Models;

public sealed class ExecutionInfoContext
{
    public FirmId FirmId { get; set; }

    public UserId UserId { get; set; }

    public RoleId RoleId { get; set; }

    public IReadOnlyCollection<string> Scopes { get; set; }

    public IReadOnlyCollection<AccessRule> UserRules { get; set; }
}