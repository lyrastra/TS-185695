(function (money) {

    money.Models.Common.Waybill = Backbone.Model.extend({
        defaults: {
            "Number": "",
            
            "Date": "",
            
            "Sum": "",

            "Amount": ""
        }
    });

})(Money.module("Models.Common"));
