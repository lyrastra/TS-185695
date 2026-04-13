import { MdLayoutView } from '@moedelo/md-frontendcore/mdCore';
import { Model } from 'backbone';

import WorkerChargesList from './components/WorkerChargesList';
import TotalSum from './components/TotalSum';
import Collection from './Collection';
import Service from './services/Service';

import template from './template.hbs';
import style from './style.m.less';

export default MdLayoutView.extend({
    template,

    initialize({ workerId, data, documentBaseId, paymentType, onChange, readonly, showLabels = true, showTotal = true }) {
        this.model = new Model({
            showLabels,
            showTotal,
            readonly
        });
        this.onChange = onChange;
        this.collection = new Collection(data);
        this.workerId = workerId;
        this.service = new Service({
            workerId: workerId,
            documentBaseId: documentBaseId,
            paymentType: paymentType,
            collection: this.collection
        });
    },

    regions: {
        listRegion: '.js-listRegion',
        sum: '.js-sumRegion'
    },

    events: {
        'click .js-add': function(e) {
            this._addRow(e);
        }
    },

    templateHelpers() {
        const collection = this._mdView.collection;

        return {
            style,
            canShowTotalSum() {
                const isSum = collection.getTotalSum();
                const moreThanOneRow = collection.length > 1;

                return isSum && moreThanOneRow;
            }
        }
    },

    onRender() {
        this._showList();
        this._showTotalSum();
    },

    instanceMethods: {
        _addRow(e) {
            e.preventDefault();
            this.collection.addRow();
        },

        _showList: function() {
            const view = new WorkerChargesList({
                collection: this.collection,
                service: this.service,
                readonly: this.model.get('readonly'),
                handlers: {
                    onRemove: model => this._onRemove(model),
                    onChange: () => this._onChange(),
                    onChangeSelect: () => this._onChangeSelect()
                }
            });
            this.regions.listRegion.show(view);
        },

        isValid() {
            const isValid = this.collection.isValid();
            this.model.trigger('change');
            return isValid;
        },

        _showTotalSum() {
            const collection = this.collection;
            const totalSum = collection.getTotalSum();

            if (totalSum && collection.length > 1 && this.model.get('showTotal')) {
                const view = new TotalSum({
                    sum: collection.getTotalSum()
                });
                this.regions.sum.show(view);
            } else {
                this.regions.sum.empty();
            }
        },

        _onRemove(model) {
            this.collection.remove(model);
            this.model.trigger('change');
            this._onChange();
        },

        _onChangeSelect() {
            this.model.trigger('change');
            this._onChange();
        },

        _onChange() {
            const data = {
                workerCharges: [{
                    WorkerId: this.workerId,
                    Charges: this.collection.toJSON()
                }],
                sum: this.collection.getTotalSum()
            };

            this._showTotalSum();

            this.onChange(data);
        },

        destroy() {
            this.service.clear();
        }
    }
});
