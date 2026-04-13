using Moedelo.Common.Enums.Enums.Money;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Finances.Business.Services.Money.Operations.Duplicates
{
    internal static class IncomingOperationsDuplicateDetector
    {
        public static DuplicateDetectionResult Detect(IReadOnlyCollection<OperationDuplicateForBatchCheck> sourceOperations, OperationForDuplicateDetection targetOperation)
        {
            var strictDuplicates = new List<OperationDuplicateForBatchCheck>();
            var notStrictDuplicates = new List<OperationDuplicateForBatchCheck>();

            foreach (var sourceOperation in sourceOperations.Where(x => x.Direction == MoneyDirection.Incoming))
            {
                if (sourceOperation.Sum != targetOperation.Sum ||
                    sourceOperation.Date.Date != targetOperation.Date.Date)
                {
                    continue;
                }

                var isEqualContractors = OperationsDuplicatesComparer.IsContractorEqual(sourceOperation, targetOperation);

                if (OperationsDuplicatesComparer.IsDescriptionEqual(sourceOperation.Description, targetOperation.Description))
                {
                    if (isEqualContractors && OperationsDuplicatesComparer.IsNumberEqual(sourceOperation.Number, targetOperation.Number))
                    {
                        strictDuplicates.Add(sourceOperation);
                        continue;
                    }
                }

                // Поле Назначение незаполнено, добавляем на повторную проверку
                if (string.IsNullOrWhiteSpace(sourceOperation.Description) || string.IsNullOrWhiteSpace(targetOperation.Description))
                {
                    notStrictDuplicates.Add(sourceOperation);
                }
            }

            return strictDuplicates.Count > 0
                ? FindInStrictDuplicate(strictDuplicates, targetOperation)
                : FindInNotStrictDuplicate(notStrictDuplicates, targetOperation);
        }

        private static DuplicateDetectionResult FindInStrictDuplicate(IReadOnlyList<OperationDuplicateForBatchCheck> strictDuplicates, OperationForDuplicateDetection targetOperation)
        {
            if (strictDuplicates.Count == 1)
            {
                return new DuplicateDetectionResult
                {
                    Guid = targetOperation.Guid,
                    SourceId = strictDuplicates[0].Id,
                    SourceBaseId = strictDuplicates[0].DocumentBaseId,
                    IsStrict = true
                };
            }

            var sourceOperation = strictDuplicates.FirstOrDefault(x => 
                OperationsDuplicatesComparer.IsNumberEqual(x.Number, targetOperation.Number));
            if (sourceOperation != null)
            {
                return new DuplicateDetectionResult
                {
                    Guid = targetOperation.Guid,
                    SourceId = sourceOperation.Id,
                    SourceBaseId = sourceOperation.DocumentBaseId,
                    IsStrict = true
                };
            }

            return DuplicateDetectionResult.NotFound(targetOperation.Guid);
        }

        private static DuplicateDetectionResult FindInNotStrictDuplicate(IReadOnlyList<OperationDuplicateForBatchCheck> notStrictDuplicates, OperationForDuplicateDetection targetOperation)
        {
            // Не найдено, потому что отсутвует Назначение в операции, ищем по номеру
            var sourceOperation = notStrictDuplicates
                .FirstOrDefault(x => OperationsDuplicatesComparer.IsContractorEqual(x, targetOperation) &&
                                     OperationsDuplicatesComparer.IsNumberEqual(x.Number, targetOperation.Number));
            if (sourceOperation != null)
            {
                return new DuplicateDetectionResult
                {
                    Guid = targetOperation.Guid,
                    SourceId = sourceOperation.Id,
                    SourceBaseId = sourceOperation.DocumentBaseId,
                    IsStrict = false
                };
            }

            return DuplicateDetectionResult.NotFound(targetOperation.Guid);
        }
    }
}