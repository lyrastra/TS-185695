using System.Collections.Generic;

namespace Moedelo.AccountV2.Dto.ProfOutsource
{
    public class FirmRelationDto
    {
        public int AccountId { get; set; }
        public IList<int> AttachableFirms { get; set; }
    }
}