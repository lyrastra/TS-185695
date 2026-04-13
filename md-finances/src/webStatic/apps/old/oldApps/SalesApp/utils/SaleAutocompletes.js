import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* eslint-disable */
(function($, md) {
    'use strict';

    $.fn.mdSaleAutocomplete = function(action, options) {
        var autocomplete = $(this).data('mdSaleAutocomplete');
        if (!autocomplete) {
            return;
        }

        switch (action) {
            case 'destroy':
                autocomplete.destroy();
                break;
            case 'search':
                autocomplete.search(options);
                break;
        }
    };

    $.fn.subcontoAutocompleteForPostings = function(options) {
        var defaultSettings = {
            addLink: true
        };
        var autocomplete = new mdSaleAutocomplete({
            url: (options.url) ? options.url : WebApp.Subcontos.GetSubcontosAutocompleteForPostings,
            el: $(this),
            className: 'kontragentSaleAutocomplete',
            onSelect: options.onSelect,
            onCreate: options.onCreate,
            onBlur: options.onBlur,
            data: options.getData,
            settings: _.extend(defaultSettings, options.settings)
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
                return { label: name, value: item.Name, object: item };
            });
        };

        autocomplete.onCreate = function() {
            if (_.isUndefined(options.onCreate)) {
                return;
            }
            options.onCreate(autocomplete);
        };

    };

    $.fn.salesFilterKontragentAutocomplete = function(options) {
        var url = WebApp.KontragentsAutocomplete;

        var autocomplete = new mdSaleAutocomplete({
            url: url,
            el: $(this),
            className: 'kontragentSaleAutocomplete',
            onSelect: options.onSelect,
            data: { 'docType': options.docType }
        });
        autocomplete.onBlur = options.onBlur;
    };

    $.fn.saleProjectAutocomplete = function(options) {
        /// <summary>автокомплит договоров</summary>
        var autocomplete = new mdSaleAutocomplete({
            url: Contracts.Autocomplete,
            el: $(this),
            clasName: 'projectSaleAutocomplete',
            onSelect: options.onSelect,
            onCreate: options.onCreate,
            checkPreloadedValue: options.checkPreloadedValue,
            data: options.getData,
            settings: $.extend({}, options.settings)
        });

        if (_.isUndefined(options.showNotFoundMessage)) {
            options.showNotFoundMessage = true;
        }

        autocomplete.parse = function(data) {
            if (options.showNotFoundMessage) {
                if (!data.length) {
                    return [{label: 'Договор не найден', value: '', object: {Id: null, Number: '', Sum: 0, New: true}}];
                }
            }
            return $.map(data, function(item) {
                var number = item.Number.length ? '№ ' + item.Number : 'б/н';

                var projectDate = item.Date;
                var date = Converter.toDate(item.Date);
                if (date) {
                    var currentYear = (new Date()).getFullYear();
                    var dateFormat = (currentYear != date.getFullYear()) ? 'D MMMM YYYY' : 'D MMMM';
                    projectDate = dateHelper(date).format(dateFormat);
                }

                var val = projectDate.length ? number + ' от ' + projectDate : number;
                return { label: val, value: item.Number, object: item };
            });
        };

        autocomplete.onBlur = options.onBlur;
    };

    $.fn.saleUnitAutocomplete = function(options) {
        /// <summary>автокомплит единиц измерения</summary>
        ///
        var autocomplete = new mdSaleAutocomplete({
            url: WebApp.ClosingDocumentsOperation.GetUnitAutocomplete,
            el: $(this),
            clasName: 'unitSaleAutocomplete',
            onBlur: options.onBlur,
            onSelect: options.onSelect,
            settings: {
                onlyFromList: false
            }
        });

        autocomplete.parse = function(data) {
            return $.map(data, function(item) {
                return { label: item.NameOfItem, value: item.NameOfItem, object: item };
            });
        };
    };

    $.fn.salesEmailAutocomplete = function(options) {

        var autocomplete = new mdSaleAutocomplete({
            url: WebApp.Kontragents.GetKontragentsByEmailAutocomplete,
            el: $(this),
            onSelect: options.onSelect,
            data: options.getData
        });

        autocomplete.parse = function(data) {
            return $.map(data, function(item) {
                return { label: item.Name + ' (' + item.Email + ')', object: item };
            });
        };
    };

    $.fn.saleProductAutocomplete = function(options) {
        /// <summary>автокомплит наименований для таблиц продажных документов</summary>
        var url = options.isStockEnabled ? StockApp.Stock.GetProductAutocomplete : WebApp.ClosingDocumentsOperation.GetProductAutocomplete;
        if (options.url) {
            url = options.url;
        }
        options.onlyFromList = options.onlyFromList || options.isStockEnabled;

        var autocomplete = new mdAutocomplete({
            url: url,
            el: $(this),
            className: 'productAutocomplete',
            onSelect: options.onSelect,
            data: options.getData,
            settings: {
                onlyFromList: false,
                addLink: options.addLink || options.isStockEnabled,
                createEventIfEmptyList: !_.isUndefined(options.createEventIfEmptyList) ? options.createEventIfEmptyList : false,
                addLinkName: options.addLinkName || 'новый товар / материал'
            },
            type: options.type,
            onBlur: options.onBlur
        });

        var parseFunction = function(data) {
            var mapFunc = function(item) {
                if (item.ProductId > 0) {
                    return {
                        label: item.Name, value: item.Name, object: {
                            Article: item.Article,
                            ShortName: item.Name,
                            Unit: item.UnitOfMeasurement,
                            UnitCode: item.UnitCode,
                            Price: item.SalePrice,
                            StockProductId: item.ProductId,
                            Nds: item.NDS,
                            NdsPositionType: item.NdsPositionType,
                            Type: item.Type,
                            Count: item.Count,
                            StockCounts: item.StockProductCounts,
                            PrimaryDocumentItemType: item.PrimaryDocumentItemType
                        }
                    };
                } else {
                    return {label: item.ShortName || item.Name, value: item.ShortName || item.Name, object: item};
                }
            };

            return $.map(data, function(item) {
                return mapFunc(item);
            });
        };

        autocomplete.parse = options.customParse || parseFunction;

        if (autocomplete.settings.addLink) {
            autocomplete.onCreate = function() {
                // Здесь создаётся диалог добавления нового товара / материала
                PrimaryDocuments.Utils.StockProductDialogHelper.showDialog(null, function(model) {
                    // Save function
                    options.onSelect({ object: model });
                }, function() {
                    // Cancel function
                }, {
                    // Если БИЗ-склад включен - работаем всегда с товарами
                    // https://confluence.moedelo.org/pages/viewpage.action?pageId=14194423
                    bizStockEnabled: options.bizStockEnabled,
                    defaultData: options.defaultData
                });
            };
        }

        return autocomplete;
    };

    $.fn.saleCountryAutocomplete = function(options) {
        /// <summary>автокомплит стран</summary>
        ///
        var autocomplete = new mdSaleAutocomplete({
            url: WebApp.ClosingDocumentsOperation.GetCountryAutocomplete,
            el: $(this),
            clasName: 'countrySaleAutocomplete',
            onSelect: options.onSelect,
            data: options.getData
        });
    };

    $.fn.saleBankAutocomplete = function(options) {
        /// <summary>автокомплит банков</summary>
        ///
        var input = $(this);

        var autocomplete = new mdSaleAutocomplete({
            url: WebApp.Banks.GetBanksAutocomplete,
            el: input,
            className: 'bankSaleAutocomplete',
            onSelect: options.onSelect,
            settings: {
                count: 5,
                selectFirst: true
            }
        });

        autocomplete._renderItem = function(item) {
            var link = $('<a></a>');
            link.addClass('ac_result');

            var replaceTerm = function(str) {
                var term = input.val();
                var re = new RegExp('(' + term + ')', 'gi');
                return str.replace(re, "<span class='mdAutocomplete-selectedTerm'>$1</span>");
            };

            var desc = 'БИК: ' + item.object.Bik;
            link.html("<div class='ac_label'>" + replaceTerm(item.label) + "</div><div class='ac_desc'>" + replaceTerm(desc) + '</div>');

            return link;
        };
    };


    $.fn.syntheticAccountAutocomplete = function(options) {
        /// <summary>автокомплит по плану счетов </summary>
        var autocomplete = new mdSaleAutocomplete({
            url: '',
            el: $(this),
            className: 'saleDocumentsAutocomplete',
            onSelect: options.onSelect,
            data: options.getData
        });

        $(this).attr('data-syntheticAccountAutocomplete', true);

        autocomplete.search = function(settings) {
            var query = options.query || autocomplete.el.val();

            var result = options.dataList.toJSON();

            if (options.dataFilter && _.isFunction(options.dataFilter)) {
                result = _.filter(result, options.dataFilter);
            }

            result = _.filter(result, function(item) {
                return item.Number.indexOf(query) != -1 && !isEmptyAccount(item);
            });

            autocomplete.lastSearch = query;
            autocomplete.collection = autocomplete.parse(_.first(result, 5));
            autocomplete.currentItem = -1;
            autocomplete.showItems();

        };

        autocomplete.parse = function(data) {
            return $.map(data, function(item) {
                var label = item.Number + ' - ' + item.Name,
                    value = item.Number;

                return { label: label, value: value, object: item };
            });
        };

        autocomplete.onBlur = options.onBlur;

        function isEmptyAccount(item) {
            return item.BalanceType == -1;
        }
    };

    $.fn.subcontoAutocomplete = function(options) {
        /// <summary> Aвтокомплит субконто в остатках. </summary>
        var autocompleteSettings = options.settings || {};
        if (_.isUndefined(options.settings.createEventIfEmptyList)){
            autocompleteSettings.createEventIfEmptyList = false;
        }
        var autocomplete = new mdSaleAutocomplete({
            url: options.Url ? options.Url : WebApp.Subcontos.GetSubcontosAutocomplete,
            el: $(this),
            clasName: 'subcontosAutocomplete',
            onSelect: options.onSelect,
            data: options.getData,
            settings: autocompleteSettings,
            type: options.type || 'post'
        });

        autocomplete.parse = function(data) {
            return $.map(data, function(item) {
                return { label: item.Name, value: item.Name, object: item };
            });
        };
        autocomplete.onCreate = function() {
            if (_.isUndefined(options.settings.onCreate)) {
                return;
            }
            options.settings.onCreate();
        };
    };

}(jQuery, Md));
