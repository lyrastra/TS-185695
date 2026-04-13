import { MdCollectionView } from '@moedelo/md-frontendcore/mdCore';
import { Model } from 'backbone';
import Row from '../Row';

import template from './template.hbs';
import { getDefaultEmptySubPayment } from '../../helpers/defaultDataHelper';

const maxRows = 30;
const Items = MdCollectionView.extend({
    initialize({ collection, parentModel }) {
        this.collection = collection;
        this.parentModel = parentModel;

        this._$marionetteViewInstance.listenTo(this.collection, `remove`, () => {
            this.toggleAdd();
        });
    },

    template,
    childViewContainer: `.js-collection`,

    onRender() {
        this.toggleAdd();
    },

    events: {
        'click .js-add': function remove() {
            this.collection.add(new Model(getDefaultEmptySubPayment()));
            this.toggleAdd();
        }
    },

    createChildView(model) {
        return new Row({
            model,
            parentModel: this.parentModel
        });
    },

    instanceMethods: {
        isValid() {
            return this._$marionetteViewInstance.children.all(child => child.isValid());
        },
        toggleAdd() {
            this.$el.find(`.js-add`).toggle(this.collection.length < maxRows);
        },
        getPostingsPromise() {
            const pmss = [];
            this._$marionetteViewInstance.children.each(c => pmss.push(c.postingsPromise));

            return Promise.all(pmss);
        }
    }
});

export default Items;
