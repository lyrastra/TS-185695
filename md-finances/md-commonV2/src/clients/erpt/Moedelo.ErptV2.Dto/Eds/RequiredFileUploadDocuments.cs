using Moedelo.Common.Enums.Enums.Uploaded;
using System;

namespace Moedelo.ErptV2.Dto.Eds
{
    public class RequiredFileUploadDocuments
    {
        public int Id { get; set; }
        public int? FirmId { get; set; }
        public string MongoId { get; set; }
        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }
        public UploadedFileContentType ContentType { get; set; }

        public string Name { get; set; }
        public string ScreenName { get; set; }
        public string Extension { get; set; }
        public bool IsDeleted { get; set; }
        public long Size { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
