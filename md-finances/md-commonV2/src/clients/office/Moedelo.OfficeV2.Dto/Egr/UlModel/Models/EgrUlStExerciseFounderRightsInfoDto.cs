using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об органе государственной власти, органе местного самоуправления или о юридическом лице, осуществляющем права учредителя (участника)
    /// </summary>
    public class EgrUlStExerciseFounderRightsInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn{ get; set; }

        /// <summary>
        /// Сведения о наименовании и (при наличии) ОГРН и ИНН органа государственной власти, органа местного самоуправления или ЮЛ
        /// </summary>
        public EgrUlBaseInfoDto AuthorityInfo { get; set; }
    }
}