namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведения о контактном телефоне
    /// </summary>
    public class EgrUlPhoneInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Контактный телефон
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}