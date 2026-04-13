using System.Collections.Generic;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class ListDto<T> where T : class, new()
    {
        public IList<T> Items { get; set; }

        public ListDto()
        {
            Items = new List<T>();
        }

        public ListDto(IList<T> items)
        {
            Items = items;
        }
    }
}
