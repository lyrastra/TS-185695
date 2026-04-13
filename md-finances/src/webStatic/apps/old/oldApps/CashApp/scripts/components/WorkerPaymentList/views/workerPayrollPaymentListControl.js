/* eslint-disable */

import { isSalaryProject } from '../../../../../../../../helpers/MoneyOperationHelper';

(function(components) {

    'use strict';

    components.WorkerPayrollPaymentListControl = Backbone.View.extend({
        template: 'WorkerPayrollPaymentListTemplate',
        templateDisabled: 'WorkerPayrollPaymentListSalaryProjectTemplate',

        events: {
            'click .add': 'add'
        },

        initialize: function() {
            var data = this.model.get(this.options.name) || [];
            data = !data.length ? [{}] : data;

            this.collection = new components.WorkerPaymentsCollection(data);
            this.collection.on('remove add reset change', this.updateSourceModel, this);
            this.collection.on('add', this.addRow, this);
            this.collection.on('remove', this.removeRow, this);
            this.collection.on('add remove', this.hideCloseLink, this);

            this.model.on('change:WorkerDocumentType', this.updateCollection, this);
            this.model.on('change:WorkerDocumentType', this.removeAllRow, this);
        },

        render: function() {
            var template = isSalaryProject(this.model.get('UnderContract'))
                ? TemplateManager.getFromPage(this.templateDisabled)
                : TemplateManager.getFromPage(this.template) ;
            this.$el.html(template);

            this.rowTemplate = this.$('.row').clone();
            this.$('.row').remove();

            this.bind();
            this.createPartials();
            this.hideCloseLink();

            if(this.options.readonly) {
                this.$('.js-addWorkerPayment').remove();
            }

            return this;
        },

        createPartials: function() {
            this.rows = {};
            this.collection.each(this.addRow, this);
        },

        add: function() {
            this.collection.add({});
        },

        addRow: function(item) {
            var options = _.extend({}, this.options, {
                el: this.rowTemplate.clone(),
                model: item,
                onRemove: childModel => this.collection.remove(childModel)
            });

            item.set({
                Date: this.model.get('Date'),
                WorkerDocumentType: this.model.get('WorkerDocumentType'),
                BaseDocumentId: this.model.get('BaseDocumentId')
            });

            item.getDate = () => {
                return this.model.get('Date');
            };

            var rowView = new components.WorkerPayrollPaymentRow(options).render();
            this.$('.body').append(rowView.$el);

            this.rows[item.cid] = rowView;
        },

        removeRow: function(model) {
            this.rows[model.cid].remove();
            this.rows = _.omit(this.rows, model.cid);

            if (this.collection.length === 0) {
                this.collection.add({});
            }
        },

        removeAllRow() {
            this.collection.each(model => this.collection.remove(model));
        },

        updateSourceModel: function() {
            var data = this.collection.getData();
            if (this.isGpd()) {
                var gpd = _.first(data) || {};
                data = [gpd];
            }

            this.model.set(this.options.name, data);
        },

        hideCloseLink: function() {
            if (this.collection.length > 1) {
                this.$('.icon.close').show();
                return;
            }

            this.$('.icon.close').hide();
        },

        isGpd: function() {
            var type = this.model.get('WorkerDocumentType');
            return type == Cash.Data.workerDocumentType.Gpd;
        },

        isDividends: function() {
            var type = this.model.get('WorkerDocumentType');
            return type == Cash.Data.workerDocumentType.Dividend;
        },

        updateCollection: function() {
            this.collection.each(function(item) {
                item.set({
                    excludedIds: '',
                    WorkerDocumentType: this.model.get('WorkerDocumentType')
                });
            }, this);
        },

        isValid: function() {
            return !_.find(this.rows, function(childView) {
                return !childView.isValid();
            });
        },

        destroy: function() {
            _.each(this.rows, function(childView) {
                childView.destroy && childView.destroy();
            });
            this.collection.off('remove add reset change');
            this.rows = {};
        },

        bindings: {
            '.footer': {
                observe: 'WorkerDocumentType',
                visible: function() {
                    return !this.isGpd() && !this.isDividends();
                }
            }
        }
    });

})(Cash.Components);
