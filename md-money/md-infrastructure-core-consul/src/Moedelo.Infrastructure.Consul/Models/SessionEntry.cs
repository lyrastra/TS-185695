using System;

namespace Moedelo.Infrastructure.Consul.Models;

internal struct SessionEntry
{
    public string Name { get; set; }
    public string TTL { get; set; }
    public string[] Checks { get; set; }
} 