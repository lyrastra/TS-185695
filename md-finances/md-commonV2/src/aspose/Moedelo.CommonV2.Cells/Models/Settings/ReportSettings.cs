using System;
using System.Collections.Generic;

namespace Moedelo.CommonV2.Cells.Models.Settings
{
    public class ReportSettings
    {
        public static ReportSettings Default => new ReportSettings
        {
            DateFormat = "dd.MM.yyyy",
            TimeFormat = "HH.mm.ss",
            MoneyFormat = "# ##0",
            RealFormat = "#,##0.00",
            ReplaceIntZeroToEmpty = false,
            ReplaceDecimalZeroToEmpty = false,
            ReplaceRealZeroToEmpty = false,
            Epsilon = 0.00f
        };

        public static ReportSettings WithReplaceZeroToEmpty => new ReportSettings
        {
            DateFormat = "dd.MM.yyyy",
            TimeFormat = "HH.mm.ss",
            MoneyFormat = "# ##0",
            RealFormat = "#,##0.00",
            ReplaceIntZeroToEmpty = true,
            ReplaceDecimalZeroToEmpty = true,
            ReplaceRealZeroToEmpty = true,
            Epsilon = 0.00f
        };

        public string DateFormat { get; set; }

        public string TimeFormat { get; set; }

        public string MoneyFormat { get; set; }

        public string RealFormat { get; set; }

        public bool ReplaceIntZeroToEmpty { get; set; }

        public bool ReplaceDecimalZeroToEmpty { get; set; }

        public bool ReplaceRealZeroToEmpty { get; set; }

        public double Epsilon { get; set; }

        [Obsolete("Не использовать в новых отчетах. Удаление столбцов/строк делать средствами Aspose.Cells")]
        public List<RangeSettings> RangesSettings { get; set; } = new List<RangeSettings>();

        public string EmptyString { get; set; } = string.Empty;
    }
}