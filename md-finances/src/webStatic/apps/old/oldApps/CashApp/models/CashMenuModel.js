(function (cash) {

    cash.Models.CashMenuModel = cash.Models.BaseApplicationModel.extend({

        url: cash.Data.GetCashsForFirm,
        
        parse: function (resp) {
            return resp.List;
        }

    });
    
})(Cash);