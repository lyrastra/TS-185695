import DialogHelper from '@moedelo/md-frontendcore/mdCommon/helpers/DialogHelper';

(function(bank, bankUrl, common) {
    const baseView = bank.Views.BaseView.extend(PrimaryDocuments.Views.Documents.BaseDocumentView.prototype);

    bank.Views.Documents.BaseDocumentView = baseView.extend({
        backUrl: '/App/Bank',

        rootUrl: BankUrl.module('Main').BaseTemplate,
        templateUrl: BankUrl.module('Main').BaseTemplate,

        initialize() {
            _.extend(this, common.Mixin.documentStateMixin);
            this.dialogHelper = new DialogHelper();
            consoel.log('vvvvvvv')
        },

        events: _.extend({}, {
            'click .download .docFormat_list li': 'downloadDocument',
            'click .download .openDocFormatList': 'openDocFormatList'

        }, PrimaryDocuments.Views.Documents.BaseDocumentView.prototype.events),

        saveAndDownloadDoc(e) {
            this.onClickSaveDocument(function(response) {
                this.downloadDocument(e, response.SavedId);
            });
        },

        onRender() {
            this.listenForClickNotDownload();
            this.checkClosedPeriod();
        },

        makeReadOnly() {
            common.Mixin.documentStateMixin.documentStateMixin.makeReadOnly();
            this.documentStateMixin.makeRequestForPayment(this.$('.buttons .mdDoubleButton'));
            this.hideElementsWhitchReadOnly();
        },

        hideElementsWhitchReadOnly() {
            this.$('.addOperation').hide();
        },

        onClickSaveDocument(onSuccess, onCancel) {
            // / <summary>Сохранение документа</summary>
            // / <param name="onSuccess">Event при нажатии на кнопку "Сохранить" или callback при прямом вызове</param>
            const view = this;
            const form = this.$('.generalFields form');

            // не сохраняем, если нажимают на disabled кнопку
            if (view.$('.mdButton').hasClass('disabled')) {
                return;
            }
            view.validationSetFunction(form);
            view.disablingButtons('lock');

            if (view.validationCheckFunction(form)) {
                this.postingsAndTaxControl && this.postingsAndTaxControl.onModelSave();
                const saveModel = function() {
                    ToolTip.globalMessage(1, true, view.messages.saveProcess, true);

                    if (view.model.get('action') !== 'edit') {
                        mdNew.Telemetry.event('Bank/Save paymentOrder', {
                            OrderType: view.model.get('OrderType')
                        }).metricFrom('Bank/Display paymentOrder').save();
                    }

                    view.model.save({}, {
                        success(model, response) {
                            if (response.Status) {
                                view.model.trigger('save');

                                if (_.isFunction(onSuccess)) {
                                    onSuccess.call(view, response);
                                }

                                $(window.document).ready(() => {
                                    _.delay(() => {
                                        ToolTip.globalMessage(1, true, view.getSaveSuccessMessage());
                                        view.returnToPreviousPage();
                                    }, 1000);
                                });
                            } else {
                                ToolTip.globalMessageClose();
                                Backbone.history.navigate('error/', { trigger: true });
                            }
                        },
                        error() {
                            ToolTip.globalMessageClose();
                            Backbone.history.navigate('error/', { trigger: true });
                        }
                    });
                };

                if (view.model.initialAttributes.Number == view.model.get('Number') && view.model.get('action') == 'edit') {
                    saveModel();
                } else {
                    view.checkIsNumberBusy(() => {
                        if (!view.ConfirmDialog) {
                            view.ConfirmDialog = new Common.Views.Dialogs.ConfirmDialogView({
                                message: 'Номер документа не уникален, вы хотите добавить еще один документ с таким номером?',
                                confirm() {
                                    view.dialogHelper.destroy();
                                    saveModel();
                                },
                                cancel() {
                                    view.dialogHelper.closeDialog();
                                    view.$('#Number').focus();
                                    if (_.isFunction(onCancel)) {
                                        onCancel.call();
                                    }
                                },
                                okButton: 'Да'
                            });
                        }
                        view.dialogHelper.showDialog({
                            contentView: view.ConfirmDialog
                        });
                    }, () => {
                        saveModel();
                    });
                }
            } else {
                if (_.isFunction(onCancel)) {
                    onCancel.call();
                }
                const position = view.$('.field-validation-error').first().position();
                if (position) {
                    $('body').animate({
                        scrollTop: position.top - 40
                    });
                }

                if (view.model.get('action') !== 'edit') {
                    mdNew.Telemetry.event('Bank/Save paymentOrder failed (validation failed)', {
                        OrderType: view.model.get('OrderType'),
                        Direction: view.model.get('Direction')
                    }).metricFrom('Bank/Display paymentOrder').save();
                }
            }
        },

        validationSetFunction(form) {
            form.validate();
        },

        validationCheckFunction(form) {
            let isPostingsValid = this.postingsAndTaxControl ? this.postingsAndTaxControl.isValid() : true,
                isFormValid = form.valid(),
                isModelValid = this.model.isValid ? this.model.isValid(true) : true;

            return isFormValid && isModelValid && isPostingsValid;
        },

        returnToPreviousPage() {
            if (document.referrer == '' || history.length <= 1) {
                Backbone.history.navigate('', { trigger: true });
            } else {
                window.history.back();
            }
        },

        checkIsNumberBusy(busyFunc, freeFunc) {
            const documentNumber = new bank.Models.Documents.DocumentNumber({
                orderType: this.model.get('OrderType'),
                number: this.model.get('Number'),
                date: this.model.get('Date'),
                settlementAccountId: this.model.get('SettlementAccountId')
            });

            documentNumber.isBusy(busyFunc, freeFunc, true);
        },

        initializeControls() {
            const view = this;
            PrimaryDocuments.Views.Documents.BaseDocumentView.prototype.initializeControls.call(this);

            const selects = view.$('.mdSelect');
            selects.each(function() {
                $(this).mdSelect();
            });
            view.$('.mdDatepicker').mdDatepicker();
        },

        // Отлавливает изменение и удалени операций
        wiretappingOperationView(operationView, callback) {
            const view = this;

            operationView.on('delete', () => {
                operationView.off();
                view.model.get('Operations').remove(operationView.model);
                view.operationsList = _.without(view.operationsList, operationView);
                operationView.clearSignsOfExistence();
                operationView.off('renderComplite');
                operationView = null;

                if (callback && _.isFunction(callback)) {
                    callback.call(view);
                }
            });

            operationView.on('changeType', (params) => {
                const oldPlace = operationView.$el;
                view.operationsList = _.without(view.operationsList, operationView);
                operationView.clearSignsOfExistence();
                view.adaptionFieldInTemplate({
                    operationView,
                    type: params.elemType,
                    placement: oldPlace
                }, callback);
            });
        },

        // Умное создание вьюшки для необходимой операции и размещение
        adaptionFieldInTemplate(params, callback) {
            let view = this,
                typeSwitcher = bank.Helpers.OperationTypesFactory,
                operationView = params.operationView,
                type = params.type,
                obj = params.obj,
                placement = params.placement;
            operationView = typeSwitcher.selectType(type, obj).view;
            operationView.PPModel = this.model;

            operationView.model.setDefaultsFromPPModel(this.model);

            const isReadonly = operationView.PPModel.get('readOnly') || view.isClosed();
            operationView.model.set({
                Type: type,
                readOnly: isReadonly,
                TotalSum: this.model.get('Sum')
            });
            view.model.get('Operations').add(operationView.model);

            view.wiretappingOperationView(operationView, callback);

            view.operationsList.push(operationView);

            view.transportingKontragentValueToChildren(view.model, operationView.model);

            view.model.on('change:Sum', (model, val) => {
                operationView.model.set('TotalSum', val);
            }, view);

            operationView.setElement(placement);

            operationView.on('operationHeaderReady delete', () => {
                view.model.trigger('addingOperation', operationView.model);
                view.checkingAccessForOperations();
            }, view);
            operationView.render();
        },

        transportingKontragentValueToChildren(parentModel, childModel) {
            childModel.set({
                KontragentId: parentModel.get('KontragentId'),
                SalaryWorkerId: parentModel.get('SalaryWorkerId'),
                DocumentDate: parentModel.get('Date')
            });

            parentModel.on('change:KontragentId', () => {
                childModel.set({ KontragentId: parentModel.get('KontragentId') });
            });
            parentModel.on('change:SalaryWorkerId', () => {
                childModel.set({ SalaryWorkerId: parentModel.get('SalaryWorkerId') });
            });
            parentModel.on('change:Date', () => {
                childModel.set({ DocumentDate: parentModel.get('Date') });
            });
        },

        applyingSumToChildren(model) {
            let notBaseOperations,
                sum = model.get('Sum');

            notBaseOperations = this.model.get('Operations').filter((collModel) => {
                return collModel.get('Type');
            });

            if (_.indexOf(notBaseOperations, model) === 0) {
                if (!sum || (sum == this.model.oldSum && this.model.get('Operations').length == 1)) {
                    model.set('Sum', this.model.get('Sum'));
                    this.model.oldSum = this.model.get('Sum');
                }
            }
        },

        operationsValidation() {
            let view = this,
                isValid = true;

            view.emptyOperationsRemove();

            $.each(view.operationsList, (ind, val) => {
                if (!val.validate() || !val.additionalValidation()) {
                    isValid = false;
                }
            });

            return isValid;
        },

        emptyOperationsRemove() {
            const newList = this.model.get('Operations').filter((elem) => {
                return elem.get('Type');
            });

            this.model.get('Operations').reset(newList, { silent: true });
        },

        deleteDocument() {
            const view = this;

            ToolTip.globalMessage(1, true, view.messages.deleteMessageProcess, true);
            this.model.destroy({
                success(model, response) {
                    view.model.trigger('destroy');
                    if (response.Status) {
                        Backbone.history.navigate(view.getBackUrl(), { trigger: true, replace: true });
                        ToolTip.globalMessage(1, true, view.messages.deleteMessageSuccess);
                    } else {
                        ToolTip.globalMessage(1, false, view.messages.deleteMessageError);
                    }
                }
            });
        },

        getBackUrl() {
            return `settlement/${this.model.get('SettlementAccountId').toString()}`;
        },

        openDocFormatList(event) {
            if ($(event.target).closest('ul').hasClass('mdDropDownList')) {
                return;
            }

            let openLink = $(event.currentTarget) || $(event.target);
            if (openLink.hasClass('actionLink') || openLink.closest('.arrowWrapper').length) {
                openLink = openLink.closest('.download');
            }

            let list = openLink.find('.docFormat_list');
            if (!list.length) {
                list = openLink.parent().find('.docFormat_list');
            }
            openLink.toggleClass('active');

            if (list.is(':visible')) {
                list.hide();
            } else {
                list.show();
            }
        },

        downloadDocument(e, Id) {
            let view = this,
                id = view.model.get('id') || Id,
                type = $(e.target).attr('data-type');
            if (type == 4) {
                view.hideDocDownLoadList();
                return;
            }

            const downloadURL = function(url) {
                let iframe = null;
                if (iframe === null) {
                    iframe = document.createElement('iframe');
                    iframe.style.display = 'none';
                    document.body.appendChild(iframe);
                }
                iframe.src = url;

                _.delay(() => {
                    $(iframe).remove();
                }, 10000);
            };

            const loadDoc = function() {
                const url = `${bankUrl.GetFile}?id=${id}&format=${type}`;
                downloadURL(url);
            };

            loadDoc();
            view.hideDocDownLoadList();
        },

        listenForClickNotDownload() {
            const view = this;
            $(document, 'input[type=checkbox]').click((event) => {
                const element = $(event.target);
                if (element.closest('.download').length || element.hasClass('download')) {
                    return;
                }
                if (element.closest('.openDocFormatList').length || element.hasClass('openDocFormatList')) {
                    return;
                }
                view.hideDocDownLoadList();
            });
        },

        hideDocDownLoadList() {
            const view = this;
            view.$('.docFormat_list').hide();
            view.$('.openDocFormatList').removeClass('active');
        }
    });

    bank.Views.Documents.BaseDocumentView.extend = function(protoProps, staticProps) {
        if (protoProps.events) {
            protoProps.events = _.extend(_.clone(this.prototype.events), protoProps.events);
        }
        return Backbone.View.extend.call(this, protoProps, staticProps);
    };
}(Bank, BankUrl, Common));
