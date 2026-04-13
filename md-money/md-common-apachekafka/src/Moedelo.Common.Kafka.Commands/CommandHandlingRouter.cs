using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Common.Kafka.Commands.Abstractions;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Kafka.Commands
{
    /// <summary>
    /// Маршрутизатор команд до их обработчиков.
    /// Обработчом команды типа TCommand выбирается класс, реализующий ICommandHandler<TCommand>.
    /// ОГРАНИЧЕНИЯ:
    /// * для инстанцирования экземпляров обработчиков используется IServiceProvider
    /// * никаких дополнительных манипуляций с экземпляром обработчиков не происводится,
    /// т.е.они должны быть либо синглетонами, либо не подразумевать зачистки каких-либо ресурсов, связанных с обработкой команд 
    /// </summary>
    [InjectAsSingleton(typeof(ICommandHandlingRouter))]
    internal sealed class CommandHandlingRouter : ICommandHandlingRouter
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger logger;

        public CommandHandlingRouter(
            IServiceProvider serviceProvider,
            ILogger<CommandHandlingRouter> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        public Task HandleCommand<TCommand>(TCommand command) where TCommand : IEntityCommandData
        {
            var handler = serviceProvider.GetService<ICommandHandler<TCommand>>();
            if (handler == null)
            {
                logger.LogErrorExtraData(command, $"Не удалось найти обработчик для команды {typeof(TCommand).Name}");

                throw new NotImplementedException($"Не удалось найти обработчик для команды {typeof(TCommand).Name}");
            }

            return handler.Handle(command, cancellationToken: default);
        }
        
        public Task<IEntityCommandReplyData> HandleCommandWithReply<TCommand>(TCommand command)
            where TCommand: IEntityCommandData
        {
            var handler = serviceProvider.GetService<ICommandHandlerWithReply<TCommand>>();
            if (handler == null)
            {
                logger.LogErrorExtraData(command, $"Не удалось найти обработчик для команды {typeof(TCommand).Name}");

                throw new NotImplementedException($"Не удалось найти обработчик для команды {typeof(TCommand).Name}");
            }

            return handler.Handle(command, cancellationToken: default);
        }
    }
}
