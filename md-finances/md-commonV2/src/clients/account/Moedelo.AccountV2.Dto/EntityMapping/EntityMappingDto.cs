using System;

namespace Moedelo.AccountV2.Dto.EntityMapping
{
    public class EntityMappingDto
    {
        public long SourceId { get; set; }
        
        public long TargetId { get; set; }
        
        public DateTime CreateDate { get; set; }
    }
}