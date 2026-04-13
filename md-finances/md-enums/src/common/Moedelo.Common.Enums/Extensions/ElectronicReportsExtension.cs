using Moedelo.Common.Enums.Enums.ElectronicReports;
using System;

namespace Moedelo.Common.Enums.Extensions
{
    public static class ElectronicReportsExtension
    {
        public static string ConvertToString(this OutgoingDirectionType type)
        {
            switch (type)
            {
                case OutgoingDirectionType.PensionFund:
                    return "Пенсионный фонд (ПФР)";
                case OutgoingDirectionType.StatisticalService:
                    return "Федеральная служба статистики (РОССТАТ)";
                case OutgoingDirectionType.SocialInsuranceFund:
                    return "Фонд социального страхования (ФСС)";
                case OutgoingDirectionType.TaxService:
                    return "Федеральная налоговая служба (ФНС + ЭДО)";
                case OutgoingDirectionType.EdmTaxService:
                    return "Электронный документооборот для ФНС (ЭДО ФНС)";
                default:
                    throw new ArgumentOutOfRangeException($"Argument OutgoingDirectionType {type} is out of range");
            }
        }

        public static OutgoingDirectionType ConvertToOutgoingDirectionType(this string value)
        {
            switch (value)
            {
                case "Пенсионный фонд (ПФР)":
                    return OutgoingDirectionType.PensionFund;
                case "Федеральная служба статистики (РОССТАТ)":
                    return OutgoingDirectionType.StatisticalService;
                case "Фонд социального страхования (ФСС)":
                    return OutgoingDirectionType.SocialInsuranceFund;
                case "Федеральная налоговая служба (ФНС + ЭДО)":
                    return OutgoingDirectionType.TaxService;
                case "Электронный документооборот для ФНС (ЭДО ФНС)":
                    return OutgoingDirectionType.EdmTaxService;
                default:
                    throw new ArgumentOutOfRangeException($"Cann't convert string {value} to OutgoingDirectionType");
            }
        }
    }
}