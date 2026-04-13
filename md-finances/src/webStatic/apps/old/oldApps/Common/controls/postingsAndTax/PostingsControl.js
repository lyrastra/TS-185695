(function(common) {
    'use strict';

    var parent = common.Controls.PostingsAndTaxBaseControl;
    common.Controls.PostingsControl = parent.extend({
        template: 'PostingsControlTemplate',
        validationMessage: 'Проверьте проводки во вкладке «Бухгалтерский учёт»',
        ownAccountingType: common.Enums.AccountingType.Posting,
        contentView: common.Controls.PostingsRowOperationControl,

        events: _.extend({}, {
            'click #ProvideInAccounting': 'changeProvideInAccounting'
        }, parent.prototype.events),

        initialize: function(options) {
            this.options = options;
            parent.prototype.initialize.call(this, options);
            this.selectExplainigMessage();
            this.setPostingStatusMsg();
            this.checkProvideInAccounting();
        },

        operationOptions: {
            syntheticCollection: null
        },

        initializeVariables: function() {
            parent.prototype.initializeVariables.call(this);
            this.sourceDocument = this.collection.sourceDocument;

            this.operationOptions.syntheticCollection = new common.Collections.SyntheticAccountAutocompleteCollection();
            this.operationOptions.syntheticCollection.fetch();

            this.operationOptions.dataFilter = this.options.dataFilter;
        },

        initializeEvents: function() {
            this.collection.on('ModelLoaded', this.removeExplainingBlock, this);

            parent.prototype.initializeEvents.call(this);
            this.collection.on('explainingMessageChanged', this.selectExplainigMessage, this);
            this.sourceDocument.on('change:PostingsAndTaxMode', this.checkPostingsAndTaxMode, this);
        },

        onRender: function() {
            parent.prototype.onRender.call(this);
            this.hideEmptyTable();
            this.checkPostingsAndTaxMode();

            if (this.options.readonly) {
                this.$('#ProvideInAccounting, .ttDelete, .addItemBlock').remove();
                this.$('input, textarea').attr('disabled', 'disabled');
            }

            this.options.onRender && this.options.onRender();
        },

        checkPostingsAndTaxMode: function() {
            if (this.collection.onlyPostingsAndTaxMode != common.Data.ProvidePostingType.ByHand) {
                if (this.sourceDocument.get('PostingsAndTaxMode') !== common.Data.ProvidePostingType.ByHand) {
                    this.sourceDocument.set('ProvideInAccounting', true);
                }
                this.checkProvideInAccounting();
            }

            this.setPostingStatusMsg();
        },

        setPostingStatusMsg: function() {
            var mode = this.sourceDocument.get('PostingsAndTaxMode');
            var byHand = common.Data.ProvidePostingType.ByHand;
            var $el = this.$('[data-block=postingStatus]');

            if (!$el.length) {
                $el = $('<div data-block="postingStatus" class="postingStatusMsg"><a id="ProvideInAccounting"></a></div>').hide().prependTo(this.$el);
            }
            $el.find('#ProvideInAccounting').hide();

            if (mode === byHand && this.collection.onlyPostingsAndTaxMode !== byHand) {
                $el.find('#ProvideInAccounting').html(this.getProvideInAccountingBlock());
                $el.find('#ProvideInAccounting').show();
            }
        },

        changeProvideInAccounting: function() {
            this.collection.sourceDocument.set('ProvideInAccounting', !this.collection.sourceDocument.get('ProvideInAccounting'));
            this.checkProvideInAccounting();
            this.setPostingStatusMsg();
        },

        getProvideInAccountingBlock: function() {
            var linkText;

            if (this.collection.sourceDocument.get('ProvideInAccounting')) {
                linkText = 'Не учитывать';
            } else {
                linkText = 'Учитывать';
            }

            return `${linkText} операцию в БУ`;
        },

        checkProvideInAccounting: function() {
            var mode = this.sourceDocument.get('PostingsAndTaxMode');
            var canProvide = this.sourceDocument.get('ProvideInAccounting');
            var isProvided = this.collection.isProvided();
            var $table = this.$('.mdTable');
            
            if (canProvide || mode === common.Data.ProvidePostingType.Auto || !isProvided) {
                this.$el.find('.postingsControlTable').slideDown('fast');
                this.unableInputFields($table);
            } else {
                this.disablingInputFields($table);
                this.$el.find('.postingsControlTable').slideUp('fast');
            }
        },

        disablingInputFields: function() {
            _.each(this.rowsViewsStack, function(rowView) {
                rowView.model.get('ManualPostings').removeValidation();
                rowView.model.get('ManualPostings').each(function(model) {
                    model.removeValidation();
                });
            });
        },

        unableInputFields: function() {
            _.each(this.rowsViewsStack, function(rowView) {
                rowView.model.get('ManualPostings').initValidation();

            });
        }
    });

})(Common);