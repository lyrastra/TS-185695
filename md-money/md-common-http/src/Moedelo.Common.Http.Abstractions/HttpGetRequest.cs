#nullable enable
using System.Collections.Generic;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Common.Http.Abstractions;

public readonly record struct HttpGetRequest(
    string AuditTrailSpanName,
    string Uri,
    string? QueryParams = null,
    IReadOnlyCollection<KeyValuePair<string, string>>? Headers = null,
    HttpQuerySetting? Settings = null);
