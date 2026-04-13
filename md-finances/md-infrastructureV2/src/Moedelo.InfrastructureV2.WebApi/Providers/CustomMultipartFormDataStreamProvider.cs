using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.WebApi.Providers
{
    public class CustomMultipartFormDataStreamProvider : MultipartStreamProvider
    {
        public NameValueCollection FormData { get; }
        public Collection<HttpContent> FileData { get; }

        public CustomMultipartFormDataStreamProvider()
        {
            FormData = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            FileData = new Collection<HttpContent>();
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            // For form data, Content-Disposition header is a requirement
            if (headers.ContentDisposition == null)
            {
                throw new InvalidOperationException("Did not find required 'Content-Disposition' header field in MIME multipart body part..");
            }
            return new MemoryStream();
        }

        public override async Task ExecutePostProcessingAsync()
        {
            foreach (var content in Contents)
            {
                var contentDisposition = content.Headers.ContentDisposition;
                if (string.IsNullOrEmpty(contentDisposition?.FileName))
                {
                    var name = contentDisposition?.Name?.Trim('\"') ?? string.Empty;
                    var value = await content.ReadAsStringAsync().ConfigureAwait(false);
                    FormData.Add(name, value);
                    continue;
                }
                FileData.Add(content);
            }
        }
    }
}