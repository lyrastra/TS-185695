/* eslint-disable */
import SyntheticAccountCodesEnum from '../../../../../../../enums/SyntheticAccountCodesEnum';

var paymentIntoBudgetPrefix = 68;

(function(common, md) {
    'use strict';

    var subcontoOptions = {
        Kontragent: getRequiredSubcontoFunc('Kontragent', 'Контрагент'),

        Stock: getRequiredSubcontoFunc('Stock', 'Склад'),

        Good: function(accountCode) {
            var isMaterial = isMaterialCode.call(this, accountCode);

            var object = {
                id: 'StockProduct',
                placeholder: isMaterial ? 'Материал' : 'Товар',
                autocomplete: nomenclatureAutocomplete(isMaterial),
                required: true
            };

            object.view = this.getInput(object.id, object.placeholder);
            return object;
        },
        Cashes: function(accountCode) {
            var object = {
                id: 'Cash',
                placeholder: 'Касса',
                autocomplete: function(input, view, type, obj) {
                    var options = {
                        onSelect: function(item) {
                            view.onSelectFunc(item, type);
                            view.changeAssotiateSpan(input, item.label || item.object.Name);
                        },
                        settings: {
                            addLink: false
                        },
                        url: accountCode === SyntheticAccountCodesEnum._50_01 ? WebApp.Subcontos.GetMainCashSubcontoAutocomplete : WebApp.Subcontos.GetOtherCashSubcontoAutocomplete
                    };

                    input.subcontoAutocompleteForPostings(options);

                    if (obj) {
                        if (obj.SubcontoId && obj.Id != obj.SubcontoId) {
                            obj.Id = obj.SubcontoId;
                        }

                        input.val(obj.Name);
                        var selectedItem = { object: obj };
                        view.onSelectFunc(selectedItem, type, { validate: false });
                        view.changeAssotiateSpan(input, obj.Name);
                    }

                    if (this.readOnly === true) {
                        input.attr('disabled', 'disabled');
                    }
                },
                required: true
            };

            object.view = this.getInput(object.id, object.placeholder);
            return object;
        },

        Kbk: function(accountCode, date) {
            var object = {
                id: 'Kbk',
                /** коды, начинающиеся с 68 относятся к платежам в бюджет, с 69 - во фонды */
                placeholder: Math.floor(accountCode / 10000) === paymentIntoBudgetPrefix ? 'Вид платежа в бюджет' : 'Вид платежа в фонды',
                autocomplete: kbkAutocomplete(accountCode, date),
                required: true
            };
            object.view = this.getInput(object.id, object.placeholder);

            return object;
        },

        SettlementAccount: function(accountCode) {
            var id = 'SettlementAccount',
                placeHolder = 'Банковский счёт';

            return {
                id: id,
                placeholder: placeHolder,
                autocomplete: function(input, view, type, object) {
                    var options = getSubcontoAutocompleteOptions.call(this, input, view, type, object);
                    options.url = '/Requisites/SettlementAccounts/SubcontoAutocomplete';

                    input.subcontoAutocompleteForPostings(_.extend(options, switchforAutocompliteDependingOnType(type, options, input, view)));
                },
                required: true,
                view: this.getInput(id, placeHolder)
            };
        },

        SeparateDivision: getRequiredSubcontoFunc('SeparateDivision', 'Подразделение'),
        NomenclatureGroup: getRequiredSubcontoFunc('NomenclatureGroup', 'Вид деятельности'),
        CostItems: getRequiredSubcontoFunc('CostItems', 'Статья затрат'),
        InvoicesRecieved: function(accountCode) {
            var object = {
                id: 'InvoicesRecieved',
                placeholder: 'Документ-основание',
                autocomplete: reasonDocumentAutokomplete(accountCode),
                required: true
            };

            object.view = this.getInput(object.id, object.placeholder);
            return object;
        },

        Worker: function(accountCode) {
            var isOtherDebitorsAndCreditors = parseInt(accountCode, 10) === SyntheticAccountCodesEnum._76_09;
            var object = {
                id: 'Worker',
                placeholder: isOtherDebitorsAndCreditors ? 'Исполнитель' : 'Сотрудник',
                autocomplete: subcontoAutokomplete,
                required: true
            };

            object.view = this.getInput(object.id, object.placeholder);

            return object;
        },

        BasicAsset: getRequiredSubcontoFunc('BasicAsset', 'Основное средство'),
        IntangibleAssets: getRequiredSubcontoFunc('IntangibleAssets', 'Нематериальные активы'),
        AcquisitionTargets: getRequiredSubcontoFunc('AcquisitionTargets', 'Объекты приобретения'),

        SpecialSettlementAccount: function(accountCode) {
            var id = 'SpecialSettlementAccount',
                placeholder = 'Специальные счета';

            return {
                id: id,
                placeholder: placeholder,
                autocomplete: function(input, view, type, object) {
                    var options = getSubcontoAutocompleteOptions.call(this, input, view, type, object);
                    options.getData.accountCode = accountCode;
                    options.url = '/Accounting/Subcontos/GetSubcontosByTypeAndCode';
                    input.subcontoAutocompleteForPostings(_.extend(options, switchforAutocompliteDependingOnType(type, options, input)));
                },
                required: true,
                view: this.getInput(id, placeholder)
            };
        },

        SpecialSettlementAccountForDigitalRuble: function(accountCode) {
            var id = 'SpecialSettlementAccountForDigitalRuble',
                placeholder = 'Специальные счета';

            return {
                id: id,
                placeholder: placeholder,
                autocomplete: function(input, view, type, object) {
                    var options = getSubcontoAutocompleteOptions.call(this, input, view, type, object);
                    options.getData.accountCode = accountCode;
                    options.url = '/Accounting/Subcontos/GetSubcontosByTypeAndCode';
                    input.subcontoAutocompleteForPostings(_.extend(options, switchforAutocompliteDependingOnType(type, options, input)));
                },
                required: true,
                view: this.getInput(id, placeholder)
            };
        },

        Securities: getRequiredSubcontoFunc('Securities', 'Ценные бумаги'),

        Contract: function(accountCode) {
            var object = {
                id: 'Contract',
                placeholder: 'Договор',
                autocomplete: function(input, view, type, object) {
                    var options = getSubcontoAutocompleteOptions.call(this, input, view, type, object);

                    var data = _.result(options, 'getData');
                    options.getData = function() {
                        var kontragentSubconto = view.getSubcontoDataByType(common.Data.SubcontoType.Kontragent || {});
                        return _.extend({ kontragentSubcontoId: kontragentSubconto.Id || -1 }, data);
                    };

                    options.url = '/Contract/Autocomplete/SubcontoAutocomplete';
                    input.subcontoAutocompleteForPostings(_.extend(options, switchforAutocompliteDependingOnType(type, options, input, view)));
                },
                required: true
            };

            object.view = this.getInput(object.id, object.placeholder);
            return object;
        },

        MiddlemanContract: getRequiredSubcontoFunc('MiddlemanContract', 'Посреднический договор'),
        EnforcementDocuments: getRequiredSubcontoFunc('EnforcementDocuments', 'Исполнительный документ'),
        AppointmentOfTrustFunds: getRequiredSubcontoFunc('AppointmentOfTrustFunds', 'Назначение целевых средств'),
        MovementOfTrustFunds: getRequiredSubcontoFunc('MovementOfTrustFunds', 'Движение целевых средств'),
        Deficit: getRequiredSubcontoFunc('Deficit', 'Номенклатура, ОС и др.'),
        UnbalanceNomenclature: getRequiredSubcontoFunc('UnbalanceNomenclature', 'Номенклатура'),
        UnbalanceStock: getRequiredSubcontoFunc('UnbalanceStock', 'Склад'),
        UnbalanceFixedAsset: getRequiredSubcontoFunc('UnbalanceFixedAsset', 'Основные средства'),
        OtherIncomeOrOutgo: getRequiredSubcontoFunc('OtherIncomeOrOutgo', 'Прочие доходы и расходы'),
        UseOfProfit: getRequiredSubcontoFunc('UseOfProfit', 'Направления использования прибыли'),
        FutureOutgoing: getRequiredSubcontoFunc('FutureOutgoing', 'Расходы будущих периодов'),

        getInput: function(id, placeholder) {
            return '<input type="text" class="medium" data-bind="' + id + '" placeholder="' + placeholder + '" id="' + id + '" name="' + id + '">';
        }
    };

    function getSubcontoCollection(postingModel, accountCode) {
        if (accountCode === postingModel.get('Debit')) {
            return postingModel.get('SubcontoDebit');
        }

        return postingModel.get('SubcontoCredit');
    }

    function getSubcontoFunc(id, name, required) {
        return function() {
            var object = {
                id: id,
                placeholder: name,
                autocomplete: subcontoAutokomplete
            };

            if (!_.isUndefined(required)) {
                object.required = required;
            }

            object.view = this.getInput(object.id, object.placeholder);
            return object;
        };
    }

    function getRequiredSubcontoFunc(id, name) {
        return getSubcontoFunc(id, name, true);
    }

    function getSubcontoAutocompleteOptions(input, view, type, object) {
        if (object) {
            input.val(object.Name);
            var selectedItem = { object: object };
            view.onSelectFunc(selectedItem, type, { validate: false });
            view.changeAssotiateSpan(input, object.Name);
        }

        if (this.readOnly === true) {
            input.attr('disabled', 'disabled');
            return;
        }

        return {
            onSelect: function(item) {
                view.onSelectFunc(item, type);
                view.changeAssotiateSpan(input, item.label || item.object.Name);
            },
            getData: { type: type },
            settings: {
                addLink: false
            },
            isStockEnabled: true
        };
    }

    var subcontoAutokomplete = function(input, view, type, object) {
        var options = getSubcontoAutocompleteOptions.call(this, input, view, type, object);
        if (options) {
            _.extend(options, switchforAutocompliteDependingOnType(type, options, input, view));
            input.subcontoAutocompleteForPostings(options);
        }
    };

    var kbkAutocomplete = function(accountCode, date) {
        var isOtherTaxes = accountCode === SyntheticAccountCodesEnum._68_10;

        return function(input, view, type, obj) {
            var onSelect = function(item) {
                view.onSelectFunc(item, type);
                view.changeAssotiateSpan(input, item.label || item.object.Name);
            };

            var options = {
                onlyFromList: true,
                onSelect: onSelect,
                onBlur: function() {
                    var obj = {
                        Name: input.val(),
                        Id: null
                    };
                    onSelect({ object: obj });
                },
                getData: {
                    accountCode: accountCode,
                    type: common.Data.SubcontoType.Kbk,
                    date: date
                }
            };

            _.extend(options, switchforAutocompliteDependingOnType(type, options, input));
            if (isOtherTaxes) {
                options.settings = { addLink: false, onlyFromList: true };
                input.subcontoAutocompleteForPostings(options);
            } else {
                input.kbkAutocomplete(options);
            }

            if (obj) {
                input.val(obj.Name);
                input.next().text(obj.Name);
            }
        };
    };

    var reasonDocumentAutokomplete = function(accountCode) {
        return function(input, view, type, obj) {
            var options = {
                onSelect: function(item) {
                    view.onSelectFunc(item, type);
                    view.changeAssotiateSpan(input, item.label || item.object.Name);
                },
                getData: function() {
                    return {
                        accountCode: accountCode
                    };
                },
                settings: {
                    addLink: false
                }
            };

            input.reasonDocumentAutocomplete(_.extend(options, switchforAutocompliteDependingOnType(type, options, input)));

            if (obj) {
                input.val(obj.Name);
                input.next().text(obj.Name);
            }
        };
    };

    var nomenclatureAutocomplete = function(isMaterial) {
        return function(input, view, type, obj) {
            var options = {
                onSelect: function(item) {
                    view.onSelectFunc(item, type);
                    view.changeAssotiateSpan(input, item.label || item.object.Name);
                },
                onCreate: function(autocomplete) {
                    var data = {};
                    data.defaultData = {
                        isDefaultType: true,
                        defaultType: isMaterial ? 1 : 0
                    };

                    PrimaryDocuments.Utils.StockProductDialogHelper.showDialog(null, function(model) {
                        options.onSelect({ object: model });
                        autocomplete.el.val(model.ShortName);
                    }, function() {
                        autocomplete.el.val('').change();
                    }, data);
                },
                getData: function() {
                    return {
                        type: type
                    };
                },
                settings: {
                    addLink: true,
                    addLinkName: isMaterial ? 'материал' : 'товар'
                },
                isStockEnabled: true,
                url: isMaterial ? '/Accounting/Subcontos/GetMaterialAutocomplete' : '/Accounting/Subcontos/GetProductAutocomplete'
            };

            input.subcontoAutocompleteForPostings(options);

            if (obj) {
                input.val(obj.Name);
                input.next().text(obj.Name);
            }
        };
    };

    var switchforAutocompliteDependingOnType = function(type, options, input, view) {
        var subcontoTypes = common.Data.SubcontoType;

        switch (type) {
            case subcontoTypes.Kontragent:
                return {
                    onCreate: function(autocomplete) {
                        subcontoKontragentCreateFunction(options, autocomplete, view);
                    },
                    settings: {
                        addLink: true,
                        addLinkName: 'контрагент'
                    },
                    url: '/Accounting/Subcontos/GetSubcontosAutocomplete'
                };
            case subcontoTypes.SeparateDivision:
            case subcontoTypes.NomenclatureGroup:
            case subcontoTypes.CostItems:
            case subcontoTypes.Worker:
            case subcontoTypes.BasicAsset:
                return {
                    onBlur: onBlurSubcontoAutocomplete(options)
                };
            case subcontoTypes.Contract:
                return {
                    settings: {
                        addLinkName: 'договор',
                        addLink: true
                    },
                    onBlur: function(input) {
                        options.onSelect({
                            object: {
                                Name: input.val(),
                                Id: 0
                            }
                        });
                    },
                    onCreate: function() {
                        var subcontoEnum = common.Data.SubcontoType;
                        var subcontoObj = view.getSubcontoDataByType(subcontoEnum.Kontragent);

                        mdNew.Contracts.addDialogHelper.showDialog({
                            data: {
                                KontragentSubcontoId: subcontoObj.Id,
                                KontragentName: subcontoObj.Name
                            },
                            onSave: function(data) {
                                var kontragentName = data.KontragentName;
                                var kontragentItem = {
                                    object: {
                                        Id: data.KontragentId,
                                        Name: kontragentName,
                                        ReadOnly: false,
                                        SubcontoId: data.KontragentSubcontoId,
                                        SubcontoType: subcontoEnum.Kontragent
                                    }
                                };

                                view.onSelectFunc(kontragentItem, subcontoEnum.Kontragent);
                                input.prev('input').val(kontragentName);

                                var name = 'Договор № ' + data.ProjectNumber + ' от ' + data.ContractDate;
                                var contractItem = {
                                    object: {
                                        Id: data.ProjectId,
                                        Name: name,
                                        SubcontoId: data.SubcontoId,
                                        SubcontoType: subcontoEnum.Contract
                                    }
                                };

                                view.onSelectFunc(contractItem, subcontoEnum.Contract);
                                input.val(name);
                            }
                        });
                    }
                };
            case subcontoTypes.MiddlemanContract:
                return {
                    settings: {
                        addLink: false,
                        onlyFromList: true
                    },
                    onBlur: function(input) {
                        options.onSelect({
                            object: {
                                Name: input.val(),
                                Id: 0
                            }
                        });
                    }
                };
            case subcontoTypes.SettlementAccount:
                return {
                    settings: {
                        addLink: false,
                        onlyFromList: true
                    }
                };
            case subcontoTypes.IntangibleAssets:
            case subcontoTypes.AcquisitionTargets:
            case subcontoTypes.SpecialSettlementAccount:
            case subcontoTypes.SpecialSettlementAccountForDigitalRuble:
            case subcontoTypes.Securities:
            case subcontoTypes.EnforcementDocuments:
            case subcontoTypes.AppointmentOfTrustFunds:
            case subcontoTypes.MovementOfTrustFunds:
            case subcontoTypes.Deficit:
            case subcontoTypes.UnbalanceNomenclature:
            case subcontoTypes.UnbalanceStock:
            case subcontoTypes.UnbalanceFixedAsset:
                return {
                    settings: {
                        addLink: false,
                        onlyFromList: false
                    },
                    onBlur: function(input) {
                        options.onSelect({
                            object: {
                                Name: input.val(),
                                Id: 0
                            }
                        });
                    }
                };
        }
    };

    function onBlurSubcontoAutocomplete(options) {
        return function() {
            options.onSelect({
                object: {
                    Name: '',
                    Id: null
                }
            });
        };
    }

    var subcontoKontragentCreateFunction = function(options, autocomplete, view) {
        _createKontragentDialog.call(this, options, autocomplete, view.parentModel);
    };

    common.Options.GetSubcontoOptions = function(subcontoType) {
        if (subcontoOptions[subcontoType]) {
            return subcontoOptions[subcontoType].apply(subcontoOptions, _.rest(arguments));
        }
    };

    common.Options.GetEntityIdByType = function(entity, type) {
        var object = {};
        object.SubcontoType = type;
        object.Name = entity.object.ShortName || entity.object.Name;
        object.Id = entity.object.SubcontoId ? entity.object.SubcontoId : entity.object.StockProductId || entity.object.Id;

        return object;
    };

    function isMaterialCode(accountCode) {
        var isSyntheticAccount_10 = Math.floor(accountCode / 10000) == 10;
        return isSyntheticAccount_10 || accountCode == SyntheticAccountCodesEnum._14_01 || accountCode == SyntheticAccountCodesEnum._013;
    }

    function _createKontragentDialog(options, autocomplete, model) {
        var dialog = new md.Core.Components.mdKontragentDialog.Component({
            defaultType: _getDefaultType(model),
            handlers: _getDialogHandlers(options, autocomplete)
        });
        dialog.show();
    }

    function _getDialogHandlers(options, autocomplete) {
        return {
            onSave: function(obj) {
                autocomplete.el.val(obj.Name);
                options.onSelect({ object: obj });
            },
            onCancel: function() {
                autocomplete.el.val('').change();
            }
        };
    }

    function _getDefaultType(model) {
        var type = md.Data.Enums.KontragentType.Buyer;

        if (model.get('IsBuy')) {
            type = md.Data.Enums.KontragentType.Seller;
        }

        return type;
    }

})(Common, Md);
