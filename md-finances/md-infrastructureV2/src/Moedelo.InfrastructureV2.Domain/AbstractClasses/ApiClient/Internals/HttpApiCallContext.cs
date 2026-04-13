using System.Collections.Generic;
using System.Net.Http;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Internals;

internal readonly record struct HttpApiCallContext(HttpMethod HttpMethod,
    string Uri,
    IReadOnlyCollection<KeyValuePair<string, string>> Headers,
    HttpQuerySetting QuerySettings,
    IAuditScope AuditTrailScope);