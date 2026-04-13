using System.ComponentModel;

namespace Moedelo.Payroll.Enums.Allowances
{
    public enum ChargeType : byte
    {
        //Нетрудоспособность
        [Description("Нетрудоспособность")]
        SickList = 1,
        //Беременность и роды
        [Description("Беременность и роды")]
        PregnancyAndChildbirth = 2,
        //Ранние сроки беременности
        [Description("Ранние сроки беременности")]
        EarlyPregnancy = 3,
        //Рождение ребенка
        [Description("Рождение ребенка")]
        ChildBirth = 4,
        //Уход за ребенком
        [Description("Уход за ребенком")]
        ChildCare = 5,
        [Description("ГПД")]
        WorkContract = 6
    }
}