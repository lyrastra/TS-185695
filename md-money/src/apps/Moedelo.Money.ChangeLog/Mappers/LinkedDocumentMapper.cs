using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.Common;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.ChangeLog.Mappers
{
    static class LinkedDocumentMapper
    {
        public static LinkedDocumentInfo MapToDefinitionState(
            this DocumentLink link, BaseDocumentDto documentDto)
        {
            return new()
            {
                Name = documentDto?.MapToName() ?? link.MapToTechnicalName(),
                LinkSum = MoneySum.InRubles(link.LinkSum)
            };
        }

        public static LinkedDocumentInfo MapToDefinitionState(
            this DocumentLink link,
            BaseDocumentDto documentDto,
            string currency)
        {
            return new()
            {
                Name = documentDto?.MapToName() ?? link.MapToTechnicalName(),
                LinkSum = new MoneySum(link.LinkSum, currency)
            };
        }

        public static LinkedDocumentInfo MapToDefinitionState(
            this BillLink link, BaseDocumentDto documentDto)
        {
            return new()
            {
                Name = documentDto?.MapToName() ?? link.MapToTechnicalName(),
                LinkSum = MoneySum.InRubles(link.LinkSum)
            };
        }

        public static LinkedDocumentInfo MapToDefinitionState(
            this LinkWithDocumentDto linkDto, long documentBaseId)
        {
            return new()
            {
                Name = linkDto?.Document?.MapToName() ?? MapToTechnicalName(documentBaseId),
                LinkSum = linkDto != null ? MoneySum.InRubles(linkDto.Sum) : null
            };
        }

        public static string MapToName(this BaseDocumentDto docDto)
        {
            if (docDto == null)
            {
                return null;
            }

            return $"{docDto.Type.MapToName()} №{docDto.Number} от {docDto.Date:dd.MM.yyyy}";
        }

        private static string MapToTechnicalName(this DocumentLink link)
        {
            return $"Неизвестный документ #{link.DocumentBaseId} (тех.идентификатор)";
        }

        private static string MapToTechnicalName(this BillLink link)
        {
            return $"Счёт #{link.BillBaseId} (тех.идентификатор)";
        }

        private static string MapToTechnicalName(long documentbaseId)
        {
            return $"Неизвестный документ #{documentbaseId} (тех.идентификатор)";
        }
    }
}
