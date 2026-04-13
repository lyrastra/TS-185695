/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import DocumentType from '@moedelo/frontend-enums/mdEnums/DocumentType';
import { cashOrderOperationResources } from '../../../../../../resources/MoneyOperationTypeResources';
import BudgetaryAccountTypeEnum from '../../../../../../enums/newMoney/budgetaryPayment/BudgetaryAccountTypeEnum';

(function(cash, cashEnums, common) {
    const parent = common.Collections.TaxOperationCollection;
    cash.Collections.PostingsAndTax.CashTaxCollection = parent.extend({
        url: WebApp.CashOrder.GetAllTaxPostings,

        names: {
            Operation: 'OperationType',
            Direction: 'DirectionType'
        },

        isOtherPayment() {
            const type = parseInt(this.sourceDocument.get('OperationType'), 10);
            return type === cashOrderOperationResources.CashOrderOutgoingOther.value;
        },

        settings() {
            const direction = this.sourceDocument.get('Direction');

            if (direction === Direction.Outgoing && (this.isOtherPayment() || this._isBackToBuyer())) {
                return {
                    incoming: {
                        allowNegative: true,
                        validation: onlyNegative
                    }
                };
            }
        },

        initialize() {
            parent.prototype.initialize.call(this);
            _.extend(this, common.Mixin.AddOperationsValidation);
        },

        getSourceData() {
            const data = parent.prototype.getSourceData.call(this);

            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            if (operationType === cashOrderOperationResources.CashOrderBudgetaryPayment.value) {
                // BP-5158 В произвольную проводку автоматически подставились данные при определенных условиях
                // Видимо нужно править на сервере, но пусть пока будет здесь
                data.BaseDocumentId = null;
            }

            return data;
        },

        sourceDocumentSumFieldName: 'Sum',

        listenSource() {
            const fields = [
                'WorkerId',
                'Sum',
                'LoanInterestSum',
                'NdsSum',
                'PaidCardSum',
                'KontragentId',
                'Date',
                'OperationType',
                'Documents',
                'TaxationSystemType',
                'PayToWorkers',
                'WorkerDocumentType',
                'IsMediation',
                'ProvideInAccounting',
                'MediationCommission',
                'MediationNdsSum',
                'MiddlemanContract',
                'KbkId',
                'Destination',
                'MyReward'
            ];

            this.listenPostingsFileds(fields);

            this.on('ModelLoaded', this.globalExplainingMessage, this);
        },

        globalExplainingMessage() {
            const explainingMessage = this.ExplainingMessage;

            if (explainingMessage) {
                this.trigger('globalMessageChanged', explainingMessage);
            }
        },

        validator: _.extend(parent.prototype.validator, {
            moreThanOperationSum: common.Mixin.FunctionForPostingsAndTaxValidation.moreThanDocumentSum
        }),

        operationsValidationRules() {
            return {
                SumValidation: {
                    moreThanOperationSum: { msg: 'Сумма проводок не может быть больше суммы ордера' }
                }
            };
        },

        checkSourceData() {
            this.explainingMessage();

            const required = _.result(this, 'requiredFields');
            return !this.notTaxable && this.checkRequiredFields(required);
        },

        explainingMessage() {
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            const notTaxableMsg = 'Не учитывается при расчёте налога.';

            const plainNotTaxableList = [
                cashOrderOperationResources.CashOrderIncomingLoanObtaining.value,
                cashOrderOperationResources.CashOrderOutgoingProfitWithdrawing.value,
                cashOrderOperationResources.CashOrderIncomingContributionAuthorizedCapital.value,
                cashOrderOperationResources.CashOrderIncomingFromAnotherCash.value,
                cashOrderOperationResources.CashOrderOutgoingPaymentAgencyContract.value,
                cashOrderOperationResources.CashOrderIncomingContributionOfOwnFunds.value
            ];

            this.notTaxable = false;
            const isIpOsno = this.isOsno() && !this.isOoo;

            if (isIpOsno) {
                const taxableOperationList = [
                    cashOrderOperationResources.CashOrderOutgoingPaymentSuppliersForGoods.value,
                    cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value,
                    cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value,
                    cashOrderOperationResources.CashOrderOutgoingLoanRepayment.value,
                    cashOrderOperationResources.CashOrderOutgoingReturnToBuyer.value,
                    cashOrderOperationResources.CashOrderOutgoingIssuanceAccountablePerson.value,
                    cashOrderOperationResources.CashOrderBudgetaryPayment.value
                ];

                /**
                 * Возможно дергание нескольких эндпойнтов
                 * http://localhost:8090/Finances/Money/Operations/181768/TaxPostings
                 * http://localhost:8080/Accounting/FirmCash/GetAllTaxPostings
                 * Выводится сообщение того, что последним отработал (воспроизводится нестабильно)
                 * Пример бага OSNO-4404
                 */
                if (taxableOperationList.includes(operationType)) {
                    return parent.prototype.explainingMessage.call(this);
                }
            }

            if (_.contains(plainNotTaxableList, operationType)) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            if (
                this.isUsn() &&
                operationType === cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value &&
                this.sourceDocument.get('IsMediation') &&
                !this.sourceDocument.get('MediationCommission')
            ) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            if (operationType === cashOrderOperationResources.CashOrderOutgoingReturnToBuyer.value && this.isOsno()) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            if (operationType === cashOrderOperationResources.CashOrderOutgoingLoanRepayment.value) {
                if (!this.isUsn15() || !this.sourceDocument.get('LoanInterestSum')) {
                    this.notTaxable = true;
                    return notTaxableMsg;
                }
            }

            if (operationType === cashOrderOperationResources.CashOrderIncomingFromSettlementAccount.value) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            if (operationType === cashOrderOperationResources.CashOrderIncomingMediationFee.value) {
                const docSum = new Backbone.Collection(this.sourceDocument.get('Documents')).sum();
                const isNullSum = this.sourceDocument.get('Sum') <= docSum;

                this.notTaxable = !this.isUsn15() || isNullSum;
                if (this.isOsno()) {
                    return notTaxableMsg;
                }

                if (this.isUsn15() && isNullSum) {
                    return notTaxableMsg;
                }
            }

            const dividendsMessage = this.getDividendsMessage();
            if (dividendsMessage.length > 0) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            const isBackToBuyer = this._isBackToBuyer();
            if (this.isNotTaxableUsn6() && !this.isOtherPayment() && !isBackToBuyer) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            if (this.isPatent() && operationType === cashOrderOperationResources.CashOrderBudgetaryPayment.value) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            if (this.isOsno()) {
                const isOutgoing = this.sourceDocument.get('Direction') === Direction.Outgoing;
                const taxableOutgoingOperations = [
                    cashOrderOperationResources.CashOrderOutgoingOther.value,
                    cashOrderOperationResources.CashOrderOutgoingReturnToBuyer.value
                ];

                if (isOutgoing && !_.contains(taxableOutgoingOperations, operationType)) {
                    this.notTaxable = true;
                    return notTaxableMsg;
                }

                if (operationType === cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value) {
                  this.notTaxable = true;
                  return notTaxableMsg;
                }

                const taxableIncomingOperations = [
                    cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value,
                    cashOrderOperationResources.CashOrderIncomingOther.value,
                    cashOrderOperationResources.CashOrderIncomingMaterialAid.value
                ];

                if (!isOutgoing && !_.contains(taxableIncomingOperations, operationType)) {
                    this.notTaxable = true;
                    return notTaxableMsg;
                }
            }

            const message = parent.prototype.explainingMessage.call(this);
            if (message) {
                return message;
            }

            if (this.isNotTaxable()) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            if (this.isSalaryPaymentWithEnvd()) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            if (this.isSalaryPaymentWithUsn15() || this.isSalaryPaymentWithUsnAndEnvd15()) {
                this.notTaxable = true;
                return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.Usn15TaxInClosingMonth;
            }

            if (operationType === cashOrderOperationResources.CashOrderOutgoingPaymentSuppliersForGoods.value
                && this.isUsn() && !this.isUsn6() && !this.hasDocuments()) {
                    return 'Не учитывается. Добавьте документ.';
            }

            if (operationType === cashOrderOperationResources.CashOrderOutgoingPaymentSuppliersForGoods.value
                && this.isUsn() && !this.isUsn6() && this.hasDocuments()) {

                const docs = this.sourceDocument.get('Documents');
                if(docs?.filter(f => f.DocumentType  === DocumentType.ReceiptStatement).length > 0) {
                    this.notTaxable = true;
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.Usn15ReceiptStatementTaxInClosingMonth;
                }
            }

            if (this.isSalaryPaymentWithUsnAndEnvd()) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            if (this.isNotTaxEnvd()) {
                this.notTaxable = true;
                return notTaxableMsg;
            }

            if (operationType === cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value) {
                const myRewardSum = parseInt(this.sourceDocument.get('MyReward'), 10);

                if (!myRewardSum) {
                    this.notTaxable = true;
                    return notTaxableMsg;
                }

                if (this.isOsno()) {
                    this.notTaxable = true;
                    return notTaxableMsg;
                }
            }
        },

        _isBackToBuyer() {
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);

            return operationType === cashOrderOperationResources.CashOrderOutgoingReturnToBuyer.value;
        },

        hasDocuments(){
           return this.sourceDocument.get('Documents') && this.sourceDocument.get('Documents').length
        },

        isNotTaxEnvd() {
            const taxSystem = this.getTaxationSystem();
            return taxSystem ? taxSystem.get('IsEnvd') && !taxSystem.get('IsUsn') && !taxSystem.get('IsOsno') && !this.isPatent() : false;
        },

        isSalaryPaymentWithUsn15() {
            if (parseInt(this.sourceDocument.get('OperationType'),10) === cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value) {
                return this.isUsn15();
            }

            return false;
        },

        isSalaryPaymentWithUsnAndEnvd15() {
            if (parseInt(this.sourceDocument.get('OperationType'),10) === cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value) {
                return this.isUsnAndEnvd15();
            }

            return false;
        },

        isSalaryPaymentWithEnvd() {
            if (parseInt(this.sourceDocument.get('OperationType'),10) === cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value) {
                return _.every(this.sourceDocument.get('PayToWorkers'), (payment) => {
                    return parseInt(payment.TaxationSystemType, 10) === common.Data.TaxationSystemType.Envd;
                });
            }

            return false;
        },

        isSalaryPaymentWithUsnAndEnvd() {
            if (parseInt(this.sourceDocument.get('OperationType'), 10) === cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value) {
                return _.every(this.sourceDocument.get('PayToWorkers'), (payment) => {
                    return parseInt(payment.TaxationSystemType, 10) === common.Data.TaxationSystemType.UsnAndEnvd;
                });
            }

            return false;
        },

        isNotTaxableUsn6() {
            const isOutgoing = this.sourceDocument.get('Direction') === Direction.Outgoing;

            return this.isUsn() && this.isUsn6() && isOutgoing;
        },

        getDividendsMessage() {
            let operationType = this.sourceDocument.get('OperationType'),
                workerDocumentType = this.sourceDocument.get('WorkerDocumentType');

            if (operationType === cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value
                && workerDocumentType === cash.Data.workerDocumentType.Dividend) {
                if (this.isOsno()) {
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableOsnoDividends;
                }

                if (this.isUsn()) {
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxablePlainMessage;
                }

                return 'Не учитывается при расчёте налога.';
            }

            return '';
        },

        isNotTaxable() {
            const operationType = this.sourceDocument.get('OperationType');

            const notTaxableOperations = [
                cashOrderOperationResources.CashOrderIncomingFromSettlementAccount.value,
                cashOrderOperationResources.CashOrderIncomingReturnFromAccountablePerson.value,
                cashOrderOperationResources.CashOrderOutcomingToSettlementAccount.value,
                cashOrderOperationResources.CashOrderOutgoingTranslatedToOtherCash.value,
                cashOrderOperationResources.CashOrderOutgoingCollectionOfMoney.value
            ];

            return _.contains(notTaxableOperations, operationType);
        },

        requiredFields() {
            const operationType = this.sourceDocument.get('OperationType');
            let fields = {
                Sum: {
                    name: 'сумму',
                    selector: '[data-bind=Sum]',
                    fieldName: ['Sum']
                }
            };

            switch (operationType) {
                case cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value:
                case cashOrderOperationResources.CashOrderIncomingMaterialAid.value:
                    fields.Kontragent = {
                        name: 'плательщика',
                        selector: '[data-bind=KontragentName]',
                        fieldName: ['KontragentId']
                    };
                    break;
                case cashOrderOperationResources.CashOrderOutgoingPaymentSuppliersForGoods.value:
                    fields.Documents = {
                        fullName: 'Не учитывается. Добавьте документ',
                        selector: '.linkedDocuments',
                        fieldName: ['Documents']
                    };
                    fields.Kontragent = {
                        name: 'получателя',
                        selector: '[data-bind=KontragentName]',
                        fieldName: ['KontragentId']
                    };
                    break;
                case cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value:
                    fields = {};
                    break;
                case cashOrderOperationResources.CashOrderOutgoingLoanRepayment.value:
                    fields.LoanInterestSum = {
                        fullName: `Не учитывается при расчете налога`,
                        selector: `[data-bind="LoanInterestSum"]`,
                        fieldName: [`LoanInterestSum`]
                    }
                    break;
            }

            if (operationType === cashOrderOperationResources.CashOrderIncomingMediationFee.value) {
                fields.MiddlemanContract = {
                    name: 'посреднический договор',
                    selector: '[data-bind=ContractNumber]',
                    fieldName: 'MiddlemanContract.Id'
                };
            }

            if (operationType === cashOrderOperationResources.CashOrderOutgoingLoanRepayment.value
                || operationType === cashOrderOperationResources.CashOrderOutgoingReturnToBuyer.value
            ) {
                fields.Kontragent = {
                    name: 'получателя',
                    selector: '[data-bind=KontragentName]',
                    fieldName: ['KontragentId']
                };
            }

            return fields;
        },

        paymentDirection() {
            const direction = this.sourceDocument.get('Direction');
            const taxPostingsDirection = common.Data.TaxPostingsDirection;

            if (direction === Direction.Incoming) {
                return taxPostingsDirection.Incoming;
            }

            if (this.isOtherPayment() && this.isUsn() && this.isUsn6()) {
                return taxPostingsDirection.Outgoing;
            }

            if (this._isBackToBuyer()) {
                return taxPostingsDirection.Incoming;
            }

            if (!this.isOtherPayment() || this.isOsno()) {
                return taxPostingsDirection.Outgoing;
            }
        },

        /**
         * Влияет не только на возможность переключать ручные/не ручные, но и на показ проводок
         * Использовать с осторожностью
         */
        canBeManual() {
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            if (operationType === cashOrderOperationResources.CashOrderIncomingMaterialAid.value && !this.hasPostings()) {
                return false;
            }

            const taxOperation = this.at(0);
            if (operationType === cashOrderOperationResources.CashOrderBudgetaryPayment.value && !(!taxOperation || taxOperation.get('HasPostings') || taxOperation.get('IsManualEdit'))) {
                return false;
            }

            return true;
        },

        hasPostings() {
            const model = this.at(0);
            if (model) {
                return model.get('HasPostings');
            }

            return true;
        },

        operationsExplainigObjects(operationType, cid) {
        },

        getLoadUrl() {
            const documentBaseId = this.sourceDocument.get('BaseDocumentId') || this.sourceDocument.get('DocumentBaseId');

            return `/Finances/Money/Operations/${documentBaseId}/TaxPostings`;
        },

        getGenerateUrl() {
            return '/Accounting/FirmCash/GetAllTaxPostings';
        }

    });

    function onlyNegative(val) {
        if (Converter.toFloat(val) > 0) {
            return { message: 'Укажите отрицательный доход' };
        }

        return { valid: true };
    }
}(Cash, CashEnums, Common));
