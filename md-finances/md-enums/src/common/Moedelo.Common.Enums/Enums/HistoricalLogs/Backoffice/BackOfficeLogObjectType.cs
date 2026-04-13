using System.ComponentModel.DataAnnotations;

namespace Moedelo.Common.Enums.Enums.HistoricalLogs.Backoffice;

public enum BackOfficeLogObjectType
{
    [Display(Name = "")]
    Empty = -1,
    [Display(Name = "Клиент")]
    User = 0,
    [Display(Name = "Фирма")]
    Firm = 1,
    [Display(Name = "Партнёр")]
    Partner = 2,
    [Display(Name = "Платёж")]
    Payment = 3,
}
