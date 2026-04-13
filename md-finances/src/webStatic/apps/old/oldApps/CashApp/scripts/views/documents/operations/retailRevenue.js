/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import TaxationSystemSelect from '../../../../../Components/TaxationSystemSelect';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import { toDate } from '@moedelo/frontend-core-v2/helpers/converter';
import PatentSelect from '../../../../../Components/PatentSelect';
import renderNdsDropdown from '../../../../../Components/NdsDropdown/NdsDropdownHelper';
import { convertAccPolToFinanceNdsType, convertFinanceToAccPolNdsType } from '../../../../../../../../resources/ndsFromAccPolResource';
import renderNdsWarningIcon from '../../../../../Components/NdsWarningIcon/NdsWarningIconHelper';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

// касса розничная выручка

(function(cash) {
    cash.Views.retailRevenue = Marionette.LayoutView.extend({
        template: '#RetailRevenueTemplate',

        regions: {
            taxationSystemTypeSelect: `.js-taxationSystemType`,
            patentSelect: `.js-patent`
        },

        initialize() {
            _.extend(this, cash.Views.initWorkerAutocompleteMixin);
            const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current(Converter.toDate(this.model.get('Date')));
            this.isUsn = taxSystem.get('IsUsn');
            this.isOsno = taxSystem.get('IsOsno');
            this.isEdit = this.model.get(`Id`);
            this.changeNdsOption();
            this.showZReport();

            this.listenTo(this.model, `change:IncludeNds`, () => {
                const newRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
                this.model.set(`NdsType`, newRate);
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            });

            this.listenTo(this.model, `change:CurrentRate`, () => {
                if (!this.isEdit) {
                    const newRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
                    this.model.set(`NdsType`, newRate);
                    this.changeNdsOption();
                }
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            });


            setTimeout(() => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            }, 0);

            if (!this.model.get(`Closed`)) {
                setTimeout(() => {
                    renderNdsWarningIcon({
                        model: this.model,
                        changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
                    });
                }, 0);
            }

            this.listenTo(this.model, `change:NdsType change:CurrentRate`, () => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
                this.checkNdsWarningIcon();
            });

            this.model.on('change:Date', function() {
                this.setCurrentRate();
                if (!this.isEdit) {
                    this.changeNdsOption();
                }

                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
                this.renderTaxationSystemSelect();
                this.renderPatentSelect();
                this.hideNdsControl();
                this.showZReport();

            }, this);
            this.model.on(`change:TaxationSystemType`,  function() {
                this.changeNdsOption();
                this.renderPatentSelect();
                this.hideNdsControl();
            }, this);
        },

        onRender() {
            this.bind();
            this.initializeControls();
            this.hideNdsControl();            
            this.renderTaxationSystemSelect();
            this.renderPatentSelect();
            this.checkNdsWarningIcon();
        },

        initializeControls() {
            this.$('[data-type=float]').decimalMask();
            this.initWorkerAutocomplete({
                url: '/Payroll/Workers/GetWorkersAndIp'
            });
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

        showZReport() {
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current();
            const isUsn = taxSystem.isUsn();
            const isOsno = taxSystem.isOsno();
            const isUsnAfter2025 = dateHelper(this.model.get('Date')).isSameOrAfter(ndsUsn2025Date) && isUsn

            const block = $(`[data-bind="ZReportNumber"]`).parent();

            if(isUsnAfter2025 || isOsno) {
                block.show();
            } else {
                block.hide();
                this.model.set({ZReportNumber: null})
                this.model.get('ZReportNumber')
            }
        },

        hideNdsControl() {
            const ndsUsn2025Date = dateHelper(`2025-01-01`);

            const row = this.$('#NdsCheckbox').parents('.fieldRow');
            if (this.isOsno || (this.isUsn && dateHelper(this.model.get('Date')).isSameOrAfter(ndsUsn2025Date))) {
                row.show();

                this.setNdsSumVisibility();
            } else {
                this.model.set({ NdsType: null });
                this.model.set({ NdsSum: null });
                this.model.set({ IncludeNds: false });
                row.hide();
            }
        },

        changeNds({ value }) {
            this.model.set(`NdsType`, value);
            renderNdsWarningIcon({
                model: this.model,
                changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
            });
        },

        checkNdsWarningIcon() {
            const isUsn = this.model.get(`IsUsn`);
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);  
            const icon = this.$(`#ndsWarningIconMountPoint`);
            const currentRate = this.model.get(`CurrentRate`);
            const ndsType = this.model.get(`NdsType`);
    
            if (currentRate === convertFinanceToAccPolNdsType[ndsType] || !isUsn || documentDate.isBefore(ndsUsn2025Date) || this.model.get(`Direction`) === Direction.Outgoing) {
                icon.addClass('hidden');
            } else {
                icon.removeClass('hidden');
            }
        },

        setNdsFromAccPolicy() {
            const currentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
    
            this.model.set({ NdsType: currentRate });
    
            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
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

        changeNdsOption() {
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const nds20PercentDate = dateHelper('2019-01-01');
            const ndsAfter2026 = dateHelper(`2026-01-01`);
            const documentDate = dateHelper(this.model.get('Date'), 'DD.MM.YYYY');
            const previousDocumentDate = dateHelper(this.model.previous('Date'), 'DD.MM.YYYY');

            const previousTaxSys = this.model.previous(`TaxationSystemType`) || this.model.get(`TaxationSystemType`);
            const currentTaxSys = this.model.get(`TaxationSystemType`);

            if (this.isOsno) {
                if (currentTaxSys === TaxationSystemType.Patent) {
                    return;
                }

                if (documentDate.isSameOrAfter(ndsAfter2026) && previousDocumentDate.isBefore(ndsAfter2026)) {
                    this.model.set({ NdsType: Common.Data.BankAndCashNdsTypes.Nds22 });
                }

                if (documentDate.isSameOrAfter(nds20PercentDate) && previousDocumentDate.isBefore(nds20PercentDate)) {
                    this.model.set({ NdsType: Common.Data.BankAndCashNdsTypes.Nds20 });
                }
    
                if (documentDate.isBefore(nds20PercentDate) && previousDocumentDate.isSameOrAfter(nds20PercentDate)) {
                    this.model.set({ NdsType: Common.Data.BankAndCashNdsTypes.Nds18 });
                }
            }

            if (this.isUsn) {
                if (documentDate.isSameOrAfter(ndsUsn2025Date) && previousDocumentDate.year() !== documentDate.year()) {
                    this.model.set({ NdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
                    this.model.set({ IncludeNds: true });
                }

                if (currentTaxSys === TaxationSystemType.Patent && previousTaxSys !== currentTaxSys) {
                    this.model.set({ NdsType: null });
                    this.model.set({ NdsSum: null });
                    this.model.set({ IncludeNds: false });
                }

                if (currentTaxSys !== TaxationSystemType.Patent && previousTaxSys !== currentTaxSys) {
                    this.model.set({ NdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
                    this.model.set({ IncludeNds: true });
                }

                if (currentTaxSys === TaxationSystemType.Patent && this.model.get(`NdsType`) === Common.Data.BankAndCashNdsTypes.None) {
                        this.model.set({ NdsSum: null });
                        this.model.set({ IncludeNds: false });
                }                
                
                renderNdsWarningIcon({
                    model: this.model,
                    changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
                });
            }
            renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) })
        },

        renderTaxationSystemSelect() {
            const select = new TaxationSystemSelect({
                model: this.model
            });
            this.getRegion(`taxationSystemTypeSelect`).show(select);
        },

        renderPatentSelect() {
            const region = this.getRegion(`patentSelect`);
            const date = toDate(this.model.get(`Date`));

            if (this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
                taxationSystemService.getActivePatents(date)
                    .then((res) => {
                        const content = new PatentSelect({
                            model: this.model,
                            activePatents: res
                        });

                        region && region.show(content);
                    })
                    .catch(() => {
                        this.unsetPatent();
                    });
            } else {
                this.unsetPatent();
            }
        },

        unsetPatent() {
            const region = this.getRegion(`patentSelect`);

            this.model.unset(`PatentId`);
            region && region.empty();
        },

        onDestroy() {
            this.unstickit(this.model, this.bindings);
            this.model.off(`change:Date`);
        },

        bindings: {
            '#ndsSumContainer': {
                observe: 'IncludeNds',
                visible: true
            }
        }
    });
}(Cash));
