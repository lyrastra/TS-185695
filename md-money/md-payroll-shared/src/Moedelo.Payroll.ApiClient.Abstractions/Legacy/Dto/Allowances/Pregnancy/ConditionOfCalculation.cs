using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy
{
    /// <summary>
    /// Условия исчисления
    /// </summary>
    public enum ConditionOfCalculation
    {
        None,
        [Description("Лицо, относящееся к категории лиц, подвергшихся воздействию радиации")]
        RadiationExposure = 43,
        
        [Description("Лицо, приступившее к работе в районах Крайнего Севера и приравненных к ним местностях до 2007 года и продолжающее работать в этих местностях")]
        FarNorthRegion = 44,
        
        [Description("Лицо, имеющее инвалидность")]
        Disability = 45,

        [Description("Срочный трудовой договор до 6 месяцев")]
        EmploymentContractUpTo6Months = 46,
        
        [Description("Заболевание или травма, которые наступили в течение 30 календарных дней со дня прекращения работы по трудовому договору, осуществления служебной или иной деятельности, в течение которых лицо подлежит обязательному социальному страхованию на случай временной нетрудоспособности и в связи с материнством")]
        AfterTerminationWithin30Days = 47,
        
        [Description("Уважительная причина нарушения условий оказания медицинской помощи")]
        ValidReasonViolation = 48,
        
        [Description("Продолжительность заболевания превышает 4 месяца подряд")]
        DurationMore4Months = 49,
        
        [Description("Продолжительность заболевания превышает 5 месяцев в календарном году")]
        DurationMore5Months = 50,

        [Description("Неполное рабочее время")]
        PartialWorkingTime = 51
    }
}