/* eslint-disable */

import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

(function(main, common) {
    main.Views.Documents.BaseDocumentView = Backbone.View.extend({
        el: '#page_content',

        deleteMessageError: "Ошибка удаления. Позвоните нам по телефону 8 800 200 77 27 или <a href='mailto:support@moedelo.org'>напишите</a>, и мы исправим ее в максимально короткие сроки.",
        documentDateNotInFilterMessage: '<br/><b>Внимание! </b>Дата документа не попадает в период, выбранный в фильтре.',

        events: {
            'change .generalFields input': 'onChangeField',
            'change .generalFields textarea': 'onChangeField',
            'change .generalFields select': 'onChangeField',

            'blur #Number': 'onBlurNumberInput',
            'click .h1 span.input_text': 'onClickTitleSpan',

            'click .copyDocument': 'copy',
            'click #deleteDocument': 'openDeleteDialog',

            'click #saveDocument': 'onClickSaveDocument',
            'click #cancelLink': 'cancel'
        },

        initialize() {
            this.initEvents();
            this.discountControlToggler;
            this.discountControl;
        },

        initEvents() {
            this.listenTo(this.model, 'change:KontragentId', this.checkBaseKontragent);
            this.listenTo(this.model, 'change:KontragentName', setKontragentNameToDOM);
            this.listenToOnce(this, 'renderComplite', this.checkBaseKontragent);
        },

        checkBaseKontragent() {
            this.checkKontragentById('#KontragentName', 'KontragentId');
        },

        checkKontragentById(selector, attrName, options) {
            options || (options = {});
            this.$(selector).checkKontragentById(_.extend({
                KontragentId: this.model.get(attrName),
                Date: this.model.get('Date')
            }, options));
        },

        isClosed() {
            return this.model.get('action') == 'edit' && this.model.get('Closed') == true;
        },

        checkClosedPeriod(options) {
            if (!window.ClosingPeriodValidation) {
                return;
            }

            options = _.extend({}, {
                replaceButtons: !_.result(this.model, 'canSaveInClosedPeriod')
            }, options);

            if (ClosingPeriodValidation.Utils.Decorator(this).decorateDocument(options)) {
                this.disableFormForClosedPeriod();
                return true;
            }
        },

        disableFormForClosedPeriod() {
            const isNotComponentFilter = function() {
                return !$(this).closest('.payments,.recoverNdsControl,[data-control="recoverNds"],.invoiceControl').length;
            };

            const isNotButton = function() {
                return !$(this).closest('.buttons').length;
            };

            const className = 'closedPeriod-disabled';
            this.$('[type=radio],[type=checkbox]').addClass(className).attr('disabled', 'disabled');
            this.$('[type=text],[type=number], textarea').filter(isNotComponentFilter).addClass(className).attr('readonly', true);
            this.$('input.hasDatepicker').filter(isNotComponentFilter).next('.datepickerIcon').remove();
            this.$('input.hasDatepicker').filter(isNotComponentFilter).next('.js-datepicker').remove();

            this.$('#deleteDocument,.mdCloseLink,.mdAutocomplete-closeIcon').filter(isNotComponentFilter).hide();

            if (!_.result(this.model, 'canSaveInClosedPeriod')) {
                this.$('#saveDocument').hide();
                this.$('.buttons').find('.download .innerText').text('Скачать');
                this.$('#cancelLink').hide();
            }

            const inputWithMask = this.$('form input:visible').filter(function() {
                return $(this).data('MdNumberInputMask');
            });
            inputWithMask.each(replaceInputWithText);

            this.$('form .collapsible-container').each(replaceWithText);
            this.$('.mdCustomSelect').attr('disabled', 'disabled');

            this.$('span, a:not([href])').filter(isNotComponentFilter).filter(isNotButton).not('[data-closedperiod=enable]')
                .on('click.closedPeriod-disabled', preventDefault);

            this.$('.additionalFieldLink .icon').addClass('disabled');
            this.$('.additionalFieldLink .link').addClass('disabled-link').unbind('click').bind('click', () => { return false; });
        },

        statusChangerControl(obj) {
            obj = obj || {};

            const view = this;
            view.statusChangerControl = new common.Controls.StatusChangerControl({
                model: this.model,
                el: view.$('.status'),
                notShowStatus: obj && obj.hideStatus,
                statusTypes: obj.statusTypes
            });

            view.statusChangerControl.render();
        },

        checkCanEdit() {
            if (this.model.get('action') == 'edit' && this.model.get('CanEdit') == false) {
                this.$('.buttons').find('.download .innerText').text('Скачать');
                this.$('#saveDocument').remove();
            }
        },

        // overwrited by bizBill/ accBill / bizAct / accAct
        render() {
            const view = this;
            view.model.initialAttributes = view.model.toJSON();
            TemplateManager.get(view.template, (template) => {
                view.$el.html($(template).clone());
                view.rendered = true;
                view.delegateEvents(view.events);
                ToolTip.globalMessageClose();
                view.fillTemplate();
                view.initializeControls();

                view.onRender && view.onRender();
                view.onTemplateLoaded && view.onTemplateLoaded();

                if (view.model.get('action') == 'new') {
                    view.$('input:visible').first().focus();
                }

                view._afterRenderActions();

                if (view.model.get('action') != 'edit') {
                    view.$('.action_links').remove();
                    if (!view.options.showStatusForNew) {
                        view.$('.status').remove();
                    }
                }
            }, view.templateUrl);
        },

        _afterRenderActions() {
            const view = this;
            view.setMinDate();
            $.validator.unobtrusive.parse(view.el);

            if (!view.canEdit()) {
                view.makeReadOnly();

                view.$el.off('click', '.status');
                view.$el.off('click', '#deleteDocument');
            }

            view.checkClosedPeriod();
            view.checkCanEdit();
            view.trigger('renderComplite');
        },

        setMinDate() {
            if (!this.model.get('MinDate')) {
                return;
            }

            const elem = this.$('#Date');

            elem.attr('MinDateValue', this.model.get('MinDate'));
            elem.attr('data-val-minDate', `Дата не может быть меньше ${this.model.get('MinDate')}`);
            elem.val(this.model.get('Date'));
        },

        canEdit() {
            const canEditModel = !(this.model.get('CanEdit') === false && this.model.get('action') === 'edit');
            const isReadonly = this.model.get('readOnly') == true;
            return canEditModel && !isReadonly;
        },

        openDeleteDialog(event) {
            const view = this;
            const linkClass = 'documentDeleteDialog-activeLink';
            const link = $(event.target);
            if (!link.hasClass(linkClass)) {
                link.addClass(linkClass);
                view.deleteDialogPreLoadData(link);
            }
        },

        deleteDialogShow(link, deleteMessage, notDelete) {
            const view = this;
            const linkClass = 'documentDeleteDialog-activeLink';

            if (!view.deleteDialog) {
                view.deleteDialog = new PrimaryDocuments.Views.Documents.Actions.DeleteDialogView({});

                link.data('dialog', view.deleteDialog);

                view.deleteDialog.on('close', () => {
                    link.removeClass(linkClass);
                }).on('success', () => {
                    view.deleteDocument();
                });

                const options = { css: {} };
                _.extend(options.css, link.offset());
                options.css.top += 30;
                options.css.left -= 125;

                if (view.model.get('IsLinkMoney')) {
                    _.extend(options, { message: view.messages.warningLinkMoneyMessage });
                }

                options.deleteMessage = deleteMessage;
                options.notDelete = notDelete;
                view.deleteDialog.render(options);
            } else {
                view.deleteDialog.options.deleteMessage = deleteMessage;
                view.deleteDialog.options.notDelete = notDelete;
                view.deleteDialog.open();
            }
        },

        deleteDialogPreLoadData(link) {
            const view = this;

            if (!this.model.get('DocumentBaseId')) {
                this.deleteDialogShow(link);
                return;
            }

            ToolTip.globalMessage(1, true, 'Подождите, идет загрузка...');

            this.requestDataForDelete().done((response) => {
                let html;
                let notDelete;

                if (response.Status === false && response.List && response.List.length > 0) {
                    html = view.getNotDeleteMessage(response.List);
                    notDelete = true;
                }

                if (response.Status && response.List.length > 0) {
                    html = view.getAdditionalDeleteMessage(response.List);
                }

                view.deleteDialogShow(link, html, notDelete);
            });
        },

        requestDataForDelete() {
            const model = this.model;
            const url = WebApp.ClosingDocumentsOperation.GetDataForDelete;
            const options = {
                data: {
                    ids: [model.get('DocumentBaseId')],
                    documentType: model.get('documentType') || model.get('DocumentType')
                },
                type: 'POST'
            };
            return $.ajax(url, options)
                .done((response) => {
                    ToolTip.globalMessageClose();
                }).error(() => {
                    ToolTip.globalMessage(1, false, 'Ошибка при загрузке данных');
                });
        },

        getAdditionalDeleteMessage(items) {
            let html = 'Также будут удалены связанные документы:<ul>';
            _.each(items, (item) => {
                if (common.Data.DocumentTypes.Invoice == item.Type) {
                    html += `<li>счет-фактура №${item.Number} от ${item.Date}</li>`;
                }
            });
            html += '</ul>';
            return html;
        },

        getNotDeleteMessage() {
            return 'Нельзя удалить документы, являющиеся субконто в проводках в бух. справках';
        },

        deleteDocument() {
            ToolTip.globalMessage(1, true, this.messages.deleteMessageProcess, true);
            return this.model.destroy().done(this.onDelete.bind(this));
        },

        onDelete(resp) {
            if (resp.Status) {
                this.deleteDialog && this.deleteDialog.remove();
                Backbone.history.navigate(this.getBackUrl(), { trigger: true, replace: true });
                ToolTip.globalMessage(1, true, this.messages.deleteMessageSuccess);
            } else {
                ToolTip.globalMessage(1, false, this.messages.deleteMessageError);
            }
        },

        getEmailDialogParameters() {
            return {
                clientData: [
                    {
                        Id: this.model.get('Id'),
                        DocumentType: this.model.getDocumentType(),
                        DocFormat: 1
                    }
                ],
                pseudonym: this.getFirmRequisites().get('Pseudonym'),
                docType: this.model.getDocumentType()
            };
        },

        createEmailDialogView() {
            this.sendMailModel = this.sendMailModel || new Sales.Models.Main.Email(this.getEmailDialogParameters());

            const emailView = new Sales.Views.Table.Operations.Email({
                model: this.sendMailModel
            });

            if (emailView.model.get('Status')) {
                _.defer(() => {
                    emailView.render();
                });
            } else {
                emailView.model.fetch({
                    success(model, response) {
                        if (response.Status) {
                            emailView.render();
                        } else {
                            emailView.trigger('error');
                        }
                    }
                });
            }

            return emailView;
        },

        sendMail(event) {
            const link = $(event.target);

            if (!link.hasClass('rendering')) {
                link.addClass('rendering');

                ToolTip.globalMessage(1, true, 'Подождите, идет загрузка...', 'endless');

                const emailView = this.createEmailDialogView();

                emailView.on('renderComplete', () => {
                    link.removeClass('rendering');
                });
                emailView.on('error', () => {
                    ToolTip.globalMessage(1, false, AjaxErrorsResource.DefaultMessage);
                    link.removeClass('rendering');
                });
            }
        },

        onChangeNullInvoice() {
            const invoiceBlock = this.$('.NullInvoicesFields');
            const input = this.$('#NullInvoice');
            const isParentBlockVisible = invoiceBlock.parent().is(':visible');
            const view = this;

            let showBlock = function() {
                invoiceBlock.slideDown('fast', () => {
                    if (getInternetExplorerVersion() == 8) {
                        invoiceBlock.find("[name='NullInvoiceNumber']").blur();
                    }

                    $(document).trigger('resize');
                });
            };
            let hideBlock = function() {
                invoiceBlock.slideUp('fast', () => {
                    $(document).trigger('resize');
                });
            };

            if (!isParentBlockVisible || getInternetExplorerVersion() == 8) {
                showBlock = function() {
                    invoiceBlock.show();
                    if (getInternetExplorerVersion() == 8) {
                        invoiceBlock.find("[name='NullInvoiceNumber']").blur();
                    }
                };
                hideBlock = function() {
                    invoiceBlock.hide();
                };
            }

            if (input.is(':checked')) {
                showBlock();
            } else {
                hideBlock();
            }
        },

        onChangeCheckAccounting() {
            this.model.set('ProvideInAccounting', this.$('#checkAccounting').is(':checked'));
        },

        changeInvoiceFieldsRelatedOnNds() {
            if (this.model.get('UseNds')) {
                this.$('#NullInvoice').parents('.field').find('.title label').text('Cчёт-фактура');
                this.$('.link[data-fields=NullInvoice_field]').text('счет-фактура в скачиваемый файл');
            } else {
                this.$('#NullInvoice').parents('.field').find('.title label').text('Cчёт-фактура (нулевой)');
                this.$('.link[data-fields=NullInvoice_field]').text('нулевой счет-фактура в скачиваемый файл');
            }
        },

        onClickTitleSpan(event) {
            const span = $(event.target || event.currentTarget);

            if (span.attr('readonly') || span.attr('disabled')) {
                return;
            }

            span.hide();
            span.next().css('display', 'inline-block');

            if (event.originalEvent) {
                const input = span.next().find('input');
                input.focus('');

                if (input.is('.hasDatepicker')) {
                    input.trigger('focus-date-input');
                }
            }
        },

        onBlurNumberInput() {
            const input = this.$('#Number');
            const span = input.parent().prev();

            if (input.hasClass('input-validation-error')) {
                return;
            }

            const text = input.val();

            if (!$.trim(text).length) {
                this.onClickTitleSpan({
                    target: span
                });

                return;
            }

            input.parent().hide();

            span.text(text);
            span.show();
        },

        // TODO: разрулить это гавно
        onBlurDateInput() {
            return;
            let input = this.$('.h1 [name=Date]'),
                inputWrapper = input.closest('.input'),
                span = inputWrapper.prev();

            if (this.model.get('action') != 'edit' && !input.valid()) {
                this.onClickTitleSpan({
                    target: span
                });
            }

            if (input.hasClass('input-validation-error')) {
                
            }

            let text = input.val();
            const date = Converter.toDate(text);
            if (!_.isDate(date)) {
                
            }

            let format = 'D MMMM';
            const today = new Date();
            if (today.getFullYear() != date.getFullYear()) {
                format = 'D MMMM YYYY [г.]';
            }
            text = dateHelper(date).format(dateFormat);

            inputWrapper.hide();
            span.text(text);
            span.show();
        },

        addField(event) {
            let link;
            if (_.isObject(event)) {
                link = $(event.target || event.currentTarget);
            } else {
                link = this.$(event);
            }

            link.next('.comma').remove();
            link.prev('.comma:not([data-fields],.icon)').remove();
            link.remove();
            const block = this.$(`.${link.attr('data-fields')}`);

            const control = block.find('input, textarea').first();
            if (control.attr('type') == 'checkbox') {
                control.prop('checked', true).change();
            }

            this.$('.additionalFieldLink').before(block);

            block.show().addClass('js-opened');
            block.find('input[type=text], textarea').first().focus();

            const $links = this.$('.additionalFieldLink .link');
            if (!$links.length) {
                this.$('.additionalFieldLink').remove();
            } else {
                this.$('.additionalFieldLink .comma').remove();
                $links.each(function() {
                    const $link = $(this);
                    if ($link.is(':not(:last-child)') && $link.next().text() !== ',') {
                        const $comma = $('<span>').addClass('comma').text(', ');
                        $link.after($comma);
                    }
                });
            }
        },

        showHideAdditionalField(attrName, fieldName) {
            let needShow = false;
            const attr = this.model.get(attrName);
            if (_.isBoolean(attr)) {
                needShow = attr;
            }
            if (_.isString(attr)) {
                needShow = attr.length > 0;
            }

            if (needShow) {
                this.addField(`.link[data-fields=${fieldName}]`);
            } else {
                this.$(`.${fieldName}`).hide();
                this.listenTo(this.model, `change:${attrName}`, function(model, val) {
                    if (val === true || $.trim(val.toString()).length > 0) {
                        this.addField(`.link[data-fields=${fieldName}]`);
                    }
                });
            }
        },

        onChangeField(event) {
            let element = $(event.currentTarget || event.target),
                fieldName = element.attr('name'),
                value;

            if (element.is('input[type=checkbox]')) {
                value = element.is(':checked');
            } else {
                value = element.val();
            }
            this.model.set(fieldName, value);
            self.localChangedValues = _.union(self.localChangedValues, fieldName);
        },

        cancel() {
            this.returnToPreviousPage();
        },

        returnToPreviousPage() {
            if (document.referrer == '' || history.length <= 1) {
                window.location = Md.Core.Engines.CompanyId.getLinkWithParams(_.result(this, 'backUrl'));
            } else {
                window.history.back();
            }
        },

        getSaveSuccessMessage() {
            let message = this.messages.saveSuccess;

            message += this.editRelatedInvoiceMessage();

            const dateFilter = SalesStore.DocumentsDateFilter;
            if (dateFilter) {
                const documentDate = Converter.toDate(this.model.get('Date'));

                if (documentDate > dateFilter.getFinalDate() || documentDate < dateFilter.getStartDate()) {
                    message += this.documentDateNotInFilterMessage;
                }
            }

            return message;
        },

        editRelatedInvoiceMessage() {
            let message = '';
            const hasNullInvoice = this.model.get('NullInvoice');
            if (this.model.get('action') == 'edit') {
                const previousValue = this.model.initialValues.NullInvoice;

                if (!previousValue && hasNullInvoice) {
                    message = '<br/>Создан счет-фактура';
                }
                if (previousValue && !hasNullInvoice) {
                    message = '<br/>Cчет-фактура удален';
                }
                if (previousValue && hasNullInvoice) {
                    message = '<br/>Cчет-фактура изменен';
                }
            } else if (hasNullInvoice) {
                message = '<br/>Создан счет-фактура';
            }
            return message;
        },

        addAutocompleteCloseLink(selector, needClean) {
            // / <summary>Добавление или получение(если уже установлен) серого крестика для очищения автокомплита</summary>
            // / <param name="needClean" type="array">селекторы полей, которые нужно очищать</param>

            const view = this;
            needClean = needClean || [];

            const input = this.$(selector);
            if (input.attr('readonly')) {
                return;
            }

            let closeLink = view.$(selector).parent().find('.mdCloseLink');
            if (!closeLink || closeLink.length == 0) {
                closeLink = $('<span>×</span>').addClass('mdCloseLink');

                input.after(closeLink);
                if (!$.trim(input.val()).length) {
                    closeLink.hide();
                }

                closeLink.click(() => {
                    input.val('').change().focus();

                    _.each(needClean, (el) => {
                        const regexp = /^[A-Za-z]+$/;
                        if (regexp.test(el)) {
                            view.model.unset(el);
                        } else {
                            view.$(el).val('').change();
                        }
                    });

                    closeLink.hide();
                });

                input.change(() => {
                    if (!$.trim(input.val()).length) {
                        closeLink.hide();
                    } else {
                        closeLink.show();
                    }
                });
            }

            return closeLink;
        },

        fillTemplate() {
            const view = this;

            const data = _.omit(view.model.toJSON(), 'Items');
            view.$('.documentForm').render(data, view.getDirective());
        },

        // не используется в продажах
        initializeControls(container) {
            const view = this;
            container = $(container || view.$el);

            const selects = container.find('.custom-select');
            selects.each(function() {
                $(this).customselect();
            });

            this.$('input[data-format]').mdNumberInputMask({ needAddition: true });

            container.find('.datepicker').each(function() {
                $(this).mdDatepicker({ minDate: Converter.toDate(view.getFirmRequisites().getRegistrationDate()) });
            });

            container.find('[watermark]').each(function() {
                const text = $(this).attr('watermark');
                $(this).watermark(text);
            });
            container.find('[placeholder]').placeholder();

            view.initializeAutocompletes();
        },

        initializeAutocompletes() {
        },

        makeReadOnly() {
            this.$('#saveDocument').off();

            this.$('input').each(function() {
                disableInput($(this));
            });

            this.$('.mdCloseLink, .delete_link').remove(); // Убираем все крестики, все равно уже удалить ниче нельзя)

            this.makeReadOnlyActionLinks();

            this.$('select').each(function() {
                $(this).attr('disabled', true);
            });

            this.$('textarea').each(function() {
                $(this).attr('readonly', true);
            });

            const disableLink = function(link) {
                $(link).addClass('disabled-link').unbind('click').bind('click', () => {
                    return false;
                });
            };

            _.each(this.$('.generalFields').find('a, span').not('[data-readonly=enable]'), disableLink);
            _.each(this.$('.document_items').find('a, span').not('[data-readonly=enable]'), disableLink);
            _.each(this.$('.incomingList').find('a, span').not('[data-readonly=enable]'), disableLink);
            disableLink(this.$('#deleteDocument, .additionalFieldLink link'));
        },

        makeReadOnlyActionLinks() {
            const actionLinks = this.$('.action_links');

            actionLinks.find('.copyDocument, .copyDocument+br').remove(); // Долой копирование
            actionLinks.find('#deleteDocument, #deleteDocument+br').remove(); // Долой удаление
            actionLinks.find('.openCreateDocumentList').remove();

            if ($.trim(actionLinks.html()).length === 0) {
                actionLinks.remove();
            }
        },

        listenForClickNotDownload() {
            const view = this;
            $(document, 'input[type=checkbox]').click((event) => {
                const element = $(event.target);
                if (element.closest('.download').length == 0 && !element.hasClass('download')) {
                    view.$('.docFormat_list').hide();
                    view.$('.mdButton.download').removeClass('active');
                }
            });
        },

        openDocFormatList(event) {
            let openLink = $(event.target);

            if (openLink.closest('ul').hasClass('mdDropDownList')) {
                return;
            }

            if (openLink.hasClass('actionLink') || openLink.hasClass('arrow_bottom') || openLink.hasClass('arrowWrapper')) {
                openLink = openLink.closest('.download');
            } else if (!openLink.hasClass('mdButton')) {
                openLink = openLink.closest('.mdButton');
            }
            const list = openLink.find('.docFormat_list');
            openLink.toggleClass('active');

            if (list.is(':visible')) {
                list.hide();
            } else {
                list.show();
            }
        },

        download(event) {
            const model = this.model;

            const link = $(event.target || event.currentTarget);
            const download = function() {
                let format = link.attr('data-docFormat'),
                    useStampSing = link.attr('data-useStampSing');

                const data = [
                    {
                        Id: model.get('Id') || model.get('SavedId'),
                        DocumentType: model.getDocumentType(),
                        UseStampSign: useStampSing,
                        DocFormat: format
                    }
                ];

                let url = WebApp.ClosingDocumentsTable.GetFile;
                url += `?clientData=${JSON.stringify(data)}`;
                url = Md.Core.Engines.CompanyId.getLinkWithParams(url);

                $.fileDownload(url);

                link.parents('.docFormat_list').hide();
            };

            const canSave = !(model.get('readOnly') || model.get('AccountingReadOnly') || model.get('CanEdit') === false);

            if (link.closest('.buttons').length && canSave) {
                this.onClickSaveDocument(download);
                link.parents('.docFormat_list').hide();
            } else {
                download.call(this);
            }
        },

        disablingButtons(type) {
            if (type == 'lock') {
                this.$('.mdButton, .mdDoubleButton').addClass('disabled');
            } else {
                this.$('.mdButton, .mdDoubleButton').removeClass('disabled');
            }
        },

        initKontragentAutocomplete(onSelect, onBlur) {
            const view = this;
            let input;

            if (!this.kontragentAsLink) {
                view.addAutocompleteCloseLink('#KontragentName', ['#KontragentId']);
            }

            input = view.$('#KontragentName');
            input.saleKontragentWaybillAutocomplete({
                onSelect(selectedItem) {
                    view.onSelectKontragentName(selectedItem.object.Id, selectedItem.object.Name, selectedItem.object.KontragentForm);
                    if (_.isFunction(onSelect)) {
                        onSelect.call(view, selectedItem);
                    }
                },
                onBlur($el) {
                    if (!$el.val()) {
                        view.model.set({
                            KontragentId: null,
                            KontragentName: null,
                            ProjectId: null
                        });
                        view.$('#ProjectNumber').val('').change();
                    }

                    if (_.isFunction(onBlur)) {
                        onBlur.call(view);
                    }
                },
                getData() {
                    return {
                        docType: view.model.getDocumentType()
                    };
                },
                IsBuy: view.model.get('IsBuy')
            });
        },

        onSelectKontragentName(id, name, kontragentForm) {
            const input = this.$('#KontragentName');
            const $form = this.$el.find('form');
            const isChangeKontragent = this.model.get('KontragentId') != id;
            const data = {
                KontragentId: id,
                KontragentForm: kontragentForm
            };

            if (isChangeKontragent) {
                data.ProjectId = null;
            }

            this.model.set(data);

            this.$('#KontragentId').val(id).change();
            input.val(name).change();

            if (isChangeKontragent) {
                this.$('#ProjectNumber').val('').change();
            }
            
            if ($form.length) {
                $form.valid();
            }

            const index = this.$('.field').index(input.closest('.field'));
            this.$(`.field:visible:gt(${index})`).find('input[type=text], textarea').filter(isEmpty).first()
                .focus();

            this.addAutocompleteCloseLink('#KontragentName', ['#KontragentId', 'KontragentId']).show();
        },

        initProjectAutocomplete(onSelect, onLoadKontragent) {
            const view = this;
            const closeLink = view.addAutocompleteCloseLink('#ProjectNumber', ['#ProjectId']);

            const input = view.$('#ProjectNumber');
            input.saleProjectAutocomplete({
                settings: {
                    addLinkName: 'договор',
                    addLink: true
                },
                onCreate() {
                    mdNew.Contracts.addDialogHelper.showDialog({
                        data: {
                            Direction: Direction.Incoming,
                            KontragentId: view.model.get('KontragentId'),
                            KontragentName: view.model.get('KontragentName'),
                            isOldView: true
                        },
                        onSave(options) {
                            view.$('#ProjectNumber').val(options.ProjectNumber).change();
                            view.$('#ProjectId').val(options.ProjectId).change();
                            view.model.set('ProjectId', options.ProjectId);

                            view.$('#KontragentId').val(options.KontragentId);
                            view.model.set({ KontragentId: options.KontragentId });
                            view.$('#KontragentName').val(options.KontragentName).change();
                            view.addAutocompleteCloseLink('#KontragentName').show();

                            closeLink.show();
                        }
                    });
                },
                onSelect(item) {
                    const selectedProject = item.object;
                    const id = selectedProject.Id;
                    const number = selectedProject.Number;

                    view.$('#ProjectNumber').val(number).change();
                    view.$('#ProjectId').val(id).change();
                    view.model.set('ProjectId', selectedProject.Id);

                    if (id) {
                        if (selectedProject.KontragentId && view.$('#KontragentName').val() == '') {
                            view.$('#KontragentId').val(selectedProject.KontragentId);
                            view.model.set({ KontragentId: selectedProject.KontragentId });

                            view.model.loadKontragentName({
                                success() {
                                    view.$('#KontragentName').val(view.model.get('KontragentName')).change();
                                    view.addAutocompleteCloseLink('#KontragentName').show();

                                    if (_.isFunction(onLoadKontragent)) {
                                        onLoadKontragent.call(view, selectedProject);
                                    }
                                }
                            });
                        }
                        closeLink.show();
                    } else {
                        closeLink.hide();
                    }

                    if (_.isFunction(onSelect)) {
                        onSelect.call(view, selectedItem);
                    }

                    view.nextInputControl(input).focus();
                },
                getData() {
                    const data = {};
                    if (view.$('#KontragentId').val().length) {
                        data.kontragentId = view.$('#KontragentId').val();
                    }
                    return data;
                },

                onBlur(item) {
                    if (!item.val()) {
                        view.model.unset('ProjectId');
                    }
                }
            });
        },

        initBillAutocomplete(onSelect, onLoadKontragent) {
            const view = this;
            const closeLink = view.addAutocompleteCloseLink('#BillNumber', ['#BillId']);
            const input = view.$('#BillNumber');
            input.saleBillAutocomplete({
                onSelect(item) {
                    const selectedItem = item.object;
                    const id = selectedItem.Id;
                    const number = selectedItem.Number;

                    view.$('#BillNumber').val(number).change();
                    view.$('#BillId').val(id).change();

                    if (id) {
                        view.model.loadBillPositions();

                        if (selectedItem.KontragentId && view.$('#KontragentName').val() == '') {
                            view.$('#KontragentId').val(selectedItem.KontragentId);
                            view.model.set({ KontragentId: selectedItem.KontragentId });

                            view.model.loadKontragentName({
                                success() {
                                    view.$('#KontragentName').val(view.model.get('KontragentName')).change();
                                    view.addAutocompleteCloseLink('#KontragentName').show();

                                    if (_.isFunction(onLoadKontragent)) {
                                        onLoadKontragent.call(view, selectedItem);
                                    }
                                }
                            });
                        }
                        if (selectedItem.ProjectId && view.$('#ProjectNumber').val() == '') {
                            view.$('#ProjectId').val(selectedItem.ProjectId);
                            view.model.set({ ProjectId: selectedItem.ProjectId });

                            view.model.loadProjectNumber({
                                success() {
                                    view.$('#ProjectNumber').val(view.model.get('ProjectNumber')).change();
                                    view.addAutocompleteCloseLink('#ProjectNumber').show();
                                }
                            });
                        }
                        closeLink.show();
                    } else {
                        closeLink.hide();
                    }

                    if (_.isFunction(onSelect)) {
                        onSelect.call(view, selectedItem);
                    }

                    view.nextInputControl(input).focus();
                },
                getData() {
                    const data = {};

                    if (view.$('#KontragentName').val() != '') {
                        data.kontragentId = view.$('#KontragentId').val();
                    }

                    return data;
                }
            });
        },

        getFirmRequisites() {
            return common.Utils.CommonDataLoader.FirmRequisites;
        },

        nextInputControl(input) {
            const view = this;
            const inputsInField = input.closest('.field').find('input[type=text]').index(input);
            const closestInputs = input.closest('.field').find(`input[type=text]:gt(${inputsInField})`);
            if (closestInputs.length) {
                return closestInputs.first();
            }
            
            const index = view.$('.field').index(input.closest('.field'));
            return view.$(`.field:gt(${index})`).find('input[type=text], textarea').first();
        },

        showDiscountControlToggler() {
            if (this.model.get('UseDiscount')) {
                this.onDiscountControlToggle();
                return;
            }

            this.discountControlToggler = new mdNew.DiscountControlToggler({
                onToggle: _.bind(this.onDiscountControlToggle, this),
                disabled: this.model.get('Closed') || this.model.get('CanEdit') === false
            });
            this.discountControlToggler.render();
            this.$('#discountControlTogglerRegion').html(this.discountControlToggler._$marionetteViewInstance.$el);

            this.listenTo(this.model, 'change:UseDiscount', function() {
                this.model.get('UseDiscount') && this.onDiscountControlToggle();
            });
        },

        _removeDiscountControlToggler() {
            const togglerContainer = this.$('#discountControlTogglerRegion');
            togglerContainer.next('.comma').remove();
            togglerContainer.prev('.comma').remove();
            togglerContainer.remove();
            this.discountControlToggler && this.discountControlToggler.destroy();

            if (!this.$('.additionalFieldLink .link, ').length) {
                this.$('.additionalFieldLink').remove();
            }
        },

        onDiscountControlToggle() {
            this._removeDiscountControlToggler();

            this.destroyDiscountControl();
            this.model.set('UseDiscount', true);
            this.showDiscountControl();
        },

        showDiscountControl() {
            this.discountControl = new mdNew.DiscountControl({
                data: this.model.toJSON(),
                onClick: _.bind(this.onDiscountControlClick, this),
                disabled: this.model.get('Closed') || this.model.get('CanEdit') === false
            });
            this.discountControl.render();
            this.$('#discountControlRegion').html(this.discountControl._$marionetteViewInstance.$el);
        },

        destroyDiscountControl() {
            this.discountControl && this.discountControl.destroy();
            this.$('#discountControlRegion').empty();
        },

        onDiscountControlClick(data) {
            const discountChecked = data.checked;
            this.model.set('UseDiscount', discountChecked);
            this.destroyDiscountControl();
            this.showDiscountControl();
        }
    });

    main.Views.Documents.BaseDocumentView.extend = function(protoProps, staticProps) {
        if (protoProps.events) {
            protoProps.events = _.extend(_.clone(this.prototype.events), protoProps.events);
        }
        return Backbone.View.extend.call(this, protoProps, staticProps);
    };

    function getInternetExplorerVersion() {
        let rv = -1;
        if (navigator.appName == 'Microsoft Internet Explorer') {
            const ua = navigator.userAgent;
            const re = new RegExp('MSIE ([0-9]{1,}[\.0-9]{0,})');
            if (re.exec(ua) != null) {
                rv = parseFloat(RegExp.$1);
            }
        }
        return rv;
    }

    /** @access private */
    function setKontragentNameToDOM(model, val) {
        this.$('#KontragentName').val(val).change();
    }

    function replaceInputWithText() {
        const input = $(this);
        input.replaceWith(input.val());
    }

    function replaceWithText() {
        const wrapper = $(this);
        wrapper.html(wrapper.text());
    }

    function preventDefault(event) {
        event.preventDefault();
        return false;
    }

    function isEmpty() {
        const $el = $(this);
        return !$.trim($el.val()).length;
    }

    function disableInput($input) {
        $input.attr('readonly', true)
            .attr('disabled', 'disabled');

        if ($input.is(':data(autocomplete)')) {
            $input.autocomplete('destroy');
        }
    }
}(PrimaryDocuments, Common));
