/* eslint-disable no-undef, no-param-reassign, eqeqeq */
import { cashOrderOperationResources } from '../../../../../../../resources/MoneyOperationTypeResources';

((cash, common, _) => {
    cash.Views.IncomingCashOrderView = cash.Views.baseCashOrderView.extend({
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
            const allOperations = _.clone(cash.Views.incomingOperationList);
            this.removeFromOperationList(allOperations);

            return allOperations;
        },

        removeFromOperationList(allOperations) {
            const fromOtherCash = cashOrderOperationResources.CashOrderIncomingFromAnotherCash.value;
            const isOoo = (new common.FirmRequisites()).get(`IsOoo`);
            const isOsno = this.options.taxSystem.IsOsno;
            const isOutsourceTariff = this.options.userInfo.AccessRuleFlags.IsOutsourceTariff;
            const isIpOsno = isOsno && !isOoo;

            if (isIpOsno && !isOutsourceTariff) {
                const ipOsnoHiddenOperationList = [
                    cashOrderOperationResources.CashOrderIncomingMediationFee.value,
                    cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value
                ];
                allOperations.collection = allOperations.collection.filter(operation => !ipOsnoHiddenOperationList.includes(operation.value));
            }

            if (this.model.get(`OperationType`) == fromOtherCash) {
                allOperations.collection = _.filter(allOperations.collection, operation => {
                    return operation.value == fromOtherCash;
                });

                return;
            }

            allOperations.collection = _.filter(allOperations.collection, operation => {
                return operation.value != fromOtherCash;
            });
        }
    });
})(Cash, Common, _);
