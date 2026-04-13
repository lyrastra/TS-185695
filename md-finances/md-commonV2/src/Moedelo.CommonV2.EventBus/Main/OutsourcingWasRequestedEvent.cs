namespace Moedelo.CommonV2.EventBus.Main
{
    /// <summary>
    /// Оставлена заявка на аутсорсинг бухгалтерии
    /// </summary>
    public class OutsourcingWasRequestedEvent
    {
        public int FirmId { get; set; }

        /// <summary>
        /// Из первого сценария вовлечения (еще можно, например из ЦП)
        /// </summary>
        public bool IsOnboarding { get; set; }
    }
}