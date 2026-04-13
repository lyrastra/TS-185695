(function (money) {

    money.Models.Common.ConfirmingStatement = Backbone.Model.extend({
        defaults: {
            "Number": "",
            
            "Date": "",
            
            "Sum": ""
        }
    });

})(Money.module("Models.Common"));
