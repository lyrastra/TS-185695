using Moedelo.Common.Enums.Enums.PaymentImport;
using Moedelo.CommonV2.EventBus;
using Moedelo.CommonV2.EventBus.PaymentImport;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.PaymentImport;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.ExecutionContext.Client;
using Moedelo.Finances.Business.Services.PaymentOrders.Events;
using Moedelo.Finances.Business.Services.PaymentOrders.Topics;
using Moedelo.PaymentImport.Client;
using Moedelo.PaymentImport.Dto;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Interfaces.Business.Payment;
using Moedelo.PaymentImport.Client.MovementList.Storage;

namespace Moedelo.Finances.Business.Services.PaymentOrders
{
    [InjectAsSingleton(typeof(IPaymentImportService))]
    public class PaymentImportService : IPaymentImportService
    {
        private const int SizeLimit = 1024 * 1024; // 1 Mb
        private readonly IMovementListUserStorageClient userStorageApiClient;
        private readonly IPaymentImportClient paymentImportClient;
        private readonly IPaymentImportNetCoreClient paymentImportNetCoreClient;
        private readonly IPublisher<ImportForUserEvent> paymentImportForUserPublisher;
        private readonly IMoedeloEntityEventKafkaTopicWriter kafkaEventWriter;
        private readonly ISettingRepository settingRepository;
        private readonly ITokenApiClient tokenApiClient;
        private const string Tag = nameof(PaymentImportService);
        private readonly ILogger logger;

        public PaymentImportService(
            IMovementListUserStorageClient userStorageApiClient,
            IPaymentImportClient paymentImportClient,
            IPaymentImportNetCoreClient paymentImportNetCoreClient,
            IPublisherFactory publisherFactory,
            IMoedeloEntityEventKafkaTopicWriter kafkaEventWriter,
            ISettingRepository settingRepository,
            ITokenApiClient tokenApiClient,
            ILogger logger)
        {
            this.userStorageApiClient = userStorageApiClient;
            this.paymentImportClient = paymentImportClient;
            this.paymentImportNetCoreClient = paymentImportNetCoreClient;
            this.kafkaEventWriter = kafkaEventWriter;
            this.settingRepository = settingRepository;
            this.tokenApiClient = tokenApiClient;
            this.logger = logger;
            paymentImportForUserPublisher = publisherFactory.GetForAllClient(EventBusMessages.PaymentImportForUserEvent);
        }

        public async Task<ImportStatus> ImportAsync(IUserContext userContext, FileData fileData)
        {
            if (fileData.Size > SizeLimit)
            {
                return new ImportStatus { Status = ImportResultStatus.WrongFile };
            }

            var fileId = await userStorageApiClient
                .SaveAsync(new SaveMovementListDto 
                { 
                    FileName = $@"paymentImport\{userContext.FirmId}\{fileData.Name}", 
                    FileData = fileData.Content,
                    FirmId = userContext.FirmId
                })
                .ConfigureAwait(false);

            if (fileId == null)
                logger.Error(Tag, "ImportAsync fileId is null error", null, null, new { userContext.FirmId, fileName = fileData.Name, fileLength = fileData.Content.Length });


            var data = new ImportFromUser
            {
                FileId = fileId,
                CheckDocuments = true,
                CheckSettlementAccount = true
            };

            var result = await CheckImportFileAsync(userContext, data).ConfigureAwait(false);
            if (result.Status == ImportResultStatus.Ok)
            {
                return await PublishImportForUserEvent(userContext, data);
            }

            return result;
        }

        public async Task<ImportStatus> ImportAsync(IUserContext userContext, ImportFromUser data)
        {
            var result = await CheckImportFileAsync(userContext, data).ConfigureAwait(false);
            if (result.Status == ImportResultStatus.Ok)
            {
                return await PublishImportForUserEvent(userContext, data);
            }

            return result;

        }

        public Task<string> GetImportMessagesAsync(IUserContext userContext, CancellationToken ctx)
        {
            return paymentImportClient.GetImportMessagesAsync(userContext.FirmId, userContext.UserId, ctx);
        }

