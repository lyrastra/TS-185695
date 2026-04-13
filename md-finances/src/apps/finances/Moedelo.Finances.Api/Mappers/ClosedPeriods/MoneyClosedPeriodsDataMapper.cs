using System.Collections.Generic;
using System.Linq;
using Moedelo.Finances.Domain.Models.ClosedPeriods;
using Moedelo.Finances.Dto.ClosedPeriods;

namespace Moedelo.Finances.Api.Mappers.ClosedPeriods
{
    public static class MoneyClosedPeriodsDataMapper
    {
        public static List<MoneyDocumentsCountDto> Map(IReadOnlyCollection<MoneyDocumentsCount> models)
        {
            return models.Select(Map).ToList();
        }

        private static MoneyDocumentsCountDto Map(MoneyDocumentsCount model)
        {
            return new MoneyDocumentsCountDto
            {
                Type = model.Type,
                DocumentType = model.DocumentType,
                Direction = model.Direction,
                Count = model.Count
            };
        }
        
        public static List<MoneyDocumentsCountDto> Map(IReadOnlyCollection<MoneyDocument> models)
        {
            var groupedDocuments = models.GroupBy(m => new { m.Type, m.Direction, m.DocumentType}).ToDictionary(g=>g.Key);
            return groupedDocuments.Select(dg => new MoneyDocumentsCountDto
            {
                Count = groupedDocuments[dg.Key].Count(),
                Direction = dg.Key.Direction,
                Type = dg.Key.Type,
                DocumentType = dg.Key.DocumentType,
                Documents = groupedDocuments[dg.Key].Select(Map).ToList()
            }).ToList();
        }

        private static MoneyDocumentDto Map(MoneyDocument document)
        {
            return new MoneyDocumentDto
            {
                Direction = document.Direction,
                Type = document.Type,
                DocumentType = document.DocumentType,
                Date = document.Date,
                Number = document.Number,
                Sum = document.Sum,
                DocumentBaseId = document.DocumentBaseId
            };
        }
    }
}