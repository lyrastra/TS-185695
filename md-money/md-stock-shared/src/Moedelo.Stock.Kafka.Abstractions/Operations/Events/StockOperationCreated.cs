using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Stock.Enums;
using System.Collections.Generic;
using System;
using Moedelo.Stock.Kafka.Abstractions.Operations.Models;

namespace Moedelo.Stock.Kafka.Abstractions.Products.Events
{
    public class StockOperationCreated : IEntityEventData
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public StockOperationTypeEnum Type { get; set; }

        public IReadOnlyCollection<StockOperationOverProductModel> OverProducts { get; set; }
    }
}
