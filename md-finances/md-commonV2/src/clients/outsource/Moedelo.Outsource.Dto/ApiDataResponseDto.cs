using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.Outsource.Dto
{
    public class ApiDataResponseDto<T>
    {
        public ApiDataResponseDto(T data)
        {
            this.data = data;
        }

        public T data { get; set; }
    }
}
