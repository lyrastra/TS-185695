using Moedelo.OfficeV2.Dto.Egr.UlModel.Models;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Доля в уставном капитале (складочном капитале, уставном фонде, паевом фонде), внесенная в ЕГРЮЛ
    /// </summary>
    public class EgrUlCapitalShareInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Размер доли (в процентах или в виде дроби - десятичной или простой)
        /// </summary>
        public EgrUlShareSizeInfoDto Size { get; set; }
        
        /// <summary>
        /// Номинальная стоимость доли в рублях
        /// </summary>
        public decimal Sum { get; set; }
    }
}