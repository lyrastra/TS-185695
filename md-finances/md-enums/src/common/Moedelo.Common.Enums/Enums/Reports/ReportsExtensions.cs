using System;

namespace Moedelo.Common.Enums.Enums.Reports
{
    public static class ReportsExtensions
    {
        public static string OutsourcerGroupAsString(this OutsourcerGroup? groupToConvert)
        {
            if (!groupToConvert.HasValue || groupToConvert == OutsourcerGroup.AllGroups)
            {
                return "Все группы";
            }

            switch (groupToConvert)
            {
                case OutsourcerGroup.WithoutGroup:
                    return "Без группы";
                case OutsourcerGroup.Group1:
                    return "Группа №1";
                case OutsourcerGroup.Group2:
                    return "Группа №2";
                case OutsourcerGroup.Group3:
                    return "Группа №3";
                case OutsourcerGroup.Group4:
                    return "Группа №4";
                case OutsourcerGroup.Group5:
                    return "Группа №5";
                case OutsourcerGroup.Agent:
                    return "Агент";
                case OutsourcerGroup.Vip:
                    return "ВИП";
                default:
                    throw new ArgumentOutOfRangeException(nameof(groupToConvert), groupToConvert, null);
            }
        }
    }
}