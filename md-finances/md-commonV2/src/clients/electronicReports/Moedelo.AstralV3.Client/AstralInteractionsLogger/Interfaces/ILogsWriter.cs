using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;

namespace Moedelo.AstralV3.Client.AstralInteractionsLogger.Interfaces
{
    /// <summary>
    /// Интерфейс для класса, который накапливает сообщения и периодически сбрасывает их в базу
    /// </summary>
    public interface ILogsWriter : IDI, IDisposable
    {
        /// <summary>
        /// Добавляет событие в внутренний буфер, при очередном сбросе в базу событие попадёт в неё
        /// </summary>
        void Log(EventToLog loggedEvent);
    }
}
