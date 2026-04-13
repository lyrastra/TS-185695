(function (bank) {

    bank.TextControl = Marionette.ItemView.extend({
        template: false,

        onRender: function() {
            this.$el.text(this.model.get('Value'));
        }
    });

})(Bank);