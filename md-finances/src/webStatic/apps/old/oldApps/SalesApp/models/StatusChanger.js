(function (sales) {
    var mainModule = Money.module("main");
    sales.Models.Main.StatusChanger = Backbone.Model.extend({

        url: WebApp.Bills.GetIncomings,

        limitationRows: 5,

        parse: function (response) {
            if (response.Status == false) {
                return {};
            }
            
            var model = this,
                tags = {
                statusPaymentsPopup: [],
                totalLength: response.List.length,
                moreLines: response.List.length - model.limitationRows
            };

            $.each(response.List, function(ind, val) {
                if (ind < model.limitationRows) {
                    tags.statusPaymentsPopup.push(val);
                }
            });

            return tags;
        }
    });

})(Sales.module("Models.Main"));
