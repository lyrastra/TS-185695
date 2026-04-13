/* global Bank, Common */
/* eslint-disable func-names */
import converter from '@moedelo/frontend-core-v2/helpers/converter';
import SyntheticAccountCodesEnum from '../../../../../../enums/SyntheticAccountCodesEnum';

(function(bank, common) {
    const parent = common.Collections.BuhOperationCollection;

    /* eslint-disable-next-line */
    bank.Collections.PostingsAndTax.PursePostingCollection = parent.extend({
        url: `/Accounting/PurseOperation/GetAccountingPostings`,

        onlyOneManualPosting: true,

        listenSource() {
            const fields = [
                `Sum`,
                `Date`,
                `PurseOperationType`,
                `PurseId`,
                `ContractBaseId`,
                `TaxationSystemType`,
                `Comment`,
                `KontragentName`,
                `Documents`,
                `ProvideInAccounting`,
                `NdsSum`
            ];

            this.listenPostingsFileds(fields);
        },

        checkSourceData() {
            return this.checkRequiredFields(this.requiredFields());
        },

        getSum() {
            const sum = this.sourceDocument.get(`Sum`);

            if (converter.toFloat(sum) > 0) {
                return sum;
            }

            return null;
        },

        requiredFields() {
            const required = {
                Sum: {
                    name: `сумму`,
                    selector: `[data-bind=Sum]`,
                    fieldName: [`Sum`],
                    otherCondition: this.getSum.bind(this)
                }
            };

            const type = this.sourceDocument.get(`PurseOperationType`);

            if (type === Md.Data.PurseOperationType.OtherOutgoing) {
                required.KontragentName = {
                    name: `получателя`,
                    selector: `[data-bind=KontragentName]`,
                    fieldName: [`KontragentName`]
                };
            }

            if (type === Md.Data.PurseOperationType.Income) {
                required.KontragentName = {
                    name: `плательщика`,
                    selector: `[data-bind=KontragentName]:last`,
                    otherCondition: function() {
                        if (!this.hasEmptyKontragents()) {
                            return true;
                        }

                        return null;
                    }.bind(this)
                };
            }

            return required;
        },

        hasEmptyKontragents() {
            return this.sourceDocument.get(`Sum`) > 0 && !this.sourceDocument.get(`KontragentName`);
        },

        getDocumentSpecialProperties() {
            const debitCodes = [
                SyntheticAccountCodesEnum._60_01,
                SyntheticAccountCodesEnum._60_02,
                SyntheticAccountCodesEnum._62_01,
                SyntheticAccountCodesEnum._62_02,
                SyntheticAccountCodesEnum._76_02,
                SyntheticAccountCodesEnum._76_05,
                SyntheticAccountCodesEnum._76_06
            ];
            const description = this.sourceDocument.get(`Id`) > 0 ? `` : this.sourceDocument.get(`Comment`);

            return {
                Sum: {
                    disabled: true
                },
                Description: description,
                Credit: {
                    disabled: true
                },
                SubcontoCredit: {
                    disabled: true
                },
                Debit: {
                    accountsFilter: debitCodes
                },
                SubcontoDebit: { disabled: true }
            };
        },

        onChangeDebit() {
            this.generatePostings();
        },

        getLoadUrl() {
            const documentBaseId = this.sourceDocument.get(`BaseDocumentId`) || this.sourceDocument.get(`DocumentBaseId`);

            return `/Finances/Money/Operations/${documentBaseId}/AccountingPostings`;
        },

        getGenerateUrl() {
            return `/Accounting/PurseOperation/GetAccountingPostings`;
        }
    });
}(Bank, Common));
