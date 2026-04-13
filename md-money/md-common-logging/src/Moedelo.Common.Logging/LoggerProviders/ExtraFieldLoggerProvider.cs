using System.Collections.Generic;
using Moedelo.Common.Logging.ExtraLog.Abstractions;

namespace Moedelo.Common.Logging.LoggerProviders
{
    internal sealed class ExtraFieldLoggerProvider : IExtraLogFieldsProvider
    {
        private readonly ExtraLogField field;

        public ExtraFieldLoggerProvider(string fieldName, string fieldValue)
        {
            this.field = new ExtraLogField(fieldName, fieldValue);
        }

        public ExtraFieldLoggerProvider(string fieldName, int fieldValue)
        {
            this.field = new ExtraLogField(fieldName, fieldValue);
        }

        public IEnumerable<ExtraLogField> Get()
        {
            yield return field;
        }
    }
}
