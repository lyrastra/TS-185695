(function (md, bankUrl) {

    md.Services = md.Services || {};

    md.Services.TradingObjectService = {

        loadList: function () {
            var promise = $.Deferred();

            $.ajax(
                {
                    url: bankUrl.PaymentOrders.GetTradingObjectsList,
                    type: "GET",
                    contentType: "application/json; charset=utf-8;",
                    dataType: "json",
                    success: function (response) {
                        promise.resolve(response.List);
                    }
                }
            );

            return promise;
        }
    };
})(Md, BankUrl);