using System.Collections.Generic;

namespace Moedelo.RequisitesV2.Dto.FrameInfo
{
    /// <summary> Информация о реквизитах фирмы для фрейма </summary>
    public class RequisitesFrameInfoForFirmResponseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Расчетные счета </summary>
        public List<RequisitesFrameInfoSettlementAccountResponseDto> SettlementAccounts { get; set; }
    }
}