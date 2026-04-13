using System.IO;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.ApiClient
{
    [InjectAsSingleton(typeof(IResponseParser))]
    public sealed class ResponseParser : IResponseParser
    {
        public TResult Parse<TResult>(string response)
        {
            var result = response.FromJsonString<TResult>(MdSerializerSettingsEnum.DateTimeZoneHandlingLocal);

            return result;
        }

        public TResult Parse<TResult>(Stream stream)
        {
            return stream.FromJsonStream<TResult>(MdSerializerSettingsEnum.DateTimeZoneHandlingLocal);
        }
    }
}