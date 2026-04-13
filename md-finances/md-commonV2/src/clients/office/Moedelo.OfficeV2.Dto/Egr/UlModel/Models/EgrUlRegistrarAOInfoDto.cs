using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    public class EgrUlRegistrarAOInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// Наименование и (при наличии) ОГРН и ИНН держателе реестра акционеров акционерного общества
        /// </summary>
        public EgrUlBaseInfoDto RegistrarAO { get; set; }
    }
}
