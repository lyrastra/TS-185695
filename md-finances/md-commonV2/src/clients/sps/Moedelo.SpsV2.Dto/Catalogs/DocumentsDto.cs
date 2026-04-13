using System;

namespace Moedelo.SpsV2.Dto.Catalogs
{
    public class DocumentsDto
    {
        public int DocumentId { get; set; }

        public byte ModuleId { get; set; }

        public string DocName { get; set; }

        public string ShortContent { get; set; }

        public int? GroupId { get; set; }

        public DateTime? BegDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
