using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto
{
    public class ListResponse<T>
    {
        public List<T> Items { get; set; }
    }
}