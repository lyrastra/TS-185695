using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.Money.Import.Domain.Models.PurseOperation;

public class ImportStatus
{
    public object ExData { get; set; }

    public PaymentImportResultStatus Status { get; set; }
}