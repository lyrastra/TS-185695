import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* eslint-disable */
(function($) {
    'use strict';

    var _mapItems = function(items) {
        return $.map(items, function(item) {
            return { label: item.Name, value: item.Name, object: item };
        });
    };

    var mdAutocompleteUtil = Md.Core.Helpers.mdAutocompleteUtil = {
        defaultOptions: {
            onlyFromList: false,
            onSelect: function(event, ui) {},
            onBlur: function() {},
            afterSelect: function(event, ui) {}
        },

        init: function(elem, options, mdOptions) {
            var inputControl = elem;

            if (!mdOptions) {
                mdOptions = {};
            }

            if (inputControl.is(':data(autocomplete)')) {
                inputControl.autocomplete('destroy');
            }
            inputControl.autocomplete({
                source: function(request, response) {
                    $.ajax({
                        type: 'POST',
                        url: mdOptions.url,
                        contentType: 'application/json',
                        dataType: 'json',
                        data: $.toJSON(_.extend({ query: request.term }, _.isFunction(mdOptions.data) ? mdOptions.data() : mdOptions.data)),
                        success: function(data) {
                            if (_.isFunction(mdOptions.onSuccess)) {
                                mdOptions.onSuccess(data, request, response);
                            } else {
                                var result = data.hasOwnProperty('d') ? data.d : data;
                                if (result.Result) {
                                    response(_mapItems(result.Items));
                                }
                            }
                        }
                    });
                },
                open: function(event, ui) {
                    var data = $(this).data('autocomplete') || $(this).data('uiAutocomplete');
                    var $list = data.widget();
                    var items = $list.children('.ui-menu-item');
                    if (items.length === 1 && items.data('uiAutocompleteItem').object.New) {
                        return;
                    }

                    if ($list.zIndexer) {
                        $list.zIndexer('max');
                    }

                    data.menu.element.addClass('money-autocomplete');
                },
                delay: 200,
                autoFocus: true,
                minLength: 0,
//                select: options.onSelect,
                select: function(event, ui) {
                    mdOptions.onSelect(event, ui);
                    mdOptions.afterSelect(event, ui);
                },
                close: options.onClose,
                onlyFromList: options.onlyFromList,
                autoSelect: mdOptions.autoSelect
            }).blur(function() {
                var isEmpty = false,
                    selectedItem = $(this).data('selectedItem');

                if ((mdOptions.autoSelect || _.result(mdOptions, 'onlyFromList')) && !selectedItem && !_.result(mdOptions, 'defaultValue')) {
                    $(this).val('').change();
                    isEmpty = true;
                }

                if (_.result(mdOptions, 'onlyFromList') && mdOptions.autoSelect) {
                    if (selectedItem && !selectedItem.value.length) {
                        $(this).val('').change();
                        isEmpty = true;
                    }
                }

                mdOptions.onBlur && mdOptions.onBlur(isEmpty);
            });

            var searchOnClick = function() {
                if ($(this).val() == 'Все контрагенты' || $(this).val() == 'Все сотрудники') {
                    $(this).autocomplete('search', '');
                } else {
                    $(this).autocomplete('search', $(this).val());
                }
            };
            inputControl.unbind('click');
            inputControl.unbind('input');
            inputControl.bind('click', searchOnClick);

            inputControl.on('autocompleteselect', function(event, ui) {
                inputControl.data('selectedItem', ui.item);
            });

            inputControl.on('autocompleteopen', function() {
                inputControl.data('selectedItem', null);
            });

            if (_.result(mdOptions, 'onlyFromList')) {
                inputControl.on('keyup', function() {
                    inputControl.data('selectedItem', null);
                });
            }
        }
    };

    function preg_quote(str) {
        return (str + '').replace(/([\\\.\+\*\?\[\^\]\$\(\)\{\}\=\!\<\>\|\:])/g, '\\$1');
    }

    /// Автокомплит для выбора только из контрагентов и клиентов
    $.fn.clientAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (!result.List.length) {
                    response([
                        {
                            label: 'Будет добавлен новый контрагент',
                            value: request.term,
                            object: { Id: 0, Type: 1, Name: request.term, New: true }
                        }
                    ]);
                } else {
                    response($.map(result.List, function(item) {
                        return { label: item.Name, value: item.Name, object: item };
                    }));
                }
            }
        };
        var moneyOptions = {
            url: window.KontragentsApp.Autocomplete.KontragentWithTypeAutocomplete,
            onSuccess: onSuccessCallback,
            data: { type: 2 },
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            afterSelect: Options.afterSelect,
            autoSelect: true,
            defaultValue: Options.defaultValue
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    /// Автокомплит для выбора только из контрагентов
    $.fn.kontragentsAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {

                if (!result.List.length) {
                    if (!Options.onlyFromList) {
                        response([
                            {
                                label: 'Будет добавлен новый контрагент',
                                value: request.term,
                                object: { Id: 0, Type: 1, Name: request.term, New: true }
                            }
                        ]);
                    } else {
                        response([
                            {
                                label: 'Контрагент не найден',
                                value: [],
                                object: { Id: 0, Type: 1, Name: '', New: true }
                            }
                        ]);
                    }
                } else {
                    response($.map(result.List, function(item) {
                        return { label: item.Name, value: item.Name, object: item };
                    }));
                }
            }
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var moneyOptions = {
            url: window.KontragentsApp.Autocomplete.KontragentWithTypeAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            autoSelect: true,
            onlyFromList: Options.onlyFromList,
            afterSelect: Options.afterSelect,
            defaultValue: Options.defaultValue
        };

        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    /// Автокомплит для субконто
    ///
    $.fn.subcontoAutocompleteForMissingTransactions = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(items, request, response) {
            var isSubstring = _.every(items, function(item) {
                return item.Name.trim().toLowerCase() !== request.term.trim().toLowerCase();
            });

            if (options.getData().accountingNumber == "86" && (!items.length || isSubstring)) {
                response([{
                    label: 'Будет добавлена новая субсидия',
                    value: request.term.trim(),
                    object: { SubcontoId: -1, Name: request.term.trim(), IsNew: true }
                }].concat(_.map(items, function(item) {
                    return { label: item.Name, value: item.Name, object: item }
                })));
            } else {
                response($.map(items, function(item) {
                    return { label: item.Name, value: item.Name, object: item };
                }));
            }
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var mdOptions = {
            url: window.WebApp.BalanceAndIncomeReport.SubcontoAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            autoSelect: true,
            onlyFromList: Options.onlyFromList,
            afterSelect: Options.afterSelect,
            defaultValue: Options.defaultValue
        };

        mdAutocompleteUtil.init(this, Options, mdOptions);
    };

    /// Автокомплит для дебита и кредита
    $.fn.accountingNumberAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            response($.map(data, function(item) {
                return {
                    label: item.AccountingNumber + ' - ' + item.AccountingName,
                    value: item.AccountingNumber,
                    object: item
                };
            }));
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var mdOptions = {
            url: window.WebApp.BalanceAndIncomeReport.AccountingNumberAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            autoSelect: true,
            onlyFromList: Options.onlyFromList,
            afterSelect: Options.afterSelect,
            defaultValue: Options.defaultValue
        };

        mdAutocompleteUtil.init(this, Options, mdOptions);
    };

    /// Автокомплит для продуктов
    $.fn.productAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            response($.map(data, function(item) {
                return { label: item.Name, value: item.Name, object: item };
            }));
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var mdOptions = {
            url: window.WebApp.BalanceAndIncomeReport.ProductAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            autoSelect: true,
            onlyFromList: Options.onlyFromList,
            afterSelect: Options.afterSelect,
            defaultValue: Options.defaultValue
        };

        mdAutocompleteUtil.init(this, Options, mdOptions);
    };

    /// Автокомплит по складам
    $.fn.stockAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            response($.map(data, function(item) {
                return { label: item.Name, value: item.Name, object: item };
            }));
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var mdOptions = {
            url: window.WebApp.BalanceAndIncomeReport.StockAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            autoSelect: true,
            onlyFromList: Options.onlyFromList,
            afterSelect: Options.afterSelect,
            defaultValue: Options.defaultValue
        };

        mdAutocompleteUtil.init(this, Options, mdOptions);
    };


    /// Автокомплит счетов для списаний
    $.fn.billOutgoingAutocomplete = function(options) {
        if (!options) {
            options = {};
        }

        if (!_.isFunction(options.canCreate)) {
            options.canCreate = function() {
                return false;
            };
        }

        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {

                    if (Options.canCreate()) {
                        response([
                            {
                                label: 'Добавить новый счет',
                                value: request.term,
                                object: { Id: 0, Number: request.term, New: true }
                            }
                        ]);
                    } else {
                        response([
                            { label: 'Счет не найден', value: '', object: { Id: 0, Number: '', New: true } }
                        ]);
                    }
                } else {
                    response($.map(result.List, function(item) {
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
                        //                         return { label: item.Number, value: item.Number, object: item };
                    }));
                }
            }
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var moneyOptions = {
            url: window.WebApp.Bills.GetBillsForOutgoingAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            afterSelect: Options.afterSelect
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    /// Автокомплит для выбора счетов для поступлений
    $.fn.billIncomingAutocomplete = function(options) {
        if (!options) {
            options = {};
        }

        if (!_.isFunction(options.canCreate)) {
            options.canCreate = function() {
                return false;
            };
        }

        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {
                    if (Options.canCreate()) {
                        response([
                            {
                                label: 'Добавить новый счет',
                                value: request.term,
                                object: { Id: 0, Number: request.term, New: true }
                            }
                        ]);
                    } else {
                        response([
                            { label: 'Счет не найден', value: '', object: { Id: 0, Number: '', New: true } }
                        ]);
                    }
                } else {
                    response($.map(result.List, function(item) {
                        var number = item.Number.length ? '№ ' + item.Number : 'б/н';

                        var projectDate = item.Date;
                        var date = Converter.toDate(item.Date);
                        if (date) {
                            var currentYear = (new Date()).getFullYear();
                            var dateFormat = (currentYear != date.getFullYear()) ? 'D MMMM YYYY' : 'D MMMM';
                            projectDate = dateHelper(date).format(dateFormat);
                        }

                        var val = projectDate.length ? number + ' от ' + projectDate : number;

                        if (!_.isUndefined(item.Sum)) {
                            val += ' на сумму ' + Converter.toAmountString(item.Sum);
                        }

                        return { label: val, value: item.Number, object: item };

                        //                         return { label: item.Number, value: item.Number, object: item };
                    }));
                }
            }
        };
        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };
        var moneyOptions = {
            url: window.WebApp.Bills.GetBillsForIncomingAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            defaultValue: Options.defaultValue,
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            afterSelect: Options.afterSelect,
            autoSelect: true
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    /// Автокомплит накладных для списаний
    $.fn.waybillAutocomplete = function(options) {
        if (!_.isFunction(options.canCreate)) {
            options.canCreate = function() {
                return true;
            };
        }
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {
//                    response([{ label: 'Добавить новую накладную', value: request.term, object: { Id: 0, Number: request.term}}]);
                    response([]);

                } else {
                    response($.map(result.List, function(item) {
                        return { label: item.Number, value: item.Number, object: item };
                    }));
                }
            }
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };
        var moneyOptions = {
            url: Options.moneyTransferOperationType == window.Enums.MoneyTransferOperationTypes.Incoming ?
                window.WebApp.Waybills.GetWaybillsForIncomingAutocomplete :
                window.WebApp.Waybills.GetWaybillsForOutgoingAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            defaultValue: Options.defaultValue,
            onlyFromList: Options.onlyFromList,
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            autoSelect: Options.autoSelect,
            afterSelect: Options.afterSelect
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    /// Автокомплит подтверждающих актов для списаний
    $.fn.statementsAutocomplete = function(options) {
        if (!_.isFunction(options.canCreate)) {
            options.canCreate = function() {
                return true;
            };
        }

        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {
                    response([]);
//                    response([{ label: 'Добавить новый акт', value: request.term, object: { Id: 0, Number: request.term}}]);
                } else {
                    response($.map(result.List, function(item) {
                        return { label: item.Number, value: item.Number, object: item };
                    }));
                }
            }
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var moneyOptions = {
            url: Options.moneyTransferOperationType == window.Enums.MoneyTransferOperationTypes.Incoming ?
                window.WebApp.Statements.GetStatementsForIncomingAutocomplete :
                window.WebApp.Statements.GetStatementsForOutgoingAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onlyFromList: Options.onlyFromList,
            onSelect: Options.onSelect,
            afterSelect: Options.afterSelect
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    /// Автокомплит для выбора из учредителей
    $.fn.partnerAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {
                    response([
                        {
                            label: 'Будет добавлен новый учредитель',
                            value: request.term,
                            object: { Id: 0, Name: request.term, New: true }
                        }
                    ]);
                } else {
                    response($.map(result.List, function(item) {
                        return { label: item.Name, value: item.Name, object: item };
                    }));
                }
            }
        };
        var moneyOptions = {
            url: window.WebApp.Kontragents.GetFoundersAutocomplete,
            onSuccess: onSuccessCallback,
            onSelect: Options.onSelect,
            afterSelect: Options.afterSelect
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    /// Автокомплит для выбора из контрагентов, сотрудников и ИП
    $.fn.kontragentAndWorkersAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (!result.List.length) {
                    if (!Options.onlyFromList) {
                        response([
                            {
                                label: 'Будет добавлен новый контрагент',
                                value: request.term,
                                object: { Id: 0, Type: 1, Name: request.term, New: true }
                            }
                        ]);
                    } else {
                        response([
                            {
                                label: 'Контрагент не найден',
                                value: [],
                                object: { Id: 0, Type: 1, Name: '', New: true }
                            }
                        ]);
                    }
                } else {
                    response($.map(result.List, function(item) {
                        if (item.Type == window.Enums.KontragentTypes.Ip) {
                            item.Id = 0;
                        }
                        return { label: item.Name, value: item.Name, object: item };
                    }));
                }
            }
        };
        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var moneyOptions = {
            url: window.WebApp.Kontragents.GetKontragentsAndWorkersAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onlyFromList: Options.onlyFromList,
            onSelect: Options.onSelect,
            autoSelect: true,
            afterSelect: Options.afterSelect,
            defaultValue: Options.defaultValue
        };

        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    /// Автокомплит для выбора из сотрудников и ИП
    $.fn.WorkersAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);

        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {
                    response([
                        { label: 'Сотрудник не найден', value: '', object: { Id: 0, Type: -1, Name: '', New: true } }
                    ]);
                } else {
                    response($.map(result.List, function(item) {
                        if (item.Type == window.Enums.KontragentTypes.Ip) {
                            item.Id = 0;
                        }
                        return {
                            label: item.Name,
                            value: item.Name,
                            object: item
                        };
                    }));
                }
            }
        };
        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };
        var moneyOptions = {
            url: Options.onlyWorkers ? window.WebApp.Kontragents.GetWorkersFromMoneyAutocomplete : window.WebApp.Kontragents.GetWorkersAndIpAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            afterSelect: Options.afterSelect,
            autoSelect: true,
            defaultValue: Options.defaultValue,
            onlyFromList: true
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    $.fn.moneyAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);

        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                response(_mapItems(result.List));
            }
        };

        var moneyOptions = {
            url: window.KontragentsApp.KontragentMoneyTable.GetAutocomplete,
            onSuccess: onSuccessCallback,
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            afterSelect: Options.afterSelect
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    $.fn.moneyWorkerAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);

        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                response($.map(result.List, function(item) {
                    return { label: item.Name, value: item.Name, object: item };
                }));
            }
        };

        var moneyOptions = {
            url: window.PayrollApp.MoneyTransferTable.WorkerAutocomplete,
            onSuccess: onSuccessCallback,
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            afterSelect: Options.afterSelect
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    $.fn.bankAutocomplete = function(options) {
        var autocomplete;
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                response($.map(result.List, function(item) {
                    return { label: item.Name, desc: 'БИК: ' + item.Bik, value: item.Name, object: item };
                }));
            }
        };

        var moneyOptions = {
            url: window.WebApp.Banks.GetBanksAutocomplete,
            onSuccess: onSuccessCallback,
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            afterSelect: Options.afterSelect
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
        autocomplete = $(this).data('autocomplete') || $(this).data('uiAutocomplete');
        autocomplete._renderItem = function(ul, item) {
            $(ul).addClass('ac_results');
            return $('<li>')
                .data('item.autocomplete', item)
                .append("<a class='ac_result'><div class='ac_label'>" + item.label + "</div><div class='ac_desc'><small>" + item.desc + '<small/></div></a>')
                .appendTo(ul);
        };
    };

    $.fn.advanceStatementAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);

        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                response($.map(result.List, function(item) {
                    var number = item.Number.length ? '№ ' + item.Number : 'б/н';
                    var val = number + ' за ' + item.Date + ' (' + item.Sum + ' р.)';
                    return { label: val, value: val, object: item };
                }));
            }
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var moneyOptions = {
            url: window.WebApp.AdvanceStatements.GetAdvanceStatementsAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            afterSelect: Options.afterSelect
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    $.fn.projectsOfFoundersAutocomplete = function(options) {
        if (!_.isFunction(options.canCreate)) {
            options.canCreate = function() {
                return false;
            };
        }

        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);

        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {
                    response([]);
                } else {
                    response($.map(result.List, function(item) {
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
                    }));
                }
            }
        };

        var getFounderFunc = _.isFunction(Options.getFounderId) ? Options.getFounderId : function() {
            return null;
        };

        var moneyOptions = {
            url: window.WebApp.Projects.GetProjectsOfFoundersAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return {
                    founderId: getFounderFunc()
                };
            },
            onSelect: Options.onSelect,
            afterSelect: Options.afterSelect
        };

        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    $.fn.primaryDocumentAutocomplete = function(options) {
        if (!_.isFunction(options.canCreate)) {
            options.canCreate = function() {
                return false;
            };
        }

        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);

        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (!result.List.length) {
                    if (Options.canCreate()) {
                        response([
                            {
                                label: 'Договор не найден',
                                value: request.term,
                                object: { Id: 0, Number: request.term, New: true }
                            }
                        ]);
                    } else {
                        response([
                            { label: 'Договор не найден', value: '', object: { Id: 0, Number: '', Sum: 0, New: true } }
                        ]);
                    }
                } else {
                    response($.map(result.List, function(item) {
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
                    }));
                }
            }
        };

        var getKontragentFunc = _.isFunction(Options.getKontragentId) ? Options.getKontragentId : function() {
            return null;
        };
        var moneyOptions = {
            url: window.WebApp.Projects.GetProjectsAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return _.extend({}, {
                    kontragentId: getKontragentFunc()
                }, options.data);
            },
            onSelect: Options.onSelect,
            onBlur: Options.onBlur,
            afterSelect: Options.afterSelect,
            autoSelect: true
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    $.fn.outgoingOperationAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);

        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {
                    response([
                        {
                            label: 'Операция не найдена',
                            value: '',
                            object: { Id: 0, Date: null, Sum: 0, Name: '', New: true }
                        }
                    ]);
                } else {
                    response($.map(result.List, function(item) {
                        var symbol = Money.Helpers.currencyHelper.getSymbol(item.Currency);
                        var symbolValue = symbol === 'р' ? symbol + '.' : symbol;
                        var name = item.Name + ' за ' + item.Date + ' (' + item.Sum + ' ' + symbolValue + ')';
                        return { label: name, value: name, object: item };
                    }));
                }
            }
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var moneyOptions = {
            url: window.WebApp.FinancialOperations.GetOutgoingMoneyTransferOperationsAutocomplete,
            onSuccess: onSuccessCallback,
            data: getDataFunc,
            onSelect: Options.onSelect,
            afterSelect: Options.afterSelect
        };

        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    $.fn.moneyBalanceWorkerAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);

        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {
                    response([
                        { label: 'Сотрудник не найден', value: '', object: { Id: 0, Type: -1, Name: '', New: true } }
                    ]);
                } else {
                    response($.map(result.List, function(item) {
                        return {
                            label: item.Name,
                            value: item.Name,
                            object: item
                        };
                    }));
                }
            }
        };
        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };
        var moneyOptions = {
            url: window.WebApp.Kontragents.GetWorkersAutocompleteForMoneyBalanceMaster,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            afterSelect: Options.afterSelect,
            autoSelect: Options.autoSelect
        };
        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    $.fn.moneyBalanceKontragentsAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {
                    response([
                        {
                            label: 'Будет добавлен новый контрагент',
                            value: request.term,
                            object: { Id: 0, Type: 1, Name: request.term, New: true }
                        }
                    ]);
                } else {
                    response($.map(result.List, function(item) {
                        return { label: item.Name, value: item.Name, object: item };
                    }));
                }
            }
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var moneyOptions = {
            url: '/Kontragents/Autocomplete/KontragentWithoutIdsAutocomplete',
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            afterSelect: Options.afterSelect
        };

        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

    $.fn.moneyBalanceKbkAutocomplete = function(options) {
        var Options = $.extend(_.clone(mdAutocompleteUtil.defaultOptions), options);
        var onSuccessCallback = function(data, request, response) {
            var result = data;
            if (result.Status) {
                if (result.List.length === 0) {
                    response([]);
                } else {
                    response($.map(result.List, function(item) {
                        return { label: item.Number, value: item.Number, object: item };
                    }));
                }
            }
        };

        var getDataFunc = _.isFunction(Options.getData) ? Options.getData : function() {
            return null;
        };

        var moneyOptions = {
            url: window.WebApp.Kbk.GetAutocomplete,
            onSuccess: onSuccessCallback,
            data: function() {
                return getDataFunc();
            },
            onSelect: Options.onSelect,
            afterSelect: Options.afterSelect
        };

        mdAutocompleteUtil.init(this, Options, moneyOptions);
    };

}(jQuery));
