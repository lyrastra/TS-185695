/* eslint-disable */

import { isSalaryProject } from '../../../../../../../../helpers/MoneyOperationHelper';

(function(components) {
    'use strict';

    var prefix = 'WorkerPayments_';

    components.WorkerPayrollPaymentRow = Backbone.View.extend({
        initialize: function(options) {
            Backbone.Validation.bind(this, { prefix: prefix });
            _.extend(this, Cash.Views.initWorkerAutocompleteMixin);

            this.onRemove = options.onRemove;
        },

        render: function() {
            this.initializeControls();

            this.setPrefix();
            this.bind({
                prefix: prefix
            });

            this._bindEvents();

            return this;
        },

        _bindEvents: function() {
            this.listenTo(this.model, 'change:WorkerId change:WorkerDocumentType', function() {
                this._clearCharges();
                this._showWorkerCharges();
            });
        },

        initializeControls: function() {
            this.initWorkerAutocomplete({ useClean: false });
            this._showWorkerCharges();
        },

        _showWorkerCharges: function() {
            var workerId = this.model.get('WorkerId');
            var $el = this.$('.js-workerChargesRegion');
            if (!workerId) {
                this._clearCharges();
            }

            var data = this._getWorkerCharges();
            var view = new mdNew.WorkerCharges({
                showLabels: false,
                showTotal: false,
                documentBaseId: this.model.get('BaseDocumentId'),
                workerId: workerId,
                paymentType: this.model.get('WorkerDocumentType'),
                data: data,
                readonly: isSalaryProject(this.model.get('WorkerDocumentType')) ? true : this.options.readonly,
                onChange: (options) => {
                    const charges = options.workerCharges[0].Charges || [];

                    if (!charges.length && this.onRemove) {
                        this.onRemove(this.model);
                    }

                    this.model.workerCharges = charges;
                    this.model.set({
                        Charges: charges
                    });

                    if (workerId) {
                        this.model.set({
                            Sum: options.sum
                        });
                    }
                }
            });
            view.render();

            this.workerCharges = view;
            $el.html(view.$el);
        },

        isValid: function() {
            return this.workerCharges && this.workerCharges.isValid();
        },

        destroy: function() {
            this._destroyWorkerCharges();
        },

        _destroyWorkerCharges: function() {
            this.workerCharges && this.workerCharges.destroy();

            this._clearCharges();
            this.workerCharges = null;
        },

        _clearCharges: function() {
            var model = this.model;

            model.unset('Sum');
            model.unset('Charges');
            model.workerCharges = null;
        },

        _getWorkerCharges: function() {
            var model = this.model;
            return model.get('Charges') || model.workerCharges;
        },

        setPrefix: function() {
            this.$('[data-bind]').each(function(index, el) {
                $(el).attr('data-bind', prefix + $(el).attr('data-bind'));
            });
        }
    });

})(Cash.Components);
