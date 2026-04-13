import { Collection } from 'backbone';
import { MdLayoutView } from '@moedelo/md-frontendcore/mdCore';
import { round } from '@moedelo/frontend-core-v2/helpers/mathHelper';

import Payments from './Payments';

import template from './template.hbs';
import style from './style.m.less';

export default MdLayoutView.extend({
    template,

    regions: {
        list: `.js-list`
    },

    templateHelpers() {
        return { style };
    },

    initialize(options) {
        this.model = options.model;
        this._$marionetteViewInstance.stopListening(this.model, `change`);

        this.collection = new Collection(this.model.get(`UnifiedBudgetarySubPayments`));

        this._$marionetteViewInstance.listenTo(this.collection, `change add remove`, () => {
            this.onCollectionChange(this.collection);
        });

        this._$marionetteViewInstance.listenTo(this.collection, `change:Sum remove`, () => {
            this.onSumChange(this.collection);
        });
    },

    onRender() {
        this.listView = new Payments({
            collection: this.collection,
            parentModel: this.model
        });

        this.regions.list.show(this.listView);
    },

    onBeforeDestroy() {
        this._$marionetteViewInstance.stopListening();
    },

    instanceMethods: {
        onCollectionChange(collection) {
            this.model.set(`UnifiedBudgetarySubPayments`, collection.toJSON(), { silent: true });
        },
        onSumChange(collection) {
            this.model.set(`Sum`, this.getSum(collection));
        },
        getSum(collection) {
            return collection.toJSON().reduce((sum, p) => {
                if (!p.Sum) {
                    return sum;
                }

                return round(sum + p.Sum, 2);
            }, 0);
        },
        isValid() {
            return this.listView.isValid();
        },
        getPostingsPromise() {
            return this.listView.getPostingsPromise();
        }
    }
});
