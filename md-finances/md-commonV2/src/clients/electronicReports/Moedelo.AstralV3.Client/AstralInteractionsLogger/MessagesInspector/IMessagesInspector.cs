using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Moedelo.AstralV3.Client.AstralInteractionsLogger.MessagesInspector
{
    /// <summary>
    /// Интерфейс перехватчика WCF-сообщений
    /// </summary>
    public interface IMessagesInspector : IClientMessageInspector, IEndpointBehavior, IDI
    {
        /// <summary>
        /// Тело запроса
        /// </summary>
        string Request { get; }

        /// <summary>
        /// Тело ответа
        /// </summary>
        string Response { get; }

        /// <summary>
        /// Длительность запроса
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Пользователь, из под которого выполняется запрос
        /// </summary>
        int? UserId { get; }

        /// <summary>
        /// Фирма, из под которой выполняется запрос
        /// </summary>
        int? FirmId { get; }
    }
}
