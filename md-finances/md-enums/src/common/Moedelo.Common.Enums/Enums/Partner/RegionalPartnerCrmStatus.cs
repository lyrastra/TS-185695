using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Partner
{
    public enum RegionalPartnerCrmStatus
    {
        [Description("")]
        Default = 0,

        [Description("В работе")]
        InWork = 1,

        [Description("Недозвон")]
        NotCalled = 2,

        [Description("Дубль")]
        Duplicate = 4,

        [Description("Ошибочная регистрация")]
        WrongRegistration = 5,

        [Description("Неликвид")]
        NotLiquid = 6,

        [Description("Отказ")]
        Failure = 7,

        [Description("Собеседование проведено. Думает")]
        InterviewСonductedThinks = 8,

        [Description("Собеседование проведено. Отказ")]
        InterviewСonductedFailure = 9,

        [Description("Назначено собеседование")]
        InterviewPlaned  = 10,

        [Description("Открыть СДО")]
        OpenDls  = 11,   
        
        [Description("Заключен договор")]
        ContractConcluded  = 12
    }
}