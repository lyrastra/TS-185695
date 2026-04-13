using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об учредителе (участнике) - российском юридическом лице
    /// </summary>
    public class EgrUlFounderUlRfInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// Сведения о наименовании и (при наличии) ОГРН и ИНН ЮЛ
        /// </summary>
        public EgrUlBaseInfoDto UlBaseInfo { get; set; }

        /// <summary>
        /// Сведения о регистрации учредителя (участника) до 01.07.2002 г
        /// </summary>
        public EgrUlOldRegInfoDto OldRegInfo { get; set; }
        
        /// <summary>
        /// Сведения о доле учредителя (участника)
        /// </summary>
        public EgrUlCapitalShareInfoDto CapitalShare { get; set; }

        /// <summary>
        /// Сведения об обременении доли участника
        /// </summary>
        public List<EgrUlEncumbranceShareInfoDto> EncumbranceShare { get; set; }
    }
}