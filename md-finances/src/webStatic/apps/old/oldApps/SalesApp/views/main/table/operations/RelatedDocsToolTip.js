/* eslint-disable */

import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
const templates = require.context(`../../../../templates`, true, /\.html$/);

(function(sales, common) {
    let companyId;

    sales.Views.Table.Operations.RelatedDocsToolTip = Backbone.View.extend({
        templateUrl: templates,
        template: 'table/operations/RelatedDocsToolTip',

        initialize(options) {
            const view = this;

            const firmRequisites = common.Data.Requisites;
            if (firmRequisites && !_.isUndefined(firmRequisites.StockIsEnabled)) {
                // Если от сервера получена информация о режиме работы БИЗ-склада
                // применяем её в модели
                view.bizStockEnabled = firmRequisites.StockIsEnabled;
            }

            view.harvestModel = options.harvestModel;
            view.showInvoiceCannotBeChangedDialog = options.showInvoiceDialog;
            view.parentView = options.parentView;
            view.on('renderWhat', view.afterRender, view);
            view.delegateEvents();
            companyId = Md.Core.Engines.CompanyId;
        },

        events: {
            'click .close': 'closeList',
            'click .rl_item a': 'openDocumentForEdit'
        },

        render(options) {
            const view = this;
            view.model = options.model;
            view.document = options.documentModel;

            TemplateManager.get(view.template, (template) => {
                const newElem = $('<div></div>').html(template);
                newElem.render(options.model.toJSON(), view.getDirectives());
                newElem.addClass('relatedListWrapper mdTooltip');

                view.$el.html(newElem);
                view.$el = view.$el.find('.relatedListWrapper').unwrap();
                view.trigger('renderWhat');
            }, this.templateUrl);

            return view;
        },

        closeList(e) {
            if (e) {
                e.preventDefault();
            }
            let view = this,
                listItem = view.$el;

            listItem.fadeOut('fast');
            $(document).off('click.related');
        },

        openDocumentForEdit(e) {
            let $elem = $(e.target),
                view = this;

            const documentModel = {
                get(attr) {
                    if (attr) {
                        return this[attr];
                    }
                }
            };

            const data = $elem.data('document');

            switch (data.ParentDocumentType) {
                case common.Data.DocumentTypes.Statement:
                    documentModel.StatementId = data.ParentDocumentId;
                    break;
                case common.Data.DocumentTypes.Waybill:
                    documentModel.WaybillId = data.ParentDocumentId;
                    break;
            }

            documentModel.DocumentType = data.Type;
            if (view.parentView.checkIsRelatedDocument(documentModel)) {
                e.preventDefault();
                view.parentView.showInvoiceCannotBeChangedDialog(documentModel);
            }
        },

        checkIsInvoiceFromOtherDocument(target) {
            const hasParentDocument = target.attr('data-parid') && (target.attr('data-pardt') == Enums.SalesDocumentTypes.Statement || target.attr('data-pardt') == Enums.SalesDocumentTypes.Waybill);
            const usnAccountant = common.Data.TariffModes.UsnAccountant == common.Utils.CommonDataLoader.FirmRequisites.get('TariffMode');
            return !!(hasParentDocument && !usnAccountant);
        },

        documentGlobalListClose() {
            const view = this;
            $(document).on('click.related', (e) => {
                if ($(e.target).closest('.relatedList').length) {
                    return;
                }

                view.closeList();
            });
        },

        afterRender() {
            this.delegateEvents();
            this.documentGlobalListClose();
        },

        centeringWindow(siblingsElem) {
            let $elem = siblingsElem.siblings('.relatedListWrapper'),
                shiftToLeft = `-${$elem.outerWidth() / 2 - 25}px`;
            $elem.css('left', shiftToLeft);
        },

        getDirectives() {
            const view = this;
            return {
                List: {
                    stNumber: {
                        html(target) {
                            return ++view.serialNumber;
                        }
                    },

                    stNdsPersent: {
                        html(target) {
                            return `(${this.NdsPercent} %)`;
                        }
                    },

                    stCost: {
                        text() {
                            return ValueCrusher.rounding(this.Price);
                        }
                    },

                    stSumm: {
                        text() {
                            return ValueCrusher.rounding(this.Count * this.Price);
                        }
                    }
                },

                relatedList: {
                    rl_item: {
                        html(options) {
                            $(options.element).append(view.getDocumentLink(this));
                        }
                    }
                }
            };
        },

        getDocumentLink(document) {
            let $doc;

            if (document.Type === 0 && !document.DocumentType) {
                $doc = $('<span />').text(document.Number);
            } else {
                let text,
                    href;

                if (document.TransferType) {
                    const operation = MoneyTools.moneyTypeSwitcher(document.TransferType);
                    const sum = document.Sum;
                    href = `#moneyDialog/${operation.type}/${operation.name}/edit/${document.Id}`;
                    text = document.Name;
                    if (sum) {
                        text += ` (${common.Utils.Converter.toAmountString(sum)} р)`;
                    }
                } else {
                    text = this.getDocName(document);
                    href = this.getDocLink(document);
                }

                $doc = this.isBADBankDoc(document.Type) ? $('<div />').text(text) : $('<a />').attr('href', href).text(text);
                $doc.data('document', document);
            }

            const $dateSpan = $('<span />').text(document.Date);

            return $doc.after($dateSpan);
        },

        isBADBankDoc(type) {
            return type === common.Data.AccountingDocumentType.PaymentOrder && Md.Data.Preloading.Application && Md.Data.Preloading.Application.CanViewBankDocs === false;
        },

        getDocName(item) {
            const name = this.getDocTypeDescription(item);
            return String.format('{0} №{1}', common.Utils.Converter.capitaliseFirstLetter(name), item.Number);
        },

        getDocTypeDescription(document) {
            const waybillCodes = PrimaryDocuments.SalesWaybillTypes;
            if (isWaybill(document) && waybillCodes && document.SubType === waybillCodes.Return.Code) {
                return 'Накладная на возврат';
            }

            return common.Data.DocumentTypeHelper.getAccountingDocumentTypeFullName(document.Type);
        },

        getDocLink(item) {
            const type = item.Type;
            let id = item.DocumentBaseId;

            let direction = item.Direction = item.Direction;
            // / ToDo: убрать после появления исходящего УПД
            if (item.Type === common.Data.AccountingDocumentType.Upd) {
                direction = Direction.Incoming;
            }

            const getUrl = direction === Direction.Incoming
                ? Md.Services.UrlGetter.getIncomingDocumentUrlByTypeAndId
                : Md.Services.UrlGetter.getOutgoingDocumentUrlByTypeAndId;

            if (type === common.Data.AccountingDocumentType.Project) {
                id = item.Id;
            }

            return getUrl(type, id);
        }
    });

    function isWaybill(document) {
        return document.Type === common.Data.DocumentTypes.Waybill;
    }
}(Sales, Common, App));
