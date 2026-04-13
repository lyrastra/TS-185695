using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Payment;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Newtonsoft.Json;

namespace Moedelo.Finances.Business.Services.Money.Operations
{
    [InjectAsSingleton]
    public class MoneyOperationsDuplicatesQualifier : IMoneyOperationDuplicatesReader
    {
        private const string Tag = nameof(MoneyOperationsDuplicatesQualifier);
        private const string RegistryPartOfDescription = "реестр";

        private readonly IFinancialOperationsDuplicatesReader financialOperationsDuplicatesReader;
        private readonly IPaymentOrdersDuplicatesReader paymentOrdersDuplicatesReader;
        private readonly ILogger logger;

        public MoneyOperationsDuplicatesQualifier(
            IFinancialOperationsDuplicatesReader financialOperationsDuplicatesReader,
            IPaymentOrdersDuplicatesReader paymentOrdersDuplicatesReader,
            ILogger logger)
        {
            this.financialOperationsDuplicatesReader = financialOperationsDuplicatesReader;
            this.paymentOrdersDuplicatesReader = paymentOrdersDuplicatesReader;
            this.logger = logger;
        }

        public async Task<int?> GetRoboAndSapeIncomingOperationIdAsync(IUserContext userContext, DuplicateOperationRequest request)
        {
            var isAccounting = await userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff).ConfigureAwait(false);
            if (isAccounting)
            {
                throw new NotImplementedException();
            }

            request.FirmId = userContext.FirmId;
            return await financialOperationsDuplicatesReader.GetRoboAndSapeIncomingOperationIdAsync(request).ConfigureAwait(false);
        }

        public async Task<int?> GetRoboAndSapeOutgoingOperationIdAsync(IUserContext userContext, DuplicateOperationRequest request)
        {
            var isAccounting = await userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff).ConfigureAwait(false);
            if (isAccounting)
            {
                throw new NotImplementedException();
            }

