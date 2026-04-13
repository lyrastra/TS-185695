namespace Moedelo.Infrastructure.Http.Abstractions.Models
{
    public sealed class HttpRequestExecutionSettings
    {
        public bool AllowUntrustedSslCertificates { get; set; }
        
        public long? MaxResponseContentBufferSize { get; set; }
    }
}
