using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;

namespace Moedelo.Common.Kafka.Commands.Abstractions
{
    /// <summary>
    /// Маршрутизатор команд до их обработчиков
    /// </summary>
    public interface ICommandHandlingRouter
    {
        Task HandleCommand<TCommand>(TCommand command) where TCommand : IEntityCommandData;
        Task<IEntityCommandReplyData> HandleCommandWithReply<TCommand>(TCommand command) where TCommand: IEntityCommandData;
    }
}
