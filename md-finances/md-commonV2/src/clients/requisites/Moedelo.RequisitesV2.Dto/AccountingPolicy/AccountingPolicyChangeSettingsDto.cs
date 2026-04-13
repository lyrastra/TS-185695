namespace Moedelo.RequisitesV2.Dto.AccountingPolicy
{
    /// <summary>
    /// Информация по текущей системе налогообложения и возможности смены СНО
    /// </summary>
    public class AccountingPolicyChangeSettingsDto
    {
        /// <summary>
        /// Система налогообложения ОСНО
        /// </summary>
        public bool IsOsno { get; set; }

        /// <summary>
        /// Система налогообложения УСН
        /// </summary>
        public bool IsUsn { get; set; }

        /// <summary>
        /// Запрет на смену СНО (есть проводки в закрытом периоде)
        /// </summary>
        public bool IsDenyChange { get; set; }

        /// <summary>
        /// Предупредить о сложностях смены СНО (пересохранение документов)
        /// </summary>
        public bool IsNeedWarnMessage { get; set; }

        /// <summary>
        /// Сообщение о возможных трудностях смены СНО
        /// </summary>
        public string Message { get; set; }
    }
}