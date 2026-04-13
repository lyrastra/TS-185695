import DialogHelper from '@moedelo/md-frontendcore/mdCommon/helpers/DialogHelper';

(function (buy, primaryDocuments, common) {

    var parent = primaryDocuments.Views.Documents.BaseDocumentView;
    
    buy.Views.Documents.BaseDocumentView = parent.extend({
    
        initialize: function(options) {
            parent.prototype.initialize.call(this, options);
            this.hasTable = true;
            this.dialogHelper = new DialogHelper();
        },

        templateUrl: 'ClientSideApps/BuyApp/templates/',

        backUrl: '/AccDocuments/Buy/',

        validate: function () {
            var dfd = $.Deferred();
            if (this.isDocumentValid()){
                dfd.resolve();
            } else {
                dfd.reject();
            }
            return dfd;
        },

        isDocumentValid: function() {
            var form = this.$('.generalFields form');
            var recoverNdsControl = this.invoice ? this.invoice.recoverNdsControl : this.Controls.recoverNdsControl;
            form.validate();

            return form.valid()
                && this.model.isValid(true)
                && isControlValid(this.table)
                && isControlValid(this.Controls.postingsAndTaxControl)
                && isControlValid(this.invoice)
                && isControlValid(recoverNdsControl)
                && isControlValid(this.ndsDeduction)
                && isControlValid(this.payments)
                && this.middlemanContractValid();
        },

        middlemanContractValid: function() {
            if (this.middlemanContract && this.model.get('IsCompensated')) {
                return isControlValid(this.middlemanContract);
            }

            return true;
        },

        onClickSaveDocument: function(options) {
            options || (options = {});

            var view = this;
            var model = this.model;

            if (this.isDocumentValid()) {
                var postingsAndTaxControl = this.Controls.postingsAndTaxControl;
                postingsAndTaxControl && postingsAndTaxControl.onModelSave();

                var saveModel = function () {
                    if (view.saveXhr) {
                        return;
                    }

                    ToolTip.globalMessage(1, true, view.messages.saveProcess, true);

                    view.saveXhr = model.save({}, {
                        success: function (model, response) {
                            if (response.Status) {
                                model.trigger('save');

                                if (options.success) {
                                    options.success();
                                }

                                view.cacheFixedAssetsData(model);

                                $(window.document).ready(function () {
                                    _.delay(function () {
                                        ToolTip.globalMessage(1, true, view.getSaveSuccessMessage());
                                        view.returnToPreviousPage();
                                    }, 1000);
                                });
                            } else {
                                view.saveXhr = null;
                                if (options.error) {
                                    options.error();
                                }
                                error();
                            }
                        },
                        error: function() {
                            if (options.error) {
                                options.error();
                            }
                            error();
                        },
                    });
                };
                 
                if (model.initialAttributes.Number == model.get("Number") && model.get("action") == "edit") {
                    saveModel();
                } else {
                    view.checkIsNumberBusy(function () {
                        view.ConfirmDialog = new Common.Views.Dialogs.ConfirmDialogView({
                            message: 'Номер документа не уникален, вы хотите добавить еще один документ с таким номером?',
                            confirm: function () {
                                view.dialogHelper.destroy();
                                saveModel();
                            },
                            cancel: function () {
                                view.dialogHelper.closeDialog();
                                if (options.error) {
                                    options.error();
                                }
                                view.$("#Number").focus();
                            },
                            okButton: "Да"
                        });

                        view.dialogHelper.showDialog({
                            contentView: view.ConfirmDialog
                        });
                    }, function () {
                        saveModel();
                    });
                }
            } else {
                if (options.error) {
                    options.error();
                }
                view.showModelError();
                var position = view.$('.input-validation-error').first().offset();
                if (position) {
                    $('body').animate({
                        scrollTop: position.top - 40
                    });
                }
            }
        },

        cacheFixedAssetsData: function (model) {
            var documentBaseId = model.get('SavedBaseId');
            var fixedBaseId = model.get('FixedAssetBaseId');
            var fixedAssetsTempData = localStorage.getItem('FixedAssetsTemp') ? JSON.parse(localStorage.getItem('FixedAssetsTemp')) : {};
            var current = fixedAssetsTempData[fixedBaseId] || [];

            if (model.get('action') === 'investment' && model.get('FixedAssetBaseId')) {
                if (!_.contains(current, documentBaseId)) {
                    current.push(documentBaseId);
                    fixedAssetsTempData[fixedBaseId] = current;
                    localStorage.setItem('FixedAssetsTemp', JSON.stringify(fixedAssetsTempData));
                }
            }
        },

        showModelError: function () {
            this.$(".model-validation-error").remove();
            if (!this.model.isValid() && _.isFunction(this.model.validateAttrs)) {
                var error = this.model.validateAttrs().split(':');
                var errorSpan = $("<span />")
                    .addClass("field-validation-error model-validation-error")
                    .attr("data-message-for", error[0])
                    .html(error[1]);

                this.$(String.format("[data-model-validation={0}]", error[0])).after(errorSpan);
            }
        },

        checkIsNumberBusy: function (busyFunc, freeFunc) {
            var model = this.model,
                documentNumber = new buy.Models.Documents.DocumentNumber({
                number: model.get("Number"),
                documentType: model.getDocumentType(),
                date: model.get("Date")
            });

            documentNumber.isBusy(busyFunc, freeFunc);
        },

        setAdoptionNdsDeductionHint: function (element) {
            var icon = $("<span />", {
                'class': 'qtip_balance_span'
            });

            element = element || this.$("#NdsForDeduction");
            element.closest(".field").find(".title").find("*").last().append(icon);
           
            common.Utils.HintHelper.setAdoptionNdsDeductionHint(icon);
        },

        setKudirHint: function (hintValue, withDelay) {
            var element = this.$("#KudirUnit"),
                icon = element.find(".qtip_balance_span");

            if (icon.length == 0) {
                icon = $("<span />", {
                    'class': 'qtip_balance_span'
                })
                .appendTo(element);
            }

            common.Utils.HintHelper.setKudirHint(icon, hintValue, withDelay);
        },

        disableFormForClosedPeriod: function () {
            var isAdvanceInvoice = this.model.has('InvoiceType') && this.model.get('InvoiceType') === common.Data.InvoiceType.Advance;

            var isNotComponentFilter = function() {
                var filter = '.payments,.recoverNdsControl,.invoiceControl,[data-control=recoverNds]';

                if (!isAdvanceInvoice) {
                    filter += ',[data-control=takeNdsForDeduction]';
                }

                return !$(this).closest(filter).length;
            };

            disableKontragent(this.$('[data-bind=KontragentName]'), this.model.pick('KontragentName', 'KontragentId'));

            var className = 'closedPeriod-disabled';
            this.$('[type=radio],[type=checkbox]').filter(isNotComponentFilter).addClass(className).attr('disabled', 'disabled');
            this.$("[type=text],[type=number], textarea").filter(isNotComponentFilter).addClass(className).attr("readonly", true).attr('disabled', 'disabled');

            var inputWithMask = this.$('form input:visible').filter(function(){ return $(this).data('MdNumberInputMask'); });
            inputWithMask.each(replaceInputWithText);

            this.$('.collapsible-container, [data-control=NdsType]').filter(isNotComponentFilter).each(replaceWithText);
            this.$('.mdCustomSelect').attr('disabled', 'disabled');
            this.$('input.hasDatepicker').attr('disabled', 'disabled');

            this.$("#cancelLink").hide();
            this.$(".buttons").find(".download .innerText").text("Скачать");

            this.$('#saveDocument,#deleteDocument,.mdCloseLink:not([data-closedperiod=enable]).delete_link').filter(isNotComponentFilter).hide();
            this.$('.mdSpanTransformerClose').hide();
            
            var enabledSelectors = '[data-closedperiod=enable]';

            if (this.model.isEnabledInClosedPeriod()) {
                enabledSelectors += ', #addNdsDeductionRow, #addNewPaymentWrap, [data-link=newPayment], [data-link=recoverNds]';
                this.$('[data-bind=NdsDeductions_Sum]').removeClass(className).attr("readonly", false).removeAttr('disabled');
                this.$('[data-bind=NdsDeductions_Date]').removeClass(className).attr("readonly", false).removeAttr('disabled');
                this.$('.closedPeriodHint').hide();
                this.$("#saveDocument").show();
                this.$("#cancelLink").show();
            }

            this.$('span, a:not([href])').not(enabledSelectors).filter(isNotComponentFilter).on("click.closedPeriod-disabled", function(event) {
                event.preventDefault();
                return false;
            });
        },

        destroy: function(){
            this.invoice && this.invoice.destroy();
            this.Controls.recoverNdsControl && this.Controls.recoverNdsControl.destroy();
        },

        checkClosedPeriod: function() {
            var isClosed = this.isClosed();
            if (isClosed) {
                this.disableFormForClosedPeriod();
            }
            return isClosed;
        },

        renderNumberAndDateDocumentComponent: function(documentService) {
            var $el = this.$el.find('.js-numberAndDate');
            new Md.Components.NumberAndDateDocument({
                model: this.model,
                el: $el,
                loadNumber: documentService ? documentService.loadDocumentNumber : null
            }).render();
        },

        renderAccountingReadonlyLabel: function() {
            new Md.Components.AccountingReadonlyLabel({
                model: this.model,
                el: this.$el
            }).render();
        }
    });

    buy.Views.Documents.BaseDocumentView.extend = function (protoProps, staticProps) {
        if (protoProps.events) {
            protoProps.events = _.extend(_.clone(this.prototype.events), protoProps.events);
        }
        return Backbone.View.extend.call(this, protoProps, staticProps);
    };

    function isControlValid(control) {
        return (control && control.isValid) ? control.isValid(true) : true;
    }

    function replaceInputWithText(){
        var input = $(this);
        input.replaceWith(input.val());
    }

    function replaceWithText(){
        var wrapper = $(this);
        wrapper.html(wrapper.text()).addClass('collapsible-container--closedPeriod').off();
    }

    function error(){
        ToolTip.globalMessageClose();
        Backbone.history.navigate('error/', { trigger: true });
    }

    function disableKontragent($input, data){
        var url = _getKontragentUrl(data.KontragentId);
        var link = '<a href="{0}" target="_blank">{1}</a>'.format(url, data.KontragentName);
        $input.closest('.controls').html(link);
    }

    function _getKontragentUrl(kontragentId) {
        var urls = window.ApplicationUrls;
        var companyId = Md.Core.Engines.CompanyId;
        var requisites = Md.Data.Preloading.Requisites || {};
        var url = urls.editKontragent;

        url = url.format(kontragentId);

        return companyId.getLinkWithParams(url);
    }

})(Buy, PrimaryDocuments, Common);
