using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об учредителе (участнике) - Российской Федерации, субъекте Российской Федерации, муниципальном образовании
    /// </summary>
    public class EgrUlFounderRfInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// Сведения о виде учредителя (участника) и (при необходимости) наименовании муниципального образования и региона
        /// </summary>
        public EgrUlFounderTypeInfoDto FounderTypeInfo { get; set; }

        /// <summary>
        /// Сведения о доле учредителя (участника)
        /// </summary>
        public EgrUlCapitalShareInfoDto CapitalShare { get; set; }

        /// <summary>
        /// Сведения об органе государственной власти, органе местного самоуправления или о юридическом лице, осуществляющем права учредителя (участника)
        /// </summary>
        public EgrUlStExerciseFounderRightsInfoDto StExerciseFounderRights { get; set; }

        /// <summary>
        /// Сведения о физическом лице, осуществляющем права учредителя (участника)
        /// </summary>
        public EgrUlFlExerciseFounderRightsInfoDto FlExerciseFounderRights { get; set; }

        /// <summary>
        /// Сведения об обременении доли участника
        /// </summary>
        public List<EgrUlEncumbranceShareInfoDto> EncumbranceShare { get; set; }
    }
}