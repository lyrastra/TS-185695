/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function ($) {


    $.fn.BudgetaryAutocomplete = function (options) {
        /// <summary>автокомплит по плану счетов </summary>
        var autocomplete = new mdSaleAutocomplete({
            url: BankUrl.GetBudgetaryTaxesFeesContributions,
            el: $(this),
            className: "saleDocumentsAutocomplete",
            onSelect: options.onSelect,
            data: options.getData
        });

        autocomplete.search = function (settings) {
            var query = options.query || autocomplete.el.val();
            var result = _.filter(options.dataList, function (item) {
                return item.Name.toLowerCase().indexOf(query.toLowerCase()) != -1 || item.FullNumber.toLowerCase().indexOf(query.toLowerCase()) != -1;
            });

            autocomplete.lastSearch = query;
            autocomplete.collection = autocomplete.parse(_.first(result, 5));
            autocomplete.currentItem = -1;
            autocomplete.showItems();

        };

        autocomplete.parse = function (data) {
            return $.map(data, function (item) {

                var label = item.FullNumber + " " + item.Name,
                    value = item.Name;

                return { label: label, value: value, object: item };
            });
        };

        autocomplete.onBlur = options.onBlur;
    };

    function bankReasonAutocomplete(url, options) {
        /// <summary>автокомплит по вх. накладным, вх.актам, договорам и авансам </summary>
        var autocomplete = new mdSaleAutocomplete({
            url: url,
            el: $(this),
            onSelect: options.onSelect,
            data: options.getData,
            className: 'nowrap'
        });

        autocomplete.parse = function (data) {
            return $.map(data, function (item) {
                return { label: createAutoCompleteLabel(item), value: item.DocumentName, object: item };
            });
        };

        autocomplete.onBlur = options.onBlur;
    }

    function createAutoCompleteLabel(item){
        var formattedDate = formatItemDate(item.DocumentDate);
        return formattedDate ? String.format('№{0} от {1}', item.DocumentName, formattedDate) : String.format('№{0}', item.DocumentName);
    }

    function formatItemDate(dateString)
    {
        var date = dateHelper(dateString, 'DD.MM.YYYY').toDate();

        if (date) {
            var currentYear = (new Date()).getFullYear();
            var dateFormat = (currentYear != date.getFullYear()) ? 'D MMMM YYYY' : 'D MMMM';
            return dateHelper(date).format(dateFormat);
        } else {
            return '';
        }
    }

    $.fn.bankIncomingReasonAutocomplete = function (options) {
        bankReasonAutocomplete.call(this, BankUrl.GetIncomingReasonDocumentAutocomplete, options);
    };

    $.fn.bankInventaryCardReasonAutocomplete = function (options) {
        bankReasonAutocomplete.call(this, '/Accounting/InventoryCard/GetInventoryCardForPaymentAutocomplete', options);
    };

    $.fn.bankOutgoingReasonAutocomplete = function (options) {
        bankReasonAutocomplete.call(this, BankUrl.GetOutgoingReasonDocumentAutocomplete, options);
    };

    $.fn.bankMediationFeeReasonAutocomplete = function (options) {
        bankReasonAutocomplete.call(this, BankUrl.GetMediationFeeReasonDocumentAutocomplete, options);
    };

    $.fn.bankBankAutocomplete = function (options) {
        /// <summary>Автокомплит по банкам</summary>

        var autocomplete = new mdSaleAutocomplete({
            url: BankUrl.GetBankAutocomplete,
            el: $(this),
            onSelect: options.onSelect
        });

        autocomplete.parse = function (data) {
            return $.map(data, function (item) {

                return { label: item.Name + " // " + item.Bik, value: item.Name, object: item };
            });
        };

        autocomplete.onBlur = options.onBlur;
    };

    // автокомплит по списку касс организации
    $.fn.cashAutocomplete = function (options) {
        var autocomplete = new mdSaleAutocomplete({
            url: BankUrl.GetCashForFirm,
            el: $(this),
            className: "cashAutocomplete",
            onSelect: options.onSelect,
            data: options.getData
        });

        autocomplete.parse = function (data) {
            return $.map(data, function (item) {
                return { label: item.Name, value: item.Name, object: item };
            });
        };

        autocomplete.onBlur = options.onBlur;
    };
} (jQuery));
