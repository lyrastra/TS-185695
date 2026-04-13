using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Commands.Abstractions;
using Moedelo.Common.Logging.ExtraLog.ExtraData;

namespace Moedelo.Common.Kafka.Commands
{
    /// <summary>
    /// Базовая реализация (базовый класс) обработчика команды
    /// </summary>
    /// <typeparam name="TCommand">Тип обрабатываемых команд</typeparam>
    public abstract class BaseCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : IEntityCommandData
    {
        protected readonly ILogger logger;

        protected BaseCommandHandler(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task Handle(TCommand command, CancellationToken cancellationToken)
        {
            logger?.LogDebugExtraData(command, "Начало обработки {commandName}", typeof(TCommand).Name);
            try
            {
                await OnCommand(command, cancellationToken);
                logger?.LogDebugExtraData(command, "Конец обработки {commandName}", typeof(TCommand).Name);
            }
            catch (Exception e)
            {
                logger?.LogErrorExtraData(new {command}, e,
                    $"При обработке {command.GetType().Name} возникло исключение");
                throw;
            }
        }

        /// <summary>
        /// В этом методе должна быть реализована логика обработки команды
        /// </summary>
        /// <param name="command">Команда </param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns></returns>
        protected abstract Task OnCommand(TCommand command, CancellationToken cancellationToken);
    }
}
