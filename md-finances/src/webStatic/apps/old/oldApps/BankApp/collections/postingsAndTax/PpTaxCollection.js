/* eslint-disable */

import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { paymentOrderOperationResources } from '../../../../../../resources/MoneyOperationTypeResources';
import { isNotTaxableDocuments } from '../../../../../../helpers/newMoney/documentsHelper';

(function(bank, common) {
    const parent = common.Collections.TaxOperationCollection;

    bank.Collections.PostingsAndTax.PpTaxCollection = parent.extend({

        paymentDirection() {
            const taxPostingsDirection = common.Data.TaxPostingsDirection;

            if (this._isReceiptGoodsPaidCreditCard() && !this.isUsn6()) {
                return null;
            }

            if (this.sourceDocument.get('Direction') === Direction.Incoming) {
                return taxPostingsDirection.Incoming;
            }

            if (this._isBackToBuyer()) {
                return taxPostingsDirection.Incoming;
            }

            if (this.isOtherPayment() && this.isUsn() && this.isUsn6()) {
                return taxPostingsDirection.Outgoing;
            }

            if (!this.isOtherPayment() || this.isOsno()) {
                return taxPostingsDirection.Outgoing;
            }
        },

        isBudgetaryPayment() {
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            return operationType === paymentOrderOperationResources.BudgetaryPayment.value;
        },

        isOtherPayment() {
            const type = parseInt(this.sourceDocument.get('OperationType'), 10);
            return type === paymentOrderOperationResources.PaymentOrderOutgoingOther.value;
        },

        _isReceiptGoodsPaidCreditCard() {
            const type = parseInt(this.sourceDocument.get('OperationType'), 10);
            return type === paymentOrderOperationResources.MemorialWarrantReceiptGoodsPaidCreditCard.value;
        },

        settings() {
            const direction = this.sourceDocument.get('Direction');
            const isOutgoings = direction === Direction.Outgoing;

            if (isOutgoings && (this.isOtherPayment() || this._isBackToBuyer())) {
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

        names: {
            Operation: 'Type',
            Direction: 'Direction'
        },

        manualOperations: [
            paymentOrderOperationResources.PaymentOrderOutgoingOther.value,
            paymentOrderOperationResources.PaymentOrderIncomingOther.value
        ],

        sourceDocumentSumFieldName: 'Sum',

        listenSource() {
            const fields = [
                'Sum',
                'Date',
                'AcquiringCommission',
                'AcquiringCommissionDate',
                'TaxationSystemType',
                'Status',
                'OperationType',
                'LoanInterestSum',
                'SalaryWorkerId',
                'KontragentId',
                'SettlementAccountId',
                'KontragentAccountCode',
                'DocumentsSum',
                'Documents',
                'NdsSum',
                'PaidCardSum',
                'UnderContract',
                'KbkId',
                'ProvideInAccounting',
                'IsMediation',
                'MediationCommission',
                'MiddlemanContract'];

            this.listenPostingsFileds(fields);
            this.on('ModelLoaded', this.globalExplainingMessage, this);


            this.listenTo(this.sourceDocument, 'change:Sum', function() {
                this.isBudgetaryPayment() && this.updateFromSourceTaxPostings({ from: 'Sum', to: 'Outgoing' });
            });
            this.listenTo(this.sourceDocument, 'change:Description', function() {
                if (this.isBudgetaryPayment()) {
                    const { sourceDocument } = this;
                    const attrsChanged = Object.keys(sourceDocument.changed).length > 0;

                    this.updateFromSourceTaxPostings({
                        from: 'Description',
                        to: 'Destination'
                    });

                    if (!sourceDocument.get('Id') || attrsChanged) {
                        this.generatePostings();
                    } else {
                        this.loadData();
                    }
                }
            });
            this.on('add', function(model) {
                this.listenTo(model.get('ManualPostings'), 'add', this.updatePostingSum);
            });
        },

        updateFromSourceTaxPostings(map) {
            const bugetaryPaymentService = new Md.Services.BudgetaryPaymentService({ model: this.sourceDocument });
            if (!bugetaryPaymentService.isTradingFees()) {
                return;
            }

            const taxPosting = this.sourceDocument.get('TaxPostings');
            const self = this;
            if (taxPosting.each) {
                taxPosting.each((model) => {
                    self.syncWithSourceDocument(model, map);
                });
            }
        },

        updatePostingSum(model) {
            const bugetaryPaymentService = new Md.Services.BudgetaryPaymentService({ model: this.sourceDocument });
            if (!bugetaryPaymentService.isTradingFees()) {
                return;
            }

            this.syncWithSourceDocument(model, { from: 'Sum', to: 'Outgoing' });
            this.syncWithSourceDocument(model, { from: 'Description', to: 'Destination' });
        },

        syncWithSourceDocument(model, map) {
            const val = this.sourceDocument.get(map.from);
            model.set(map.to, val);
        },

        globalExplainingMessage() {
            const explainingMessage = this.ExplainingMessage;

            if (explainingMessage) {
                this.trigger('globalMessageChanged', explainingMessage);
            }
        },

        checkSourceData() {
            this.explainingMessage();
            return !this.notTaxable;
        },

        onlyEnvd() {
            const taxSystem = this.getTaxationSystem();
            if (hasSeveralTaxSystem(taxSystem)) {
                const taxSystemType = parseInt(this.sourceDocument.get('TaxationSystemType'), 10);
                return taxSystemType === common.Data.TaxationSystemType.Envd;
            }

            return taxSystem.isEnvd();
        },

        hasUsn6() {
            const taxSystem = this.getTaxationSystem();
            return taxSystem.get('IsUsn') && taxSystem.get('UsnSize') === 6;
        },

        getMemorialExplainingMessage() {
            if (this.onlyEnvd()) {
                this.notTaxable = true;
                return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableEnvd;
            }
            const model = this.sourceDocument;
            const type = parseInt(model.get('OperationType'), 10);

            if ((type === paymentOrderOperationResources.BankFee.value && !this.hasUsn6()) ||
                type === paymentOrderOperationResources.MemorialWarrantAccrualOfInterest.value) {
                return parent.prototype.explainingMessage.call(this);
            }

            const noAcquiringSums = !model.get('Sum') && !model.get('AcquiringCommission');

            if (type === paymentOrderOperationResources.MemorialWarrantReceiptGoodsPaidCreditCard.value && !this.hasUsn6() && (noAcquiringSums || model.get('AcquiringCommission'))) {
                return parent.prototype.explainingMessage.call(this);
            }

            this.notTaxable = true;
            return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxablePlainMessage;
        },

        getTaxSystem() {
            const documentDate = Converter.toDate(this.sourceDocument.get('Date'));
            const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            return ts.Current(documentDate);
        },

        checkDocStatus() {
            const isPaid = parseInt(this.sourceDocument.get('Status'), 10) === common.Data.DocumentStatuses.Payed;
            if (!isPaid) {
                this.notTaxable = true;
                return 'Не учитывается. Платежное поручение <a data-scroll-to-el=".status">не оплачено</a>.';
            }
        },

        getBudgetaryExplainingMessage() {
            this.notTaxable = false;

            const taxationSystem = this.getTaxSystem();
            const isUsn = taxationSystem.get('IsUsn');
            const isOsno = taxationSystem.get('IsOsno');

            const bugetaryPaymentService = new Md.Services.BudgetaryPaymentService({ model: this.sourceDocument });
            if (bugetaryPaymentService.isTradingFees() && (isUsn || isOsno)) {
                return this.checkDocStatus();
            }

            if (taxationSystem.get('IsEnvd') && !isUsn && !isOsno) {
                this.notTaxable = true;
                return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableEnvd;
            }

            if (isUsn) {
                if (taxationSystem.get('UsnSize') === 6) {
                    this.notTaxable = true;
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableUsn6;
                }

                const isNotTaxKbk = _.any(this.sourceDocument.get('NotTaxKbkIds'), (item) => {
                    return item == this.sourceDocument.get('KbkId');
                });

                if (isNotTaxKbk) {
                    this.notTaxable = true;
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.Usn15TaxInClosingMonth;
                }
            }

            if (isOsno) {
                this.notTaxable = true;
                return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableOsno;
            }

            return this.checkDocStatus();
        },

        hasPostings() {
            const model = this.at(0);
            let result = true;

            if (model) {
                result = model.get('HasPostings');
            }

            return result;
        },

        explainingMessage() {
            if (this.sourceDocument.isMemorial()) {
                return this.getMemorialExplainingMessage();
            }

            if (this.isBudgetaryPayment()) {
                if (this.notTaxable) {
                    this.each((model) => {
                        model.get('MainPostings').reset();
                        model.get('ManualPostings').reset();
                    });
                }

                return this.getBudgetaryExplainingMessage();
            }

            const notTaxableOsnoMsg = 'Не учитывается при расчёте налога.';
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            const msg = common.Mixin.PostingsAndTaxTools.explainingMessagesLib;
            const isBackToBuyer = this._isBackToBuyer();
            const notTaxableOperationsList = [
                paymentOrderOperationResources.PaymentOrderOutgoingProfitWithdrawing.value,
                paymentOrderOperationResources.PaymentOrderIncomingContributionOfOwnFunds.value,
                paymentOrderOperationResources.PaymentOrderIncomingLoanObtaining.value,
                paymentOrderOperationResources.PaymentOrderIncomingContributionAuthorizedCapital.value,
                paymentOrderOperationResources.PaymentOrderOutgoingPaymentAgencyContract.value
            ];

            this.notTaxable = false;

            if (_.contains(notTaxableOperationsList, operationType)) {
                this.notTaxable = true;
                return msg.notTaxablePlainMessage;
            }

            if (operationType === paymentOrderOperationResources.PaymentOrderOutgoingLoanRepayment.value) {
                if (!this.isUsn15() || !this.sourceDocument.get('LoanInterestSum')) {
                    this.notTaxable = true;
                    return msg.notTaxablePlainMessage;
                }
            }

            if (operationType === paymentOrderOperationResources.PaymentOrderIncomingReturnFromAccountablePerson.value && this.isUsn()) {
                this.notTaxable = true;
                return msg.notTaxablePlainMessage;
            }

            if (isBackToBuyer && !this.isUsn()) {
                this.notTaxable = true;
                return msg.notTaxablePlainMessage;
            }

            if (operationType === paymentOrderOperationResources.PaymentOrderIncomingMediationFee.value) {
                const sourceDocument = this.sourceDocument;
                const sum = sourceDocument.get('Sum') - this.sourceDocument.get('DocumentsSum');
                const middlemanContract = sourceDocument.get('MiddlemanContract');
                if (this.isOsno()) {
                    this.notTaxable = true;
                    return notTaxableOsnoMsg;
                }

                if (!sum || !middlemanContract) {
                    this.notTaxable = true;
                    return msg.notTaxablePlainMessage;
                }

                if (middlemanContract) {
                    if (!middlemanContract.get('ContractNumber')) {
                        this.notTaxable = true;
                        return msg.notTaxablePlainMessage;
                    }
                }
            }

            const underContract = parseInt(this.sourceDocument.get('UnderContract'), 10);
            if (this.isSalaryPayment() && underContract === Moedelo.Data.workerDocumentType.Dividends) {
                this.notTaxable = true;
                if (this.isEnvd()) {
                    return 'Не облагается налогом';
                }
                if (this.isOsno()) {
                    return msg.notTaxableOsnoDividends;
                }
                return msg.notTaxablePlainMessage;
            }
            if (this.isNotTaxableUsn6() && !this.isOtherPayment() && !isBackToBuyer) {
                this.notTaxable = true;
                return msg.notTaxableUsn6;
            }

            if (this.isNotTaxableOsno()) {
                this.notTaxable = true;
                const isIncoming = this.sourceDocument.get('Direction') == Direction.Incoming;
                return isIncoming ? notTaxableOsnoMsg : msg.notTaxableOsno;
            }

            if (!this.isEnvd() && this.isNotTaxable()) {
                this.notTaxable = true;
                return msg.notTaxable;
            }

            if (!this.isEnvd() && operationType === paymentOrderOperationResources.PaymentOrderIncomingFromPurse.value) {
                this.notTaxable = true;
                return msg.notTaxablePlainMessage;
            }

            if (this.isSalaryPaymentWithUsn15()) {
                this.reset();
                this.notTaxable = true;

                return msg.Usn15TaxInClosingMonth;
            }

            const message = parent.prototype.explainingMessage.call(this);

            if (message) {
                return message;
            }

            if (operationType === paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value && this.isUsn() && !this.isUsn6()) {
                const documents = this.sourceDocument.get('Documents').filter(doc => doc.Id > 0);

                if (!documents || !documents.length) {
                    this.notTaxable = true;
                    return 'Не учитывается. Добавьте документ.';
                }

                if (isNotTaxableDocuments(documents)) {
                    this.notTaxable = true;
                    return common.Mixin.PostingsAndTaxTools.explainingMessagesLib.ExpenseGoodsAtTheClosingMonthsIfConfirmed;
                }
            }
        },

        _isBackToBuyer() {
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            return operationType === paymentOrderOperationResources.PaymentOrderOutgoingReturnToBuyer.value;
        },

        isSalaryPaymentWithUsn15() {
            if (this.isSalaryPayment()) {
                return this.isUsn15();
            }

            return false;
        },

        isNotTaxableUsn6() {
            const direction = this.sourceDocument.get('Direction');

            if (direction == Direction.Incoming) {
                return false;
            }

            return this.isUsn() && this.isUsn6();
        },

        isNotTaxableOsno() {
            if (!this.isOsno()) {
                return false;
            }
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);

            return !(operationType === paymentOrderOperationResources.PaymentOrderOutgoingOther.value
                || operationType === paymentOrderOperationResources.PaymentOrderIncomingOther.value
                || operationType === paymentOrderOperationResources.PaymentOrderIncomingMaterialAid.value);
        },

        isOsno() {
            const taxSystem = this.getTaxationSystem();
            return taxSystem ? taxSystem.get('IsOsno') : false;
        },

        isUsn() {
            const taxSystem = this.getTaxationSystem();
            return taxSystem ? taxSystem.get('IsUsn') : false;
        },

        isSalaryPayment() {
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            return operationType === paymentOrderOperationResources.PaymentOrderOutgoingForTransferSalary.value;
        },

        isNotTaxable() {
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            return operationType === paymentOrderOperationResources.PaymentOrderOutgoingTransferToAccount.value
                || operationType === paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value;
        },

        validator: _.extend(parent.prototype.validator, {
            moreThanOperationSum: common.Mixin.FunctionForPostingsAndTaxValidation.moreThanDocumentSum
        }),

        operationsValidationRules() {
            return {
                SumValidation: {
                    moreThanOperationSum: { msg: 'Сумма проводок не может быть больше общей суммы операции' }
                }
            };
        },

        operationsExplainigObjects(operationType, cid) {
            const isOsno = this.isOsno();
            const isUsn = this.isUsn();
            const isUsn6 = this.isUsn6();
            const isEnvd = this.isEnvd();
            let explainingMessage;
            let notTaxable;

            switch (operationType) {
                case paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value:
                    if (isOsno) {
                        notTaxable = true;
                        explainingMessage = common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxable;
                    }
                    break;

                case paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value:
                case paymentOrderOperationResources.PaymentOrderIncomingReturnFromAccountablePerson.value:
                    break;
                case paymentOrderOperationResources.PaymentOrderOutgoingTransferToAccount.value:
                    notTaxable = true;
                    explainingMessage = common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxable;
                    break;
                case paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value:
                    if (this.ifOnlyOneReasonDocWithProduct(cid)) {
                        explainingMessage = common.Mixin.PostingsAndTaxTools.explainingMessagesLib.ExpenseGoodsAtTheClosingMonthsIfConfirmed;
                    }
                    break;
                case paymentOrderOperationResources.PaymentOrderOutgoingForTransferSalary.value:
                case paymentOrderOperationResources.PaymentToAccountablePerson.value:
                    break;
            }

            if (isEnvd && !isOsno && !isUsn) {
                notTaxable = true;
                explainingMessage = common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxable;
            }

            if (this.sourceDocument.get('Direction') == Direction.Outgoing && !isEnvd) {
                if (isUsn && isUsn6) {
                    notTaxable = true;
                    explainingMessage = common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableUsn6;
                }

                if (isOsno && operationType != paymentOrderOperationResources.PaymentOrderOutgoingOther.value) {
                    notTaxable = true;
                    explainingMessage = common.Mixin.PostingsAndTaxTools.explainingMessagesLib.notTaxableOsno;
                }
            }

            // необходимые поля для операций
            if (this.setOperationExplainMessageWithAnchor(operationType, cid) && !notTaxable) {
                explainingMessage = this.setOperationExplainMessageWithAnchor(operationType, cid);
            }

            if (explainingMessage) {
                return {
                    ExplainingMessage: explainingMessage
                };
            }
        },

        requiredFields() {
            const self = this;
            const direction = parseInt(self.sourceDocument.get('Direction'), 10);
            const isOutgoing = direction === Direction.Outgoing;
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            let required = {
                Status: {
                    fullName: 'Не учитывается. Платежное поручение <a data-scroll-to-el=".status">не оплачено</a>.',
                    otherCondition() {
                        return !(isOutgoing
                            && !self.sourceDocument.isMemorial()
                            && self.sourceDocument.get('Status') == common.Data.DocumentStatuses.NotPayed);
                    }
                },
                Sum: {
                    name: 'сумму',
                    selector: '[data-bind=Sum]',
                    fieldName: ['Sum']
                }
            };

            if (operationType === paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value ||
                this.isSalaryPayment() ||
                operationType === paymentOrderOperationResources.PaymentOrderOutgoingLoanRepayment.value ||
                operationType === paymentOrderOperationResources.PaymentOrderIncomingMaterialAid.value ||
                operationType === paymentOrderOperationResources.PaymentOrderOutgoingReturnToBuyer.value ||
                operationType === paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value) {
                required.Kontragent = {
                    name() {
                        return isOutgoing ? 'получателя' : 'плательщика';
                    },
                    selector: '[data-bind=KontragentName]',
                    fieldName: [this.isSalaryPayment() ? 'SalaryWorkerId' : 'Kontragent.KontragentName']
                };
            }

            if (operationType === paymentOrderOperationResources.MemorialWarrantReceiptGoodsPaidCreditCard.value) {
                const commission = self.sourceDocument.get('AcquiringCommission');
                const sum = self.sourceDocument.get('Sum');
                if (!sum && commission) {
                    required = _.omit(required, 'Sum');

                    required.AcquiringCommission = {
                        name: 'комиссию',
                        selector: '[data-bind=AcquiringCommission]',
                        fieldName: ['AcquiringCommission']
                    };
                } else if (!sum && !commission) {
                    required = _.omit(required, 'Sum');

                    required.Sum = {
                        name: 'сумму или комиссию',
                        selector: '[data-bind=Sum]',
                        fieldName: ['Sum']
                    };
                }
            }

            return required;
        },

        ifOnlyOneReasonDocWithProduct(cid) {
            const currentModel = _.find(this.models, (localModel) => {
                return localModel.get('Cid') == cid;
            });
            const currentOperation = this.sourceDocument.get('Operations').get(cid);
            const resonCount = currentOperation.get('Documents').length;

            if (currentModel
                && !currentModel.get('MainPostings').length
                && !currentModel.get('LinkedDocuments').length
                && !currentModel.get('ManualPostings').length
                && resonCount == 1) {
                return true;
            }
        },

        canBeManual() {
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            let result = true;

            if (operationType === paymentOrderOperationResources.PaymentOrderIncomingMaterialAid.value && !this.hasPostings()) {
                result = false;
            }

            return result;
        },

        getLoadUrl() {
            const documentBaseId = this.sourceDocument.get('BaseDocumentId') || this.sourceDocument.get('DocumentBaseId');

            return `/Finances/Money/Operations/${documentBaseId}/TaxPostings`;
        },

        getGenerateUrl() {
            return '/Accounting/PaymentOrders/GetAllTaxPostings';
        }
    });

    function onlyNegative(val) {
        if (Converter.toFloat(val) > 0) {
            return { message: 'Укажите отрицательный доход' };
        }

        return { valid: true };
    }

    function hasSeveralTaxSystem(taxSystem) {
        return taxSystem.isEnvd() && (taxSystem.isUsn() || taxSystem.isOsno());
    }
}(Bank, Common));
