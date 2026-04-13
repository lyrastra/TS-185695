namespace Moedelo.Infrastructure.Http.Abstractions.Models
{
    public sealed class HttpClientSettings
    {
        public bool AllowUntrustedSslCertificates { get; set; }
        
        public long? MaxResponseContentBufferSize { get; set; }

        public static HttpClientSettings Default => new HttpClientSettings();
    }
}
