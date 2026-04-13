using System.Collections.Generic;

namespace Moedelo.OfficeV2.Client
{
    // для совместимости с v1 api
    public class ListDto<T> 
    {
        public ListDto()
        {
            this.Items = (IList<T>) new List<T>();
        }

        public ListDto(IList<T> items)
        {
            this.Items = items;
        }

        public IList<T> Items { get; set; }
    }
}