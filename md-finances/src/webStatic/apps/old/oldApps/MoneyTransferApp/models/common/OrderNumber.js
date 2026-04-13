(function(money) {
    'use strict';

    money.Models.Common.OrderNumber = Backbone.Model.extend({
        initialize: initialize,
        parse: parse
    });

    function initialize(options) {
        options || (options = {});
        this.url = _getUrl(options.operationType, options.isBankFee);
    }

    function _getUrl(operationType, isBankFee) {
        var moneyTransferOperation = WebApp.MoneyTransferOperation;
        var moneyTransferOperationTypes = Enums.MoneyTransferOperationTypes;
        var result;

        if (isBankFee) {
            result = moneyTransferOperation.GetBankFeeOperationNextNumber;
        } else {
            if (operationType == moneyTransferOperationTypes.Outgoing) {
                result = moneyTransferOperation.GetOutgoingMoneyTransferOperationNextNumber;
            } else {
                result = moneyTransferOperation.GetIncomingMoneyTransferOperationNextNumber;
            }
        }

        return result;
    }

    function parse(response) {
        if (!response.Number) {
            response.Number = 1;
        }
        return response;
    }

})(Money);
