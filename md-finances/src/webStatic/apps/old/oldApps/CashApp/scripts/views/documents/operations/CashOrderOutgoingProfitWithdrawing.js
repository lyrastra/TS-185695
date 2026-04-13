/* global Backbone, TemplateManager, Cash */
import { cashOrderOperationResources } from '../../../../../../../../resources/MoneyOperationTypeResources';
import { description } from '../../../../../../../../helpers/MoneyOperationHelper';

(function() {
    Cash.Views.CashOrderOutgoingProfitWithdrawing = Backbone.View.extend({
        template: `CashOrderOutgoingProfitWithdrawingTemplate`,

        initialize() {
            this.model.set(`Destination`, this.getDescription());
        },

        render() {
            const template = TemplateManager.getFromPage(this.template);

            this.$el.html(template);
            this.bind();

            return this;
        },

        getDescription() {
            const currentDestination = this.model.get(`Destination`);
            const defaultDestination = description(cashOrderOperationResources.CashOrderOutgoingProfitWithdrawing);

            return currentDestination === defaultDestination ? `Доход от предпринимательской деятельности` : currentDestination;
        }
    });
}(Cash));
