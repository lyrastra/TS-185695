namespace Moedelo.CommonV2.EventBus.Crm
{
    public class IlliquidLeadMovingCommand
    {
        /// <summary>
        /// Идентификатор лида в автообзвоне
        /// </summary>
        public string DialerLeadId { get; set; }
        
        /// <summary>
        /// Тип сервиса определения неликвидных лидов
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Повторное перемещение неликвидного лида
        /// </summary>
        public bool Retry { get; set; }
    }
}