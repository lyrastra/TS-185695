/* global _ */
/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toDate } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import operationBillsBehavior from '../../../../../../../../components/OperationBills/operationBillsBehavior';
import TaxationSystemSelect from '../../../../../Components/TaxationSystemSelect';
import PatentSelect from '../../../../../Components/PatentSelect';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import { convertAccPolToFinanceNdsType, convertFinanceToAccPolNdsType } from '../../../../../../../../resources/ndsFromAccPolResource';
import renderNdsDropdown from '../../../../../Components/NdsDropdown/NdsDropdownHelper';
import renderNdsWarningIcon from '../../../../../Components/NdsWarningIcon/NdsWarningIconHelper';

// касса поступление оплата от покупателя

(function(cash, md, common) {
    const kontragentPayment = Marionette.LayoutView.extend({
        initialize() {
            this.$additionalBox = this.options.$additionalBox || ``;
            _.extend(this, cash.Views.initKontragentAutocompleteMixin);
            _.extend(this, cash.Views.setMaxSumTooltipMixin);
            _.extend(this, cash.Views.ndsUsnMessage);
            this.isOoo = new common.FirmRequisites().get(`IsOoo`);

            const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current();
            const isUsn = taxSystem.isUsn();
            const isOsno = taxSystem.isOsno();
            this.isEdit = this.model.get(`Id`);

            if (this.model.get('isChangeOrderType')) {
                this.model.set('Destination', this.model.get(`Direction`) === Direction.Incoming ? 'Оплата от покупателя' : 'Оплата поставщику');
            }

            this.model.set(`IsUsn`, isUsn);
            this.model.set(`IsOsno`, isOsno);
            this.listenTo(this.model, `change:TaxationSystemType`, this.changeNdsOption);
            this.listenTo(this.model, `change:IncludeNds`, ()=>{
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
                renderNdsDropdown({ model: this.model, changeNds: this.setMediationNdsType.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true});
            });

            this.listenTo(this.model, `change:CurrentRate`, () => {
                if (!this.isEdit && this.model.get(`Direction`) === Direction.Incoming) {
                    const newRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
                    this.model.set(`NdsType`, newRate);
                    this.model.set(`MediationNdsType`, newRate);
                }
                    
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
                renderNdsDropdown({ model: this.model, changeNds: this.setMediationNdsType.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true});
            });

            const taxationSystemType = this.model.get(`TaxationSystemType`);
            const direction = this.model.get(`Direction`);

            if (direction === Direction.Outgoing && taxationSystemType === TaxationSystemType.Patent) {
                let currentTaxSys = taxationSystemType;

                if (isUsn) {
                    currentTaxSys = TaxationSystemType.Usn;
                }
                
                if (isOsno) {
                    currentTaxSys = TaxationSystemType.Osno;
                }

                this.model.set(`TaxationSystemType`, currentTaxSys);
            }
        },

        regions: {
            taxationSystemTypeSelect: `.js-taxationSystemType`,
            patentSelect: `.js-patent`
        },

        onRender() {
            this.bind();
            this.initializeControls();
            this.initTooltips();
            this.model.on(`change:IncludeNds change:Date`, this.showNdsMessage, this);
            this.model.on(`change:Date`, this.renderTaxationSystemRow, this);
            this.model.on(`change:IsMediation`, this.mediationChange, this);
            this.model.on(`change:TaxationSystemType`, this.changeTaxationSystemType, this);
            this.showNdsMessage();
            this.checkMediation();
            this.checkMediationNds();
            this.changeNdsOption();
            this.mediationChange();
            this.renderTaxationSystemRow();
            this.setNdsSumVisibility();
            this.setMediationNdsSumVisibility();
            setTimeout(() => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
                renderNdsDropdown({ model: this.model, changeNds: this.setMediationNdsType.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true});
            }, 0);

            if (!this.model.get(`Closed`)) {
                setTimeout(() => {
                    renderNdsWarningIcon({
                        model: this.model,
                        changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
                    });
                    renderNdsWarningIcon({
                        model: this.model,
                        changeNdsFromAccPolicy: this.setMediationNdsFromAccPolicy.bind(this),
                        mountPointId: `#mediationNdsWarningIconMountPoint`
                    });
                }, 0);
            }
    
            this.checkNdsWarningIcon();
            this.checkMediationNdsWarningIcon();
            this.listenTo(this.model, `change:Date`, () => {
                if (!this.isEdit) {
                    this.changeNdsOption();
                }
            });
            this.listenTo(this.model, `change:Date`, this.checkMediation);
            this.listenTo(this.model, `change:Date`, () => {
                if (!this.isEdit) {
                    this.checkMediationNds();
                }
            });
            this.listenTo(this.model, `change:Date`, () => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
                renderNdsDropdown({ model: this.model, changeNds: this.setMediationNdsType.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true});
            });
            this.listenTo(this.model, `change:IsMediation`, () => {
                if (!this.isEdit) {
                    this.checkMediationNds()
                }
            });
            this.listenTo(this.model, `change:IncludeMediationNds`, () => {
                this.setCurrentRate();
                this.setMediationNds();
            });
            this.listenTo(this.model, `change:TaxationSystemType`, this.changeMediationNds);
            this.listenTo(this.model, `change:IncludeNds`, this.setNdsSumVisibility());
            this.listenTo(this.model, `change:Date`, this.setNdsSumVisibility());
            this.listenTo(this.model, `change:NdsType change:CurrentRate`, () => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
                renderNdsWarningIcon({
                    model: this.model,
                    changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
                });
                this.checkNdsWarningIcon()});
            this.listenTo(this.model, `change:MediationNdsType change:CurrentRate`, () => {
                renderNdsWarningIcon({
                    model: this.model,
                    changeNdsFromAccPolicy: this.setMediationNdsFromAccPolicy.bind(this),
                    mountPointId: `#mediationNdsWarningIconMountPoint`
                });
                renderNdsDropdown({ model: this.model, changeNds: this.setMediationNdsType.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true});
                this.setMediationNdsSumVisibility();
                this.checkMediationNdsWarningIcon()});
            this.listenTo(this.model, `change:IsMediation`, this.checkNdsWarningIcon);
            this.listenTo(this.model, `change:IsMediation`, this.checkMediationNdsWarningIcon);

            const $kontragentAccountCode = this.$(`[data-bind=KontragentAccountCode]`);

            if (!$kontragentAccountCode.is(`:checked`)) {
                $kontragentAccountCode.first().attr(`checked`, `checked`).change();
            }

            return this;
        },

        onDestroy() {
            this.clearMediation();
            this.unstickit(this.model, this.bindings);
            this.undelegateEvents();
            this.isMediationTooltip && this.isMediationTooltip.destroy();
            this.mediationCommissionTooltip && this.mediationCommissionTooltip.destroy();

            this.model.off(`change:Date`, this.checkMediation);
            this.model.off(`change:Date`, this.changeNdsOption);
            this.model.off(`change:Date`, this.renderTaxationSystemRow, this);
            this.model.off(`change:TaxationSystemType`, this.changeTaxationSystemType);
        },

        checkNdsWarningIcon() {
            const isUsn = this.model.get(`IsUsn`);
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);  
            const icon = this.$(`#ndsWarningIconMountPoint`);
            const currentRate = this.model.get(`CurrentRate`);
            const ndsType = this.model.get(`NdsType`);
            const isMediation = this.model.get(`IsMediation`)
    
            if (currentRate === convertFinanceToAccPolNdsType[ndsType] || isMediation || !isUsn || documentDate.isBefore(ndsUsn2025Date) || this.model.get(`Direction`) === Direction.Outgoing) {
                icon.hide();
            } else {
                icon.show();
            }
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

        checkMediationNdsWarningIcon() {
            const icon = this.$(`#mediationNdsWarningIconMountPoint`);
            const currentRate = this.model.get(`CurrentRate`);
            const ndsType = this.model.get(`MediationNdsType`);    
    
            if (currentRate === convertFinanceToAccPolNdsType[ndsType] || this.model.get(`Direction`) === Direction.Outgoing) {
                icon.hide();
            } else {
                icon.show();
            }
        },

        setNdsFromAccPolicy() {
            const currentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
    
            this.model.set({ NdsType: currentRate });
    
            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
        },

        setMediationNdsFromAccPolicy() {
            const currentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];    
            this.model.set({ MediationNdsType: currentRate });    
            renderNdsDropdown({ model: this.model, changeNds: this.setMediationNdsType.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true});            
        },

        setMediationNdsType({ value }) {            
            this.model.set('MediationNdsType', value)
            this.setMediationNdsSumVisibility();
        },

        setMediationNds() {
            if (!this.model.get(`IncludeMediationNds`)) {
                    this.model.set({ MediationNdsType: null });
                    this.model.set({ MediationNdsSum: null });
                } else{
                this.model.set({ MediationNdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
            }
            this.setMediationNdsSumVisibility();
            renderNdsDropdown({ model: this.model, changeNds: this.setMediationNdsType.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true});
            
            renderNdsWarningIcon({
                model: this.model,
                changeNdsFromAccPolicy: this.setMediationNdsFromAccPolicy.bind(this),
                mountPointId: `#mediationNdsWarningIconMountPoint`
            });
        },

        changeMediationNds() {
            const isUsn = this.model.get(`IsUsn`);
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);

            const previous = dateHelper(this.model.previous(`Date`), `DD.MM.YYYY`).year();
            const current = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`).year();

            const previousTaxSys = this.model.previous(`TaxationSystemType`) || this.model.get(`TaxationSystemType`);
            const currentTaxSys = this.model.get(`TaxationSystemType`);
            
            if (current === previous && (+previousTaxSys === +currentTaxSys || !previousTaxSys || !currentTaxSys)) {                  
                return;
            }

            if (isUsn && documentDate.isSameOrAfter(ndsUsn2025Date)) {
                if (this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
                    this.model.set({ MediationNdsType: null });
                    this.model.set({ MediationNdsSum: null });
                    this.model.set({ IncludeMediationNds: null });
                } else{
                    this.model.set({ IncludeMediationNds: true });
                    this.model.set({ MediationNdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
                }
            }
        },

        checkMediationNds() {
            const isUsn = this.model.get(`IsUsn`);
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);
            const MediationNds = this.$(`.js-mediationNds`);

            if (isUsn && documentDate.isSameOrAfter(ndsUsn2025Date)) {
                MediationNds.removeClass(`hidden`);

                if ((typeof this.model.get(`IncludeMediationNds`) !== `boolean` || !this.model.get(`MediationCommission`)) && this.model.get(`TaxationSystemType`) !== TaxationSystemType.Patent) {
                    this.model.set({ IncludeMediationNds: true });
                    this.model.set({ MediationNdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
                } else if (!this.model.get(`IncludeMediationNds`)) {
                    this.model.set({ MediationNdsSum: null });
                    this.model.set({ MediationNdsType: null });
                }

                if (this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent && !this.isEdit) {
                    this.model.set({ MediationNdsType: null });
                    this.model.set({ MediationNdsSum: null });
                    this.model.set({ IncludeMediationNds: null });
                }

                renderNdsDropdown({ model: this.model, changeNds: this.setMediationNdsType.bind(this), mountPointId: `#mediationNdsTypeSelect`, isMediationNds: true});
            } else {
                MediationNds.addClass(`hidden`);
                this.clearMediationNds();
            }
        },

        clearMediationNds() {
            this.model.unset(`MediationNdsType`);
            this.model.unset(`MediationNdsSum`);
            this.model.unset(`IncludeMediationNds`);
        },


        changeNds({ value }) {
            this.model.set('NdsType', value)
        },

        changeNdsOption() {
            const nds20PercentDate = dateHelper(`2019-01-01`);
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const ndsAfter2026 = dateHelper(`2026-01-01`);
            const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);
            const isOsno = this.model.get(`IsOsno`);
            const isUsn = this.model.get(`IsUsn`);

            const previous = dateHelper(this.model.previous(`Date`), `DD.MM.YYYY`).year();
            const current = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`).year();

            const previousTaxSys = this.model.previous(`TaxationSystemType`) || this.model.get(`TaxationSystemType`);
            const currentTaxSys = this.model.get(`TaxationSystemType`);
            
            if (current === previous && (+previousTaxSys === +currentTaxSys || !previousTaxSys || !currentTaxSys)) {    
                return;
            }

            if (documentDate.isBefore(ndsUsn2025Date) && isUsn && !this.model.get(`NdsSum`)) {
                this.model.set({ IncludeNds: false });
            }

            if (isUsn && documentDate.isBefore(ndsUsn2025Date)) {
                this.model.set({ IncludeNds: false });
            }
            
            if (documentDate.isSameOrAfter(nds20PercentDate) && documentDate.isBefore(ndsAfter2026)) {
                this.model.set({ NdsType: common.Data.BankAndCashNdsTypes.Nds20 });
            }

            if (documentDate.isBefore(nds20PercentDate)) {
                this.model.set({ NdsType: common.Data.BankAndCashNdsTypes.Nds18 });
            }

            if (documentDate.isAfter(ndsAfter2026) && isOsno) {
                this.model.set({ NdsType: common.Data.BankAndCashNdsTypes.Nds22 });
            }

            if (documentDate.isSameOrAfter(ndsUsn2025Date) && isUsn && this.model.get(`TaxationSystemType`) !== TaxationSystemType.Patent) {
                if (this.model.get(`Direction`) === Direction.Outgoing) {
                    this.model.set({ NdsType: common.Data.BankAndCashNdsTypes.None });
                } else {
                    this.model.set({ NdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
                }
                this.model.set({ IncludeNds: true });                
            }

            if (documentDate.isSameOrAfter(ndsUsn2025Date) && isUsn && this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
                this.model.set({ NdsType: null });
                this.model.set({ NdsSum: null });
                this.model.set({ IncludeNds: false });
            }

            this.setNdsSumVisibility();

            renderNdsWarningIcon({
                model: this.model,
                changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
            });

            renderNdsWarningIcon({
                model: this.model,
                changeNdsFromAccPolicy: this.setMediationNdsFromAccPolicy.bind(this),
                mountPointId: `#mediationNdsWarningIconMountPoint`
            });

            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) })
        },

        changeTaxationSystemType() {
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);

            if (documentDate.isSameOrAfter(ndsUsn2025Date) && this.model.get(`IsUsn`) && this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
                this.model.set({ NdsType: null });
                this.model.set({ NdsSum: null });
                this.model.set({ IncludeNds: false });
            }
             
            this.renderPatentSelect();
        },

        initializeControls() {
            this.initKontragentAutocomplete();
            this.setMaxSumTooltip();

            this.$(`[data-type=float]`).decimalMask();
            this.$(`select`).change();
            this.controls = {
                documents: this.renderDocumentTable()
            };
            const { model } = this;
            this.$(`[data-bind=ProjectNumber]`).projectAutocomplete(model, {
                direction: model.get(`Direction`)
            });
        },

        setNdsSumVisibility() {
            const includeNds = this.model.get(`IncludeNds`);

            if (!includeNds) {
                return;
            }

            const hideSumForTypes = [0, -1];
            const ndsSumBlock = this.$(`[data-bind="NdsSum"]`).parent();
            const ndsType = this.model.get(`NdsType`);

            if (_.contains(hideSumForTypes, ndsType)) {
                ndsSumBlock.hide();

                return;
            }

            ndsSumBlock.show();
        },

        setMediationNdsSumVisibility() {
            const includeMediationNds = this.model.get(`IncludeMediationNds`);

            if (!includeMediationNds) {
                return;
            }

            const hideSumForTypes = [0, -1];
            const MediationNdsSumBlock = this.$(`[data-bind="MediationNdsSum"]`).parent();
            const MediationNdsType = this.model.get(`MediationNdsType`);

            if (_.contains(hideSumForTypes, MediationNdsType)) {
                MediationNdsSumBlock.hide();

                return;
            }

            MediationNdsSumBlock.show();
        },

        renderTaxationSystemType() {
            if (!this.hasTaxationSystemRow) {
                return;
            }

            const select = new TaxationSystemSelect({
                model: this.model
            });
            const region = this.getRegion(`taxationSystemTypeSelect`);

            region && region.show(select);
        },

        renderPatentSelect() {
            const region = this.getRegion(`patentSelect`);
            const date = toDate(this.model.get(`Date`));

            if (!this.isPatentSelectVisible()) {
                this.unsetPatent();

                return;
            }

            taxationSystemService.getActivePatents(date)
                .then(res => {
                    const content = new PatentSelect({
                        model: this.model,
                        activePatents: res
                    });

                    region && region.show(content);
                })
                .catch(() => {
                    this.unsetPatent();
                });
        },

        unsetPatent() {
            const region = this.getRegion(`patentSelect`);

            this.model.unset(`PatentId`);
            region && region.empty();
        },

        renderTaxationSystemRow() {
            this.renderTaxationSystemType();
            this.renderPatentSelect();
        },

        isPatentSelectVisible() {
            return this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent;
        },

        mediationChange() {
            const isMediation = this.model.get(`IsMediation`);
            const mediationField = this.$(`[data-mediationField]`);
            const kontragentAccountCode = this.$(`[data-control=kontragentAccountCode]`);

            if (isMediation) {
                mediationField.removeClass(`hidden`);
                kontragentAccountCode.addClass(`hidden`);
                this.model.set(`KontragentAccountCode`, SyntheticAccountCodesEnum._62_02);
            } else {
                mediationField.addClass(`hidden`);
                kontragentAccountCode.removeClass(`hidden`);
                this.model.set({ MediationCommission: null });
            }
        },

        mediationListener() {
            this.model.on(`change:IsMediation`, this.mediationChange, this);
        },

        checkMediation() {
            const selectDate = Converter.toDate(this.model.get(`Date`));

            const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current(selectDate);

            const checkboxField = this.$(`[data-control=MediationCheckboxField]`);

            if (!taxSystem.get(`IsUsn`)) {
                checkboxField.addClass(`hidden`);
                this.clearMediation();
            } else {
                checkboxField.removeClass(`hidden`);
            }
        },

        clearMediation() {
            this.model.unset(`MediationCommission`);
            this.model.unset(`IsMediation`);
        },

        renderDocumentTable() {
            const control = new Core.Components.LinkedDocumentsControl({
                el: this.$('[data-control=linkedDocuments]'),
                model: this.model,
                autocomplete: this.getLinkedDocumentAutocomplete()
            }).render();

            const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current();

            const isIpOsno = !this.isOoo && taxSystem.isOsno();

            if (taxSystem.isUsn15() || isIpOsno) {
                control.addDocumentToMenu(common.Data.AccountingDocumentType.InventoryCard);
            }

            control.addDocumentToMenu(common.Data.AccountingDocumentType.Upd);

            if (this.model.get(`Direction`) === Direction.Outgoing) {
                control.addDocumentToMenu(common.Data.AccountingDocumentType.ReceiptStatement);
            }

            this.on(`resetDocuments`, documents => {
                control.resetDocuments(documents);
            });

            this.model.on(`change:Sum`, () => {
                control.toggleAddDocLink();
            });

            return control;
        },

        initKontragentAutocomplete() {
            const { model } = this;

            this.$('[data-bind=KontragentName]').saleKontragentWaybillAutocomplete({
                onSelect(selected) {
                    model.set({
                        KontragentId: selected.object.Id,
                        KontragentName: selected.object.Name
                    });
                },
                clean() {
                    model.unset(`KontragentId`);
                    model.unset(`KontragentName`);
                },
                onBlur() {
                    if (!model.get(`KontragentName`)) {
                        model.unset(`KontragentId`);
                    }
                },
                IsBuy: model.get(`Direction`) === Direction.Outgoing
            });
        },

        initTooltips() {
            this.isMediationTooltip = new mdNew.MdTooltip({
                $anchor: this.$('.js-isMediationIcon'),
                content: `Включите, если это поступление, с которого Вы хотите удержать посредническое вознаграждение` +
                `(если такой способ комиссии предусмотрен посредническим договором).`
            });
            this.mediationCommissionTooltip = new mdNew.MdTooltip({
                $anchor: this.$(`.js-mediationCommissionIcon`),
                content: `Укажите сумму, если по условию посреднического договора комиссия посредника удерживается ` +
                `в момент поступления денежных средств от покупателя. Эта сумма будет учтена как доход в УСН.`
            });
        },

        bindings: {
            '#mediationNdsSumContainer': {
                observe: `IncludeMediationNds`,
                visible: true
            },

            '#ndsSumContainer': {
                observe: `IncludeNds`,
                visible: true
            },

            '[data-bind=IsMediation]': {
                observe: `IsMediation`,
                events: `change`
            },

            '[data-bind=KontragentAccountCode]': {
                observe: `KontragentAccountCode`,
                events: `change`
            },

            '[data-bind=MediationCommission]': {
                observe: `MediationCommission`,
                events: [`blur`, `change`]
            },

            '[data-bind=MediationNdsSum]': {
                observe: `MediationNdsSum`,
                events: [`blur`, `change`]
            },

            '[data-bind=MediationNdsType]': {
                observe: `MediationNdsType`,
                selectOptions: {
                    collection() {
                        return getMediationNdsOptions();
                    }
                }
            }
        },

        behaviors() {
            if (this.options.model.get(`Id`) > 0) {
                return;
            }

            return this.getPaymentAutomationBehavior();
        }
    });

    cash.Views.toSupplier = kontragentPayment.extend({
        template: '#ToSupplierTemplate',

        getLinkedDocumentAutocomplete() {
            return function(options, model) {
                switch (model.get('DocumentType')) {
                    case common.Data.AccountingDocumentType.InventoryCard:
                        $.fn.bankInventaryCardReasonAutocomplete.call(this, options);

                        break;
                    default:
                        $.fn.bankIncomingReasonAutocomplete.call(this, options);
                }
            };
        },

        getPaymentAutomationBehavior() {
            return {
                PaymentAutomation: {
                    behaviorClass: md.Behaviors.PaymentAutomation,
                    autoDocsUrl: `/Accounting/PaymentAutomation/GetIncomingReasonDocuments`
                }
            };
        }
    });

    cash.Views.fromKontragent = kontragentPayment.extend({
        template: `#FromKontragentTemplate`,

        hasTaxationSystemRow: true,

        getLinkedDocumentAutocomplete() {
            return $.fn.bankOutgoingReasonAutocomplete;
        },

        getPaymentAutomationBehavior() {
            return {
                PaymentAutomation: {
                    behaviorClass: md.Behaviors.PaymentAutomation,
                    autoDocsUrl: `/Accounting/PaymentAutomation/GetOutgoingReasonDocuments`
                }
            };
        },

        behaviors() {
            return {
                OperationBills: {
                    behaviorClass: operationBillsBehavior
                }
            };
        }
    });
}(Cash, Md, Common));
