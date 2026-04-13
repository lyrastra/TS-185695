using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;

namespace Moedelo.Common.Kafka.Commands.Abstractions
{
    /// <summary>
    /// Интерфейс обработчика команды
    /// </summary>
    /// <typeparam name="TCommand">Тип команды</typeparam>
    public interface ICommandHandler<in TCommand> where TCommand : IEntityCommandData
    {
        /// <summary>
        /// Выполняет команду
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Интерфейс обработчика команды с ответом
    /// </summary>
    /// <typeparam name="TCommand">Тип команды</typeparam>
    public interface ICommandHandlerWithReply<in TCommand> where TCommand : IEntityCommandData
    {
        /// <summary>
        /// Выполняет команду
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        Task<IEntityCommandReplyData> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
