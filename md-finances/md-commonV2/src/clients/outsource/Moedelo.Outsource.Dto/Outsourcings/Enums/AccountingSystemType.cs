namespace Moedelo.Outsource.Dto.Outsourcings.Enums
{
    public enum AccountingSystemType
    {
        /// <summary>
        ///     Нет
        /// </summary>
        No = 1,

        /// <summary>
        ///     Биз
        /// </summary>
        Biz = 2,

        /// <summary>
        ///     УС
        /// </summary>
        Acc = 3,

        /// <summary>
        ///     1C (сервер клиента)
        /// </summary>
        ServerClient1C = 4,

        /// <summary>
        ///     1C (сервер наш)
        /// </summary>
        ServerOur1C = 5,

        /// <summary>
        ///     1C фреш (клиент)
        /// </summary>
        FreshClient1C = 6,

        /// <summary>
        ///     1C фреш (наше)
        /// </summary>
        FreshOur1C = 7,

        /// <summary>
        ///     1С (индивидуальное подключение)
        /// </summary>
        IndividualConnection1C = 8,
    }
}