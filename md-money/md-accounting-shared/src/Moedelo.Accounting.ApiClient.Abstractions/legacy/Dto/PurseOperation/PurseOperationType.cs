namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.PurseOperation
{
    public enum PurseOperationType
    {
        /// <summary>
        /// Не определенная операция
        /// </summary>
        Default = 0,

        /// <summary>
        /// Поступление
        /// </summary>
        Income = 1,

        /// <summary>
        /// Перевод на р/с
        /// </summary>
        Transfer = 2,

        /// <summary>
        /// Удержание комиссии
        /// </summary>
        Comission = 3,

        /// <summary>
        /// Прочие списания
        /// </summary>
        OtherOutgoing = 4
    }
}
