using System.IO;
using System.Web;
using Moedelo.CommonV2.Models.io;

namespace Moedelo.CommonV2.Utils
{
    public static class HttpPostedFileUtils
    {
        public static FileDataInfo Convert(this HttpPostedFile file)
        {
            return new FileDataInfo(Path.GetFileNameWithoutExtension(file.FileName), Path.GetExtension(file.FileName), file.InputStream);
        }
    }
}
