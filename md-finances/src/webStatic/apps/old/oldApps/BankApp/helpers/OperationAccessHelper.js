/* eslint-disable func-names */
/* global $, Bank,  */

import { paymentOrderOperationResources } from '../../../../../resources/MoneyOperationTypeResources';

(function(bank) {
    // Проверяет доступность типа операции в зависимости от состояния модели
    // eslint-disable-next-line no-param-reassign
    bank.Helpers.OperationAccessHelper = {

        getAvailableOperaions(model, operations, existingOperations) {
            const self = this;
            const result = [];

            $.each(operations, function() {
                if (self.checkAccess(model, this.OperationType, existingOperations)) {
                    result.push(this.OperationType);
                }
            });

            return result;
        },

        checkAccess(model, operation, existingOperations) {
            if (this.accessCheckers[operation]) {
                return this.accessCheckers[operation](model, existingOperations);
            }

            return this.defaultAccessChecker(model);
        },

        // Проверка по умолчанию - заполнено поле "Контрагент"
        defaultAccessChecker(model) {
            return model.get(`KontragentId`) || model.get(`SalaryWorkerId`) || model.get(`IsNewKontragent`);
        },

        accessCheckers: { }
    };
    // Проверки для каждого типа операции
    const checkers = bank.Helpers.OperationAccessHelper.accessCheckers;

    /** Поступление с другого расчетного счета */
    checkers[paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value] = function() {
        return false;
    };

    //     Возврат от подотчетного лица
    checkers[paymentOrderOperationResources.PaymentOrderIncomingReturnFromAccountablePerson.value] = function(model) {
        return model.get(`SalaryWorkerId`);
    };

    checkers[paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value] = function(model) {
        return model.get(`KontragentId`) || model.get(`IsNewKontragent`);
    };

    //    Прочий платеж
    checkers[paymentOrderOperationResources.PaymentOrderOutgoingOther.value] = function(model) {
        return model.get(`KontragentId`) || model.get(`IsNewKontragent`);
    };

    //    Платеж поставщику за товар/материал/работу/услуги
    checkers[paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value] = function(model) {
        return model.get(`KontragentId`) || model.get(`IsNewKontragent`);
    };

    checkers[paymentOrderOperationResources.PaymentOrderOutgoingForTransferSalary.value] = function(model) {
        return model.get(`SalaryWorkerId`);
    };

    checkers[paymentOrderOperationResources.PaymentOrderOutgoingTransferToAccount.value] = function(model, operationsList) {
        let result = true;

        if (operationsList.length) {
            $.each(operationsList, (ind, val) => {
                if (parseInt(val.model.get(`Type`), 10) === paymentOrderOperationResources.PaymentOrderOutgoingTransferToAccount.value) {
                    result = false;
                }
            });
        }

        return result && !model.get(`SalaryWorkerId`) && model.get(`KontragentName`) && !model.get(`KontragentId`) && !model.get(`IsNewKontragent`);
    };

    checkers[paymentOrderOperationResources.BankFee.value] = function() { return true; };

    checkers[paymentOrderOperationResources.MemorialWarrantCreditingCollectedFunds.value] = function() { return true; };

    checkers[paymentOrderOperationResources.MemorialWarrantReceiptFromCash.value] = function() { return true; };

    checkers[paymentOrderOperationResources.MemorialWarrantReceiptGoodsPaidCreditCard.value] = function() { return true; };

    checkers[paymentOrderOperationResources.WithdrawalFromAccount.value] = function() { return true; };
}(Bank));
