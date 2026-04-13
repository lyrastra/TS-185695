/* eslint-disable */
import DialogHelper from '@moedelo/md-frontendcore/mdCommon/helpers/DialogHelper';
import { getId, getUrlWithId } from '@moedelo/frontend-core-v2/helpers/companyId';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import localStorageHelper from "@moedelo/frontend-core-v2/helpers/localStorageHelper";
import { cashOrderOperationResources } from '../../../../../../../resources/MoneyOperationTypeResources';
import MoneyOperationHelper, { canCopyOperation, availableCashOperations } from '../../../../../../../helpers/MoneyOperationHelper';
import storage from '../../../../../../../helpers/newMoney/storage';
import { isUnifiedBP } from './operations/CashBudgetaryPayment/helpers/checkHelper';
import { getPayloadPayments } from './operations/CashBudgetaryPayment/helpers/mapper';
import { getStartEnpDate } from '../../../../../../../services/enpEnabledService';
import { convertAccPolToFinanceNdsType } from '../../../../../../../resources/ndsFromAccPolResource';

const saveAnCreateOperationTypeFlag = `cashOperationType_${getId()}`;

(function(cash, common, closingPeriodValidation) {
    cash.Views.baseCashOrderView = Marionette.ItemView.extend({
        events: {
            'click #cancelLink': 'toPreviousPage',
            'click #saveDocument': 'save',
            'click #saveAndDownload li': 'saveAndDownload',
            'click #saveAndCreate': 'saveAndCreate',
            'click #download li': 'download',
            'click #copyDocument': 'copyOrder',
            'click #deleteDocument': 'confirmDeletion',
            'change input.sum': 'formatSum'
        },

        uncontrolledOperations: [
            cashOrderOperationResources.CashOrderOutgoingProfitWithdrawing.value,
            cashOrderOperationResources.CashOrderIncomingContributionOfOwnFunds.value
        ],

        isUncontrolledOperation() {
            return _.contains(this.uncontrolledOperations, this.model.get('OperationType'));
        },

        getRelatedCashOperations() {
            throw Error('function getRelatedCashOperations not implemented in child view');
        },

        bindings: {
            'select[data-bind=OperationType]': {
                observe: 'OperationType',
                selectOptions: {
                    collection() {
                        return this.getRelatedCashOperations().collection;
                    },
                    labelPath: 'text'
                }
            },
            '[data-control=bill]': {
                observe: 'OperationType',
                visible() {
                    const isIncoming = this.model.get('Direction') == Direction.Incoming;
                    return isIncoming && (this.model.isKontragentPayment() || this.model.isOtherPayment());
                }
            },
            '[data-control=workerType]': {
                observe: 'OperationType',
                visible() {
                    return this.model.isSalaryPayment();
                }
            },
            '[data-control=zReportNumber]': {
                observe: ['OperationType', 'Date'],
                visible() {
                    return this.model.zReportRequired();
                }
            },
            'select[data-bind=WorkerDocumentType]': {
                observe: 'WorkerDocumentType',
                selectOptions: {
                    collection() {
                        return this.getWorkerDocumentTypes();
                    }
                }
            },
            'select[data-bind=Cash]': {
                observe: 'CashId',
                selectOptions: {
                    collection() {
                        const list = new window.Cash.Collections.CashCollection().getAll();
                        const cash = (_.findWhere(list, { Id: parseInt(this.model.get('CashId'), 10) }) || _.findWhere(list, { IsMain: true }));

                        // return list.map(cash => ({ value: cash.get('Id'), label: cash.get('Name') }));

                        return [
                            { value: cash.Id, label: cash.Name }
                        ];
                    }
                },
                onGet(cashId) {
                    const list = new window.Cash.Collections.CashCollection().getAll();
                    const cash = _.findWhere(list, { Id: parseInt(cashId, 10) }) || _.findWhere(list, { IsMain: true });
                    return cash.Id;
                }
            }
        },

        onRender() {
            const view = this;
            const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current();
            this.model.set('IsUsn', taxSystem.isUsn() )
            this.model.set ('IsOsno', taxSystem.isOsno())

            const ndsRates =  window.Common.Utils.CommonDataLoader.NdsRates;
            this.model.set({NdsRates: ndsRates});
            this.setCurrentRate();

            this._setIncludeMediationNds();
            this._setIncludeNds(() => {
                view._onRender();
            });            

            return this;
        },

        _setIncludeNds(callback) {
            const model = this.model;

            mdNew.MoneyOperationHelper.getDefaultIncludeNdsForCash(model)
                .then((includeNds) => {
                    model.set('IncludeNds', includeNds,  { silent: true });
                    callback();
                });
        },

        _setDefaultIncludeNds(callback) {
            const model = this.model;

            mdNew.MoneyOperationHelper.getDefaultIncludeNds(model)
                .then((includeNds) => {
                    model.set('IncludeNds', includeNds, { silent: true });
                    model.setNdsSum()
                    callback();
                });
        },

        _setIncludeMediationNds() {
            const model = this.model;

            mdNew.MoneyOperationHelper.getDefaultIncludeMediationNdsForCash(model)
                .then((includeMediationNds) => {

                    model.set('IncludeMediationNds', includeMediationNds, { silent: true });
                });
        },

        _onRender() {
            this.Controls = this.Controls || {};
            this.setDefaultOrderType();
            this.bind();
            this.initializeControls();
            this.delegateModelEvents();
            this.checkFormAction();
            this.checkClosedPeriod();
            this.checkCanEdit();
            this.listenHistoryBack();
            this.model.setInitialsAttributes();
            this.toggleSumWarning();
            this.toggleDividendsView();
            this.handlePrevOperationType();
            this.trigger('after:render');

            this.dialogHelper = this.dialogHelper || new DialogHelper();
        },

        handlePrevOperationType() {
            const prevOperationType = parseInt(localStorageHelper.get(saveAnCreateOperationTypeFlag), 10);

            if (prevOperationType) {
                this.model.set({ OperationType: prevOperationType });
                localStorageHelper.remove(saveAnCreateOperationTypeFlag);
            }
        },

        delegateModelEvents() {
            const model = this.model;
            model.on('change:WorkerDocumentType', this.onChangeWorkerDocType, this);
            model.on('change:OperationType', function() {
                const view = this;
                this._clearProject();
                this._clearLinkedDocuments();
                this._setIncludeNds(() => {
                    view.createOperationView();
                });
                model.setNdsSum()
            }, this);
            model.on('change:OperationType', this.changeOrderType, this);
            model.on('change:IncludeNds', this.setDefaultNdsType, this);
            model.on('change:NdsType change:IncludeNds change:Sum', model.setNdsSum, model);
            model.on('change:MediationNdsType change:IncludeMediationNds change:MediationCommission change:MyReward', model.setMediationNdsSum, model);
            model.on('change:NdsType change:OperationType', this.setNdsSumVisibility, this);
            model.on('change:MediationNdsType change:OperationType', this.setMediationNdsSumVisibility, this);
            model.on('change:Sum', this.toggleSumWarning, this);
            model.on('forceValid', this.forceValid, this);
            model.on(`change:Date`, this.setCurrentRate, this);
            
            
            this.listenTo(model, `change:BudgetaryTaxesAndFees`, (m) => {
                const isUnified = isUnifiedBP(m.toJSON());
                if (isUnifiedBP(m.previousAttributes()) !== isUnified) {
                    this.$('[data-control=postingsAndTax]').toggle(!isUnified);
                }
            });
            this.$('[data-control=postingsAndTax]').toggle(!isUnifiedBP(model.toJSON()));
        },

        setCurrentRate() {
            if (!this.model.get('IsUsn')) {
                return;
            }
    
            const date = this.model.get(`Date`);
            const ndsRates = this.model.get(`NdsRates`);
            const currrentRateFromAccPol = ndsRates?.find(r => dateHelper(date).isBetween(r.StartDate, r.EndDate, undefined, `[]`))?.Rate;
    
            this.model.set(`CurrentRate`, currrentRateFromAccPol || Common.Data.BankAndCashNdsTypes.None);
        },

        setDefaultNdsType() {
            const nds20PercentDate = dateHelper('2019-01-01');
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const ndsAfter2026 = dateHelper(`2026-01-01`);
            const documentDate = dateHelper(this.model.get('Date'), 'DD.MM.YYYY');

            if (documentDate.isSameOrAfter(ndsUsn2025Date) && this.model.get('IsUsn')) {
                if (this.model.get(`Direction`) === Direction.Outgoing) {
                    this.model.set({ NdsType: common.Data.BankAndCashNdsTypes.None });
                } else {
                    this.model.set({ NdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
                }
            } else if (documentDate.isSameOrAfter(ndsAfter2026) && this.model.get('IsOsno')) {
                this.model.set('NdsType', common.Data.BankAndCashNdsTypes.Nds22);
            } else if (documentDate.isBefore(nds20PercentDate)) {
                this.model.set('NdsType', common.Data.BankAndCashNdsTypes.Nds18);
            } else {
                this.model.set('NdsType', common.Data.BankAndCashNdsTypes.Nds20);
            }

            this.setNdsSumVisibility();
        },

        forceValid(attr) {
            Backbone.Validation.callbacks.valid(this, attr, 'data-bind');
        },

        checkFormAction() {
            if (!this.model.get('Id')) {
                this.$('.actions').remove();
                this.changeOrderType();
            }
        },

        _clearProject() {
            const model = this.model;

            if (model.isLoanObtain() || model.isAgencyContract()) {
                model.unset('ProjectId');
                model.unset('ProjectNumber');
            }
        },

        _clearLinkedDocuments() {
            const model = this.model;

            if ( model.get('Documents') && model.get('Documents').length ) {
                model.unset('Documents');
            }
        },

        setNdsSumVisibility() {
            const includeNds = this.model.get('IncludeNds');
            if (!includeNds) {
                return;
            }

            const hideSumForTypes = [0, -1];
            const ndsSumBlock = this.$('[data-bind="NdsSum"]').parent();
            const ndsType = this.model.get('NdsType');

            if (_.contains(hideSumForTypes, ndsType)) {
                ndsSumBlock.hide();
                return;
            }

            ndsSumBlock.show();
        },

        setMediationNdsSumVisibility() {
            const includeMediationNds = this.model.get('IncludeMediationNds');

            if (!includeMediationNds) {
                return;
            }

            const hideSumForTypes = [0, -1];
            const MediationNdsSumBlock = this.$('[data-bind="MediationNdsSum"]').parent();
            const MediationNdsType = this.model.get('MediationNdsType');

            if (_.contains(hideSumForTypes, MediationNdsType)) {
                MediationNdsSumBlock.hide();
                return;
            }

            MediationNdsSumBlock.show();
        },

        checkCanEdit() {
            if (!this.model.get('CanEdit')) {
                this.disableFormForClosedPeriod();
            }

            if (UserAccessManager.AccessRule.AccessToCash == Enums.TypeAccessRule.AccessView) {
                this.disableFormForClosedPeriod();
                this.$('#copyDocument').remove();
            }
        },

        checkClosedPeriod() {
            const decorator = closingPeriodValidation.Utils.Decorator(this);
            if (decorator.decorateDocument()) {
                this.disableFormForClosedPeriod();
            }
        },

        isClosed() {
            const requisites = new common.FirmRequisites();
            return Converter.toDate(this.model.get('Date')) <= Converter.toDate(requisites.get('FinancialResultLastClosedPeriod'));
        },

        disableFormForClosedPeriod() {
            this.$('.saveDocumentButton').hide();
            this.$('#deleteDocument').hide();
            this.$('.linkedDocuments .add.link').closest('[data-mddropdown]').hide();
            this.$('#saveAndCreate').hide();

            this.$('input, select').attr('disabled', 'disabled');
        },

        setDefaultOrderType() {
            const operations = this.getRelatedCashOperations();
            const orderType = this.model.get('OperationType') || operations.getDefaultType();
            this.model.set('OperationType', orderType);
        },

        changeOrderType() {
            const operations = this.getRelatedCashOperations();
            const operationType = this.model.get('OperationType');
            const { operation } = this.controls;
            let description;

            if (this.model.isOtherPayment()) {
                description = '';
            } else if (operation && operation.getDescription) {
                description = operation.getDescription();
            } else {
                description = operations.getDescription(operationType);
                this.model.set('isChangeOrderType', true);
            }

            this.model.set('Destination', description);

            if (operationType == cashOrderOperationResources.CashOrderIncomingMaterialAid.value) {
                this.model.set('KontragentAccountCode', 910100);
            } else if (operationType == cashOrderOperationResources.CashOrderIncomingMediationFee.value ||
            operationType == cashOrderOperationResources.CashOrderIncomingMiddlemanRetailRevenue.value) {
                this.model.set('KontragentAccountCode', 760700);
            }

            if (availableCashOperations.includes(operationType)) {
                this._setDefaultIncludeNds(() => {
                    this.createOperationView();
                });
                
                this.setDefaultNdsType();
            } else {
                setTimeout(() => this.model.set('IncludeNds', false), 0);
            }

        },

        toggleSumWarning(model, value) {
            if ((!this.model.isKontragentPayment() && !this.model.isOtherPayment()) || !this.model.isValid('Sum')) {
                return;
            }

            const sum = value || this.model.get('Sum');

            sum > 100000 ? this.showSumWarning() : this.hideSumWarning();
        },

        showSumWarning() {
            this.$('input[data-bind=Sum]').addClass('incorrectSum');
            this.$('[data-message=sumWarning]').removeClass('hidden');
        },

        hideSumWarning() {
            this.$('input[data-bind=Sum]').removeClass('incorrectSum');
            this.$('[data-message=sumWarning]').addClass('hidden');
        },

        createOperationView() {
            try {
                if (this.controls.operation && typeof this.controls.operation.destroy === 'function') {
                    this.controls.operation.destroy();
                }

                const operationType = this.model.get('OperationType');
                const operationDate = this.model.get('Date');

                const view = this.getRelatedCashOperations().getRelatedView(operationType);
                const $additionalBox = this.$('#additionalField');
                const operationView = new cash.Views[view]({
                    model: this.model,
                    taxSystem: this.options.taxSystem,
                    el: $('<div>').appendTo(this.$('[data-control=operationFields]').empty()),
                    $additionalBox
                }).render();

                this.removeCopy(operationType, operationDate);

                if (!canProvidePostings()) {
                    this.removePostingFieldsInOperation();
                }

                this.controls.operation = operationView;
            } catch (e) {
                window.console && console.error(e);
                this.trigger('error');
            }
        },

        async removeCopy(OperationType, Date) {
            const enpStartDate = await getStartEnpDate();

            if (canCopyOperation({ OperationType, Date, enpStartDate })) {
                return;
            }

            const copyLink = this.$('#copyDocument');
            copyLink.next().remove();
            copyLink.remove();
        },

        removePostingFieldsInOperation() {
            this.$('[data-control=kontragentAccountCode],.taxationSystemField').remove();
        },

        initializeControls() {
            this.controls = {};
            _.extend(this, cash.Views.initBillAutocompleteMixin);
            this.initBillAutocomplete();

            this.$('[data-collapsible]').not('[data-type=date]').collapsible();

            this.$('[data-type=date]').mdDatepicker({
                minDate: this.model.getMinDate()
            }).change();

            this.$('select').change();

            if (canProvidePostings()) {
                this.usePostingsAndTaxControl();
            } else {
                this.model.set({
                    ProvideInAccounting: false,
                    PostingsAndTaxMode: common.Data.ProvidePostingType.ByHand
                });
            }
            this.createOperationView();
            this.setNdsSumVisibility();
            this.setMediationNdsSumVisibility();
            this.initHeaderControl();
        },

        initHeaderControl() {
            this.Controls.documentHeader = new common.Controls.DocumentHeaderControl({
                model: this.model,
                el: this.$('[data-control=documentheader]'),
                validate: false
            });
            this.Controls.documentHeader.render();

            this.listenTo(this.model, 'change:Date', function(model, val) {
                if (this.model.preValidate('Date', val)) {
                    this.$('[data-text-for=Date]:visible').click();
                } else {
                    this.$('input[data-bind=Date]:visible').blur();
                }
            });
        },

        usePostingsAndTaxControl() {
            this.taxModel = new cash.Collections.PostingsAndTax.CashTaxCollection();
            this.postingsModel = new cash.Collections.PostingsAndTax.CashPostingsCollection();
            this.controls.postings = new common.Controls.PostingsAndTaxControl({
                model: this.model,
                taxModel: this.taxModel,
                postingsModel: this.postingsModel,
                el: this.$('[data-control=postingsAndTax]')
            });
        },

        backUrl() {
            return getUrlWithId('/Finances');
        },

        removeEvents() {
            this.undelegateEvents();
            this.model.off();
        },

        toPreviousPage() {
            this.removeEvents();
            this.destroyView && this.destroyView();

            const current = window.location.href;

            window.history.back();

            const timeout = setTimeout(() => {
                if (window.location.href === current) {
                    window.location = _.result(this, 'backUrl');
                }
            }, 1000);

            $(window).one('hashchange', () => {
                clearTimeout(timeout);
            });
        },

        toCreatePage() {
            this.removeEvents();

            const cashId = this.model.get('CashId');
            const operationType = this.model.get('OperationType');
            const url = this.model.get('Direction') === Direction.Incoming ? `add/incoming/cash/${cashId}` : `add/outgoing/cash/${cashId}`;
            localStorageHelper.set(saveAnCreateOperationTypeFlag, operationType);
            window.location.replace(`#reload/${url}`);
        },

        isControlsValid() {
            const operationType = this.model.get('OperationType');
            const isUncontrolled = _.contains(this.uncontrolledOperations, operationType);

            isUncontrolled && this.clearUncontrolledOperationModel();

            return isUncontrolled || _.every(this.controls, function(control) {
                const hasValidFunc = !_.isUndefined(control.isValid);
                if (hasValidFunc) {
                    return control.isValid(true);
                }

                if (control.controls) {
                    return this.isControlsValid.call(control);
                }

                return true;
            }, this);
        },

        clearUncontrolledOperationModel() {
            this.model.unset('KontragentId');
            this.model.unset('KontragentName');
            this.model.unset('NdsSum');
            this.model.set({
                ProvideInAccounting: false,
                Postings: [],
                TaxPostings: []
            });
        },

        showNotUniqueNumberWarning(confirm) {
            const view = this;
            const dialog = new common.Views.Dialogs.ConfirmDialogView({
                message: 'Номер документа не уникален, вы хотите добавить еще один документ с таким номером?',
                confirm() {
                    view.dialogHelper.destroy();
                    _.bind(confirm, view)();
                },
                cancel() {
                    view.dialogHelper.closeDialog();
                    view.$('[data-bind=Number]').focus();
                },
                okButton: 'Да'
            });
            view.dialogHelper.showDialog({
                contentView: dialog
            });
        },

        async saveOrder(onSave) {
            if (MoneyOperationHelper.canOperationUseBills(this.model.get('OperationType'))) {
                const { operationBillsStore } = this.model;

                if (operationBillsStore) {
                    const { getBillsList } = operationBillsStore;

                    getBillsList && this.model.set(getBillsList);
                }
            } else {
                this.model.set('Bills', []);
            }

            if (!this.model.isValid(true) || !this.isControlsValid()) {
                this.scrollToError();
                return;
            }

            if (this.saveProcess || (canProvidePostings() && (this.taxModel.loading || this.postingsModel.loading))) {
                return;
            }

            await this.controls.operation?.getSubPaymentsPostingsPromise?.();

            const view = this;

            if(!this.model.get(`Id`)) {
                storage.save(`Scroll`, 0);
                storage.save(`tableData`, {});
            }

            if (isUnifiedBP(this.model.toJSON())) {
                this.model.set({
                    UnifiedBudgetarySubPayments: getPayloadPayments(this.model.get(`UnifiedBudgetarySubPayments`)),
                    OperationType: cashOrderOperationResources.UnifiedCashOrderBudgetaryPayment.value
                }, { silent:true });
            }

            if (this.model.get(`NdsType`) === common.Data.BankAndCashNdsTypes.Empty) {
                this.model.set(`NdsType`, ``, { silent: true });
            }

            if(!this.model.get('IsMediation') && this.model.get(`OperationType`) === cashOrderOperationResources.CashOrderIncomingPaymentForGoods.value) {
                this.model.set({MediationNdsType: null});
                this.model.set({MediationNdsSum: null});
                this.model.set({IncludeMediationNds: null}); 
            }

            if (this.model.get('IncludeMediationNds') === false) {
                this.model.set({MediationNdsType: null});
                this.model.set({MediationNdsSum: null});
            }

            const save = function() {
                this.saveProcess = this.model.save({}, {
                    success: function() {
                        if(this.model.get(`OperationType`) === cashOrderOperationResources.UnifiedCashOrderBudgetaryPayment.value) {
                            this.model.set(`OperationType`, cashOrderOperationResources.CashOrderBudgetaryPayment.value, { silent:true });
                        }

                        return onSave ? onSave.call(this) : this.model.trigger('saved');
                    }.bind(this),
                    error: this.error.bind(this)
                });
            }.bind(this);

            this.model.checkNumberIsUnique((uniq) => {
                !uniq ? view.showNotUniqueNumberWarning(save) : save();
            });
        },

        downloadOrder(id, type, callback) {
            const url = `${Cash.Data.GetFile}?${[`id=${id}`, `extention=${type}`].join('&')}`;
            common.Utils.AttachmentLoader.download(url, callback);
        },

        download(event) {
            const fileType = $(event.target).attr('data-type');
            this.downloadOrder(this.model.get('Id'), fileType);
        },

        save() {
            this.saveOrder();
        },

        saveAndDownload(event) {
            const { model } = this;
            const fileType = $(event.target).attr('data-type');

            this.saveOrder(function() {
                this.downloadOrder(model.get('SavedId'), fileType, () => {
                    model.trigger('saved');
                });
            });
        },

        saveAndCreate() {
            this.saveOrder(this.toCreatePage);
        },

        error() {
            this.trigger('error');
        },

        copyOrder() {
            this.removeEvents();

            window.location = `#copy/cash/${this.model.get('BaseDocumentId')}`;
        },

        confirmDeletion(event) {
            const linkClass = 'documentDeleteDialog-activeLink';
            const link = $(event.target);
            if (!link.hasClass(linkClass)) {
                link.addClass(linkClass);
                if (!this.deleteDialog) {
                    this.deleteDialog = new PrimaryDocuments.Views.Documents.Actions.DeleteDialogView()
                        .on('close', link.removeClass.bind(link, linkClass))
                        .on('success', this.deleteOrder.bind(this))
                        .render({ anchor: link });
                } else {
                    this.deleteDialog.open();
                }
            }
        },

        deleteOrder() {
            this.model.remove({
                success: function() {
                    this.model.trigger('delete');
                }.bind(this),
                error: this.error.bind(this)
            });
        },

        listenHistoryBack() {
            const view = this;
            var onHashChange = function() {
                Backbone.history.off('route', onHashChange);
                $(window).off('hashchange', onHashChange);
                view.removeEvents();
            };

            if (!('onhashchange' in window)) {
                Backbone.history.on('route', onHashChange);
            } else {
                $(window).on('hashchange', onHashChange);
            }
        },

        formatSum(event) {
            const input = $(event.target);

            const sum = input.val();
            const formatSum = Common.Utils.Converter.toAmountString(sum);

            input.val(formatSum);
        },

        getWorkerDocumentTypes() {
            return [
                { value: cash.Data.workerDocumentType.WorkContract, label: 'Трудовой' },
                { value: cash.Data.workerDocumentType.Gpd, label: 'ГПД' }
            ];
        },

        toggleDividendsView() {
            const workerDocType = this.model.get('WorkerDocumentType');

            if (workerDocType === cash.Data.workerDocumentType.Dividend) {
                this.onChangeWorkerDocType(this.model, workerDocType);
            }
        },

        onChangeWorkerDocType(model, workerDocType) {
        },

        scrollToError() {
            const el = this.$('.input-validation-error').first();
            if (!el.length) {
                return;
            }

            const onScroll = function() {
                el.focus();
            };

            let duration = 300,
                topPadding = 55;

            $('html, body').animate({
                scrollTop: el.offset().top - topPadding
            }, duration, onScroll);
        }
    });

    function canProvidePostings() {
        return window._preloading.CanViewPosting;
    }
}(Cash, Common, ClosingPeriodValidation));
