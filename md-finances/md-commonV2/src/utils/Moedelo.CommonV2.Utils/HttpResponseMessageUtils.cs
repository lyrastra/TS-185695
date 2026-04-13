using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Moedelo.CommonV2.Utils
{
    public static class HttpResponseMessageUtils
    {
        public static HttpContent CreateFileContent(string fileName, string fileContentType, Stream fileStream)
        {
            var content = new StreamContent(fileStream);
            var contentHeaders = content.Headers;
            contentHeaders.ContentType = new MediaTypeHeaderValue(fileContentType);
            var fileEncodeName = HttpUtility.UrlPathEncode(fileName);//https://msdn.microsoft.com/en-us/library/4fkewx0t(v=vs.110).aspx
            var disposition = new ContentDispositionHeaderValue("attachment") {FileName = fileEncodeName};
            contentHeaders.ContentDisposition = disposition;

            return content;
        }

        public static HttpContent CreateFileContent(string fileName, string fileContentType, byte[] byteArray)
        {
            var content = new ByteArrayContent(byteArray);
            var contentHeaders = content.Headers;
            contentHeaders.ContentType = new MediaTypeHeaderValue(fileContentType);
            var fileEncodeName = HttpUtility.UrlPathEncode(fileName);//https://msdn.microsoft.com/en-us/library/4fkewx0t(v=vs.110).aspx
            var disposition = new ContentDispositionHeaderValue("attachment") { FileName = fileEncodeName };
            contentHeaders.ContentDisposition = disposition;

            return content;
        }
    }
}