using System;
using System.Collections.Generic;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Common.ExecutionContext.Abstractions.Models;

public sealed class ExecutionContextClaims
{
    public int UserId { get; set; }

    public int FirmId { get; set; }

    public int RoleId { get; set; }

    public IReadOnlyCollection<string> Scopes { get; set; }

    public string Config { get; set; } = "default";

    public DateTime CreateDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public IReadOnlyCollection<AccessRule> UserRules { get; set; }
}