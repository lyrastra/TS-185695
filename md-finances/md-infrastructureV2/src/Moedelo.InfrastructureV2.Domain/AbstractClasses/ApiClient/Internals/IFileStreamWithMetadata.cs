using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Internals;

internal interface IFileStreamWithMetadata
{
    HttpFileStream FileStream { get; }
    object Metadata { get; }
}
