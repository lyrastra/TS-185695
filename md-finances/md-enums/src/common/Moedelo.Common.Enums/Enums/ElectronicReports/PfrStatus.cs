namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum PfrStatus
    {
        /// <summary> Нет реквизитов </summary>
        None = 0,

        /// <summary>Не использовать в коде</summary>
        MasterStep1Ended = 1,
        MasterStep2Ended = 2,
        MasterStep3Ended = 3,
        MasterStep4Ended = 4,

        /// <summary> Документы загружены </summary>
        DocumentUploaded = 30,
        /// <summary> Пользователь отклонён </summary>
        UserRejected = 50,
        /// <summary> Пользователь подтверждён </summary>
        UserApplied = 60
    }
}