/* eslint-disable */
/* global Bank, Common, WebApp, _ */

import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import isPostingsOfTypeOtherForIp from '../../../../../../helpers/postingsForIpHelper';
import { paymentOrderOperationResources } from '../../../../../../resources/MoneyOperationTypeResources';
import SyntheticAccountCodesEnum from '../../../../../../enums/SyntheticAccountCodesEnum';

(function(bank, common) {
    const parent = common.Collections.BuhOperationCollection;

    bank.Collections.PostingsAndTax.PPostingCollection = parent.extend({
        url: WebApp.AccountingPaymentOrder.GetAllPostings,

        onlyOneManualPosting: true,

        names: {
            Operation: 'Type',
            Direction: 'Direction'
        },

        listenSource() {
            const fields = [
                'Sum',
                'Date',
                'Status',
                'OperationType',
                'KontragentId',
                'AcquiringCommission',
                'AcquiringCommissionDate',
                'TaxationSystemType',
                'SalaryWorkerId',
                'IsLongTermLoan',
                'LoanInterestSum',
                'SettlementAccountId',
                'KontragentAccountCode',
                'DocumentSum',
                'NdsSum',
                'ContractBaseId',
                'UnderContract',
                'MiddlemanContract',
                'BudgetaryTaxesAndFees',
                'KbkId',
                'ProvideInAccounting',
                'Documents',
                'KBK',
                'TransferSettlementaccountId',
                'Period'];

            this.listenPostingsFileds(fields);
        },

        manualOperations: [
            paymentOrderOperationResources.PaymentOrderOutgoingOther.value,
            paymentOrderOperationResources.PaymentOrderIncomingOther.value
        ],

        checkSourceData() {
            this.explainingMessage();

            return !this.notTaxable;
        },

        explainingMessage() {
            const notTaxableOperationList = [
                paymentOrderOperationResources.PaymentOrderOutgoingProfitWithdrawing.value,
                paymentOrderOperationResources.PaymentOrderIncomingContributionOfOwnFunds.value
            ];
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);

            let messageWithAnchor = this.setExplainMessageWithAnchor();

            if (operationType === paymentOrderOperationResources.PaymentOrderOutgoingForTransferSalary.value) {
                messageWithAnchor = this.validateWorkerCharges(this.sourceDocument.get('WorkerCharges'));
            }

            if (_.contains(notTaxableOperationList, operationType)) {
                messageWithAnchor = 'Не учитывается.';
            }

            this.notTaxable = false;

            if (messageWithAnchor) {
                this.notTaxable = true;
                return messageWithAnchor;
            }

            if (this.loading) {
                return 'Подождите, идет загрузка…';
            }
        },

        validateWorkerCharges(charges) {
            const message = 'Заполните необходимые поля.';

            if (charges && charges.length) {
                const isValid = _.find(charges, (item) => {
                    let chargesSum = 0;
                    if (!_.isUndefined(item.Charges)) {
                        chargesSum = item.Charges.reduce((total, charge) => {
                            return total + charge.Sum;
                        }, 0);
                    }

                    return chargesSum <= 0 || !item.WorkerId;
                });

                if (!isValid) {
                    return '';
                }
            }

            return message;
        },

        operationsExplainigObjects(operationType, cid) {
            let explainingMessage;

            // необходимые поля для операций
            if (this.setOperationExplainMessageWithAnchor(operationType, cid)) {
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
            const status = parseInt(self.sourceDocument.get('Status'), 10);

            let required = {
                Status: {
                    fullName: 'Не учитывается. Платежное поручение <a data-scroll-to-el=".status">не оплачено</a>.',
                    otherCondition() {
                        return !(direction === Direction.Outgoing && !self.sourceDocument.isMemorial() && status === common.Data.DocumentStatuses.NotPayed);
                    }
                },
                Sum: {
                    name: 'сумму',
                    selector: '[data-bind=Sum]',
                    fieldName: ['Sum']
                }
            };

            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);

            if (operationType === paymentOrderOperationResources.PaymentOrderPaymentToSupplier.value ||
                operationType === paymentOrderOperationResources.PaymentOrderIncomingLoanObtaining.value ||
                operationType === paymentOrderOperationResources.PaymentOrderIncomingContributionAuthorizedCapital.value ||
                operationType === paymentOrderOperationResources.PaymentOrderIncomingMaterialAid.value ||
                operationType === paymentOrderOperationResources.PaymentOrderOutgoingReturnToBuyer.value ||
                operationType === paymentOrderOperationResources.PaymentOrderOutgoingLoanRepayment.value ||
                operationType === paymentOrderOperationResources.PaymentOrderIncomingPaymentForGoods.value ||
                operationType === paymentOrderOperationResources.PaymentOrderOutgoingPaymentAgencyContract.value) {
                required.KontragentId = {
                    name() {
                        return direction === Direction.Outgoing ? 'получателя' : 'плательщика';
                    },
                    selector: '[data-bind=KontragentName]',
                    fieldName: ['KontragentId']
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

            if (operationType === paymentOrderOperationResources.PaymentOrderIncomingMediationFee.value) {
                required.MiddlemanContract = {
                    name: 'посреднический договор',
                    selector: '[data-bind=ContractNumber]',
                    fieldName: 'MiddlemanContract.ContractNumber'
                };
            }

            if (operationType === paymentOrderOperationResources.PaymentOrderOutgoingTransferToAccount.value) {
                required.TransferSettlementaccountId = {
                    name: 'посреднический договор',
                    selector: '[data-bind=TransferSettlementaccountId]',
                    fieldName: ['TransferSettlementaccountId']
                };
            }

            if (operationType === paymentOrderOperationResources.PaymentOrderOutgoingForTransferSalary.value) {
                required.WorkerCharges = {
                    name: 'Сотрудника',
                    selector: '[data-bind=WorkerPayments_WorkerName]',
                    fieldName: ['WorkerCharges']
                };
            }

            if (operationType === paymentOrderOperationResources.BudgetaryPayment.value) {
                required.KBK = {
                    name: 'КБК',
                    selector: '[data-bind=KBK]',
                    fieldName: ['KBK']
                };
            }

            return required;
        },

        getDocumentSpecialProperties() {
            const {
                _50_01, _50_02, _51_01, _53_01, _55_03, _55_04, _57_01, _58_02, _58_03_01, _58_03_02,
                _60_01, _60_02, _62_01, _62_02, _66_01, _66_02, _66_03, _66_04, _67_01,
                _67_02, _67_03, _67_04, _68_01, _68_02, _68_04, _68_06, _68_07, _68_08, _68_10,
                _68_11, _68_12, _68_13, _69_01, _69_02_01, _69_02_02, _69_03, _69_11,
                _70_01, _71_01, _73_01, _75_01, _75_02, _75_03, _76_02, _76_03, _76_05, _76_06,
                _76_07, _76_09, _76_41, _80_09, _81_09, _860100, _91_01, _91_02_01, _91_02_02,
                _91_02_03, _97_01, _98_01, _98_02,  _99_09
            } = SyntheticAccountCodesEnum;
            const direction = parseInt(this.sourceDocument.get('Direction'), 10);
            const isForIp = isPostingsOfTypeOtherForIp(this.sourceDocument.get('OperationType'));
            const posting = {};

            if (direction === Direction.Outgoing) {
                posting.Credit = {
                    defaultAccountCode: SyntheticAccountCodesEnum._51_01,
                    disabled: true
                };

                if (isForIp) {
                    posting.Debit = {
                        defaultAccountCode: SyntheticAccountCodesEnum._57_01,
                        disabled: true
                    };
                } else {
                    posting.Debit = {
                        accountsFilter: [_50_01, _51_01, _53_01, _55_03,
                            _55_04, _57_01, _58_02, _58_03_01, _58_03_02,
                            _60_01, _60_02,

                            _62_01, _62_02,

                            _66_01, _66_02, _66_03,
                            _66_04, _67_01, _67_02, _67_03, _67_04,

                            _68_01, _68_02, _68_04, _68_06,
                            _68_07, _68_08, _68_10, _68_11, _68_12, _68_13,

                            _69_01, _69_02_01, _69_02_02, _69_03, _69_11,

                            _70_01, _71_01, _73_01, _75_01, _75_02, _75_03,
                            _76_02, _76_05, _76_06, _76_07, _76_09, _76_41,
                            _80_09,
                            _81_09,
                            _860100,

                            _91_02_01, _91_02_02, _91_02_03, _97_01, _99_09]
                    };
                }

                return posting;
            }

            if (direction === Direction.Incoming) {
                posting.Debit = {
                    defaultAccountCode: _50_01,
                    disabled: true
                };

                if (isForIp) {
                    posting.Credit = {
                        defaultAccountCode: _57_01,
                        disabled: true
                    };
                } else {
                    posting.Credit = {
                        accountsFilter: [
                            _50_01, _50_02, _53_01, _55_03,
                            _55_04, _57_01, _58_02, _58_03_01, _58_03_02,

                            _60_01, _60_02,

                            _62_01, _62_02, _66_01, _66_03, _67_01, _67_03,

                            _68_01, _68_02, _68_04, _68_06,
                            _68_07, _68_08, _68_10, _68_11, _68_12, _68_13,

                            _69_01, _69_02_01, _69_02_02, _69_03, _69_11,

                            _73_01, _75_01, _75_02, _75_03,
                            _76_02, _76_03, _76_05, _76_06, _76_07, _76_09,
                            _80_09,

                            _860100,

                            _91_01, _98_01,
                            _98_02, _99_09
                        ]
                    };
                }
                return posting;
            }

            return null;
        },

        getLoadUrl() {
            const documentBaseId = this.sourceDocument.get('BaseDocumentId') || this.sourceDocument.get('DocumentBaseId');

            return `/Finances/Money/Operations/${documentBaseId}/AccountingPostings`;
        },

        getGenerateUrl() {
            return '/Accounting/PaymentOrders/GetAllPostings';
        }
    });
}(Bank, Common));
