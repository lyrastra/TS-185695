namespace Moedelo.Pdf417V2.Dto
{
    public enum PdfPrintStatus
    {
        /// <summary>
        /// Ошибка обработки данных.
        /// </summary>
        Error = -1,

        /// <summary>
        /// Файл еще не ушел в обработку
        /// </summary>
        New = 0,

        /// <summary>
        /// Файл находится в обработке.
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Файл обработан без ошибок.
        /// </summary>
        Ok = 2,
    }
}