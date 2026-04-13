using Moedelo.Infrastructure.Consul.Abstraction;

namespace Moedelo.Common.Consul.Abstractions;

/// <summary>
/// Реализация методов <see cref="IConsulCatalogWatcher"/>, обёрнутая в auditTrail спаны
/// </summary>
public interface IMoedeloConsulCatalogWatcher : IConsulCatalogWatcher;
