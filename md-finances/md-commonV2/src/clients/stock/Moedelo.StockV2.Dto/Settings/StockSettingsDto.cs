using System;
using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Settings
{
    public class StockSettingsDto
    {
        public bool Enabled { get; set; }

        public DateTime StockActivationDate { get; set; }

        public DebitMaterialsType DebitMaterials { get; set; }
    }
}