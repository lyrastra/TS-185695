import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* eslint-disable */
(function ($, primaryDocuments, common, md) {
    'use strict';

    var coreData = md.Data.Preloading;

    $.fn.paymentAutocomplete = function(options, directionText) {
        var autocomplete = new mdAutocomplete({
            url: options.url,
            el: $(this),
            className: 'invoiceReasonAutocomplete',
            onSelect: options.onSelect,
            onCreate: options.onCreate,
            data: options.getData,
            settings: $.extend({
                onlyFromList: true,
                addLink: false
            }, options.settings)
        });

        autocomplete.parse = function (data) {
            return $.map(data, function (item) {
                if(isAccounting()){
                    return accountingParse(item, directionText);
                } else{
                    return bizParse(item);
                }
            });
        };

        this.mapData = function (item) {
            var isBiz = !isAccounting() && !Md.Account.isOffice;
            if(isBiz){
                return documentTypeToAutocompleteStringForBiz(item);
            }
            return documentTypeToAutocompleteString(item, directionText) + String.format('№{0}', item.Number);
        };

        return this;
    };

    $.fn.outgoingPaymentAutocompleteBiz = function(options){
        options.url = '/Accounting/FinancialOperations/GetConfirmingOperationsAutocomplete';
        $.fn.paymentAutocomplete.call(this, options);
    };

    $.fn.outgoingPaymentAutocomplete = function(options){
        options.url = '/Accounting/LinkedDocuments/GetOutgoingPaymentsAutocomplete';
        $.fn.paymentAutocomplete.call(this, options, getOutgoingDirection);
    };

    $.fn.incomingPaymentAutocomplete = function(options){
        options.url = '/Accounting/LinkedDocuments/GetIncomingPaymentsAutocomplete';
        $.fn.paymentAutocomplete.call(this, options, getIncomingDirection);
    };

    function getOutgoingDirection(type) {
        return type == common.Data.AccountingDocumentType.PaymentOrder ? 'Исходящее ' : '';
    }

    function getIncomingDirection(type) {
        return type == common.Data.AccountingDocumentType.PaymentOrder ? 'Входящее ' : '';
    }

    /** @access private */
    function accountingParse(item, directionText) {
        var docName = documentTypeToAutocompleteString(item, directionText);
        var label = docName + String.format('№{0} от {1}', item.Number, getDate(item));
        var val = docName + String.format('№{0}', item.Number);
        return { label: label, value: val, object: item };

    }

    /** @access private */
    function bizParse(item) {
        var label = documentTypeToAutocompleteStringForBiz(item);
        return { label: label, value: label, object: item };
    }

    /** @access private */
    function documentTypeToAutocompleteString(item, directionText) {
        var direction = directionText(item.Type);
        if (direction.length > 0) {
            return common.Utils.Converter.capitaliseFirstLetter(direction + common.Data.DocumentTypeHelper.getAccountingDocumentTypeName(item.Type).toLowerCase());
        }
        return  common.Data.DocumentTypeHelper.getAccountingDocumentTypeName(item.Type);
    }

    /** @access private */
    function documentTypeToAutocompleteStringForBiz(item) {
        var isAdvanceStatement = item.Type === common.Data.AccountingDocumentType.AccountingAdvanceStatement;
        var number = item.Number ? '№ ' + item.Number : '';
        var sum = 'на сумму ' + item.OperationSum + ' р.';
        var date = getDate(item) ? 'от ' + getDate(item) : '';
        var label;

        if (isAdvanceStatement) {
            label = 'Авансовый отчет';
        } else if (item.IsCash) {
            label = 'РКО';
        } else {
            label = 'ПП';
        }

        return [label, number, sum, date].join(' ');
    }

    /** @access private */
    function getDate(item) {
        var date = Converter.toDate(item.Date);

        if (date) {
            var currentYear = (new Date()).getFullYear();
            var dateFormat = (currentYear != date.getFullYear()) ? 'D MMMM YYYY' : 'D MMMM';
            date = dateHelper(date).format(dateFormat);
        } else {
            date = '';
        }

        return date;
    }

    /** @access private */
    function isAccounting() {
        return coreData.Requisites && coreData.Requisites.IsAccounting;
    }

})(jQuery, PrimaryDocuments, Common, Md);
