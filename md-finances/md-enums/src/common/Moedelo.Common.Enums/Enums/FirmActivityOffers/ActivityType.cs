namespace Moedelo.Common.Enums.Enums.FirmActivityOffers
{
    public enum ActivityType
    {
        // Переход в бух. консультации пользователя, не имеющего прав на создание вопросов
        VisitingConsultationQuestionsWithoutAccess = 14,

        // Попытка сохранения валютного р/с, при отсутствии прав на валютные счета
        AttemptSaveCurrencySettlementAccountWithoutPermission = 15,

        // Переход в бух. консультации
        VisitingConsultationsPage = 16,
    }
}
