/* eslint-disable no-undef, no-param-reassign, eqeqeq */
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import { purseOperationResources as operationTypes } from '../../../../../../resources/MoneyOperationTypeResources';

(common => {
    common.Controls.PostingsAndTaxControl = Backbone.View.extend({
        switcherStates: {
            ByHand: `Отменить и учитывать автоматически`,
            Auto: `Учитывать вручную`
        },

        templates: {
            ooo: `PostingsAndTaxControlTemplate`,
            ip: `PostingsAndTaxControlTemplateIp`
        },

        initialize(options) {
            this.options = options;
            this.isOoo = new common.FirmRequisites().get(`IsOoo`);
            this.initializeVariables();
            this.initializeEvents();
            this.render();
        },

        initializeEvents() {
            this.model.on(`change:AccountingType`, this.setSelectedAccountingItem, this);
            this.model.on(`change:PostingsAndTaxMode`, this.fillTypeSwitcher, this);
            this.taxCollection.on(`add remove reset`, this.hideAndShowTypeSwitcher, this);
            this.postingsCollection.on(`add remove reset checkSourceFailed`, this.hideAndShowTypeSwitcher, this);

            this.taxCollection.on(`change:ManualPostings`, this.validateTaxControl, this);
        },

        initializeVariables() {
            this.taxCollection = this.options.taxModel;
            this.taxCollection.sourceDocument = this.model;

            this.postingsCollection = this.options.postingsModel;
            this.postingsCollection.sourceDocument = this.model;

            this.switcherMode = common.Data.ProvidePostingType;

            this.requisites = new common.FirmRequisites();
        },

        events: {
            "click .typeItem": `onChangeAccountingType`,
            "click .typeSwitcher": `selectMode`
        },

        hideAndShowTypeSwitcher() {
            const accountingType = parseInt(this.model.get(`AccountingType`), 10);
            /** прочее списание в платежных системах не нуждается
             *  в переключении учета "вручную/автоматически". По умолчанию вручную.
             *  по этому скрываем */
            const isPurseOther = parseInt(this.model.get(`PurseOperationType`), 10) === operationTypes.PurseOperationOtherOutgoing.purseOperationType;
            const typeSwitcher = this.$(`.typeSwitcher`);
            const isIpOsno = !this.isOoo && this.taxCollection.isOsno();
            const taxationSystemType = +this.model.get(`TaxationSystemType`);
            const isPatent = taxationSystemType === TaxationSystemType.Patent;

            if (isPatent || isIpOsno) {
                typeSwitcher.hide();

                return;
            }

            if (accountingType === common.Enums.AccountingType.Tax) {
                const { taxCollection } = this;
                const myModel = taxCollection.models[0];

                const canBeManual = taxCollection.canBeManual ? taxCollection.canBeManual() : true;

                if (!taxCollection.length || taxCollection.notTaxable || !canBeManual || isPurseOther) {
                    typeSwitcher.hide();
                } else if (myModel && !myModel.get(`HasPostings`)) {
                    typeSwitcher.hide();
                } else {
                    typeSwitcher.show();
                }
            }

            if (accountingType == common.Enums.AccountingType.Posting && !isPurseOther) {
                typeSwitcher.toggle(this.postingsCollection.isProvided());
            }
        },

        render() {
            const templateId = this.requisites.get(`IsOoo`) ? this.templates.ooo : this.templates.ip;
            const template = TemplateManager.getFromPage(templateId);
            this.$el.html(template);
            this.onRender();
        },

        onRender() {
            this.checkClosedPeriod();
            this.checkOnlyMode();
            this.fillTypeSwitcher();
            this.onChangeAccountingType();
            this.initializeControls();
            this.setSelectedAccountingItem();
        },

        checkClosedPeriod() {
            if (!this.canEdit()) {
                this.$(`.typeSwitcher`).remove();
            }
        },

        canEdit() {
            const isClosed = this.requisites.inClosedPeriodWithBalanceDate(this.model.get(`Date`));
            
            return !(isClosed || this.options.readonly);
        },

        initializeControls() {
            this.useTaxControl();
            this.usePostingsControl();
        },

        checkOnlyMode() {
            const postingOnly = this.postingsCollection.onlyPostingsAndTaxMode;
            const taxOnly = this.taxCollection.onlyPostingsAndTaxMode;
            const isOnlyMode = taxOnly || postingOnly;

            if (!_.isUndefined(isOnlyMode)) {
                this.model.set({ PostingsAndTaxMode: isOnlyMode }, { silent: true });
                this.postingsAndTaxModeOnly = true;
            }
        },

        fillTypeSwitcher() {
            if (this.postingsAndTaxModeOnly) {
                return;
            }

            const switcher = this.$(`.typeSwitcher`);
            const switchLink = switcher.find(`a`);
            const mode = this.model.get(`PostingsAndTaxMode`);
            const switchElem = (switchLink.length) ? switchLink : $(`<a></a>`);
            let text = this.switcherStates.Auto;

            if (mode == this.switcherMode.ByHand) {
                text = this.switcherStates.ByHand;
            }

            switchElem.text(text);
            switcher.hide().html(switchElem).fadeIn(`fast`);
        },

        // перенести в модель
        selectMode() {
            const mode = this.model.get(`PostingsAndTaxMode`);
            let type = this.switcherMode.ByHand;

            if (mode == this.switcherMode.ByHand) {
                type = this.switcherMode.Auto;
            }

            this.model.set(`PostingsAndTaxMode`, type);
        },

        onChangeAccountingType(e) {
            this.selectAccountingType(e);
            this.hideAndShowTypeSwitcher();

            if (e && this.alreadyValidated) {
                this.isValid();
            }
        },

        selectAccountingType(e) {
            let selectedType;

            if (e) {
                selectedType = $(e.currentTarget || e.target).attr(`data-type`);
            } else {
                selectedType = this.model.get(`AccountingType`) || common.Enums.AccountingType.Tax;
            }

            this.model.set(`AccountingType`, selectedType);
        },

        setSelectedAccountingItem() {
            const items = this.$(`.typeItem`);
            const currentitem = items.filter(`[data-type="${this.model.get(`AccountingType`)}"]`);

            items.removeClass(`selected`);
            currentitem.addClass(`selected`);
        },

        onModelSave() {
            // т.к не учитывается в БУ, то необходимо очистить модель от имеющихся проводок
            const provideInAccounting = this.model.get(`ProvideInAccounting`);

            if (provideInAccounting && provideInAccounting == false) {
                this.postingsCollection.each(model => {
                    model.get(`ManualPostings`).reset();
                });
            }
        },

        isValid() {
            const taxValidationObj = this.taxControlValidation();
            const postingsValidationObj = this.isOoo ? this.postingsControlValidation() : { isValid: true, message: `` };
            const isValid = taxValidationObj.isValid && postingsValidationObj.isValid;
            let validationMessage;
            
            this.alreadyValidated = true;

            if (!isValid) {
                if (taxValidationObj.operations) {
                    this.taxControl.showOperationsValidation(taxValidationObj.operations, taxValidationObj.message);
                    validationMessage = postingsValidationObj.message;
                } else {
                    validationMessage = taxValidationObj.message + postingsValidationObj.message;
                }

                this.$(`.validationMessage.controlMessage`).html(validationMessage).slideDown(`fast`);
            } else {
                this.taxControl.hideOperationsValidation();
                this.$(`.validationMessage.controlMessage`).slideUp(`fast`).empty();
            }

            return isValid;
        },

        validateTaxControl() {
            const taxValidationObj = this.taxControlValidation();
            const $validationMessage = this.$(`.validationMessage.controlMessage`);

            this.alreadyValidated = true;

            if (!taxValidationObj.isValid) {
                if (taxValidationObj.operations) {
                    this.taxControl.showOperationsValidation(taxValidationObj.operations, taxValidationObj.message);
                } else {
                    $validationMessage.html(taxValidationObj.message).slideDown(`fast`);
                }
            } else {
                this.taxControl.hideOperationsValidation();
                $validationMessage.slideUp(`fast`).empty();
            }
        },

        // проверка на то, чтобы не было возможности сохранить документы с незаполнеными проводками
        checkForCustomPostingsWasNotRendered() {
            if (this.model.get(`Operations`)) {
                if (this.postingsControl.renderComplite === false || this.taxControl.renderComplite === false) {
                    return true;
                }
            }

            return false;
        },

        taxControlValidation() {
            const validation = this.taxControl.collection.isValid();
            const validationObj = { isValid: true, message: `` };

            if (!validation) {
                this.taxControlSimpleValidation(validationObj);
            } else if (_.isObject(validation)) {
                this.taxControlObjectValidation(validationObj, validation);
            }

            return validationObj;
        },

        taxControlSimpleValidation(validationObj) {
            validationObj.isValid = false;

            if (this.model.get(`AccountingType`) != common.Enums.AccountingType.Tax) {
                validationObj.message = `${this.taxControl.validationMessage}<br/>`;
            }

            return validationObj;
        },

        taxControlObjectValidation(validationObj, validation) {
            validationObj.isValid = false;

            if (this.model.get(`AccountingType`) == common.Enums.AccountingType.Tax) {
                const validateOperations = validation.options.operations;

                if (validateOperations && validateOperations.length) {
                    validationObj.operations = validation.options.operations;
                }

                validationObj.message = `${validation.message}<br/>`;
            } else {
                validationObj.message = `${this.taxControl.validationMessage}<br/>`;
            }

            return validationObj;
        },

        postingsControlValidation() {
            let isValid = true;
            let message = ``;
            const validation = this.postingsControl.collection.isValid();

            if (!validation) {
                isValid = false;

                if (this.model.get(`AccountingType`) == common.Enums.AccountingType.Tax) {
                    message = `${this.postingsControl.validationMessage}<br/>`;
                }
            }

            return { isValid, message };
        },

        useTaxControl() {
            this.taxControl = new common.Controls.TaxControl({
                collection: this.taxCollection,
                el: this.$(`[data-control=tax]`),
                onRender: this.options.onRenderTax,
                readonly: !this.canEdit()
            });
        },

        usePostingsControl() {
            this.postingsControl = new common.Controls.PostingsControl({
                collection: this.postingsCollection,
                el: this.$(`[data-control=posting]`),
                dataFilter: this.options.dataFilter,
                onRender: this.options.onRenderPosting,
                readonly: !this.canEdit()
            });
        }
    });
})(Common);
