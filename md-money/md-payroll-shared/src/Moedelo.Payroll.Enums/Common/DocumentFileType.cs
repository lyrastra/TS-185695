using System.ComponentModel;

namespace Moedelo.Payroll.Shared.Enums.Common;

public enum DocumentFileType
{
    [Description("pdf")] Pdf,
    [Description("xls")] Xls,
    [Description("xlsx")] Xlsx,
}