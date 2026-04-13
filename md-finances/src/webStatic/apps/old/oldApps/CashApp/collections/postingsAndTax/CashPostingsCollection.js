/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import isPostingsOfTypeOtherForIp from '../../../../../../helpers/postingsForIpHelper';
import { cashOrderOperationResources } from '../../../../../../resources/MoneyOperationTypeResources';

(function(cash, cashEnums, common) {
    const parent = common.Collections.BuhOperationCollection;
    cash.Collections.PostingsAndTax.CashPostingsCollection = parent.extend({

        url() {
            return cash.Data.GetAllPostings;
        },

        onlyOneManualPosting: true,

        listenSource() {
            const fields = [
                'WorkerId',
                'KontragentId',
                'IsLongTermLoan',
                'KontragentAccountCode',
                'Date',
                'Sum',
                'PaidCardSum',
                'OperationType',
                'IncludeNds',
                'NdsSum',
                'NdsType',
                'Documents',
                'ContractBaseId',
                'ProjectNumber',
                'DestinationCashId',
                'TaxationSystemType',
                'MiddlemanContract',
                'ProvideInAccounting',
                'LoanInterestSum'
            ];
            this.listenPostingsFileds(fields);
        },

        requiredFields() {
            const fields = {
                Sum: {
                    name: 'сумму',
                    selector: '[data-bind=Sum]',
                    fieldName: 'Sum'
                }
            };

            const isOutgoing = this.sourceDocument.get('Direction') === Direction.Outgoing;
            const kontragentLabel = isOutgoing ? 'получателя' : 'плательщика';

            if (this.sourceDocument.isKontragentPayment()
                || (this.sourceDocument.isCashContributing && this.sourceDocument.isCashContributing())
                || this.sourceDocument.isLoanObtain()
                || this.sourceDocument.isMaterialAid()) {
                fields.KontragentId = {
                    name: kontragentLabel,
                    selector: '[data-bind=KontragentName]',
                    fieldName: 'KontragentId'
                };
            }

            if (this.sourceDocument.isWorkerPayment()) {
                fields.WorkerId = {
                    name: kontragentLabel,
                    selector: '[data-bind=WorkerName]',
                    fieldName: 'WorkerId'
                };
            }

            if (this.sourceDocument.isMediationPayment()) {
                fields.MiddlemanContract = {
                    name: 'посреднический договор',
                    selector: '[data-bind=ContractAutocomplete]',
                    fieldName: 'MiddlemanContract.ContractNumber'
                };
            }

            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10);
            if (operationType === cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value) {
                fields.MiddlemanContract = {
                    name: 'посреднический договор',
                    selector: '.js-middlemanContract',
                    fieldName: 'MiddlemanContract.ContractNumber'
                };
            }

            return fields;
        },

        checkSourceData() {
            this.explainingMessage();

            return !(!this.checkRequiredFields(_.result(this, 'requiredFields')) || this.notTaxable);
        },

        explainingMessage() {
            const operationType = parseInt(this.sourceDocument.get('OperationType'), 10) || null;
            const notTaxableList = [
                cashOrderOperationResources.CashOrderOutgoingProfitWithdrawing.value,
                cashOrderOperationResources.CashOrderIncomingContributionOfOwnFunds.value
            ];
            if (operationType) {
                if (_.contains(notTaxableList, operationType)) {
                    this.notTaxable = true;
                    return 'Не учитывается.';
                }

                const message = parent.prototype.explainingMessage.call(this);

                if (message) {
                    return message;
                }
            }
        },

        getDocumentSpecialProperties() {
            const isMain = new cash.Collections.CashCollection().find(function(item) {
                return item.get('Id') == this.sourceDocument.get('CashId');
            }, this).get('IsMain');
            const isForIp = isPostingsOfTypeOtherForIp(this.sourceDocument.get('OperationType'));
            const direction = parseInt(this.sourceDocument.get('Direction'), 10);
            const {
                CreditMainCash,
                CreditOtherCash,
                DebitMainCash,
                DebitOtherCash
            } = this.sourceDocument.get('AvailableAccountCodes');
            let credit;
            let debit;

            if (isForIp) {
                if (direction === Direction.Outgoing) {
                    credit = isMain ? CreditMainCash : CreditOtherCash;
                    debit = [570100];
                } else {
                    credit = [570100];
                    debit = isMain ? DebitMainCash : DebitOtherCash;
                }
            } else {
                credit = isMain ? CreditMainCash : CreditOtherCash;
                debit = isMain ? DebitMainCash : DebitOtherCash;
            }

            const specialProps = {
                Credit: mapAccountCodeList(credit),
                Debit: mapAccountCodeList(debit)
            };

            return specialProps;
        },

        getLoadUrl() {
            const documentBaseId = this.sourceDocument.get('BaseDocumentId') || this.sourceDocument.get('DocumentBaseId');

            return `/Finances/Money/Operations/${documentBaseId}/AccountingPostings`;
        },

        getGenerateUrl() {
            return '/Accounting/FirmCash/GetAllPostings';
        }
    });


    function mapAccountCodeList(codes) {
        if (codes.length === 1) {
            return { defaultAccountCode: codes[0], disabled: true };
        }

        return { accountsFilter: codes };
    }
}(Cash, CashEnums, Common));
