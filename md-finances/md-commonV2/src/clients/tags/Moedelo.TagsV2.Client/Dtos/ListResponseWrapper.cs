using System.Collections.Generic;

namespace Moedelo.TagsV2.Client.Dtos
{
    class ListResponseWrapper<T>
    {
        public List<T> Items { get; set; }
    }
}
