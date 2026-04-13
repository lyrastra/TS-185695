using System;
using Aspose.Cells;
using Moedelo.CommonV2.Cells.Models.Settings;

namespace Moedelo.CommonV2.Cells.Helpers
{
    internal static class CellValueHelper
    {
        public static void PutValue(Cell cell, object value, ReportSettings settings)
        {
            if (value == null)
            {
                cell.PutValue(settings.EmptyString);
                return;
            }

            var style = cell.GetStyle();

            if (value is int)
            {
                var valueAsInt = (int) value;

                if (settings.ReplaceIntZeroToEmpty && valueAsInt == 0)
                {
                    cell.PutValue(settings.EmptyString);
                }
                else
                {
                    cell.PutValue(valueAsInt);
                }
                
                style.Number = 1;
            }

            else if (value is decimal)
            {
                var valueAsDecimal = (decimal) value;

                if (settings.ReplaceDecimalZeroToEmpty && valueAsDecimal == 0)
                {
                    cell.PutValue(settings.EmptyString);
                }
                else
                {
                    cell.PutValue(valueAsDecimal);
                }

                style.Custom = settings.MoneyFormat;
            }

            else if (value is double)
            {
                var valueAsDouble = (double) value;

                if (settings.ReplaceRealZeroToEmpty && Math.Abs(valueAsDouble) < settings.Epsilon)
                {
                    cell.PutValue(string.Empty);
                }
                else
                {
                    cell.PutValue(valueAsDouble);
                }

                style.Custom = settings.RealFormat;
            }

            else if (value is DateTime)
            {
                var valueAsDateTime = (DateTime?) value;
                cell.PutValue(valueAsDateTime);

                style.Custom = valueAsDateTime.Value.Date == valueAsDateTime.Value
                    ? settings.DateFormat
                    : $"{settings.DateFormat} {settings.TimeFormat}";
            }

            else
            {
                cell.PutValue(value?.ToString() ?? string.Empty);
            }

            cell.SetStyle(style);
        }
    }
}