        private async Task<ImportStatus> CheckImportFileAsync(IUserContext userContext, ImportFromUser data)
        {
            var result = await paymentImportNetCoreClient
                .CheckImportFileAsync(userContext.FirmId, userContext.UserId, Map(data))
                .ConfigureAwait(false);

            return new ImportStatus
            {
                Status = result.Status,
                FileId = data.FileId,
                ExData = result.ExData
            };
        }

        private async Task<ImportStatus> PublishImportForUserEvent(IUserContext userContext, ImportFromUser data)
        {
            var mongoFileInfo = await userStorageApiClient.GetFileNameAsync(data.FileId).ConfigureAwait(false);
            
            // Получаем настройку из Consul для выбора между RabbitMQ и Kafka
            var useKafka = settingRepository.Get("PaymentImport.UseKafka").GetBoolValueOrDefault(false);
            
            if (useKafka)
            {
                await PublishToKafkaAsync(userContext, data, mongoFileInfo).ConfigureAwait(false);
            }
            else
            {
                await PublishToRabbitMQAsync(userContext, data, mongoFileInfo).ConfigureAwait(false);
            }
            
            return new ImportStatus
            {
                Status = ImportResultStatus.InProcess,
                FileId = data.FileId,
                ExData = new
                {
                    IsProcessSettlementAccount = true,
                    IsCheckDocuments = true
                }
            };
        }

        private async Task PublishToRabbitMQAsync(IUserContext userContext, ImportFromUser data, string mongoFileInfo)
        {
            var importForUserEvent = Map(userContext.FirmId, userContext.UserId, data, mongoFileInfo);
            await paymentImportForUserPublisher.PublishAsync(importForUserEvent).ConfigureAwait(false);
            logger.Info(Tag, "PaymentImport event sent to RabbitMQ", extraData: new { userContext.FirmId, userContext.UserId, data.FileId });
        }

        private async Task PublishToKafkaAsync(IUserContext userContext, ImportFromUser data, string mongoFileInfo)
        {
            var kafkaEvent = MapToKafkaEvent(userContext.FirmId, userContext.UserId, data, mongoFileInfo);
            var key = $"{userContext.FirmId}_{userContext.UserId}_{data.FileId}";
            var token = await GetUserTokenAsync(userContext).ConfigureAwait(false);
            
            await kafkaEventWriter.WriteEventDataAsync(
                ImportTopics.ImportForUser.Document.Event.Topic,
                key,
                ImportTopics.ImportForUser.Document.EntityName,
                kafkaEvent,
                token).ConfigureAwait(false);
            
            logger.Info(Tag, "PaymentImport event sent to Kafka", extraData: new { userContext.FirmId, userContext.UserId, data.FileId });
        }

        private async Task<string> GetUserTokenAsync(IUserContext userContext)
        {
            return await tokenApiClient.GetFromUserContextAsync(userContext.FirmId, userContext.UserId).ConfigureAwait(false);
        }

        private static ImportForUserKafkaEvent MapToKafkaEvent(int firmId, int userId, ImportFromUser data, string fileName)
        {
            return new ImportForUserKafkaEvent
            {
                FirmId = firmId,
                UserId = userId,
                FileName = fileName,
                FileId = data.FileId,
                SecondSettlementAccount = data.SecondSettlementAccount,
                SettlementAccountType = data.SettlementAccountType
            };
        }

        private static ImportForUserEvent Map(int firmId, int userId, ImportFromUser data, string fileName)
        {
            return new ImportForUserEvent
            {
                FirmId = firmId,
                UserId = userId,
                FileName = fileName,
                FileId = data.FileId,
                SecondSettlementAccount = data.SecondSettlementAccount,
                SettlementAccountType = data.SettlementAccountType
            };
        }

        private static ImportFromUserDto Map(ImportFromUser data)
        {
            return new ImportFromUserDto
            {
                FileId = data.FileId,
                CheckDocuments = data.CheckDocuments,
                CheckSettlementAccount = data.CheckSettlementAccount,
                SecondSettlementAccount = data.SecondSettlementAccount,
                SettlementAccountType = data.SettlementAccountType
            };
        }
    }
}
