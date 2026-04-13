using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentBaseValidator))]
    internal class BudgetaryPaymentBaseValidator : IBudgetaryPaymentBaseValidator
    {
        public Task ValidateAsync(BudgetaryPaymentBase paymentBase, BudgetaryPeriod period)
        {
            if (paymentBase == BudgetaryPaymentBase.Tr && period.Type != BudgetaryPeriodType.Date)
            {
                throw new BusinessValidationException("Period.Type", $"Тип периода {(int)period.Type} не соответствует основанию платежа {paymentBase.ToText()}. Ожидается {(int)BudgetaryPeriodType.Date}");
            }
            return Task.CompletedTask;
        }

        public Task ValidateDocumentDateAsync(BudgetaryPaymentSaveRequest request)
        {
            if (ContainsCode(request.DocumentNumber))
            {
                var dateIsValid = IsValidDocumentDate(request.DocumentDate);
                if (!dateIsValid)
                {
                    var typeStr = request.DocumentNumber.Substring(0, 2);
                    throw new BusinessValidationException("Document Date", $"Для платежей с типом {typeStr} необходимо выбрать дату");
                }
            }
            return Task.CompletedTask;
        }

        public Task ValidateReasonAsync(BudgetaryPaymentSaveRequest request)
        {
            if (request.PaymentBase == BudgetaryPaymentBase.FreeDebtRepayment)
            {
                var clearDocNumber = GetClearDocNumber(request.DocumentNumber);
                var codeIsContain = ContainsCode(request.DocumentNumber);
                var documentNumberExists = !string.IsNullOrWhiteSpace(clearDocNumber);
                var isValidDate = IsValidDocumentDate(request.DocumentDate);
                _ = (codeIsContain, documentNumberExists, isValidDate) switch
                {
                    (true, true, false) =>
                        throw new BusinessValidationException("Document Date", "Укажите дату документа"),
                    (true, false, false) =>
                        throw new BusinessValidationException("Document Date", "Укажите дату и номер документа"),
                    (true, false, true) =>
                        throw new BusinessValidationException("Document Number", "Укажите номер документа"),
                    (false, false, true) =>
                        throw new BusinessValidationException("Document Number", "Укажите номер документа"),
                    _ => Task.CompletedTask
                };
                if (ContaindNotValidCode(request.DocumentNumber))
                {
                    throw new BusinessValidationException("Document number", $"Код платеже не можеть быть {request.DocumentNumber.Substring(0, 2)}");
                }
            }
            return Task.CompletedTask;
        }

        private bool IsValidDocumentDate(string date)
        {
            return DateTime.TryParse(date, out DateTime dateTime);
        }

        private bool ContainsCode(string docNumber)
        {
            return Regex.IsMatch(docNumber, @"^[ТР]|[ПР]|[АП]|[АР]");
        }
        
        private static string GetClearDocNumber(string fullNumber)
        {
            return Regex.Replace(fullNumber, @"^[ТР]|[ПР]|[АП]|[АР]", string.Empty);
        }

        private static bool ContaindNotValidCode(string docNumber)
        {
            var code = docNumber.Length >= 2 ? docNumber.Substring(0, 2) : docNumber;
            var codeEnum = BudgetaryPaymentBaseExtensions.ToEnum(code);
            var result = codeEnum != BudgetaryPaymentBase.None && !(codeEnum == BudgetaryPaymentBase.Tr || codeEnum == BudgetaryPaymentBase.Pr ||
                codeEnum == BudgetaryPaymentBase.Ap || codeEnum == BudgetaryPaymentBase.Ar || codeEnum == BudgetaryPaymentBase.Other);
            return result;
        }
    }
}