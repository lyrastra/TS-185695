/* global Marionette, BankApp, Md, Common, Bank */

import { paymentOrderOperationResources } from '../../../../../../../resources/MoneyOperationTypeResources';

(function(app, md, common, bank) {
    app.Views.PostingControlsBehavior = Marionette.Behavior.extend({
        ui: {
            postings: '[data-control=postingsAndTax]'
        },

        onRender() {
            if (this.canProvidePostings()) {
                this.createPostingsControl();
            } else {
                this.removePostingFields();
            }
        },

        canProvidePostings() {
            return window._preloading.CanViewPosting;
        },

        removePostingFields() {
            const { view } = this;

            view.model.set('ProvideInAccounting', false);
            view.model.set('PostingsAndTaxMode', common.Data.ProvidePostingType.ByHand);

            this.ui.postings.remove();

            this.removePostingFieldsInOperation();
            this.listenTo(view.model, 'change:OperationType', this.removePostingFieldsInOperation);
        },

        removePostingFieldsInOperation() {
            const { operation } = this.view;
            if (operation) {
                operation.$('[data-control=kontragentAccountCode],#taxationSystemField').remove();
            }
        },

        createPostingsControl() {
            const { view } = this;
            const postingCollection = this.postingCollection();
            const taxCollection = this.taxCollection();
            const { onErrorLoaded } = view;

            view.postingsAndTaxControl = new common.Controls.PostingsAndTaxControl({
                model: view.model,
                taxModel: taxCollection,
                postingsModel: postingCollection,
                el: this.ui.postings,
                readonly: view.isClosed(),
                onRenderTax() {
                    const { operation } = view;
                    operation && operation.setTaxControlMessage && operation.setTaxControlMessage();
                }
            });

            this.listenTo(postingCollection, 'ModelLoaded', this.showServerMessageIfNeed);

            this.listenTo(postingCollection, 'ErrorLoaded', () => {
                onErrorLoaded && onErrorLoaded();
            });
        },

        postingCollection() {
            return new (this.getOption('postingCollectionClass') || bank.Collections.PostingsAndTax.PPostingCollection)();
        },

        taxCollection() {
            return new (this.getOption('taxCollectionClass') || bank.Collections.PostingsAndTax.PpTaxCollection)();
        },

        showServerMessageIfNeed() {
            const { view } = this;

            if (parseInt(view.model.get('OperationType'), 10) !== paymentOrderOperationResources.PaymentOrderIncomingFromAnotherAccount.value) {
                return;
            }

            if (this.ServerMessage && this.ServerMessage.length > 0) {
                const postingsBlock = view.$('[data-control=posting]');
                const messageBlock = postingsBlock.find('[data-block=explainigmessage]').text(this.ServerMessage);
                postingsBlock.html(messageBlock);
            }
        }
    });
}(BankApp, Md, Common, Bank));
