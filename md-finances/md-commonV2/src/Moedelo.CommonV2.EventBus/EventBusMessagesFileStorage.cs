using Moedelo.CommonV2.EventBus.FileStorage;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.CommonV2.EventBus
{
    public partial class EventBusMessages
    {
        /// <summary>
        /// Домен: FileStorage
        /// Команда "Перевести файл на хранение через FileStorage API"
        /// </summary>
        // ReSharper disable once UnassignedReadonlyField
        public static readonly EventBusEventDefinition<MoveFileToFileStorageApiCommand> MoveFileToFileStorageApi;
    }
}