using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.ContractsV2.Dto
{
    public class ContractFileDto
    {
        public byte[] Content { get; set; }

        public string MimeType { get; set; }

        public string Filename { get; set; }

        public DateTime DocDate { get; set; }

        public int KontragentId { get; set; }
    }
}
