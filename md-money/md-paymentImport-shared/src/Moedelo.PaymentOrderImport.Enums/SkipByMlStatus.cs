namespace Moedelo.PaymentOrderImport.Enums
{
    /// <summary>
    /// Можно ли убрать с массовой страницы операцию, обработанную ML
    /// </summary>
    public enum SkipByMlStatus
    {
        /// <summary>
        /// Можно пропустить
        /// </summary>
        Allowed = 0,

        /// <summary>
        /// Применено аутсорс правило импорта
        /// </summary>
        DenyOutsourceRuleApplied = 1,

        /// <summary>
        /// Не разрешено в настройках клиента BPM
        /// </summary>
        DenyByClientSetting = 2,

        /// <summary>
        /// Применено пользовательское правило импорта
        /// </summary>
        DenyUserRuleApplied = 3,

        /// <summary>
        /// Операция НЕ в стандартном состоянии после импорта
        /// </summary>
        DenyNotDefaultOperationState = 4,

        /// <summary>
        /// ML не удалось определить тип операции
        /// </summary>
        DenyMlDontDeterminantOperationType = 5,

        /// <summary>
        /// Тип из импорта и тип из ML не совпадают
        /// </summary>
        DenyTypesNotEquals = 6,
    }
}
