namespace Moedelo.Money.Enums
{
    /// <summary>
    /// ВАЖНО:
    /// синхронизировать с https://github.com/moedelo/md-enums/blob/master/src/common/Moedelo.Common.Enums/Enums/Finances/Money/OperationState.cs
    /// расширил enum - поправь OperationStateExtensions
    /// </summary>
    public enum OperationState
    {
        Default = 0,
        Imported = 1,
        Duplicate = 2,
        MissingKontragent = 3,
        MissingWorker = 4,
        ImportProcessing = 5,
        DuplicateProcessing = 6,
        MissingKontragentProcessing = 7,
        MissingWorkerProcessing = 8,
        Invalid = 9,
        MissingExchangeRate = 10,
        MissingCurrencySettlementAccount = 11,
        MissingContract = 12,
        MissingCommissionAgent = 13,
        NoAutoDelete = 14,
        NoSubPayments = 15,
        /// <summary>
        /// Неоднозначный тип операции (SPEC-14350) 
        /// </summary>
        AmbiguousOperationType = 16,
        /// <summary>
        /// Операция "обработана" (подтверждена) сотрудником аутсорса
        /// Важно (!), стейт операции может переходить между Default и OutsourceApproved
        /// </summary>
        OutsourceApproved = 17,
        /// <summary>
        /// Ошибка валидации при обработке сотрудником Аута
        /// </summary>
        OutsourceProcessingValidationFailed = 19,
    }
}
