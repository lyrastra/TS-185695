using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.WebApi.HttpActionResults;

namespace Moedelo.InfrastructureV2.WebApi.Extensions
{
    public static class ApiControllerExtensions
    {
        public static FileActionResult File(this ApiController controller, byte[] content, string fileName, string contentType = null)
        {
            var result = new FileActionResult(controller.Request, content, fileName);
            if (!string.IsNullOrEmpty(contentType))
            {
                result.ContentType = contentType;
            }
            return result;
        }

        public static FileActionResult File(this ApiController controller, Stream content, string fileName, string contentType = null)
        {
            var result = new FileActionResult(controller.Request, content, fileName);
            if (!string.IsNullOrEmpty(contentType))
            {
                result.ContentType = contentType;
            }
            return result;
        }
        
        public static async Task<List<HttpFileModel>> GetUploadedFilesAsync(this ApiController controller)
        {
            var requestContent = controller.Request.Content;
            if (!requestContent.IsMimeMultipartContent())
            {
                return new List<HttpFileModel>();
            }

            var provider = await requestContent.ReadAsMultipartAsync(new MultipartMemoryStreamProvider()).ConfigureAwait(false);
            var tasks = provider.Contents
                .Where(i => !string.IsNullOrEmpty(i.Headers.ContentDisposition.FileName))
                .Select(async content =>
                {
                    var stream = new MemoryStream();
                    await content.CopyToAsync(stream).ConfigureAwait(false);
                    stream.Position = 0;
                    
                    return new HttpFileModel
                    {
                        Stream = stream,
                        FileName = content.Headers.ContentDisposition.FileName.Trim('\"'),
                        ContentType = content.Headers.ContentType.MediaType
                    };
                })
                .ToList();
                
               await Task.WhenAll(tasks).ConfigureAwait(false);
               return tasks
                   .Where(x => x.Result != null && x.Result.Stream.Length > 0)
                   .Select(x => x.Result)
                   .ToList();
        }
    }
}
