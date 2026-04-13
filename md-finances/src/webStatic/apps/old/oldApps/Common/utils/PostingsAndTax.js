(function(module, common) {

    module.PostingsAndTax = {        
        taxPostingsStatusDescription: function (statusCode) {
            var statuses = common.Data.TaxPostingStatus;
            switch (statusCode) {
                case statuses.NotTax:
                    return "Не обл";
                case statuses.ByHand:
                    return "В ручн";
                case statuses.Yes:
                    return "Да";
                case statuses.No:
                    return "Нет";
                case statuses.ByLinkedDocument:
                    return "По связ";
            }
        }
    };

})(window, Common);