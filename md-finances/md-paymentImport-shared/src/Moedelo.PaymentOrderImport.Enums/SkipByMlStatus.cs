namespace Moedelo.PaymentOrderImport.Enums
{
    /// <summary>
    /// Можно ли убрать с массовой страницы операцию, обработанную ML
    /// </summary>
    public enum SkipByMlStatus : short
    {
        None = 0,

        /// <summary>
        /// Можно пропустить
        /// </summary>
        Allowed = 1,

        /// <summary>
        /// Применено аутсорс правило импорта
        /// </summary>
        DenyOutsourceRuleApplied = 2,

        /// <summary>
        /// Не разрешено в настройках клиента BPM
        /// </summary>
        DenyByClientSetting = 3,

        /// <summary>
        /// Применено пользовательское правило импорта
        /// </summary>
        DenyUserRuleApplied = 4,

        /// <summary>
        /// Операция НЕ в стандартном состоянии после импорта
        /// </summary>
        DenyNotDefaultOperationState = 5,

        /// <summary>
        /// ML не удалось определить тип операции
        /// </summary>
        DenyMlDontDeterminantOperationType = 6,

        /// <summary>
        /// Тип из импорта и тип из ML не совпадают
        /// </summary>
        DenyTypesNotEquals = 7,
    }
}
