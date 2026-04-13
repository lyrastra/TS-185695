namespace Moedelo.Common.Enums.Enums.Integration
{
    public enum SberbankPaymentStatus
    {
        /// <summary> Статус не указан. </summary>
        Empty = 0,

        /// <summary> Платежное требование успешно создано, но еще не обработано. </summary>
        Created = 10,

        /// <summary> Платежное требование обработано и подтвержден банком. </summary>
        Confirmed = 20,

        /// <summary> Произошла ошибка при обработке платежа в банке. </summary>
        Error = 30,

        /// <summary> Платежное требование выставлено с ошибкой, ошибка на стороне МД при выполнении запроса в Сбербанк. </summary>
        MoedeloTechnicalError = 31,

        /// <summary> Платежное требование выставлено создан, но возникли вопросы при его обработке. </summary>
        Suspicios = 40,

        /// <summary> Платежное требование выставлено с ошибкой, ошибка обработана и будет выставлено заново. </summary>
        ErrorSkipped = 50
    }
}