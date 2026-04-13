using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments.Commands
{
    /// <summary>
    /// Устанавливает статус НУ базового документа
    /// </summary>
    public class SetTaxStatusCommand
    {
        public long Id { get; set; }

        public TaxPostingStatus? TaxStatus { get; set; }
    }
}
