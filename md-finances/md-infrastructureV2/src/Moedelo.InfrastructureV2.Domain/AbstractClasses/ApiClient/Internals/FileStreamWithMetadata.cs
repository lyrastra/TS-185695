using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient.Internals;

internal record FileStreamWithMetadata<TMetadata>(HttpFileStream FileStream, TMetadata Metadata) : IFileStreamWithMetadata
    where TMetadata : class
{
    object IFileStreamWithMetadata.Metadata => Metadata;
}
