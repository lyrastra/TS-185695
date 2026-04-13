using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Internals;

internal record FileModelWithMetadata<TMetadata>(HttpFileModel File, TMetadata Metadata) : IFileModelWithMetadata
    where TMetadata : class
{
    object IFileModelWithMetadata.Metadata => Metadata;
}
