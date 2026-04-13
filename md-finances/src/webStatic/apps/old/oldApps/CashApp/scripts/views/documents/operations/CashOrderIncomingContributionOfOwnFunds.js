/* global Backbone, TemplateManager, Cash */
import { cashOrderOperationResources } from '../../../../../../../../resources/MoneyOperationTypeResources';
import { description } from '../../../../../../../../helpers/MoneyOperationHelper';

(function() {
    Cash.Views.CashOrderIncomingContributionOfOwnFunds = Backbone.View.extend({
        template: `CashOrderIncomingContributionOfOwnFundsTemplate`,

        render() {
            const template = TemplateManager.getFromPage(this.template);

            this.model.set(`Destination`, this.getDescription());
            this.$el.html(template);
            this.bind();

            return this;
        },

        getDescription() {
            const currentDestination = this.model.get(`Destination`);
            const defaultDestination = description(cashOrderOperationResources.CashOrderIncomingContributionOfOwnFunds.value);

            return currentDestination === defaultDestination ? `Взнос собственных средств. НДС не облагается.` : currentDestination;
        }
    });
}(Cash));
