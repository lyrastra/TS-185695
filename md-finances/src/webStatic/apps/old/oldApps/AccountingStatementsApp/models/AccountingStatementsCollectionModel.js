(function (module) {
    module.Models.AccountingStatementsCollectionModel = Backbone.Model.extend({
        initialize: function(options) {
            this.on("change:AccountingDate", this.setDate);
        },

        isCompliteData: function () {
            var fill = this.get("Date") && this.get("Debit") && this.get("Credit") && this.get('Description') && this.get("Sum");
            return fill;
        },
        
        setDate: function() {
            this.set({
                Date: this.get("AccountingDate")
            });
        }
    });

})(AccountingStatements);