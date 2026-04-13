namespace Moedelo.RequisitesV2.Dto.TradingObjects
{
    public class TradingObjectAddressDto
    {
        /// <summary>
        /// Код региона
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// Полный адрес в виде строки
        /// </summary>
        public string AddressString { get; set; }
    }
}