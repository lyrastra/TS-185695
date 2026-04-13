using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto.PaymentTransactions;

public class GetTransactionsCriteriaDto
{
    /// <summary>
    /// Список идентификаторов и типов транзакций
    /// </summary>
    public IReadOnlyCollection<PaymentTransactionIdAndTypeDto> IdAndTypes { get; set; }

    /// <summary>
    /// Список идентификаторов платежей
    /// </summary>
    public IReadOnlyCollection<int> PaymentHistoryIds { get; set; }

    // начало параметров для NotLinked транзакций
    /// <summary>
    /// Получать только не связанные с платежами транзакции
    /// </summary>
    public bool NotLinked { get; set; }

    /// <summary>
    /// Транзакции для аутсорсинга
    /// </summary>
    public bool IsOutsource { get; set; }

    /// <summary>
    /// Фильтр для поиска транзакций
    /// </summary>
    public string Query { get; set; }
    // конец параметров для NotMapped транзакций
}