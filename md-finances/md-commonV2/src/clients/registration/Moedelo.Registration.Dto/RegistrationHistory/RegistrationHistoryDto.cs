using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.RegistrationService;

namespace Moedelo.Registration.Dto.RegistrationHistory
{
    public class RegistrationHistoryDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public int? TrialPaymentId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string UtmSource { get; set; }

        public string UtmSourceExtension { get; set; }

        public string UtmMedium { get; set; }

        public string UtmCampaign { get; set; }

        public string UtmContent { get; set; }

        public string UtmTerm { get; set; }
        
        public string UtmMrkt { get; set; }

        /// <summary>
        /// Внешний ресурс, с которого пришёл пользователь
        /// </summary>
        public string ExternalUrl { get; set; }

        /// <summary>
        /// Первая страница сайта, которую посетил пользователь
        /// </summary>
        public string FirstLocalUrl { get; set; }

        /// <summary>
        /// Страница, с которой произошла регистрация пользователя
        /// </summary>
        public string RegistrationUrl { get; set; }

        public string GoogleAnalyticId { get; set; }

        /// <summary>
        /// Идентификатор клиента в Яндекс.Метрика
        /// </summary>
        public string YandexMetrikaUid { get; set; }

        /// <summary>
        /// Идентификатор счётчика Яндекс.Метрика
        /// </summary>
        public string YandexCounterId { get; set; }

        /// <summary>
        /// Идентификатор клика по рекламному объявлению Яндекс.Директа
        /// </summary>
        public string YandexClickId { get; set; }

        public int? TrialCardId { get; set; }
        
        /// <summary>
        /// Выбор на форме Группа Компаний на форме регистрации
        /// </summary>
        public bool? IsCompanyGroup { get; set; }
        
        /// <summary>
        /// отслеживание перехода на сайт для cpa сетей (Id перехода)
        /// </summary>
        public string TrackingId { get; set; }

        /// <summary>
        /// Сколько компаний обслуживает
        /// </summary>
        public string FirmsOnServiceCount { get; set; }

        /// <summary>
        /// Количество сотрудников
        /// </summary>
        public string EmployeeCount { get; set; }

        /// <summary>
        /// сфера деятельности компании
        /// </summary>
        public string FirmActivity { get; set; }

        public bool IsPatent { get; set; }

        /// <summary>
        /// текст оффера на лендинге, с которого произошёл переход на форму регистрации
        /// используется для автоматизации по офферу в TDC
        /// </summary>
        public string OfferText { get; set; }
        
        /// <summary>
        /// Потребность в оффере для передачи в BRMS с целью рассчета
        /// и передачи одного или нескольких тегов потребности в crm
        /// Например: tax, szvm и др.
        /// </summary>
        public string OfferNeed { get; set; }
        
        /// <summary>
        /// Идентификатор сервиса andata (для оптимизации контекстной рекламы)
        /// </summary>
        public string AndataUbtcuid { get; set; }

        /// <summary>
        /// Id партнера
        /// </summary>
        public int? PartnerId { get; set; }

        /// <summary>
        /// Id юзера-сотрудника партнёра, от которого выполняется регистрация
        /// </summary>
        public int? PartnerEmployeeUserId { get; set; }

        /// <summary>
        /// Мультиворонки - является ли лид целевым
        /// </summary>
        public bool? IsTargetLead { get; set; }

        /// <summary>
        /// Продукт регистрации
        /// </summary>
        public string RegistrationProduct { get; set; }

        /// <summary>
        /// Статус регистрации
        /// </summary>
        public RegistrationStatus? RegistrationStatus { get; set; }

        /// <summary>
        /// Метод регистрации
        /// </summary>
        public RegistrationMethod RegistrationMethod { get; set; }

        /// <summary>
        /// ID триального тарифа
        /// </summary>
        public int? TrialTariffId { get; set; }

        /// <summary>
        /// ID прайс-листа
        /// </summary>
        public int? PriceListId { get; set; }

        /// <summary>
        /// ID коммерческого прайс-листа для триальных аутсорс-платежей
        /// </summary>
        public int? OutsourcingPriceListId { get; set; }

        /// <summary>
        /// Технический код ПУ
        /// </summary>
        public string ProductConfigurationCode { get; set; }

        public string UserAgent { get; set; }

        public string IpAddress { get; set; }

        public string AntiFrodAnalyticId { get; set; }

        public string PreviousPageBeforeRegistration { get; set; }

        public string MindboxDeviceUUID { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string JobTitle { get; set; }

        public string WebinarName { get; set; }

        public DateTime? WebinarDate { get; set; }

        public IReadOnlyCollection<int> YclientsSalonIds { get; set; }

        /// <summary>
        /// Предпочтительная дата и время начала звонка
        /// </summary>
        public DateTime? PreferredCallStartDateTime { get; set; }

        /// <summary>
        /// Идентификатор сотрудника внешнего партнера
        /// </summary>
        public int? ExternalPartnerEmployeeId { get; set; }

        /// <summary>
        /// Юридическое лицо = true, физическое лицо = false
        /// </summary>
        public bool? IsLegalRelation { get; set; }

        /// <summary>
        /// Приоритетный лид
        /// </summary>
        public bool? IsPriorityLead { get; set; }

        /// <summary>
        /// Ответственный за канал лидгена рекомендателя по программе "Пригласи друга"
        /// </summary>
        public string InviterLeadGenResponsible { get; set; }

        /// <summary>
        /// Email, который был указан при регистрации
        /// </summary>
        public string CurrentRegistrationEmail { get; set; }

        /// <summary>
        /// Количество дней с момента последней повторной попытки регистрации
        /// </summary>
        public int? CountOfDaysFromLastAttemptRepeated { get; set; }

        public RegistrationVerificationMethod? RegistrationVerificationMethod { get; set; }

        /// <summary>
        /// Идентификатор истории смс из верификации регистрации
        /// </summary>
        public int? RegistrationVerificationSmsHistoryId { get; set; }

        public long? CalltouchCallId { get; set; }

        /// <summary>
        /// Уникальный идентификатор сеанса связи с АТС
        /// </summary>
        public string SipCallId { get; set; }

        /// <summary>
        /// Guid верификации телефона регистрации
        /// </summary>
        public Guid? RegistrationPhoneVerificationGuid { get; set; }

        /// <summary>
        /// Поля заполненные пользователем
        /// </summary>
        public string RegistrationSource { get; set; }

        /// <summary>
        /// Поля заполненные пользователем при регистрации
        /// </summary>
        public HashSet<RegistrationFieldType> UserFilledFields { get; set; }
    }
}