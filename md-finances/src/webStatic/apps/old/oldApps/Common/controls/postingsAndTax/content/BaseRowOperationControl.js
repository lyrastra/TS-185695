import {
    cashOrderOperationResources,
    paymentOrderOperationResources,
    purseOperationResources
} from '../../../../../../../resources/MoneyOperationTypeResources';

/* eslint-disable */
(function(common) {
    common.Controls.BaseRowOperationControl = Backbone.View.extend({
        tagName: 'li',
        className: 'operationRow',

        events: {
            'click .slamRow .slamRowText, .slamRow .slamState': 'manipulateSlamRow',
            'click .addItem': 'addNewItem',
            'click .mdCloseLink': 'removeRow'
        },

        initialize(options) {
            this.options = options;
            this.collectionBinder = new Backbone.MdCollectionBinder();
            this.isIpOsno = !this.model.collection.isOoo && this.model.collection.isOsnoMode();
            this.initializeVariables();
            this.initializeEvents();
            _.extend(this, common.Mixin.PostingsAndTaxTools);
        },
        initializeVariables() {},

        initializeEvents() {},

        render() {
            const template = TemplateManager.getFromPage(this.template);
            const $beforeRenderedEl = $('<div />').html(template);
            $beforeRenderedEl.render(this.model.toJSON(), this.getDirectives());

            this.$el.hide().html($beforeRenderedEl.html()).fadeIn('fast');
            $beforeRenderedEl.remove();

            this.onRender();
            return this;
        },

        onRender() {
            this.removeEmptyHeaders();
            this.removeUselessElements();

            if (this.isIpOsno) {
                this.$(`.addItemBlock`).remove();
            }
        },

        removeEmptyHeaders() {
            if (!this.model.get('OperationName')) {
                this.$('[data-bind=operationHeader]').remove();
            }
        },

        removeUselessElements() {
            let model = this.model,
                linkedDocs = model.get('LinkedDocuments');

            if (!model) { return; }

            if (!linkedDocs || !linkedDocs.length) {
                this.$('.linkedDocumentsHeader').remove();
            }
        },

        emptyPostingsHandling() {
            let operationType = parseInt(this.options.sourceDocument.get('PurseOperationType') || this.options.sourceDocument.get('OperationType'), 10);

            let mode = this.options.sourceDocument.get('PostingsAndTaxMode'),
                isManualEmpty = !this.model.get('ManualPostings').length,
                isMainEmpty = !this.model.get('MainPostings').length,
                hasPostings = this.model.get('HasPostings'),
                isLinkedEmpty = !_.values(this.model.get('LinkedDocuments')).length;

            if ([purseOperationResources.PurseOperationIncome.purseOperationType, purseOperationResources.PurseOperationComission.purseOperationType, purseOperationResources.PurseOperationComission.value].includes(operationType) && !hasPostings) {
                this.createAndPlacementExplainigBlock(`Не учитывается при расчёте налога.`);
                return;
            }

            if (mode == common.Data.ProvidePostingType.ByHand) {
                if (isManualEmpty) {
                    this.addNewItem();
                }
            } else if (_.isUndefined(mode) || mode == common.Data.ProvidePostingType.Auto) {
                const operationsWithoutEmptyMessage = [
                    paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value,
                    cashOrderOperationResources.CashOrderIncomingFromRetailRevenue.value
                ];

                if (this.model.get('IsManualEdit') && isManualEmpty) {
                    this.addNewItem();
                } else if (purseOperationResources.PurseOperationTransferToSettlement.value === operationType) {
                    const message = 'Не учитывается при расчёте налога.';
                    this.createAndPlacementExplainigBlock(message);
                } else if (isMainEmpty && isManualEmpty && isLinkedEmpty && !operationsWithoutEmptyMessage.includes(operationType)) {
                    this.addAndRemoveEmptyMessage(true);
                } else {
                    this.addAndRemoveEmptyMessage();
                }
            }
        },

        manipulateSlamRow(e) {
            let $elem = $(e.currentTarget || e.target),
                $parentElem = $elem.closest('.slamRow'),
                isDisabled = $parentElem.hasClass('disabled'),
                $content,
                isOpen;

            if (isDisabled) { return; }

            $content = $elem.siblings('.slamRowContent'),
            isOpen = $parentElem.hasClass('open');

            if (isOpen) {
                $content.slideUp('fast');
                $parentElem.removeClass('open');
            } else {
                $content.slideDown('fast');
                $parentElem.addClass('open');
            }
        },

        bindPostings(postingsField) {
            if (this.model.get(postingsField).length > 0) {
                const elSelector = String.format('[data-bind={0}]', postingsField);
                this.bindCollection(elSelector, this.model.get(postingsField));
            }
        },

        bindCollection(elSelector, collection) {
            const $el = this.$(elSelector);
            this.collectionBinder.unbind();
            this.collectionBinder.bind(collection, { el: $el, $el }, 'li');
        },

        renderItem(model) {
            const row = $('<li>', {
                class: 'mdTableRow',
                html: TemplateManager.getFromPage(this.emptyRowTemplate)
            });

            row.render(model.toJSON(), this.getDirectives());
            this.applyDecimalMask(row.find('[data-number=float]'));

            this.$('[data-bind=ManualPostings]').append(row);
            $('body').trigger('resize');

            return row;
        },

        createItemModel() {
            const collection = this.model.get('ManualPostings');
            collection.add({
                Direction: this.model.get('Direction')
            });

            return collection.last();
        },

        addNewItem() {
            const model = this.createItemModel();

            this.renderItem(model);
            this.afterNewItemAdded && this.afterNewItemAdded(model);
        },

        setExplainigMessage() {
            const message = this.model.get('ExplainingMessage');
            if (message) {
                this.createAndPlacementExplainigBlock(message);
                return true;
            }
        },

        removeRow(e) {
            let $el = $(e.currentTarget || e.target).closest('.mdTableRow'),
                cid = $el.attr('id'),
                collection = this.model.get('ManualPostings'),
                currentModel;

            if (!collection.getByCid) {
                currentModel = collection.get(cid);
            } else {
                currentModel = collection.getByCid(cid);
            }

            $el.remove();
            collection.remove(currentModel);
        },

        hideHeader() {
            this.$('[data-bind=operationHeader]').remove();
        },

        getDirectives() {
            return {};
        },

        onChangeCollection() {
            const collection = this.model.get('ManualPostings');
            if (!collection.length && this.model.get('IsManualEdit')) {
                this.addNewItem();
            }

            this.hideAndShowDeleteRowLink();
        },

        hideAndShowDeleteRowLink() {
            const postings = this.model.get('ManualPostings');

            if (!postings.length || (postings.length == 1 && !_.values(postings.at(0).attributes).length)) {
                this.$('.ttDelete').hide();
            } else {
                this.$('.ttDelete').fadeIn('fast').css('display', 'inline-block');
            }
        },

        applyDecimalMask(input) {
            input.mdNumberInputMask({
                numberOfDecimals: 2,
                allowedNegative: _.result(this.model.collection, 'allowNegativeSum')
            });

            input.on('blur change', (event) => { formatSum($(event.target)); });
            input.each(function() { formatSum($(this)); });
        },

        addDatePicker() {
            this.$('.mdDatepicker').mdDatepicker({
                minDate: new common.FirmRequisites().getMinDocumentDate()
            });
        }
    });

    function formatSum(el) {
        el.val(common.Utils.Converter.toAmountString(el.val()));
    }
}(Common));
