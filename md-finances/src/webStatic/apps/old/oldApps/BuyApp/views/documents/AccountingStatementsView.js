/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
const templates = require.context(`../../../AccountingStatementsApp/templates`, true, /\.html$/);

(function(main, taxPosting, common) {
    const parent = main.Views.Documents.BaseDocumentView;

    main.Views.Documents.AccountingStatementsView = parent.extend({
        templateUrl: templates,
        template: 'AccountingStatements',

        saveProcessRun: false,
        documentType: common.Data.DocumentTypes.AccountingStatement,
        downloadUrl: WebApp.IncomingDocumentsTable.GetFile,

        messages: {
            deleteMessageProcess: 'Удаление бухгалтерской справки',
            deleteMessageSuccess: 'Бухгалтерская справка удалена',
            warningLinkMoneyMessage: 'У счета есть связь с операциями в Деньгах. При удалении связь будет разорвана. <br/><b>Внимание!</b> Это действие необратимо.',
            saveProcess: 'Сохранение бухгалтерской справки',
            saveSuccess: 'Бухгалтерская справка сохранена'
        },

        initialize(options) {
            Backbone.Validation.bind(this);

            parent.prototype.initialize.call(this, options);
            const action = this.model.get('action');

            this.hasTable = false;

            this.binder = new Backbone.MdModelBinder();
            this.collectionBinder = new Backbone.MdCollectionBinder();
            this.isEdit = action === 'edit';
            this.isCopy = action === 'copy';
            this.Controls = {};

            common.Mixin.DocumentDownloadMixin(this);

            const view = this;
            this.model.on('change', () => {
                const attrs = _.keys(view.model.changed);
                view.model.validate(view.model.pick.apply(view.model, attrs));
            });
        },

        getDocumentType() {
            return common.Data.AccountingDocumentType.AccountingStatement;
        },

        backUrl() {
            return Md.Services.UrlGetter.getOutgoingDocumentListUrl(this.getDocumentType());
        },

        render() {
            const view = this;

            view.model.initialAttributes = view.model.initialValues = view.model.toJSON();

            TemplateManager.get(view.template, function(template) {
                view.$el.html(template);
                ToolTip.globalMessageClose();

                view.binder.bind(view.model, view, ['Description']);
                view.delegateEvents(view.events);
                view.fillTemplate();
                view.initializeControls();

                $.validator.unobtrusive.parse(view.el);

                view.onTemplateLoaded();

                if (view.model.get('action') == 'new') {
                    view.$('input,textarea').filter("[value='']:visible").first().focus();
                }

                if (view.model.get('readOnly') == true || !view.canEdit()) {
                    view.makeReadOnly();
                }

                view.checkClosedPeriod();
                view.checkCanEdit();
                view.trigger('renderComplite');

                this.$('#Description').focus();
            }, view.templateUrl);
        },

        getBackUrl() {
            return 'accountingstatements/';
        },

        onTemplateLoaded() {
            this.createTaxSystemControl();
            this.renderNumberAndDateDocumentComponent(mdNew.AccountingStatementService);
            this.usePostingsAndTaxControl();
            this.listenForClickNotDownload();
        },

        validateHeaderField(attr) {
            if (!this.model.isValid(attr)) {
                this.$(`[data-text-for=${attr}]:visible`).click();
            } else {
                this.$(`input[data-bind=${attr}]:visible`).blur();
            }
        },

        usePostingsAndTaxControl() {
            this.Controls.postingsAndTaxControl = new common.Controls.PostingsAndTaxControl({
                model: this.model,
                taxModel: new main.Collections.PostingsAndTax.AccountingStatementTaxCollection(),
                postingsModel: new main.Collections.PostingsAndTax.AccountingStatementPostingsCollection(),
                el: this.$('[data-control=postingsAndTax]'),
                dataFilter(item) {
                    const mainCode = item.Number.substring(0, 2);
                    return mainCode != '70' && mainCode != '71' && item.Code != 760400;
                },
                readonly: !this.canEdit() || this.isClosed()
            });
        },

        createTaxSystemControl() {
            if (this.model.get('FixedAssetBaseId') > 0) {
                return;
            }

            this.taxSystemControl = new common.Controls.TaxationSystemControl(this.$('.generalFields form'), this.model);
            this.taxSystemControl.render();

            if (_.isUndefined(this.model.get('TaxationSystemType'))) {
                this.model.set('TaxationSystemType', this.taxSystemControl.getTaxationSystem().defaultType());
            }
        },

        onClickSaveDocument(options) {
            options || (options = {});

            if (!this.Controls.postingsAndTaxControl.isValid()) {
                if (options.error) {
                    options.error();
                }
                return;
            }

            parent.prototype.onClickSaveDocument.call(this, options);
        },

        copy() {
            const id = this.model.get('DocumentBaseId');
            const type = this.getDocumentType();
            const urlGetter = Md.Services.UrlGetter;

            let fn = urlGetter.getCopyOutgoingDocumentUrl;
            if (this.model.get('Direction') === Direction.Incoming) {
                fn = urlGetter.getCopyIncomingDocumentUrl;
            }

            window.location = fn(type, id);
        },

        saveAndDownloadDocument(options) {
            options || (options = {});

            const view = this;
            this.downloadType = options.docformat;

            if (this.isClosed() || !this.canEdit()) {
                this.downloadDocument();
            } else {
                this.onClickSaveDocument({
                    success() {
                        view.downloadDocument();
                        if (options.success) {
                            options.success();
                        }
                    },
                    error: options.error
                });
            }
        },

        downloadEvent(docformat) {
            this.downloadType = docformat;
            this.downloadDocument();
        },

        openPeriod() {
            const closingPeriodEventId = this.model.get('ClosingPeriodEventId');
            if (closingPeriodEventId > 0) {
                window.location = String.format('/ClosingWizards/ForPeriod?{0}#financialResult', closingPeriodEventId);
                return;
            }

            parent.prototype.openPeriod.call(this);
        },

        getDirective() {
            return {
                Description: {
                    text(target) {
                        // чертов хак - для того чтобы Опера (не с webKit'ом) могла спокойно отобразить текст вставляемый в textarea
                        $(target.element).val(target.value);
                    }
                }
            };
        }
    });
}(Buy, TaxPosting, Common));
