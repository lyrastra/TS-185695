using System;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto
{
    public class CodeKindOfBusinessDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}