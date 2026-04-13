using Moedelo.AstralV3.Client.AstralAuthServiceReference;
using Moedelo.AstralV3.Client.AstralExternalServiceReference;
using Moedelo.AstralV3.Client.AstralFlcServiceReference;
using Moedelo.AstralV3.Client.AstralInteractionsLogger.MessagesInspector;
using Moedelo.AstralV3.Client.AstralRegServiceRef;
using Moedelo.AstralV3.Client.AstralTransferService;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Moedelo.AstralV3.Client.BankArchiveService;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.AstralV3.Client.AstralClientsFactory
{
    /// <summary>
    /// Фабрика, генерирующая клиенты для AstralClient
    /// </summary>
    [InjectAsSingleton]
    public class AstralClientsFactory : IAstralClientsFactory
    {
        private readonly ISettingRepository settingRepository;

        public AstralClientsFactory(ISettingRepository settingRepository)
        {
            this.settingRepository = settingRepository;
        }

        /// <summary>
        /// Адрес конечной точки астрального AuthorizationSerivce
        /// </summary>
        private const string AuthorizationServiceUrl = "http://iotchet.ru/api/autorization";

        /// <summary>
        /// Адрес конечной точки астрального WorkflowService
        /// </summary>
        private const string WorkflowServiceUrl = "http://iotchet.ru/api/workflow";

        /// <summary>
        /// Адрес сервиса получения архива для банков
        /// </summary>
        private const string BankArchiveServiceUrl = "http://iotchet.ru/api/zip";

        /// <summary>
        /// Адрес конечной точки астрального FlkService
        /// </summary>
        private const string FlkServiceUrl = "http://iotchet.ru/api/flk/";

        /// <summary>
        /// Адрес конечной точки астрального RegServiceSoap
        /// </summary>
        private const string RegServiceSoapUrl = "http://regservice.keydisk.ru/regservice.asmx"; 

        /// <summary>
        /// Адрес конечной точки астрального ExternalServiceSoap
        /// </summary>
        private const string ExternalServiceSoapUrl = "https://reg.astralnalog.ru/Services/ExternalService.asmx";

        /// <summary>
        /// Создаёт AutorizationServiceClient.
        /// </summary>
        public AutorizationServiceClient CreateAuthorizationServiceClient(IMessagesInspector inspector)
        {
            var binding = new BasicHttpBinding
            {
                Name = "AutorizationService",
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
            };

            var endpoint = new EndpointAddress(AuthorizationServiceUrl);
            var client = new AutorizationServiceClient(binding, endpoint);
            AddMessageInspectorToEndpoint(client.Endpoint, inspector);

            return client;
        }

        /// <summary>
        /// Создаёт WorkflowServiceClient.
        /// </summary>
        public WorkflowServiceClient CreateWorkflowServiceClient(IMessagesInspector inspector)
        {
            var binding = new BasicHttpBinding
            {
                Name = "WorkflowService",
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
            };

            var endpoint = new EndpointAddress(WorkflowServiceUrl);
            var client = new WorkflowServiceClient(binding, endpoint);
            AddMessageInspectorToEndpoint(client.Endpoint, inspector);

            return client;
        }
        
        /// <summary>
        /// Создаёт BankArchiveServiceClient.
        /// </summary>
        public ZipServiceClient CreateBankArchiveServiceClient(IMessagesInspector inspector)
        {
            var binding = new BasicHttpBinding
            {
                Name = "BankArchiveService",
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
            };

            var endpoint = new EndpointAddress(BankArchiveServiceUrl);
            var client = new ZipServiceClient(binding, endpoint);
            AddMessageInspectorToEndpoint(client.Endpoint, inspector);

            return client;
        }

        /// <summary>
        /// Создаёт FlkServiceClient.
        /// </summary>
        public FlkServiceClient CreateFlkServiceClient(IMessagesInspector inspector)
        {
            var binding = new BasicHttpBinding
            {
                Name = "FlkService",
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
            };

            var endpoint = new EndpointAddress(FlkServiceUrl);
            var client = new FlkServiceClient(binding, endpoint);
            AddMessageInspectorToEndpoint(client.Endpoint, inspector);

            return client;
        }

        /// <summary>
        /// Создаёт RegServiceSoapClient.
        /// </summary>
        public RegServiceSoapClient CreateRegServiceSoapClient(IMessagesInspector inspector)
        {
            var binding = new BasicHttpBinding
            {
                Name = "RegServiceSoap",
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
                SendTimeout = TimeSpan.FromMinutes(5)
            };

            var endpoint = new EndpointAddress(RegServiceSoapUrl);
            var client = new RegServiceSoapClient(binding, endpoint);
            AddMessageInspectorToEndpoint(client.Endpoint, inspector);

            return client;
        }

        /// <summary>
        /// Создаёт ExternalServiceSoapClient.
        /// </summary>
        public ExternalServiceSoapClient CreateExternalServiceSoapClient(IMessagesInspector inspector)
        {
            // Создаём биндинг, при создании его нельзя настроить на работу с HTTPS
            var binding = new BasicHttpBinding
            {
                Name = "ExternalServiceSoap",
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue
            };

            // Переключаем биндинг в режим HTTPS вместо HTTP
            binding.Security.Mode = BasicHttpSecurityMode.Transport;

            var endpoint = new EndpointAddress(ExternalServiceSoapUrl);
            var client = new ExternalServiceSoapClient(binding, endpoint);
            AddMessageInspectorToEndpoint(client.Endpoint, inspector);

            return client;
        }

        /// <summary>
        /// Навешивает инспектора на эндпойнт
        /// </summary>
        private void AddMessageInspectorToEndpoint(ServiceEndpoint endpoint, IMessagesInspector inspector)
        {
            if (inspector != null)
            {
                endpoint.EndpointBehaviors.Add(inspector);
            }
        }
    }
}
