using System.Collections.Generic;

namespace Moedelo.KontragentsV2.Dto
{
    public class GetByNameExceptIdsOrderByNameParams
    {
        public string Query { get; set; }
        public List<int> ExceptIds { get; set; }
        public int Count { get; set; }
        public bool OnlyFounders { get; set; }
    }
}