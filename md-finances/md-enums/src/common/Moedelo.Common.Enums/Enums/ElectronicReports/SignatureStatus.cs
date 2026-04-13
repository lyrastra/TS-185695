namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum SignatureStatus
    {
        /// <summary>Не установлен  </summary>
        Undefined = 0,

        /// <summary> Нет статуса </summary>
        None = 1,

        /// <summary> Реквизиты подтверждены пользователем </summary>
        RequisitesConfirmed = 5,

        /// <summary> Телефон подтверждён пользователем </summary>
        PhoneCohfirmed = 6,

        /// <summary> Документы на выпуск или продление загружены </summary>
        DocumentUploaded = 10,

        /// <summary> Документы на перевыпуск загружены </summary>
        ReRequestUploaded = 15,

        /// <summary> Подвтерждено партнером </summary>
        PartnerConfirmed = 20,

        /// <summary> Отправлена на пересоздание </summary>
        Recreated = 21,

        /// <summary> Запрос отправленн </summary>
        RequestSended = 30,

        /// <summary> Проблемный пользователь </summary>
        Problem = 40,

        /// <summary> Пользователь отклонён </summary>
        UserRejected = 50,

        /// <summary> Ошибка валидации запроса на DSS </summary>
        ValidationFailed = 51,

        /// <summary> Ошибка СМЭВ валидации на DSS </summary>
        SmevValidationFailed = 52,

        /// <summary> Ошибка регистрации запроса на DSS </summary>
        RegistrationFailed = 53,

        /// <summary> Ошибка паплайна выпуска ЭП </summary>
        RestartableError = 54,
        
        /// <summary> Неизвестная ошибка на DSS </summary>
        UnpredictableError = 55,

        /// <summary> Подпись отозвана </summary>
        SignatureRevoked = 59,

        /// <summary> Пользователь подтверждён </summary>
        SignatureCreated = 60,
    }
}
