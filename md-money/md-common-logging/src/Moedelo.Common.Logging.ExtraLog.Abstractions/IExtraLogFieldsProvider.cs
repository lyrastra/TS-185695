using System.Collections.Generic;

namespace Moedelo.Common.Logging.ExtraLog.Abstractions
{
    /// <summary>
    /// Реализуйте этот интерфейс, чтобы добавить в лог дополнительные общие поля
    /// </summary>
    public interface IExtraLogFieldsProvider
    {
        IEnumerable<ExtraLogField> Get();
    }
}