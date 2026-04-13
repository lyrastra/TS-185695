using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildBirth
{
    public enum ChildBirthAllowanceReferenceType
    {
        [Description("Справка о рождении ребенка, выданной органами ЗАГСа форма 24")]
        Form24 = 24,
        [Description("Справка о рождении ребенка, выданной органами ЗАГСа форма 25")]
        Form25 = 25,
        [Description("Справка о рождении ребенка, выданной органами ЗАГСа форма 26")]
        Form26 = 26,
        [Description("Иной документ, подтверждающий рождение ребенка")]
        OtherDocument = 35,
        [Description("Справка о рождении ребенка, выданной органами ЗАГСа форма 1")]
        Form1 = 48,
        [Description("Справка о рождении ребенка, выданной органами ЗАГСа форма 2")]
        Form2 = 49,
        [Description("Справка о рождении ребенка, выданной органами ЗАГСа форма 3")]
        Form3 = 50,
        [Description("Справка о рождении ребенка, выданной органами ЗАГСа форма 4")]
        Form4 = 51
    }
}