
using System;

namespace Moedelo.Docs.ApiClient.Abstractions
{
    public class NdsSumDto
    {
        public long DocumentBaseId { get; set; }

        public DateTime DocumentDate { get; set; }

        public decimal NdsSum { get; set; }
    }
}