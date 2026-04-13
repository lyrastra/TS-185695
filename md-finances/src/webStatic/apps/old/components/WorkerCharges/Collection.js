import { Collection } from 'backbone';
import Model from './components/WorkerChargesListRow/Model';

export default Collection.extend({

    model: Model,

    initialize() {
        this.addRow();
    },

    addRow() {
        this.add({ Description: 'Без начисления', ChargeId: 0 });
    },

    getTotalSum() {
        let sum = 0;

        this.each(model => {
            sum += (model && model.get('Sum')) || 0;
        });

        return sum;
    },

    isValid() {
        return !this.find(function(model) {
            return !model.isValid(true);
        });
    },

    getChargesIds() {
        const list = [];

        this.each(function(model) {
            const chargeId = model.get('ChargeId');
            chargeId && list.push(chargeId);
        });

        return list;
    }
});
