/* global Bank, Common, Converter, _ */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

// eslint-disable-next-line func-names
(function(bank, common) {
    const parent = common.Collections.TaxOperationCollection;

    // eslint-disable-next-line no-param-reassign
    bank.Collections.PostingsAndTax.PurseTaxCollection = parent.extend({
        url: `/Accounting/PurseOperation/GetTaxPostings`,

        listenSource() {
            const fields = [
                `Sum`,
                `Date`,
                `PurseOperationType`,
                `Documents`,
                `TaxationSystemType`,
                `ProvideInAccounting`,
                `IncludeNds`,
                `NdsSum`,
                `NdsType`,
                `NdsSum`
            ];
            this.listenPostingsFileds(fields);
        },

        settings() {
            const operationType = this.sourceDocument.get(`PurseOperationType`);

            if (this.isUsn() && operationType === Md.Data.PurseOperationType.OtherOutgoing) {
                return {
                    incoming: { allowNegative: true }
                };
            }

            return {};
        },

        initialize() {
            parent.prototype.initialize.call(this);
            _.extend(this, common.Mixin.AddOperationsValidation);
            this.isOoo = new common.FirmRequisites().get(`IsOoo`);
        },

        requiredFields() {
            return {
                Sum: {
                    name: `сумму`,
                    selector: `[data-bind=Sum]`,
                    fieldName: [`Sum`],
                    otherCondition: this.getSum.bind(this)
                }
            };
        },

        getSum() {
            const sum = this.sourceDocument.get(`Sum`);

            if (Converter.toFloat(sum) > 0) {
                return sum;
            }

            return null;
        },

        validator: _.extend(parent.prototype.validator, {
            moreThanOperationSum: common.Mixin.FunctionForPostingsAndTaxValidation.moreThanDocumentSum
        }),

        operationsValidationRules() {
            return {
                SumValidation: {
                    moreThanOperationSum: { msg: `Сумма проводок не может быть больше общей суммы операции` }
                }
            };
        },

        checkSourceData() {
            this.explainingMessage();

            return !this.notTaxable;
        },

        explainingMessage() {
            const operationType = this.sourceDocument.get(`PurseOperationType`);
            const msg = common.Mixin.PostingsAndTaxTools.explainingMessagesLib;
            const selectedTaxSystem = this.getSelectedTaxSystem();
            const firmTaxSystem = this.getTaxationSystem();
            const isUsn6 = selectedTaxSystem === common.Data.TaxationSystemType.Usn && firmTaxSystem.isUsn6();
            const isUsn6AndEnvd = selectedTaxSystem === common.Data.TaxationSystemType.UsnAndEnvd && firmTaxSystem.isUsnAndEnvd() && firmTaxSystem.isUsn6();
            const isOutgoing = operationType !== Md.Data.PurseOperationType.Income;
            const isOutgoingPatent = selectedTaxSystem === common.Data.TaxationSystemType.Patent && isOutgoing;
            const year = dateHelper(this.sourceDocument.get(`Date`)).year();
            const isOsno = this.getTaxationSystem().isOsno();
            this.notTaxable = true;

            if (isOutgoingPatent) {
                return msg.notTaxable;
            }

            if (selectedTaxSystem === common.Data.TaxationSystemType.Envd || isUsn6AndEnvd) {
                return msg.notTaxableEnvd;
            }

            if (operationType === Md.Data.PurseOperationType.Comission && year >= 2026 && isOsno && this.isOoo) {
                this.notTaxable = true;
            }

            if (operationType === Md.Data.PurseOperationType.Income || operationType === Md.Data.PurseOperationType.Transfer) {
                if (isOsno && this.isOoo) {
                    return msg.notTaxableOsno;
                }
            } else if (operationType !== Md.Data.PurseOperationType.OtherOutgoing && isUsn6) {
                return msg.notTaxablePlainMessage;
            }

            if (operationType !== Md.Data.PurseOperationType.Transfer) {
                this.notTaxable = false;

                if (!this.checkRequiredFields(this.requiredFields())) {
                    this.notTaxable = true;

                    return this.setExplainMessageWithAnchor();
                }
            }

            return msg.notTaxablePlainMessage;
        },

        getSelectedTaxSystem() {
            const taxSystem = parseInt(this.sourceDocument.get(`TaxationSystemType`), 10);

            if (taxSystem) {
                return taxSystem;
            }

            return this.getTaxationSystem().getTaxSystemType();
        },

        getLoadUrl() {
            const documentBaseId = this.sourceDocument.get(`BaseDocumentId`) || this.sourceDocument.get(`DocumentBaseId`);

            return `/Finances/Money/Operations/${documentBaseId}/TaxPostings`;
        },

        getGenerateUrl() {
            return `/Accounting/PurseOperation/GetTaxPostings`;
        }
    });
}(Bank, Common));
