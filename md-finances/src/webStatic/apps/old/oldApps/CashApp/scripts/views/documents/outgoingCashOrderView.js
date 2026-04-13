/* eslint-disable no-undef, no-param-reassign, eqeqeq */
import { cashOrderOperationResources } from '../../../../../../../resources/MoneyOperationTypeResources';

((cash, common) => {
    cash.Views.OutgoingCashOrderView = cash.Views.baseCashOrderView.extend({
        template: `#CashOrderTemplate`,

        behaviors: {
            AutoDocNumberBehaviour: {
                behaviorClass: common.Behaviours.AutoDocNumberBehaviour
            }
        },

        initialize() {
            Backbone.Validation.bind(this);
            this.controls = [];
        },

        getRelatedCashOperations() {
            const operationType = cash.Views.outgoingOperationList;

            const cashList = new cash.Collections.CashCollection();
            const isOoo = (new common.FirmRequisites()).get(`IsOoo`);
            const isOsno = this.options.taxSystem.IsOsno;
            const isOutsourceTariff = this.options.userInfo.AccessRuleFlags.IsOutsourceTariff;
            const isIpOsno = isOsno && !isOoo;

            if (isIpOsno && !isOutsourceTariff) {
                const ipOsnoHiddenOperationList = [cashOrderOperationResources.CashOrderOutgoingPaymentAgencyContract.value];
                operationType.collection = operationType.collection.filter(operation => !ipOsnoHiddenOperationList.includes(operation.value));
            }

            if (cashList.length === 1) {
                operationType.collection = _.filter(operationType.collection, operation => {
                    return operation.value != cashOrderOperationResources.CashOrderOutgoingTranslatedToOtherCash.value;
                });
            }

            return operationType;
        },

        getWorkerDocumentTypes() {
            const requisites = new common.FirmRequisites();
            const result = [
                { value: cash.Data.workerDocumentType.WorkContract, label: `Трудовой` },
                { value: cash.Data.workerDocumentType.Gpd, label: `ГПД` }
            ];

            if (requisites && requisites.get(`IsOoo`) === true) {
                result.push({ value: cash.Data.workerDocumentType.Dividend, label: `Дивиденды` });
            }

            return result;
        },

        onChangeWorkerDocType(model, workerDocType) {
            const toggleOn = workerDocType === cash.Data.workerDocumentType.Dividend;
            const { toggleDividendsState } = this.controls.operation;

            toggleDividendsState && toggleDividendsState(toggleOn);
            this.changeDestinationField(toggleOn);
            this.toggleCommentsPlaceholder(toggleOn);
        },

        changeDestinationField(toggleDividendsOn) {
            const operations = this.getRelatedCashOperations();
            const defaultDestination = this.model.isOtherPayment() ? `` : operations.getDescription(this.model.get(`OperationType`));
            const salaryDestination = toggleDividendsOn ? `Выплата дивидендов` : `Выплаты физ. лицам`;
            const destination = this.model.get(`OperationType`) === cashOrderOperationResources.CashOrderOutgoingPaymentForWorking.value ? salaryDestination : defaultDestination;
            this.model.set(`Destination`, destination);
        },

        toggleCommentsPlaceholder(toggleOn) {
            const text = toggleOn ? `Номер и дата протокола общего собрания участников` : ``;
            this.$(`[data-bind=Comments]`).attr(`placeholder`, text);
        },

        destroyView() {
            const { operation } = this.controls;
            operation && operation.destroy && operation.destroy();
        }
    });
})(Cash, Common);
