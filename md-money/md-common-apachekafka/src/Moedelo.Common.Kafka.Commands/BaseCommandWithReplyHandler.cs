using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Common.Kafka.Commands.Abstractions;
using Moedelo.Common.Logging.ExtraLog.ExtraData;

namespace Moedelo.Common.Kafka.Commands
{
    /// <summary>
    /// Базовая реализация (базовый класс) обработчика команды с ответом
    /// </summary>
    /// <typeparam name="TCommand">Тип обрабатываемых команд</typeparam>
    public abstract class BaseCommandWithReplyHandler<TCommand>
        : ICommandHandlerWithReply<TCommand>
        where TCommand : IEntityCommandData
    {
        protected readonly ILogger logger;
    
        protected BaseCommandWithReplyHandler(ILogger logger)
        {
            this.logger = logger;
        }
    
        public async Task<IEntityCommandReplyData> Handle(TCommand command, CancellationToken cancellationToken)
        {
            logger?.LogDebugExtraData(command, "Начало обработки {commandName}", typeof(TCommand).Name);
            try
            {
                var responseData = await OnCommand(command, cancellationToken);
                logger?.LogDebugExtraData(command, "Конец обработки {commandName}", typeof(TCommand).Name);

                return responseData;
            }
            catch (Exception exception)
            {
                logger?.LogErrorExtraData(
                    new {command},
                    exception,
                    "При обработке {commandName} возникло исключение", command.GetType().Name);

                return new EntityCommandErrorReplyData();
            }
        }
    
        /// <summary>
        /// В этом методе должна быть реализована логика обработки команды
        /// </summary>
        /// <param name="command">Команда </param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Ответ - результат обработки команды</returns>
        protected abstract Task<IEntityCommandReplyData> OnCommand(TCommand command, CancellationToken cancellationToken);
    }
}
