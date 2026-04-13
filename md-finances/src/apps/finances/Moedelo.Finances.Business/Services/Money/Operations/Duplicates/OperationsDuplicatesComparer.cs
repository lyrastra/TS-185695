using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using System;

namespace Moedelo.Finances.Business.Services.Money.Operations.Duplicates
{
    static class OperationsDuplicatesComparer
    {
        public static bool IsNumberEqual(string sourceOperationNumber, string targetOperationNumber)
        {
            return sourceOperationNumber == targetOperationNumber;
        }

        public static bool IsContractorEqual(OperationDuplicateForBatchCheck sourceOperation, OperationForDuplicateDetection targetOperation)
        {
            if (HasContractorSettlementAccount(sourceOperation, targetOperation) &&
                sourceOperation.ContractorSettlementAccount == targetOperation.ContractorSettlementAccount)
            {
                return true;
            }
            if (HasContractorInn(sourceOperation, targetOperation) &&
                sourceOperation.ContractorInn == targetOperation.ContractorInn)
            {
                return true;
            }
            return !HasContractorSettlementAccount(sourceOperation, targetOperation) &&
                   !HasContractorInn(sourceOperation, targetOperation);
        }

        private static bool HasContractorSettlementAccount(OperationDuplicateForBatchCheck sourceOperation, OperationForDuplicateDetection targetOperation)
        {
            return !string.IsNullOrWhiteSpace(sourceOperation.ContractorSettlementAccount) &&
                   !string.IsNullOrWhiteSpace(targetOperation.ContractorSettlementAccount);
        }

        private static bool HasContractorInn(OperationDuplicateForBatchCheck sourceOperation, OperationForDuplicateDetection targetOperation)
        {
            return !string.IsNullOrWhiteSpace(sourceOperation.ContractorInn) &&
                   !string.IsNullOrWhiteSpace(targetOperation.ContractorInn);
        }

        public static bool IsDescriptionEqual(string source, string target)
        {
            const int maxTruncateLenght = 210; // максимальная длина описания, которая разрешена при отправке пп/п в банк
            const int minTruncateLenght = 180; // минимальная длина описания, до который можно обрезать строку при сравнении

            // только если заполнены оба назначения
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target))
            {
                return false;
            }
            
            //вырезаем точки.
            var trimChar = '.';
            var sourceWithoutDots = source.Replace(trimChar.ToString(), String.Empty);
            var targetWithoutDots = target.Replace(trimChar.ToString(), String.Empty);
            

            // вырезаем whitespace символы, так как банк может их изменять (менять 2 пробела на один и т.п.)
            var sourceWithoutWhitespace = RemoveWhitespace(sourceWithoutDots);
            var targetWithoutWhitespace = RemoveWhitespace(targetWithoutDots);

            // если одна из двух строк короче максимальной длинны, но обе длиннее максимальной, то обрезаем по минимальной длинне
            var truncateLength = maxTruncateLenght;
            if ((sourceWithoutWhitespace.Length < maxTruncateLenght || targetWithoutWhitespace.Length < maxTruncateLenght) &&
                sourceWithoutWhitespace.Length >= minTruncateLenght && targetWithoutWhitespace.Length >= minTruncateLenght)
            {
                truncateLength = minTruncateLenght;
            }

            // обрезаем строки по заданной длинне
            var truncatedSource = Truncate(sourceWithoutWhitespace, truncateLength);
            var truncatedTarget = Truncate(targetWithoutWhitespace, truncateLength);

            return truncatedSource.ToLower() == truncatedTarget.ToLower();
        }

        private static string RemoveWhitespace(string source)
        {
            return string.Join(string.Empty, source.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        private static string Truncate(string source, int length)
        {
            return source.Length > length
                ? source.Substring(0, length)
                : source;
        }
    }
}