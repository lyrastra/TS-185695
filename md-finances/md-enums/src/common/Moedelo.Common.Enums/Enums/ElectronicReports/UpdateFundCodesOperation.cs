using System;

namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum UpdateFundCodesOperation
    {
        ForAll,
        ByFirmIds
    }

    public static class UpdateFundCodesOperationExtensions
    {
        public static string ConvertToString(this UpdateFundCodesOperation type)
        {
            switch (type)
            {
                case UpdateFundCodesOperation.ForAll:
                    return "Обновить для всех";
                case UpdateFundCodesOperation.ByFirmIds:
                    return "Обновить выбранным фирмам по FirmId";
                default:
                    throw new ArgumentOutOfRangeException($"Argument UpdateFundCodesOperation {type} is out of range");
            }
        }

        public static UpdateFundCodesOperation ConvertToUpdateFundCodesOperation(this string value)
        {
            switch (value)
            {
                case "Обновить для всех":
                    return UpdateFundCodesOperation.ForAll;
                case "Обновить выбранным фирмам по FirmId":
                    return UpdateFundCodesOperation.ByFirmIds;
                default:
                    throw new ArgumentOutOfRangeException($"Cann't convert string {value} to UpdateFundCodesOperation");
            }
        }
    }

}
