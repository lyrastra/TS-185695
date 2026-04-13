using System.Collections.Generic;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

public interface ILogEventExtender
{
    IEnumerable<KeyValuePair<string, object>> EnumerateLogExtraEventFields();
}
