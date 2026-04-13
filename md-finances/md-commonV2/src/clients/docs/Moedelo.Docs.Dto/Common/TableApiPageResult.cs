using System.Collections.Generic;

namespace Moedelo.Docs.Dto.Common
{
    public class TableApiPageResult<T>
    {
        public IReadOnlyCollection<T> data;
        public int offset;
        public int limit;
        public int totalCount;
    }
}