using System.Collections.Generic;

namespace Moedelo.Common.ExecutionContext.Client;

internal interface IAuditHeadersGetter
{
    IReadOnlyCollection<KeyValuePair<string, string>> GetHeaders();
}
