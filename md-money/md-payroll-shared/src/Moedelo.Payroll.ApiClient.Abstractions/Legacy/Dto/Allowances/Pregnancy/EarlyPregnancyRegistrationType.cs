using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy
{
    /// <summary>
    /// Постановка на учет в ранние сроки 
    /// </summary>
    public enum EarlyPregnancyRegistrationType : byte
    {
        [Description("Нет")]
        NoRegistration = 0,
        [Description("Поставлена")]
        Registered,
        [Description("Нет информации")]
        NoInformation
    }
}