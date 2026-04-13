using Moedelo.Common.Enums.Enums.Money;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Finances.Business.Services.Money.Operations.Duplicates
{
    internal static class OutgoingOperationsDuplicateDetector
    {
        private const string RegistryPartOfDescription = "реестр";
        private const string BudgetaryPaymentDescription = "единый налоговый платеж";

        public static DuplicateDetectionResult Detect(IReadOnlyCollection<OperationDuplicateForBatchCheck> sourceOperations, OperationForDuplicateDetection targetOperation)
        {
            var notStrictDuplicates = new List<OperationDuplicateForBatchCheck>();

            foreach (var sourceOperation in sourceOperations.Where(x => x.Direction == MoneyDirection.Outgoing))
            {
                var isContractorEqual = OperationsDuplicatesComparer.IsContractorEqual(sourceOperation, targetOperation);
                var isNumberEqual = OperationsDuplicatesComparer.IsNumberEqual(sourceOperation.Number, targetOperation.Number);
                var isDateEqual = sourceOperation.Date.Date == targetOperation.Date.Date;
                var isDescriptionEqual = OperationsDuplicatesComparer.IsDescriptionEqual(sourceOperation.Description, targetOperation.Description);
                
                if (IsOperationTypeEqual(OperationType.PaymentOrderOutgoingForTransferSalary, sourceOperation, targetOperation) &&
                    isDateEqual &&
                    isNumberEqual &&
                    isContractorEqual)
                {
                    return new DuplicateDetectionResult
                    {
                        Guid = targetOperation.Guid,
                        SourceId = sourceOperation.Id,
                        SourceBaseId = sourceOperation.DocumentBaseId,
                        IsStrict = true
                    };
                }

                if (IsOperationTypeEqual(OperationType.PaymentOrderOutgoingIssuanceAccountablePerson, sourceOperation, targetOperation) &&
                    isDescriptionEqual &&
                    isDateEqual &&
                    isNumberEqual &&
                    sourceOperation.Sum == targetOperation.Sum)
                {
                    return new DuplicateDetectionResult
                    {
                        Guid = targetOperation.Guid,
                        SourceId = sourceOperation.Id,
                        SourceBaseId = sourceOperation.DocumentBaseId,
                        IsStrict = true
                    };
                }

                if (sourceOperation.IsSalaryOperation &&
                    IsDescriptionEqualForSalaryProject(sourceOperation.Description, targetOperation.Description) && 
                    (sourceOperation.Sum != targetOperation.Sum ||
                     sourceOperation.Date.Date.AddDays(-5) > targetOperation.Date.Date ||
                     sourceOperation.Date.Date.AddDays(5) < targetOperation.Date.Date))
                {
                    continue;
                }
                
                if (sourceOperation.Sum != targetOperation.Sum ||
                    sourceOperation.Date.Date.AddDays(-15) > targetOperation.Date.Date ||
                    sourceOperation.Date.Date.AddDays(15) < targetOperation.Date.Date)
                {
                    continue;
                }

                // Для операций по снятию прибыли всегда в контрагенте подставляется сам ИП. Решено не смотреть на контрагента TS-120292
                var isContractorEqualAndIsProfitWithdrawingOperation = sourceOperation.IsProfitWithdrawingOperation || isContractorEqual;

                // заплатка для Зарплатного проекта (не участвуют номер и описание)
                if (sourceOperation.IsSalaryOperation &&
                    isContractorEqualAndIsProfitWithdrawingOperation &&
                    IsDescriptionEqualForSalaryProject(sourceOperation.Description, targetOperation.Description))
                {
                    return new DuplicateDetectionResult
                    {
                        Guid = targetOperation.Guid,
                        SourceId = sourceOperation.Id,
                        SourceBaseId = sourceOperation.DocumentBaseId,
                        IsStrict = true
                    };
                }

                if (IsDescriptionEqualForBudgetaryPayment(sourceOperation.Description, targetOperation.Description) || 
                    isDescriptionEqual)
                {
                    if (isContractorEqualAndIsProfitWithdrawingOperation && isNumberEqual)
                    {
                        return new DuplicateDetectionResult
                        {
                            Guid = targetOperation.Guid,
                            SourceId = sourceOperation.Id,
                            SourceBaseId = sourceOperation.DocumentBaseId,
                            IsStrict = true
                        };
                    }
                    continue;
                }

                // Поле Назначение незаполнено, добавляем на повторную проверку
                if (string.IsNullOrWhiteSpace(sourceOperation.Description) ||
                    string.IsNullOrWhiteSpace(targetOperation.Description))
                {
                    notStrictDuplicates.Add(sourceOperation);
                }
            }

            // 2-й проход: не найдено, потому что отсутвует Назначение в операции, ищем по номеру
            if (notStrictDuplicates.Count > 0)
            {
                var payment = notStrictDuplicates.FirstOrDefault(x =>
                    OperationsDuplicatesComparer.IsNumberEqual(x.Number, targetOperation.Number) &&
                    OperationsDuplicatesComparer.IsContractorEqual(x, targetOperation));
                if (payment != null)
                {
                    return new DuplicateDetectionResult
                    {
                        Guid = targetOperation.Guid,
                        SourceId = payment.Id,
                        SourceBaseId = payment.DocumentBaseId,
                        IsStrict = false
                    };
                }
            }

            return DuplicateDetectionResult.NotFound(targetOperation.Guid);
        }

        private static bool IsOperationTypeEqual(OperationType operationType,
            OperationDuplicateForBatchCheck sourceOperation, 
            OperationForDuplicateDetection targetOperation)
        {
            return targetOperation.OperationType == operationType && sourceOperation.OperationType == operationType;
        }

        private static bool IsDescriptionEqualForSalaryProject(string source, string target)
        {
            return !string.IsNullOrWhiteSpace(source) &&
                !string.IsNullOrWhiteSpace(target) &&
                source.ToLower().Contains(RegistryPartOfDescription) &&
                target.ToLower().Contains(RegistryPartOfDescription);
        }
        
        private static bool IsDescriptionEqualForBudgetaryPayment(string source, string target)
        {
            return !string.IsNullOrWhiteSpace(source) &&
                   !string.IsNullOrWhiteSpace(target) &&
                   source.ToLower().Contains(BudgetaryPaymentDescription) &&
                   target.ToLower().Contains(BudgetaryPaymentDescription);
        }
    }
}