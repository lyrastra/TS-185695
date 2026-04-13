using System.Collections.Generic;

namespace Moedelo.Common.Http.Abstractions.Headers;

public interface IDefaultHeadersGetter
{
    IEnumerable<KeyValuePair<string, string>> EnumerateHeaders();
}