            request.FirmId = userContext.FirmId;
            return await financialOperationsDuplicatesReader.GetRoboAndSapeOutgoingOperationIdAsync(request).ConfigureAwait(false);
        }

        public async Task<int?> GetYandexIncomingOperationIdAsync(IUserContext userContext, DuplicateOperationRequest request)
        {
            var isAccounting = await userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff).ConfigureAwait(false);
            if (isAccounting)
            {
                throw new NotImplementedException();
            }

            request.FirmId = userContext.FirmId;
            return await financialOperationsDuplicatesReader.GetYandexIncomingOperationIdAsync(request).ConfigureAwait(false);
        }

        public async Task<int?> GetYandexOutgoingOperationIdAsync(IUserContext userContext, DuplicateOperationRequest request)
        {
            var isAccounting = await userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff).ConfigureAwait(false);
            if (isAccounting)
            {
                throw new NotImplementedException();
            }

            request.FirmId = userContext.FirmId;
            return await financialOperationsDuplicatesReader.GetYandexOutgoingOperationIdAsync(request).ConfigureAwait(false);
        }

        public async Task<int?> GetYandexMovementOperationIdAsync(IUserContext userContext, DuplicateOperationRequest request)
        {
            var isAccounting = await userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff).ConfigureAwait(false);
            if (isAccounting)
            {
                throw new NotImplementedException();
            }

            request.FirmId = userContext.FirmId;
            return await financialOperationsDuplicatesReader.GetYandexMovementOperationIdAsync(request).ConfigureAwait(false);
        }

        // Новый метод поиска дубликатов Исходящие
        public async Task<DuplicateResult> GetOutgoingOperationIdExtAsync(IUserContext userContext, DuplicateOperationRequest request)
        {
            request.FirmId = userContext.FirmId;

            var isAccounting = await userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff).ConfigureAwait(false);
            var payments = isAccounting
                ? await paymentOrdersDuplicatesReader.GetAllPaymentOrdersAsync(request, MoneyDirection.Outgoing).ConfigureAwait(false)
                : await financialOperationsDuplicatesReader.GetAllOperationsDuplicateAsync(request, FinancialOperationDirection.Outgoing).ConfigureAwait(false);


            if (payments == null || payments.Count == 0)
            {
                return new DuplicateResult();
            }

            logger.Info(Tag, $"Payments like: {JsonConvert.SerializeObject(payments, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}", userContext.GetAuditContext());

            var notStrictDuplicates = new List<OperationDuplicate>();
            foreach (var payment in payments.OrderBy(x => x.Id))
            {
                // сравниваем р/с Получателя
                if (!string.IsNullOrEmpty(payment.RecipientSettlementNumber) &&
                    !string.IsNullOrEmpty(request.ContractorSettlementAccount) ||
                    !string.IsNullOrEmpty(payment.RecipientInn) &&
                        !string.IsNullOrEmpty(request.ContractorInn)
                        && !payment.IsSalaryOperation)
                {
                    payment.IdenticalKontragent = request.ContractorSettlementAccount == payment.RecipientSettlementNumber ||
                        request.ContractorInn == payment.RecipientInn;
                }

                // заплатка для Зарплатного проекта (не участвуют номер и описание)
                if (payment.IsSalaryOperation &&
                    payment.IdenticalKontragent &&
                    !string.IsNullOrEmpty(payment.Description) &&
                    !string.IsNullOrEmpty(request.DestinationDescription) &&
                    payment.Description.ToLower().Contains(RegistryPartOfDescription) &&
                    request.DestinationDescription.ToLower().Contains(RegistryPartOfDescription) &&
                    payment.Direction == MoneyDirection.Outgoing)
                {
                    return new DuplicateResult
                    {
                        Id = payment.Id,
                        BaseId = payment.DocumentBaseId,
                        IsStrict = true
                    };
                }

                if (IsDestinationDescriptionEqual(payment.Description, request.DestinationDescription))
                {
                    var isNumbersEqual = request.PaymentOrderNumber == payment.Number;

                    if (payment.IdenticalKontragent && isNumbersEqual)
                    {
                        return new DuplicateResult
                        {
                            Id = payment.Id,
                            BaseId = payment.DocumentBaseId,
                            IsStrict = true
                        };
                    }
                    continue;
                }

                // Поле Назначение незаполнено, добавляем на повторную проверку
                if (string.IsNullOrWhiteSpace(payment.Description) || string.IsNullOrWhiteSpace(request.DestinationDescription))
                {
                    notStrictDuplicates.Add(payment);
                }
            }

            // 2-й проход: не найдено, потому что отсутвует Назначение в операции, ищем по номеру
            if (notStrictDuplicates.Count > 0)
            {
                var payment = notStrictDuplicates.FirstOrDefault(x =>
                    x.Number == request.PaymentOrderNumber && x.IdenticalKontragent);
                if (payment != null)
                {
                    return new DuplicateResult
                    {
                        Id = payment.Id,
                        BaseId = payment.DocumentBaseId,
                        IsStrict = false
                    };
                }
            }

            return new DuplicateResult();
        }

        // Новый метод поиска дубликатов Входящие
        public async Task<DuplicateResult> GetIncomingOperationIdExtAsync(IUserContext userContext,
            DuplicateOperationRequest request)
        {
            request.FirmId = userContext.FirmId;

            var isAccounting = await userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff).ConfigureAwait(false);
            var payments = isAccounting
                ? await paymentOrdersDuplicatesReader.GetAllPaymentOrdersAsync(request, MoneyDirection.Incoming).ConfigureAwait(false)
                : await financialOperationsDuplicatesReader.GetAllOperationsDuplicateAsync(request, FinancialOperationDirection.Incoming).ConfigureAwait(false);

            if (payments == null || payments.Count == 0)
            {
                return new DuplicateResult();
            }

            var strictDuplicates = new List<OperationDuplicate>();
            var notStrictDuplicates = new List<OperationDuplicate>();

            foreach (var payment in payments)
            {
                // Сравниваем р/с Плательщика
                if (isAccounting)
                {
                    if ((!string.IsNullOrEmpty(payment.PayerSettlementNumber) &&
                        !string.IsNullOrEmpty(request.ContractorSettlementAccount) ||
                        !string.IsNullOrEmpty(payment.PayerInn) &&
                        !string.IsNullOrEmpty(request.ContractorInn)) &&
                        !payment.IsSalaryOperation)
                    {
                        payment.IdenticalKontragent = payment.PayerSettlementNumber == request.ContractorSettlementAccount ||
                            payment.PayerInn == request.ContractorInn;
                    }
                }
                else
                {
                    if ((!string.IsNullOrEmpty(payment.RecipientSettlementNumber) && 
                        !string.IsNullOrEmpty(request.ContractorSettlementAccount) ||
                        !string.IsNullOrEmpty(payment.RecipientInn) &&
                        !string.IsNullOrEmpty(request.ContractorInn)) &&
                        !payment.IsSalaryOperation)
                    {
                        payment.IdenticalKontragent = payment.RecipientSettlementNumber == request.ContractorSettlementAccount ||
                            payment.RecipientInn == request.ContractorInn;
                    }
                }


                if (IsDestinationDescriptionEqual(payment.Description, request.DestinationDescription))
                {
                    if (payment.IdenticalKontragent && request.PaymentOrderNumber == payment.Number)
                    {
                        strictDuplicates.Add(payment);
                        continue;
                    }
                }

                // Поле Назначение незаполнено, добавляем на повторную проверку
                if (string.IsNullOrWhiteSpace(payment.Description) || string.IsNullOrWhiteSpace(request.DestinationDescription))
                {
                    notStrictDuplicates.Add(payment);
                }
            }

            return strictDuplicates.Count > 0
                ? FindInStrictDuplicate(strictDuplicates, request.PaymentOrderNumber)
                : FindInNotStrictDuplicate(notStrictDuplicates, request.PaymentOrderNumber);
        }

        private static DuplicateResult FindInStrictDuplicate(IReadOnlyList<OperationDuplicate> strictDuplicates, string paymentOrderNumber)
        {
            if (strictDuplicates.Count == 1)
            {
                return new DuplicateResult
                {
                    Id = strictDuplicates[0].Id,
                    BaseId = strictDuplicates[0].DocumentBaseId,
                    IsStrict = true
                };
            }

            var paymentsStep2 = strictDuplicates.Where(x => x.Number == paymentOrderNumber).ToList();
            var payment = paymentsStep2.FirstOrDefault();
            if (payment != null)
            {
                return new DuplicateResult
                {
                    Id = payment.Id,
                    BaseId = payment.DocumentBaseId,
                    IsStrict = true
                };
            }

            return new DuplicateResult();
        }

        private static DuplicateResult FindInNotStrictDuplicate(IReadOnlyList<OperationDuplicate> notStrictDuplicates, string paymentOrderNumber)
        {
            //Не найдено, потому что отсутвует Назначение в операции, ищем по номеру
            var paymentsStep3 = notStrictDuplicates.Where(x => x.Number == paymentOrderNumber && x.IdenticalKontragent);
            var paymentOrderOperations = paymentsStep3.ToList();
            var payment = paymentOrderOperations.FirstOrDefault();
            if (payment != null)
            {
                return new DuplicateResult
                {
                    Id = payment.Id,
                    BaseId = payment.DocumentBaseId,
                    IsStrict = false
                };
            }
            return new DuplicateResult();
        }

        public static string RemoveWhitespace(string source)
        {
            return string.Join(string.Empty, source.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        public static string Truncate(string source, int length)
        {
            return source.Length > length
                ? source.Substring(0, length)
                : source;
        }

        private static bool IsDestinationDescriptionEqual(string source, string target)
        {
            const int maxTruncateLenght = 210; // максимальная длина описания, которая разрешена при отправке пп/п в банк
            const int minTruncateLenght = 180; // минимальная длина описания, до который можно обрезать строку при сравнении

            // только если заполнены оба назначения
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target))
            {
                return false;
            }

            // вырезаем whitespace символы, так как банк может их изменять (менять 2 пробела на один и т.п.)
            var sourceWithoutWhitespace = RemoveWhitespace(source);
            var targetWithoutWhitespace = RemoveWhitespace(target);

            // если одна из двух строк короче максимальной длинны, но обе длиннее максимальной, то обрезаем по минимальной длинне
            var truncateLength = maxTruncateLenght;
            if ((sourceWithoutWhitespace.Length < maxTruncateLenght || targetWithoutWhitespace.Length < maxTruncateLenght) &&
                (sourceWithoutWhitespace.Length >= minTruncateLenght && targetWithoutWhitespace.Length >= minTruncateLenght))
            {
                truncateLength = minTruncateLenght;
            }

            // обрезаем строки по заданной длинне
            var truncatedSource = Truncate(sourceWithoutWhitespace, truncateLength);
            var truncatedTarget = Truncate(targetWithoutWhitespace, truncateLength);

            return truncatedSource.ToLower() == truncatedTarget.ToLower();
        }
    }
}