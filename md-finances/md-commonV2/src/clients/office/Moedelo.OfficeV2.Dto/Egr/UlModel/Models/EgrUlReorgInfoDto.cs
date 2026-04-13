using System.Collections.Generic;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об участии в реорганизации
    /// </summary>
    public class EgrUlReorgInfoDto
    {
        /// <summary>
        /// Сведения о форме реорганизации (статусе) юридического лица
        /// </summary>
        public EgrUlReorgStatusInfoDto Status { get; set; }
        
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ записи, содержащей сведения о начале реорганизации
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// ГРН и дата внесения записи, которой в ЕГРЮЛ внесены сведения об изменении состава участвующих в реорганизации юридических лиц
        /// </summary>
        public EgrUlGrnDateInfoDto GrnChangeRearg { get; set; }

        /// <summary>
        /// Сведения о юридических лицах, участвующих  в реорганизации
        /// </summary>
        public List<EgrUlReorgUlInfoDto> ReargUL { get; set; }
    }
}