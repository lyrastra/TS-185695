using System;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.KontragentDocuments.ReconciliationStatements
{
    /// <summary>
    /// Данные о договоре по которому делается акт сверки
    /// </summary>
    public sealed class ContractDto
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int KontragentId { get; set; }

        public long DocumentBaseId { get; set; }

        public bool IsMainContract { get; set; }
    }
}