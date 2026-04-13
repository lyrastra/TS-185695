namespace Moedelo.RequisitesV2.Dto.FrameInfo
{
    /// <summary> Информация о расчетном счете фирмы для фрейма </summary>
    public class RequisitesFrameInfoSettlementAccountResponseDto
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Номер </summary>
        public string Number { get; set; }

        /// <summary> Отделение банка </summary>
        public string BankBranch { get; set; }

        /// <summary> Включена ли интеграция по этому рассчётному счёту </summary>
        public bool IsIntegrationOn { get; set; }
    }
}