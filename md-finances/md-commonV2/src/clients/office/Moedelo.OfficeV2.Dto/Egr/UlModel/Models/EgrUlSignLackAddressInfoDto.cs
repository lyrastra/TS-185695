using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о недостоверности адреса или отсутствии связи с ЮЛ по указанному адресу
    /// </summary>
    public class EgrUlSignLackAddressInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Сведения о решении суда, на основании которого адрес признан недостоверным
        /// </summary>
        public EgrUlCourtDecisionInfoDto EgrUlCourtDecision { get; set; }

        /// <summary>
        /// Признак невозможности взаимодействия с юридическим лицом по содержащемуся в ЕГРЮЛ адресу
        /// </summary>
        public EgrUlSignLackAddress EgrUlSignLackAddress { get; set; }
    }
}