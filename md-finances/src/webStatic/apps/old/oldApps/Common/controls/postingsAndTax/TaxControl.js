/* eslint-disable no-param-reassign */
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';

(common => {
    const parent = common.Controls.PostingsAndTaxBaseControl;
    common.Controls.TaxControl = parent.extend({
        template: `TaxControlTemplate`,
        ownAccountingType: common.Enums.AccountingType.Tax,

        validationMessage: `Проверьте проводки во вкладке «Налоговый учёт»`,

        contentView: common.Controls.TaxRowOperationControl,

        initialize(options) {
            parent.prototype.initialize.call(this, options);

            if (!this.selectExplainigMessage()) {
                return;
            }

            this.render();
        },

        initializeVariables() {
            parent.prototype.initializeVariables.call(this);
            this.sourceDocument = this.collection.sourceDocument;
            this.postingType = common.Data.ProvidePostingType;

            const isOooOsno = this.collection.isOsnoMode() && this.collection.isOoo;

            this.template = isOooOsno ? `OsnoTaxControlTemplate` : `TaxControlTemplate`;
            this.contentView = isOooOsno ? common.Controls.OsnoTaxRowOperationControl : common.Controls.TaxRowOperationControl;
        },

        initializeEvents() {
            parent.prototype.initializeEvents.call(this);
            this.collection.on(`explainingMessageChanged`, this.selectExplainigMessage, this);
            this.collection.on(`globalMessageChanged`, this.globalExplainingMessageDrawing, this);
        },

        initializeControls() {
            common.Utils.HintHelper.setHint(this.$(`.normalizedCostTypeQtip`), `Нормируемые расходы переносятся в закрытие квартального месяца, где их можно полностью или частично признать расходом для налога на прибыль`);

            const isIpOsno = this.collection.isOsnoMode() && !this.collection.isOoo;
            const isPatent = this.collection.sourceDocument.get(`TaxationSystemType`) === TaxationSystemType.Patent;

            if (isIpOsno && !isPatent) {
                this.$(`header .ttDescription`).remove();
                this.$(`header .ttDate`).remove();
            }
        },

        events: _.extend({}, {}, parent.prototype.events),

        onRender: function() {
            parent.prototype.onRender.call(this);
            this.hideEmptyTable();
            this.initPostingsValidation();
            this.initializeControls();

            if (this.options.readonly) {
                this.disableControl();
            }

            this.options.onRender && this.options.onRender();
        },

        disableControl: function() {
            this.$('[data-bind="ManualPostings"] .mdTableRow').filter(function() {
                var $row = $(this);
                var incoming = $row.find('.ttIncoming input').val();
                var outgoing = $row.find('.ttOutgoing input').val();

                return !incoming && !outgoing;
            }).remove();

            this.$('.addItemBlock').remove();
            this.$('.mdCloseLink').remove();

            this.$('input').replaceWith(function() {
                return $(this).val();
            });
            this.$('select').remove();
            this.$('.datepickerWrapper, .mdCustomSelectWrap').replaceWith(function() {
                return $(this).text();
            });
        },

        globalExplainingMessageDrawing: function(message) {
            if (message) {
                this.createAndPlacementExplainigBlock(message);
            }
        },

        initPostingsValidation: function() {
            common.Mixin.BindViewValidationEvent.bindCollectionValidation(this, this.collection);
        },

        showOperationsValidation: function(operations, message) {
            _.each(this.rowsViewsStack, function(rowView) {
                if (_.indexOf(operations, rowView.model.get('Cid')) != -1) {
                    rowView.showErrorMessage(message);
                } else {
                    rowView.hideErrorMessage();
                }
            });
        },

        hideOperationsValidation: function() {
            _.each(this.rowsViewsStack, function(rowView) {
                rowView.hideErrorMessage();
            });
        },

        beforeContentRender: function($beforeRenderedEl) {
            parent.prototype.beforeContentRender.call(this, $beforeRenderedEl);
        },

        scrollToNotFilledFields: function() {
        },

        setMainIntoManual: function() {
            this.collection.each(function(model) {
                if (model.get('ManualPostings').length) {
                    model.get('ManualPostings').each(function(item) {
                        model.get('MainPostings').add(item);
                    });
                }
                var mainPostings = model.get('MainPostings').toJSON();
                model.get('MainPostings').reset();
                model.get('ManualPostings').reset(mainPostings);
            });
        },

        invalidSource: function() {
            this.$el.empty();
            var message = _.result(this.collection, 'explainingMessage') || 'Заполните все необходимые поля';
            this.createAndPlacementExplainigBlock(message);
        },

        onChangeMode: function() {
            var currentMode = this.sourceDocument.get('PostingsAndTaxMode');
            this.hideTableHeaderForEmptyOperations();

            if (currentMode == this.switcherMode.Auto) {
                this.collection.generatePostings();
            } else if (currentMode == this.switcherMode.ByHand) {
                this.setMainIntoManual();
                this.collection.updatePostingsInSourceDocument();
                if (_.isFunction(this.collection.isNotCanSetTaxPostingByHand) && this.collection.isNotCanSetTaxPostingByHand()) {
                    this.$('.taxControl').hide();
                }
            }
        }

    });

})(Common);
