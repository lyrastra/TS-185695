using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Billing;
using Moedelo.Common.Enums.Enums.Leads;
using Moedelo.Common.Enums.Enums.RegistrationService;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.Registration.Dto
{
    public class RegistrationDataNewDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Fio { get; set; }

        public string Phone { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Ogrn { get; set; }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public string OrgName { get; set; }

        public string ClientSource { get; set; }

        public string RegLink { get; set; }

        public string UtmCampaign { get; set; }

        public string UtmContent { get; set; }

        public string UtmMedium { get; set; }

        public string UtmSource { get; set; }

        public string UtmSourceExtension { get; set; }

        public string UtmTerm { get; set; }

        public string UtmMrkt { get; set; }

        public long? UtmReferralLink { get; set; }

        public FirmRegistrationExternalPartner? ExternalPartner { get; set; }

        public int TimeZoneOffset { get; set; }

        public int? ReferralId { get; set; }

        public string DocumentRefferer { get; set; }

        public string ReferrerFirstUrl { get; set; }

        public string TrialCardNumber { get; set; }

        public string Region { get; set; }

        public Tariff? Tariff { get; set; }
        
        /// <summary>
        /// ID прайс-листа триального платежа
        /// </summary>
        public int PriceListId { get; set; }

        /// <summary>
        /// ID коммерческого прайс-листа для триальных аутсорс-платежей
        /// </summary>
        public int? CommercialPriceListId { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string JobTitle { get; set; }

        public bool IsOoo { get; set; }

        public EmployerMode EmployerMode { get; set; }

        public bool IsEnvd { get; set; }

        public bool IsUsn { get; set; }

        public UsnTypes UsnType { get; set; }

        public double UsnSize { get; set; }

        public bool IsOsno { get; set; }

        public bool IsPatent { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public RegistrationType RegistrationType { get; set; }

        public LeadMarkType LeadMark { get; set; }

        public int SalerId { get; set; }

        public string IpAddress { get; set; }

        public int OperatorId { get; set; }

        public string AdLenceId { get; set; }

        public string GoogleAnalyticId { get; set; }

        public bool IsRegistrationWithoutPassword { get; set; }

        public string CommentText { get; set; }

        // Id эксперимента в Google Analytics
        public string ExperimentId { get; set; }

        // Id варианта в рамках этого эксперимента
        public string VariantId { get; set; }
        
        // Группа компаний
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

        /// <summary>
        /// Тип фримиум платежа
        /// </summary>
        public FreemiumType? FreemiumType { get; set; }
        
        /// <summary>
        /// Тип доступа (trial, freemium)
        /// </summary>
        public AccessType AccessType { get; set; }
        
        /// <summary>
        /// Кол-во дней доступа
        /// </summary>
        public int AccessDays { get; set; }
        
        /// <summary>
        /// Идентификатор сервиса andata (для оптимизации контекстной рекламы)
        /// </summary>
        public string AndataUbtcuid { get; set; }

        /// <summary>
        /// Id партнера
        /// </summary>
        public int? PartnerId { get; set; }

        /// <summary>
        /// True, если форма регистрации с параметрами <br/>
        /// Если передать IsEnableFirmFlags = true и IsSetPassword = true, то включится фирм флаг IsSetPassword
        /// </summary>
        public bool? IsSetPassword { get; set; }

        /// <summary>
        /// Способ регистрации
        /// </summary>
        public RegistrationMethod? RegistrationMethod { get; set; }

        public string UserAgent { get; set; }

        public string AntiFrodAnalyticId { get; set; }

        public string PreviousPageBeforeRegistration { get; set; }

        public string MindboxDeviceUUID { get; set; }

        /// <summary>
        /// Откуда происходит регистрация
        /// </summary>
        public string RegistrationSource { get; set; }

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

        public string ChatData { get; set; }

        public RegistrationVerificationMethod? RegistrationVerificationMethod { get; set; }

        /// <summary>
        /// Идентификатор истории смс из верификации регистрации
        /// </summary>
        public int? RegistrationVerificationSmsHistoryId { get; set; }

        /// <summary>
        /// Требуется ли включение фирм флагов <br/>
        /// Например: Если передать IsEnableFirmFlags = true и IsSetPassword = true, то включится фирм флаг IsSetPassword
        /// </summary>
        public bool IsEnableFirmFlags { get; set; }

        /// <summary>
        /// True, если регистрация происходит на форме с 3 полями: имя, телефон, почта <br/>
        /// Например: Если передать IsEnableFirmFlags = true и IsNeedShowFirstScenario = true, то включится фирм флаг IsNeedShowFirstScenario
        /// </summary>
        public bool IsNeedShowFirstScenario { get; set; }

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
        public HashSet<RegistrationFieldType> UserFilledFields { get; set; }
    }
}