using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Internals;

internal interface IFileModelWithMetadata
{
    HttpFileModel File { get; }
    object Metadata { get; }
}
