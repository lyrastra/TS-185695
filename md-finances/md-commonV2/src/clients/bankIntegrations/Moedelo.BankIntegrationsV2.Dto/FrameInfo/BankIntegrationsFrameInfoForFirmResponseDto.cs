using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.FrameInfo
{
    /// <summary> Информация о банковской интеграции фирмы для фрейма </summary>
    public class BankIntegrationsFrameInfoForFirmResponseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Банки с интеграцией </summary>
        public List<BankIntegrationsFrameInfoIntegratedBankResponseDto> IntegratedBanks { get; set; }
    }
}