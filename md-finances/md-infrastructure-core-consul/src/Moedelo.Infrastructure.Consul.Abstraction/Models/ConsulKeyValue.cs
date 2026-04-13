namespace Moedelo.Infrastructure.Consul.Abstraction.Models;

public readonly record struct ConsulKeyValue<TValue>(string Key, TValue Value);
