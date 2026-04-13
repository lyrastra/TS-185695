(function(common) {
    'use strict';

    common.Controls.PostingsAndTaxBaseControl = Backbone.View.extend({

        events: {
            'click a[data-scroll-to-el]': 'scrollToEl'
        },

        initialize: function(options) {
            this.options = options;
            _.extend(this, common.Mixin.PostingsAndTaxTools);

            this.initializeVariables();
            this.initializeEvents();
        },

        initializeVariables: function() {
            this.switcherMode = common.Data.ProvidePostingType;
            this.rowsViewsStack = [];
            this.operationOptions = {};
        },

        initializeEvents: function() {
            this.collection.on('ModelLoaded', this.render, this);
            this.collection.on('modelStartLoad', this.prerender, this);
            this.collection.on('checkSourceFailed', this.invalidSource, this);
            this.collection.startListen();

            this.collection.sourceDocument.on('change:AccountingType', this.onChangeAccountingType, this);
            this.collection.sourceDocument.on('change:PostingsAndTaxMode', this.onChangeMode, this);
        },

        contentView: common.Controls.TaxRowOperationControl,

        prerender: function() {
            this.renderComplite = false; // хак, для того, чтобы не проходила валидация покуда не отрендрился контрол
        },

        render: function() {
            var template = TemplateManager.getFromPage(this.template);
            this.$el.html(template);
            if (this.rowsViewsStack.length) {
                _.each(this.rowsViewsStack, function(rowView) {
                    rowView.remove();
                });
            }
            this.$('#OperationsContent').empty();
            this.collection.each(function(model) {
                var operationView = this.rowElementRender(model);
                this.$('#OperationsContent').append(operationView.$el);
                operationView.onRender();
            }, this);

            this.onRender();
            this.renderComplite = true;
        },

        rowElementRender: function(model) {
            this.operationOptions.model = model;
            this.operationOptions.sourceDocument = this.collection.sourceDocument;
            this.operationOptions.documentSpecialProperties = _.bind(this.collection.getDocumentSpecialProperties, this.collection);
            this.operationOptions.onlyOneManualPosting = this.collection.onlyOneManualPosting;
            this.operationOptions.readonly = this.options && this.options.readonly;

            var operation = new this.contentView(this.operationOptions);
            this.rowsViewsStack.push(operation);
            return operation.render();
        },

        onChangeAccountingType: function() {
            var currentType = this.collection.sourceDocument.get('AccountingType');

            if (currentType == this.ownAccountingType) {
                this.$el.fadeIn('fast');
            } else {
                this.$el.hide();
            }
        },

        hideEmptyTable: function() {
            var collection = this.collection;
            var canBeManual = collection.canBeManual ? collection.canBeManual() : true;

            if (!collection.length || !canBeManual) {// || !this.collection.checkFillCollection()) {
                this.selectExplainigMessage();
                this.$('.mdTable').hide();
            } else {
                this.hideTableHeaderForEmptyOperations();
            }
        },

        selectExplainigMessage: function() {
            if (!_.isFunction(this.collection.explainingMessage)) {
                return true;
            }
            var message = this.collection.explainingMessage(),
                accountingType = this.collection.sourceDocument.get('AccountingType');
            this.createAndPlacementExplainigBlock(message);
            if (accountingType == this.ownAccountingType) {
                this.$el.show();
            }
        },

        onRender: function() {
            this.onChangeAccountingType();
            this.hideHeaderForOneOperation();
        },

        hideTableHeaderForEmptyOperations: function() {
            var allEmpty = true,
                header = this.$('.mdTable .mdTableHeader');

            this.collection.each(function(model) {
                if (model.get('ExplainingMessage')) {
                    return;
                } else if (!model.get('emptyOperation') || this.sourceDocument.get('PostingsAndTaxMode') == this.postingType.ByHand || model.get('IsManualEdit')) {
                    allEmpty = false;
                }
            }, this);

            if (allEmpty) {
                header.hide();
            } else {
                header.show();
            }
        },

        explainigMessageDrafting: function(text, additionalFilter) {
            var messageBlock = this.$('[data-block=explainigmessage]').filter(additionalFilter);

            if (text.length) {
                messageBlock.hide().html(text).fadeIn('fast');
            } else {
                messageBlock.slideUp('fast');
            }
        },

        invalidSource: function() {
            this.$el.empty();
            var message = _.result(this.collection, 'explainingMessage') || 'Заполните все необходимые поля';
            this.createAndPlacementExplainigBlock(message);
        },

        focusEl: function($targetField) {
            if ($targetField.prop('tagName') == 'INPUT') {
                $targetField.focus();
            } else {
                $targetField.find('input').filter(function() {
                    return !$(this).val().length;
                }).first().focus();
            }
        },

        scrollToEl: function(e) {
            var control = this;
            var $link = $(e.target);
            var modelCid = $link.data('model');
            var marginTopFromDocument = 50,
                elSelector = $link.data('scroll-to-el'),
                $targetField = $(elSelector).first();

            if (modelCid) {
                $targetField = $('[data-id=' + modelCid + ']').find($link.data('scroll-to-el')).first();
            }

            if ($targetField && $targetField.length > 0) {
                $('html, body').animate({
                    scrollTop: $targetField.offset().top - marginTopFromDocument
                }, 500, function() {
                    control.focusEl($targetField);
                });
            }
        },

        hideHeaderForOneOperation: function() {
            var operCount = this.collection.length;
            if (operCount && operCount === 1) {
                this.rowsViewsStack[0].hideHeader();
            }
        }
    });

})(Common);
