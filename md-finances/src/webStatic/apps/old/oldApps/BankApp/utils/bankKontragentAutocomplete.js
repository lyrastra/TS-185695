(function($, md) {
    'use strict';

    $.fn.bankKontragentAutocomplete = function(options) {
        /// <summary>автокомплит контрагентов</summary>
        var defaultSettings = {
            addLink: true
        };

        var autocomplete = new mdSaleAutocomplete({
            url: options.url || BankUrl.GetKontragentsForAccountingPaymentOrderAutocomplete,
            el: $(this),
            className: 'kontragentSaleAutocomplete',
            onSelect: options.onSelect,
            onBlur: options.onBlur,
            data: options.getData,
            settings: _.extend(defaultSettings, options, options.settings)
        });

        autocomplete.parse = function(data) {
            return $.map(data, function(item) {
                var name = item.Name;
                if (item.SettlementAccount && item.SettlementAccount.length) {
                    name += ' // р/с ' + item.SettlementAccount;
                    if (item.BankName && item.BankName.length) {
                        name += ' в ' + item.BankName;
                    }
                }

                return {label: name, value: item.Name, object: item};
            });
        };

        autocomplete.onCreate = function() {
            _showKontragentDialog.call(autocomplete, options);
        };
    };

    function _showKontragentDialog(options) {
        var dialog = new md.Core.Components.mdKontragentDialog.Component({
            defaultType: _getDefaultType(options),
            defaultName: this.el.val(),
            handlers: _getDialogHandlers.call(this, options)
        });
        dialog.show();
    }

    function _getDialogHandlers(options) {
        var $input = this.el;

        return {
            onSave: function(resultData) {
                $input.val(resultData.Name).trigger('blur', {mdSaleAutocomplete: true});
                options.onSelect({object: resultData});
            },
            onCancel: function() {
                $input.val('').trigger('change');
            }
        };
    }

    function _getDefaultType(options) {
        var type = md.Data.Enums.KontragentType.Seller;

        if (options.IsIncoming) {
            type = md.Data.Enums.KontragentType.Buyer;
        }

        return type;
    }
}(jQuery, Md));
