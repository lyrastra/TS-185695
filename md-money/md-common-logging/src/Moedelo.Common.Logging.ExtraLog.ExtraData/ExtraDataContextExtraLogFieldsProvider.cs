using System;
using System.Collections.Generic;
using Moedelo.Common.Logging.ExtraLog.Abstractions;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Logging.ExtraLog.ExtraData
{
    internal sealed class ExtraDataContextExtraLogFieldsProvider : IExtraLogFieldsProvider
    {
        public IEnumerable<ExtraLogField> Get()
        {
            var extraDataContext = ExtraDataContextAccessor.ExtraDataContext;

            if (extraDataContext == null)
            {
                yield break;
            }

            yield return new ExtraLogField("ExtraData", SerializeValue(extraDataContext));
        }

        private static string SerializeValue(object extraData)
        {
            try
            {
                return extraData.ToJsonString();
            }
            catch
            {
                return "Ошибка сериализации данных при попытке залогировать";
            }
        }
    }
}