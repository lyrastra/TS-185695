/* global Backbone, Bank */

import { paymentOrderOperationResources } from '../../../../../../resources/MoneyOperationTypeResources';

(function(bank) {
    bank.Models.OperationTypeCollection = Backbone.Collection.extend({
        fillTypes() {
            this.add(bank.Utils.CommonDataLoader.IncomingOperationTypes.toJSON());
            this.add(bank.Utils.CommonDataLoader.OutgoingOperationTypes.toJSON());
            this.add({
                OperationType: paymentOrderOperationResources.BudgetaryPayment.value,
                OperationName: 'Бюджетный платеж'
            });
        },

        getOperationTypeName(operationType) {
            const type = this.find((item) => {
                return parseInt(item.get('OperationType'), 10) === parseInt(operationType, 10);
            });

            return type ? type.get('OperationName') : 'Не определена';
        }
    });
}(Bank));
