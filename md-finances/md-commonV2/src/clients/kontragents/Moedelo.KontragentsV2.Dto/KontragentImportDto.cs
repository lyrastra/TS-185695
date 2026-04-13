using System;

namespace Moedelo.KontragentsV2.Dto
{
    public class KontragentImportDto
    {
        public int Id { get; set; }

        public string Kontragent { get; set; }

        public string KontragentName { get; set; }

        public Guid ImportGuid { get; set; }

        public int? KontragentId { get; set; }
    }
}
