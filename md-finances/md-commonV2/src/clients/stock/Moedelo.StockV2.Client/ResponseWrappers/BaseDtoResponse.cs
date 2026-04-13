using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.StockV2.Client.ResponseWrappers
{
    public class BaseDtoResponse
    {
        public bool ResponseStatus { get; set; }

        public string ResponseMessage { get; set; }
    }
}
