using System.Collections.Generic;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Logging.ExtraLog.Abstractions;

namespace Moedelo.Common.Logging.ExtraLog.ExecutionContext
{
    internal sealed class ExecutionInfoContextExtraLogFieldsProvider : IExtraLogFieldsProvider
    {
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;

        public ExecutionInfoContextExtraLogFieldsProvider(IExecutionInfoContextAccessor executionInfoContextAccessor)
        {
            this.executionInfoContextAccessor = executionInfoContextAccessor;
        }

        public IEnumerable<ExtraLogField> Get()
        {
            var executionInfoContext = executionInfoContextAccessor?.ExecutionInfoContext;
            
            if (executionInfoContext == null)
            {
                yield break;
            }

            yield return new ExtraLogField("FirmId", (int) executionInfoContext.FirmId);
            yield return new ExtraLogField("UserId", (int) executionInfoContext.UserId);
        }
    }
}