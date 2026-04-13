using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PaymentImport.Kafka.File.Events;
using System.Threading.Tasks;

namespace Moedelo.PaymentImport.Kafka.File
{
    public interface IFileImportEventWriter : IDI
    {
        Task WriteFileParsingCompletedAsync(string key, string token, FileParsingCompleted eventData);
        Task WriteFileParsingFaiedAsync(string key, string token, FileParsingFailed eventData);
        Task WriteFileImportCompletedAsync(string key, string token, FileImportCompleted eventData);
    }
}