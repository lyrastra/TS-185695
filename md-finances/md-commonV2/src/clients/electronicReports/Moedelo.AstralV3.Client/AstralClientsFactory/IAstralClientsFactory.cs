using Moedelo.AstralV3.Client.AstralAuthServiceReference;
using Moedelo.AstralV3.Client.AstralExternalServiceReference;
using Moedelo.AstralV3.Client.AstralFlcServiceReference;
using Moedelo.AstralV3.Client.AstralInteractionsLogger.MessagesInspector;
using Moedelo.AstralV3.Client.AstralRegServiceRef;
using Moedelo.AstralV3.Client.AstralTransferService;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.AstralV3.Client.BankArchiveService;

namespace Moedelo.AstralV3.Client.AstralClientsFactory
{
    /// <summary>
    /// Интерфейс фабрики, генерирующей клиенты для AstralClient
    /// </summary>
    public interface IAstralClientsFactory : IDI
    {
        /// <summary>
        /// Создаёт AutorizationServiceClient и навешивает на него переданный инспектор.
        /// </summary>
        AutorizationServiceClient CreateAuthorizationServiceClient(IMessagesInspector inspector);

        /// <summary>
        /// Создаёт WorkflowServiceClient и навешивает на него переданный инспектор.
        /// </summary>
        WorkflowServiceClient CreateWorkflowServiceClient(IMessagesInspector inspector);

        /// <summary>
        /// Создаёт ZipServiceClient
        /// </summary>
        ZipServiceClient CreateBankArchiveServiceClient(IMessagesInspector inspector);

        /// <summary>
        /// Создаёт FlkServiceClient и навешивает на него переданный инспектор.
        /// </summary>
        FlkServiceClient CreateFlkServiceClient(IMessagesInspector inspector);

        /// <summary>
        /// Создаёт RegServiceSoapClient и навешивает на него переданный инспектор.
        /// </summary>
        RegServiceSoapClient CreateRegServiceSoapClient(IMessagesInspector inspector);

        /// <summary>
        /// Создаёт ExternalServiceSoapClient и навешивает на него переданный инспектор.
        /// </summary>
        ExternalServiceSoapClient CreateExternalServiceSoapClient(IMessagesInspector inspector);
    }
}
