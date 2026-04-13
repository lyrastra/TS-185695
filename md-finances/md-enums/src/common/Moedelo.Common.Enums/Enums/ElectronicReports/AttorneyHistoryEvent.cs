namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum AttorneyHistoryEvent
    {
        EnablingElectronicReport = 60,
        /// <summary>Информационное уведомление о скором истечении доверенности</summary>
        NotificationBeforeExpiration = 61,
        /// <summary>Уведомление о скором истечении доверенности с просьбой продлить (или подключить ЭП)</summary>
        RecommendationUploadAttorney = 62,
        /// <summary>Уведомление о скором истечении доверенности с просьбой выпустить ЭП</summary>
        RecommendationReleaseEds = 63,
        /// <summary>Уведомление об истечении доверенности и отключение отчётности</summary>
        DisablingElectronicReport = 70
    }
}
