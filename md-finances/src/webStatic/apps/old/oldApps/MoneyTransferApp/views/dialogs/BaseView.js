import DialogHelper from '@moedelo/md-frontendcore/mdCommon/helpers/DialogHelper';

(function(money, common, md) {
    'use strict';

    var companyIdEngine;
    var operationModel;
    var routeService = money.Services.MoneyDialogRouteService;

    money.Views.Dialogs.BaseView = money.Views.ApplicationBaseView.extend({
        templateUrl: '/ClientSideApps/MoneyTransferApp/templates/',

        el: $('#moneyDialog'),

        baseViewEvents: {
            'click #save': function(e) {
                this.sendStatEventSave(this.title);
                this.save(e);
            },
            'click .dialog-backLink a': goBack,
            'click #saveAndCreate': function(e) {
                this.sendStatEventSaveAndCreate(this.title);
                this.saveAndCreate(e);
            },
            keypress: 'pressEnter',
            'click .simplemodal-close': 'goToMainPage',
            'change [name=MoneyBayType]': function() { // не менять [name=MoneyBayType] иначе...
                this._showSendBankButton();
            },
            'click [name=MoneyBayType]': function(e) { // не менять [name=MoneyBayType] иначе...
                e.stopPropagation();
                this.sendStatEventChangeMoneyBayType(this.title, this.$('input[name=MoneyBayType]:checked + span').text());
            },
            'change #Settlement': function(e) {
                var value = $(e.currentTarget).val();

                this._onChangeSettlement(value);
                this._showSendBankButton();
            }
        },

        events: {},
        title: '',
        dialogWidth: 710,

        initialize: function(options) {
            this.options = options;

            common.Helpers.Mixer.addMixin(this, money.Mixins.MoneyDialogStateMixin);
            common.Helpers.Mixer.addMixin(this, money.Mixins.InfoBlockMixin);
            this.initializeEvents();

            operationModel = this.model;
            this.dialogHelper = new DialogHelper();
        },

        initializeEvents: function() {
            this.listenTo(this.model, 'change:KontragentId', _onChangeKontragent);
            this.listenTo(this.model, 'change:FounderId', _onChangeKontragent);
            this.listenTo(this.model, 'change:MoneyBayType', this._showSendBankButton);
        },

        sendStatEventBack: function(title) { console.log('sendStatEventBack', title); },
        sendStatEventSave: function(title) { console.log('sendStatEventSave', title); },
        sendStatEventSaveAndCreate: function(title) { console.log('sendStatEventSaveAndCreate', title); },
        sendStatEventChangeMoneyBayType: function(title, name) { console.log('sendStatEventChangeMoneyBayType', title, name); },

        pressEnter: function(event) {
            if (event.ctrlKey && (event.keyCode === 13 || event.keyCode === 10)) {
                this.save();
            }
        },

        onChangeModel: function() {
            var data = this.model.toJSON();
            this.$el.render(data, this.getDirectives());
        },

        setDefaultDescription: function(endPartDescription) {
            var model = this.model;
            var action = model.get('action');
            var isEdit = action ? action === 'edit' : model.get('id');
            var isCopy = action === 'copy';
            var description = model.get('Description');
            var canModifyDescription = typeof arguments[0] === 'object' && arguments[0].canModifyDescription;
            var canSetDescription = !(isCopy || isEdit) || canModifyDescription;

            if (canSetDescription || (arguments[0] && arguments[0].originalEvent) || _.isUndefined(description)) {
                this.setDefaultDescriptionToDom(endPartDescription);
            }
        },

        setDefaultDescriptionToDom: setDefaultDescriptionToDom,

        setCurrencySymbol: function() {
            var currency = this.getCurrentCurrency();
            var $currencyBlock = this.$('.js-currencyBlock');

            money.Helpers.currencyHelper.setCurrencyToDOM($currencyBlock, currency);
        },

        setMinDate: function() {
            var min = this.model && this.model.get('FirmRequisites') && this.model.get('FirmRequisites').getRegistrationDate();
            if (_.isDate(min)) {
                view.$('.datepicker, #Date').each(function() {
                    $(this).mdDatepicker({ minDate: min });
                });
            } else {
                view.$('.datepicker').each(function() {
                    $(this).mdDatepicker();
                });
            }
            if (view.el.find('.has-monthpicker').length) {
                view.el.find('.has-monthpicker').monthPicker();
            }
        },

        /* При изменении даты, суммы и валюты расчитываем сумму, чтоб потом завязаться на неё */
        calculateCurrencySum: function(options) {
            options = options || {};

            var model = this.model;
            var currency = this.getCurrentCurrency();
            var date = model.get('Date');
            var sum = options.sum || model.get('Sum');

            if (currency && date && !_.isUndefined(sum)) {
                money.Services.CurrencyService.getExchangeRate({
                    date: date,
                    currency: currency
                }, {
                    onSuccess: function(rate) {
                        sum = Converter.toFloat(sum);
                        var currencySum = +(sum * rate).toFixed(2);
                        model.set('CurrencyRate', rate);

                        if (options.initialCall) {
                            model.set('CurrencySum', currencySum, { silent: true });
                            model.trigger('change:CurrencySum', { initialCall: true });
                        } else {
                            model.set('CurrencySum', currencySum);
                        }
                    }
                });
            } else if (model.get('CurrencySum')) {
                model.unset('CurrencyRate');
                model.unset('CurrencySum');
            } else {
                model.trigger('change:CurrencySum');
            }
        },

        setCurrencyRate: function() {
            var model = this.model;
            var currency = this.getCurrentCurrency();
            var date = model.get('Date');

            if (currency && date) {
                money.Services.CurrencyService.getExchangeRate({
                    date: date,
                    currency: currency
                }, {
                    onSuccess: function(rate) {
                        model.set('CurrencyRate', rate, { silent: true });
                        model.trigger('change:CurrencyRate');
                    }
                });
            } else if (model.get('CurrencyRate')) {
                model.unset('CurrencyRate');
            } else {
                model.trigger('change:CurrencyRate');
            }
        },

        showCurrencyRateBlock: function(options) {
            options = options || {};

            var $block = options.$block || this.$('.js-currencyRate');
            var currency = this.getCurrentCurrency();
            var date = this.model.get('Date');
            var sum = options.sum || this.model.get('Sum');

            if (currency && date && sum) {
                var view = new money.Components.CurrencyRateBlock({
                    currency: currency,
                    date: date,
                    sum: sum
                });
                view.render();

                $block.html(view.$el);
            } else {
                $block.empty();
            }
        },

        render: function() {
            companyIdEngine = md.Core.Engines.CompanyId;

            var view = this;
            var dialogContainer = $('#moneyDialog');
            if (!dialogContainer.length) {
                dialogContainer = $("<div id='moneyDialog' class='moneyDialog'></div>");

                $('body').append(dialogContainer);
            }
            this.el = dialogContainer;
            this.$el = dialogContainer;
            TemplateManager.get(view.getTemplate(), function(template) {
                view.el.html(template);

                if (view.model) {

                    view.onChangeModel();
                }

                $.validator.unobtrusive.parse(view.el);
                view.el.dialog({
                    open: function() {
                        window.setTimeout(function() {
                            jQuery(document).unbind('mousedown.dialog-overlay')
                                .unbind('mouseup.dialog-overlay');
                        }, 100);
                    },
                    dialogClass: 'money-ui-dialog',
                    draggable: false,
                    autoOpen: false,
                    modal: true,
                    resizable: false,
                    width: view.dialogWidth,
                    title: view.title,
                    close: function(event, ui) {

                        if (event) {
                            view.trigger('cancel');
                        }

                        view.close();
                        view._goToBack();
                    }
                });

                removeDialogFocusBinding.call(view);

                var selects = view.$('.custom-select');
                selects.each(function() {
                    $(this).customselect();
                });

                var events = _.extend(view.events, view.baseViewEvents);
                view.delegateEvents(events);

                view.setMinDate();
                view.onRender(view);
                ToolTip.globalMessageClose();

                view.el.dialog('open');
                view.el.dialog('option', 'position', 'center');

                view.onDialogOpen();
                view.checkDateForClosedGroup();
                view.afterRender();
                view.preventChangingOperation();

                blockWriteOff.call(view);

                view._showSaveButton();
                view._showSendBankButton();

                if (view.isReadOnly()) {
                    view.readOnly && view.readOnly(); // MoneyDialogStateMixin
                }

                companyIdEngine.setParamsToHtml(view.$el);
                view.bindOnRenderEvents && view.bindOnRenderEvents();
            }, view.templateUrl);
        },

        isReadOnly: function() {
            var isInit = this.model && this.model.has('readOnly') && this.model.has('isPayed');
            return isInit && (this.model.get('readOnly') || !this.model.get('isPayed'));
        },

        preventChangingOperation: function() {
            var view = this;
            if (view.model.get('canNotChangeType')) {
                view.$('.dialog-backLink').remove();
            }
        },

        onRender: function() {
            _initKontragentSettlementsComponent.call(this);
        },

        onDialogOpen: function() {
            var view = this;
            this.centerDialog();

            $(window).resize(function() {
                view.centerDialog();
            });
        },

        afterRender: function() {
            this.centerDialog();
            this.trigger('afterRender');
        },

        centerDialog: function() {
            $('.money-ui-dialog').position({
                my: 'center center',
                at: 'center center',
                of: window,
                collision: 'fit'
            });
        },

        close: function() {

            this.trigger('remove');
            if (this.$el.is(':data(dialog)')) {
                this.$el.dialog('destroy');
            }

            this.dialogSliderButton && this.dialogSliderButton.destroy();

            this.remove();
        },

        getTemplate: function() {
            if (_.isFunction(this.template)) {
                return this.template();
            }
            return this.template;
        },

        getDirectives: function() {
            return {};
        },

        save: function(successCallback) {
            $('.revenueOfficeToolTip').hide();

            var view = this;
            view.model.set({ StatisticsAction: Enums.StatisticsAction.Save });
            var saved = this.saveModel(function(data) {
                _.isFunction(successCallback) && successCallback(data);
                view._goToBack();
            });
            if (!saved) {
                return {};
            }

        },

        saveAndCreate: function() {
            var view = this;
            var model = view.model;

            model.set({ StatisticsAction: Enums.StatisticsAction.SaveAndNew });

            var saved = this.saveModel(function() {
                model.set({ StatisticsAction: Enums.StatisticsAction.SaveAndNew }); // да, 2ой раз
                view.clear();
                model.unset('id');
                model.unset('Id');
                view.changeTaxationSystem();
            });
            if (!saved) {
                return {};
            }
        },

        clear: function() {
        },

        parseFormData: function(options) {
            var form = $('#menuDialogForm');
            var unindexedArray;
            var indexedArray = {};

            if (options && options.all) {
                var $disabledFields = form.find('[disabled]');
                $disabledFields.removeAttr('disabled');
                unindexedArray = form.serializeArray();
                $disabledFields.attr('disabled', 'disabled');
            } else {
                unindexedArray = form.serializeArray();
            }

            $.map(unindexedArray, function(n) {
                indexedArray[n['name']] = n['value'];
            });

            if (indexedArray.Settlement) {
                indexedArray.SettlementAccountId = indexedArray.Settlement;
            }

            if (options && options.cleanUsn) {
                indexedArray = _.omit(indexedArray, ['UsnSum', 'EnvdSum']);
            }

            return indexedArray;
        },

        onChangeSettlements: function() {
            this.onChangeModel();

            var selectedValue = this.model.get('SettlementAccountId');
            this.$('#Settlement').val(selectedValue).change();
        },

        disableDialog: function() {
            var disabledDialog = $('#disableDialogDiv');
            if (!disabledDialog.length) {
                disabledDialog = $('<div id=\'disableDialogDiv\'></div>');
                $('body').append(disabledDialog);
            }
            disabledDialog.dialog({
                dialogClass: 'disableDialogDiv',
                modal: true
            });
        },

        enableDialog: function() {
            var $disableDialogDiv = $('#disableDialogDiv');
            if ($disableDialogDiv.is(':data(dialog)')) {
                $disableDialogDiv.dialog('destroy');
            }
        },

        getKontragentName: function() {
            return this.$('#PurseId option:selected').text()
                || this.$('#KontragentName').val()
                || this.$('#FounderName').val()
                || this.$('#WorkerName').val()
                || '';
        },

        changeTaxationSystem: function() {
        },

        saveModel: function(onSuccess, onError) {
            var view = this;
            var form = this.$('#menuDialogForm');
            var isNotSaveAndDeleteStatisticsAction = view.model.get('StatisticsAction') != Enums.StatisticsAction.SaveAndNew;
            if (!form.length) {
                return false;
            }
            var isValidModel = true;
            var isValidView = true;

            if (this.isValid) {
                isValidView = this.isValid();
            }

            if (this.model.isValidModel) {
                isValidModel = this.model.isValidModel();
            }

            form.validate();
            if (form.valid() && isValidModel && isValidView) {
                if (isNotSaveAndDeleteStatisticsAction) {
                    if (view.$el.is(':data(dialog)')) {
                        view.$el.dialog('destroy');
                    }
                } else {
                    view.disableDialog();
                }
                var edit = this.model.get('id');
                var dataForSave = view.parseFormData();

                if (edit) {
                    view.model.unset('StatisticsAction');
                }

                if (view.model.getDataForSave) {
                    dataForSave = view.model.getDataForSave(dataForSave);
                }

                var saveViewModel = function() {

                    ToolTip.globalMessage(1, true, 'Сохранение операции ...');
                    view.model.save(dataForSave, {
                        success: function(model, response) {
                            view.enableDialog();
                            if (response.Status) {
                                if (_.isFunction(onSuccess)) {
                                    ToolTip.globalMessage(1, true, edit ? view.getEditSuccessMessage() : view.getSaveSuccessMessage());
                                    setIdsAfterSave.call(view.model, response);
                                    view.model.setRemains();
                                    view.model.trigger('save');

                                    onSuccess({
                                        id: response.SavedId
                                    });

                                }
                            } else {
                                if (_.isFunction(onError)) {
                                    onError();
                                }
                            }
                        },
                        error: function() {
                            ToolTip.globalMessage(1, false, view.getSaveErrorMessage(edit));
                            view.goToMainPage();
                            view.enableDialog();
                        },
                        silent: true
                    });
                };
                if (this.model.get('ShowCashOperationAlert') === true) {
                    var confirmDialog = new Common.Views.Dialogs.ConfirmDialogView({
                        message: 'Ранее была разорвана связь этой операции с кассовыми. <br/>Создать заново кассовые ордера для данной операции?',
                        confirm: function() {
                            view.dialogHelper.destroy();
                            view.model.set('NeedProvideCashOperations', true);
                            saveViewModel();
                        },
                        cancel: function() {
                            view.dialogHelper.closeDialog();
                            view.model.set('NeedProvideCashOperations', false);
                            saveViewModel();
                        },
                        okButton: 'Создать'
                    });

                    view.dialogHelper.showDialog({
                        contentView: confirmDialog
                    });
                } else {
                    saveViewModel();
                }

                return true;
            }
            return false;
        },

        getSaveSuccessMessage: function() {
            var operationType = this.model.getType();
            switch (operationType) {
                case Enums.MoneyTransferOperationTypes.Movement:
                    return 'Движение добавлено';
                case Enums.MoneyTransferOperationTypes.Incoming:
                    return 'Поступление добавлено';
                case Enums.MoneyTransferOperationTypes.Outgoing:
                    return 'Списание добавлено';
                default:
                    return 'Операция добавлена';
            }
        },

        getSaveErrorMessage: function() {
            var message = 'Ошибка сохранения ';
            //            message += edit ? "сохранения " : "добавления ";
            var typeName;
            var operationType = this.model.getType();
            switch (operationType) {
                case Enums.MoneyTransferOperationTypes.Movement:
                    typeName = 'движения';
                    break;
                case Enums.MoneyTransferOperationTypes.Incoming:
                    typeName = 'поступления';
                    break;
                case Enums.MoneyTransferOperationTypes.Outgoing:
                    typeName = 'списания';
                    break;
                default:
                    typeName = 'операции';
            }

            message += typeName;
            message += ".<br/>Позвоните нам по телефону 8 800 200 77 27 или <a href='mailto:support@moedelo.org'>напишите</a>, и мы исправим ее в максимально короткие сроки.";

            return message;
        },

        getEditSuccessMessage: function() {
            return 'Изменения сохранены';
        },

        getCurrentCurrency: function() {
            var $moneyBayType = this.$('input[name=MoneyBayType]:checked');
            var moneyBayType = $moneyBayType.length && parseInt($moneyBayType.val(), 10);
            var result = this.model.get('SettlementCurrency');

            if (!_.isUndefined(moneyBayType)) {
                result = moneyBayType === 0 || moneyBayType === 6 ? this.model.get('SettlementCurrency') : null;
            }

            return result;
        },

        isForeignCurrency: function() {
            var currency = this.getCurrentCurrency();
            return currency && currency !== 'RUB';
        },

        goToMainPage: function() {
            if (this.model.get('noRedirect')) {
                return this._closeDialog();
            }

            var backUrl = this.options && this.options.isIntegrated ? '' : 'money/';
            var backUrlFromHref = _getBackUrlFromHref();
            var backUrlFromStorage = _getBackUrlFromStorage();
            var scrollPosition = $(document).scrollTop();
            var $el = this.$el;

            if (backUrlFromHref) {
                backUrl = backUrlFromHref;
            } else if (backUrlFromStorage) {
                backUrl = backUrlFromStorage;
            }

            $('body').trigger('closeMoneyTransferDialog');
            this.trigger('closeMoneyTransferDialog');
            if (this.model.get('isLocalCreate')) {
                Backbone.history.navigate(backUrl, { trigger: false });
            } else if (!this.options.isForWizard) {
                Backbone.history.navigate(backUrl, { trigger: true });
            }

            $(document).scrollTop(scrollPosition);

            if ($el.is(':data(dialog)')) {
                $el.dialog('destroy');
            }

            $el.remove();
        },

        _closeDialog: function() {
            var $el = this.$el;

            $('body').trigger('closeMoneyTransferDialog');
            this.trigger('closeMoneyTransferDialog');

            if ($el.is(':data(dialog)')) {
                $el.dialog('destroy');
            }

            $el.remove();
        },

        _goToBack: function() {
            var urlToBack = money.urlToBack;
            if (urlToBack) {
                window.location = urlToBack;
            } else {
                this.goToMainPage();
            }
        },

        goToMainPageWhithReload: function() {
            var router = new Backbone.Router();
            router.navigate('moneyDialog/reload', { trigger: true });

            if (this.$el.is(':data(dialog)')) {
                this.$el.dialog('destroy');
            }

            this.el.remove();
        },

        goToPreviosMenu: function(event) {
            var router = new Backbone.Router();
            router.navigate('money/' + event.currentTarget.id + '/', { trigger: true });
            this.el.dialog('destroy');
            this.el.remove();
        },

        /** Тригаем валидацию на предмет закрытого кассового периода */
        checkDateForClosedGroup: function() {
            var element = this.$('#Date, #AgentDate');
            if (element.attr('data-val-checkCashClosedGroup-useCash')) {
                element.blur();
            }
        },

        useCashClosedGroupValidation: function(check, elem) {
            if (!elem) {
                elem = $('#Date');
            }

            if (check) {
                elem.attr('data-val-checkCashClosedGroup-useCash', true);
            } else {
                elem.attr('data-val-checkCashClosedGroup-useCash', false);
            }
            elem.blur();
            var agentElem = $('#AgentDate');

            if (check) {
                agentElem.attr('data-val-checkCashClosedGroup-useCash', true);
            } else {
                agentElem.attr('data-val-checkCashClosedGroup-useCash', false);
            }
            agentElem.blur();
        },

        _showSaveButton: function() {
            var $region = this.$('.js-saveMainButton');
            var $this = this;

            if (!$region.length) {
                return;
            }
            var component = this.dialogSliderButton = new money.Components.DialogSliderButton({
                handlers: {
                    onSave: function() {
                        $this.sendStatEventSave($this.title);
                        return $this.save.call($this);
                    },
                    onSaveAndCreate: function() {
                        $this.sendStatEventSaveAndCreate($this.title);
                        return $this.saveAndCreate.call($this);
                    }
                }
            });

            component.render();

            $region.html(component.$el);
        },

        _onChangeSettlement: function(settlement) {
            var model = this.model;
            var isIp = model.get('FirmRequisites').get('IsOoo') === false;
            var fieldValue = parseInt(settlement, 10);

            model.set({
                SettlementAccountId: fieldValue
            });

            if (!isIp) {
                return;
            }

            var settlementAccount = fieldValue || model.get('SettlementAccountId');
            var settlementObj = model.get('SettlementAccounts').getById(settlementAccount);
            var currency = settlementObj.get('Currency');

            model.set({
                SettlementCurrency: currency
            });

            this.kontragentSettlementsComponent && this.kontragentSettlementsComponent.renderListByCurrency(currency);
        },

        _showSendBankButton: function() {
            var $region = this.$('.js-saveAdditionButton');
            var settlementObj = this.getSettlementObj && this.getSettlementObj();
            var moneyBayType = this._getMoneyBayType();

            if (_.isNumber(moneyBayType) && this._needToRemoveSendIntegrationButton(moneyBayType)) {
                $region.empty();
                return;
            }

            if (!$region.length || !settlementObj) {
                return;
            }

            var id = this.model.get('id');
            var component = new money.Components.SendBankFooterButton({
                data: {
                    id: id,
                    disabled: !settlementObj.HasBankIntegration,
                    type: settlementObj.IntegrationPartner
                },
                handlers: {
                    onSave: this.save.bind(this)
                }

            });

            component.render();

            $region.html(component.$el);
        },

        settlementDirective: {
            SettlementAccounts: {
                Account: {
                    html: function() {
                        var isCurrency = operationModel.get('SettlementAccounts').isCurrencySettlements();
                        return getSettlementAccountName.call(this, isCurrency);
                    },

                    value: function() {
                        return this.Id;
                    }
                }
            },
            SettlementAccountsFrom: {
                Account: {
                    html: function() {
                        var isCurrency = operationModel.get('SettlementAccountsFrom').isCurrencySettlements();
                        return getSettlementAccountName.call(this, isCurrency);
                    },

                    value: function() {
                        return this.Id;
                    }
                }
            },
            SettlementAccountsTo: {
                Account: {
                    html: function() {
                        var isCurrency = operationModel.get('SettlementAccountsTo').isCurrencySettlements();
                        return getSettlementAccountName.call(this, isCurrency);
                    },

                    value: function() {
                        return this.Id;
                    }
                }
            },
            Settlement: {
                text: function() {
                    return '';
                }
            }
        },

        _getMoneyBayType: function() {
            var input = this.$('input[name=MoneyBayType]:checked');
            var payoutType = this.model.payoutType;
            var value = input.val();

            if (payoutType && payoutType !== 0 || !input.length) {
                value = this.model.get('MoneyBayType');
            }

            return Converter.toInteger(value);
        },

        _needToRemoveSendIntegrationButton: function(moneyBayType) {
            var operationType = this.model.operationType;

            if (operationType === 'RemovingTheProfitOperation') {
                return moneyBayType !== 6;
            } else {
                return moneyBayType !== 0;
            }
        },

        _setSettlementAccountId: function(data) {
            var model = this.model;

            if (data && data.Settlement) {
                var settlement = parseInt(data.Settlement, 10);
                var settlementObj = model.get('SettlementAccounts').getById(settlement);
                var number = settlementObj.get('Number');

                data.Settlement = number;

                model.set({
                    SettlementAccountId: settlement,
                    Settlement: number
                }, { silent: true });
            } else {
                model.unset('SettlementAccountId');
            }
        },

        getSumDescription: function(sum) {
            var model = this.model;
            var description = '';

            if (Converter.toFloat(sum)) {
                var currentCurrency = this.getCurrentCurrency();
                var currency = currentCurrency || 'руб';
                description += ' на сумму ' + Converter.toAmountString(sum) + ' ' + currency;

                if (currentCurrency && model.get('CurrencyRate')) {
                    description += ', курс ЦБ 1 ' + currentCurrency + ' = ' + model.get('CurrencyRate') + ' руб';
                }

                description += '.';
            }

            return description;
        },

        updateConfirmingWaybillByCurrency: function() {
            var currentCurrency = this.getCurrentCurrency() || '';

            if (currentCurrency === this.currentCurrencyForWaybill) {
                return;
            }

            this.currentCurrencyForWaybill = currentCurrency;
            this.ConfirmingWaybillView && this.ConfirmingWaybillView.destroyView();
            this.addWaybills({
                canUseAutocomplete: !this.getCurrentCurrency()
            });
        },

        updateConfirmingStatementsByCurrency: function() {
            var currentCurrency = this.getCurrentCurrency() || '';

            if (currentCurrency === this.currentCurrencyForStatement) {
                return;
            }

            this.currentCurrencyForStatement = currentCurrency;
            this.ConfirmingStatementsView && this.ConfirmingStatementsView.destroyView();
            this.addStatements({
                canUseAutocomplete: !this.getCurrentCurrency()
            });
        },

        //***************** BILL REGION ************************//
        showOrHideBillFieldsForCurrency: function() {
            var currency = this.getCurrentCurrency();

            if (currency) {
                this.hideBillFields();
            } else {
                this.showBillFields();
            }
        },

        showBillFields: function() {
            var currency = this.getCurrentCurrency();
            var $el = this.$('#billFields');

            if (!currency) {
                $el.removeClass('hidden');
            }
        },

        hideBillFields: function() {
            var $el = this.$('#billFields');
            var $billNumber = this.$('#BillNumber');
            var $billId = this.$('#BillId');

            $billNumber.val('');
            $billId.val('');

            $el.addClass('hidden');
            this.model.unset('BillId');
            this.model.unset('BillNumber');
        },
        //****************************************************//

        //***************** BILL REGION ************************//
        showOrHideProjectFieldsForCurrency: function() {
            var currency = this.getCurrentCurrency();

            if (currency) {
                this.hideProjectFields();
            } else {
                this.showProjectFields();
            }
        },

        showProjectFields: function() {
            var currency = this.getCurrentCurrency();
            var $el = this.$('.js-projectFields');

            if (!currency) {
                $el.removeClass('hidden');
            }
        },

        hideProjectFields: function() {
            var $el = this.$('.js-projectFields');
            var $projectNumber = this.$('#ProjectNumber');
            var $projectId = this.$('#ProjectId');
            var $projectDate = this.$('#ProjectDate');
            var $dateOver = this.$('#DateOver');

            $projectNumber.val('');
            $projectId.val('');
            $projectDate.val('');
            $dateOver.val('');

            $el.addClass('hidden');
            this.model.unset('ProjectId');
            this.model.unset('ProjectNumber');
            this.model.unset('DateOver');
            this.model.unset('ProjectDate');
        }

        //****************************************************//
    });

    function getSettlementAccountName(isCurrency) {
        var currencySymbol = '';

        if (isCurrency) {
            currencySymbol = this.Currency ? money.Helpers.currencyHelper.getSymbol(this.Currency) : 'RUB';
            currencySymbol = ' (' + currencySymbol + ')';
        }

        return this.Number + currencySymbol + ' в ' + this.BankName;
    }

    function setDefaultDescriptionToDom(endPartDescription) {
        var sumAdditional = '',
            endPart = endPartDescription,
            model = this.model,

            returnSum = this.$('#ReturnSumm').val() || 0,
            kontragent = typeof arguments[0] === 'object' && arguments[0].notShowKontragent ? '' : this.getKontragentName(),

            sum = this.$('#Sum').val(),
            projectNumber = this.$('#ProjectNumber').val(),
            billNumber = this.$('#BillNumber').val(),
            projectDate = this.model.get('ProjectDate'),
            billDate = this.model.get('BillDate'),
            description = this.title + (kontragent === '' ? '' : ' ' + kontragent);

        if (typeof arguments[0] === 'object') {
            endPart = arguments[0].endPartDescription;
            sumAdditional = arguments[0].sumAdditionalDescription;
        }

        sum = this.$('#SumOfLoan').val() && this.$('#SumOfLoan').val() > 0 ? this.$('#SumOfLoan').val() : sum;
        sum = returnSum ? returnSum : sum;

        if (projectNumber != '' && projectNumber != undefined) {
            description += ' по договору № ' + this.$('#ProjectNumber').val();
            if (projectDate !== undefined) {
                description += ' от ' + projectDate;
            }
        }

        if (billNumber != '' && billNumber != undefined) {
            description += ' по счету № ' + this.$('#BillNumber').val();
            if (billDate != undefined) {
                description += ' от ' + billDate;
            }
        }

        description += this.getSumDescription(sum);

        if (Converter.toFloat(sum) && sumAdditional) {
            description += sumAdditional;
        }

        if (endPart && !_.isObject(endPart)) {
            description += endPart;
        }

        if (this.completeDescription) {
            description += this.completeDescription() || '';
        }

        if (this.getCustomDescription) {
            description = this.getCustomDescription({
                    kontragent: kontragent,
                    sum: sum,
                    sumAdditional: sumAdditional
                }) || description;
        }

        this.$('#Description').text(description) // лучше использовать val, но поскольку все завязано на text, то будем юзать и то и другое ;)
            .val(description);
    }

    /** @access private */
    function goBack(e) {
        this.sendStatEventBack(this.title);
        var backRouteMethod = this.model.getBackRouteName;
        var routeName;

        if (this.model.get('isLocalCreate')) {
            e.preventDefault();
            this.trigger('redirectToChange');
        } else if (this.model.get('isDirectMethods')) {
            e.preventDefault();
            routeName = backRouteMethod && backRouteMethod();
            routeService.moneyDialogRoute(routeName);
        }
    }

    /** @access private */
    function setIdsAfterSave(response) {
        this.set('Id', response.SavedId);
        this.set('DocumentBaseId', response.SavedBaseId);
    }

    /**
     * Делаем readonly списания по флагу
     * @access private
     */
    function blockWriteOff() {
        var $el = this.$('input[name=WriteOffBy]');
        if (this.model.get('isWriteOffBlocked')) {
            $el.attr('disabled', 'disabled');
            $el.closest('label').attr('disabled', 'disabled');
        }
    }

    /** @access private */
    function removeDialogFocusBinding() {
        if (this.el.data('dialog')) {
            this.el.data('dialog').uiDialogTitlebar.unbind('mousedown');
        }
    }

    function _getBackUrlFromStorage() {
        var backUrl;
        var amplifyStorage = amplify.store.sessionStorage;
        var urls = amplifyStorage('appHistory');
        if (urls && urls.length) {
            var salesUrls = _.filter(urls, function(url) {
                if (/^(?!moneyDialog).*/.test(url.route)) {
                    return true;
                }
            });
            if (salesUrls.length) {
                backUrl = _.last(salesUrls).route;
            }
        }
        return backUrl;
    }

    function _getBackUrlFromHref() {
        var href = window.location.href;
        var regExp = /_backUrl=(\w+)/g;
        var backUrl = regExp.exec(href);
        return backUrl && backUrl[1];
    }

    function _onChangeKontragent() {
        var settlementsComponent = this.kontragentSettlementsComponent;
        var kontragentId = this.model.get('KontragentId') || this.model.get('FounderId');

        if (settlementsComponent) {
            settlementsComponent.setKontragent(kontragentId);
            settlementsComponent.loadList(kontragentId);
        }
    }

    function _initKontragentSettlementsComponent() {
        var model = this.model;
        this.kontragentSettlementsComponent = new money.Components.KontragentSettlements.Component({
            $el: this.$('.js-kontragentSettlementRegion'),
            KontragentType: model.get('KontragentType'),
            KontragentId: model.get('KontragentId') || model.get('FounderId'),
            KontragentSettlement: model.get('KontragentSettlementAccountId'),
            currency: this.getCurrentCurrency()
        });
        this.kontragentSettlementsComponent.render();
    }
})(Money, Common, Md);
