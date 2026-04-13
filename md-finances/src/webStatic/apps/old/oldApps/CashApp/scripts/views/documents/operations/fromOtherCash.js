(function(cash) {
    cash.Views.fromOtherCash = Backbone.View.extend({
        template: 'FromOtherCashTemplate',
        
        initialize() {
            const destinationCash = new cash.Collections.CashCollection().find(function(item) {
                return item.get('Id') == this.model.get('DestinationCashId');
            }, this);

            this.model.set('DestinationCash', destinationCash.get('Name'));
        },
        
        render() {
            const template = TemplateManager.getFromPage(this.template);
            this.$el.html(template);

            this.bind();
            return this;
        }
    });
}(Cash));
