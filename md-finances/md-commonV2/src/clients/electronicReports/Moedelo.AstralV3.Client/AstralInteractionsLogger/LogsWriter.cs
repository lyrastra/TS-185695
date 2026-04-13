using Moedelo.AstralV3.Client.AstralInteractionsLogger.Enums;
using Moedelo.AstralV3.Client.AstralInteractionsLogger.Interfaces;
using Moedelo.AstralV3.Client.DAO.DbObjects;
using Moedelo.AstralV3.Client.DAO.Interfaces;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.AstralV3.Client.AstralInteractionsLogger
{
    /// <summary>
    /// Класс для накопления и сброса в базу залогинованных сообщений
    /// </summary>
    [InjectAsSingleton(typeof(ILogsWriter))]
    public class LogsWriter : ILogsWriter
    {
        /// <summary>
        /// Интервал между сбросами лога в базу в секундах
        /// </summary>
        private const int LogPushInterval = 120;

        /// <summary>
        /// Список ещё не запушенных в базу сообщений
        /// </summary>
        private List<EventToLog> _events = new List<EventToLog>();

        /// <summary>
        /// Объект для блокировки
        /// </summary>
        private object _lockObject = new object();

        /// <summary>
        /// Токен для остановки сброса логов в базу
        /// </summary>
        private CancellationTokenSource _cts = new CancellationTokenSource();

        private readonly IInteractionMethodsSettingsRepository _cachingMethodsSettingsGetter;
        private readonly IAstralInteractionLogsDao _interactionLogsDao;
        private readonly ILogger _kibanaLogger;
        private readonly string _kibanaLoggerTag;

        public LogsWriter(IInteractionMethodsSettingsRepository cachingMethodsSettingsGetter, IAstralInteractionLogsDao interactionLogsDao, ILogger kibanaLogger)
        {
            _cachingMethodsSettingsGetter = cachingMethodsSettingsGetter;
            _interactionLogsDao = interactionLogsDao;
            _kibanaLogger = kibanaLogger;
            _kibanaLoggerTag = GetType().ToString();

            // Запускаем пушер в базу
            Task.Run(LogToDbTask, _cts.Token);
        }

        public void Log(EventToLog loggedEvent)
        {
            lock(_lockObject)
            {
                // To avoid spamming database with successfull events we decided to log only errors
                // See https://youtrack.moedelo.org/youtrack/issue/eRP-1274
                if ((loggedEvent.EventType == EventType.Info) || (loggedEvent.EventType == EventType.Warning))
                {
                    return;
                }

                _events.Add(loggedEvent);
            };
        }

        /// <summary>
        /// Принудительно сбрасываем логи при завершении
        /// </summary>
        public void Dispose()
        {
            _cts.Cancel();
            FlushEventsToDbWithLogging().Wait();
        }

        /// <summary>
        /// Таска, которая крутится и записывает логи в базу
        /// </summary>
        private async Task LogToDbTask()
        {
            while (_cts.IsCancellationRequested == false)
            {
                await FlushEventsToDbWithLogging().ConfigureAwait(false);
                await Task.Delay(TimeSpan.FromSeconds(LogPushInterval)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Пушит изменения в базу, и логирует эксепшны, возникающие в процессе.
        /// </summary>
        private async Task FlushEventsToDbWithLogging()
        {
            try
            {
                // Пушаем изменения в базу
                await FlushEventsToDb().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Глотаем исключения, чтобы не обрывать цикл
                _kibanaLogger.Error(_kibanaLoggerTag, "Failed to push logs to database", ex);
            }
        }

        private List<EventToLog> GetEvents()
        {
            lock(_lockObject)
            {
                var events = new List<EventToLog>(_events);
                _events.Clear();
                return events;
            };
        }

        /// <summary>
        /// В зависимости от режима логирования заnull'ывает запрос, ответ, оба параметра или ничего.
        /// </summary>
        private Tuple<string, string> FilterDataToLog(MethodLoggingMode mode, string requestMessage, string responseMessage)
        {
            switch (mode)
            {
                case MethodLoggingMode.LogAll: // Ничего не затираем
                    return new Tuple<string, string>(requestMessage, responseMessage);

                case MethodLoggingMode.LogNothing: // Затираем всё
                    return new Tuple<string, string>(null, null);

                case MethodLoggingMode.LogRequest: // Логируем запрос
                    return new Tuple<string, string>(requestMessage, null);

                case MethodLoggingMode.LogResponse: // Логируем ответ
                    return new Tuple<string, string>(null, responseMessage);

                default:
                    throw new ArgumentException("Недопустимый режим");
            }
        }

        /// <summary>
        /// Сбрасывает сообщения из накопителя в базу.
        /// </summary>
        private async Task FlushEventsToDb()
        {
            var loggedEvents = GetEvents();

            // Нет событий - и нечего кеш дёргать (кеш дёргается при получении настроек)
            if (!loggedEvents.Any())
            {
                return;
            }

            // Группируем по имени метода, чтобы не получать одну и ту же настройку несколько раз
            var methodNames = loggedEvents
                .Select(le => le.MethodId)
                .Distinct()
                .ToList();

            // Получаем настройки
            var loggingSettings = await _cachingMethodsSettingsGetter.GetSettings(methodNames).ConfigureAwait(false);

            // Фильтруем данные событий
            foreach (var methodName in methodNames)
            {
                var loggingSettingsWithThisMethod = loggingSettings[methodName];

                var eventsWithThisMethod = loggedEvents
                    .Where(le => le.MethodId == methodName);

                foreach (var eventWithThisMethod in eventsWithThisMethod)
                {
                    var result = FilterDataToLog(loggingSettingsWithThisMethod.Mode, eventWithThisMethod.Request, eventWithThisMethod.Response);

                    eventWithThisMethod.Request = result.Item1;
                    eventWithThisMethod.Response = result.Item2;
                }
            }

            // Запихиваем всё в базу
            foreach (var methodName in methodNames)
            {
                var methodId = loggingSettings[methodName].Id;

                var eventsWithThisMethod = loggedEvents
                    .Where(le => le.MethodId == methodName);

                foreach (var eventWithThisMethod in eventsWithThisMethod)
                {
                    // Готовим объект события
                    var eventToPush = new AstralInteractionLogRecord()
                    {
                        MethodId = methodId,
                        FirmId = eventWithThisMethod.FirmId,
                        UserId = eventWithThisMethod.UserId,
                        EventDateTime = eventWithThisMethod.Timestamp,
                        EventType = eventWithThisMethod.EventType,
                        ExceptionMessage = eventWithThisMethod.ExceptionMessage,
                        Request = eventWithThisMethod.Request,
                        Response = eventWithThisMethod.Response,
                        Duration = eventWithThisMethod.Duration.Milliseconds
                    };

                    await _interactionLogsDao.Add(eventToPush).ConfigureAwait(false);
                }
            }
        }
    }
}
