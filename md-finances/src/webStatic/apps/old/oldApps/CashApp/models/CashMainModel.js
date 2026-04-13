(function (cash) {

    cash.Models.CashMainModel = cash.Models.BaseApplicationModel.extend({
         defaults: {
             Cash: false,
             CanEdit: false
         }
    });

})(Cash);