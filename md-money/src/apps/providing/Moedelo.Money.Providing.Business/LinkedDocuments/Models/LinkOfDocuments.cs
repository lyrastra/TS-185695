using Moedelo.LinkedDocuments.Enums;
using System;

namespace Moedelo.Money.Providing.Business.LinkedDocuments.Models
{
    class TwoWayLink
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public long LinkedFromId { get; set; }
        
        public LinkType LinkedFromType { get; set; }

        public long LinkedToId { get; set; }

        public LinkType LinkedToType { get; set; }
    }
}
