using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;

namespace Moedelo.Money.ChangeLog.Mappers
{
    static class AdvanceStatementMapper
    {
        public static string MapToName(this AdvanceStatementDto advanceStatement)
        {
            return advanceStatement != null
                ? $"Авансовый отчет №{advanceStatement.Number} от {advanceStatement.Date:dd.MM.yyyy}"
                : null;
        }

        public static string MapToName(
            BaseDocumentDto documentDto)
        {
            return documentDto != null
                ? $"Авансовый отчет №{documentDto.Number} от {documentDto.Date:dd.MM.yyyy}"
                : null;
        }
    }
}
