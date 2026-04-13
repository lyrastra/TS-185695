/* eslint-disable no-param-reassign, no-undef */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toDate } from '@moedelo/frontend-core-v2/helpers/converter';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import operationBillsBehavior from '../../../../../../../../components/OperationBills/operationBillsBehavior';
import TaxationSystemSelect from '../../../../../Components/TaxationSystemSelect';
import PatentSelect from '../../../../../Components/PatentSelect';
import renderNdsDropdown from '../../../../../Components/NdsDropdown/NdsDropdownHelper';
import { convertAccPolToFinanceNdsType, convertFinanceToAccPolNdsType } from '../../../../../../../../resources/ndsFromAccPolResource';
import renderNdsWarningIcon from '../../../../../Components/NdsWarningIcon/NdsWarningIconHelper';

// касса поступление прочее

((cash, common) => {
    const otherPayment = Marionette.LayoutView.extend({
        initialize() {
            this.$additionalBox = this.options.$additionalBox || ``;
            _.extend(this, cash.Views.setMaxSumTooltipMixin);
            _.extend(this, cash.Views.ndsUsnMessage);

            const cashList = new cash.Collections.CashCollection().map(item => { return item.get(`Id`); });

            this.model.set(`CashList`, cashList);

            const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current();
            const isUsn = taxSystem.isUsn();
            const isOsno = taxSystem.isOsno();
            this.model.set(`IsUsn`, isUsn);
            this.model.set(`IsOsno`, isOsno);

            const taxationSystemType = this.model.get(`TaxationSystemType`);
            const direction = this.model.get(`Direction`);

            if (this.model.get(`isChangeOrderType`)) {
                this.model.set(`Destination`, ``);
            }

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

            setTimeout(() => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            }, 0);

            this.model.on(`change:Date`, () => {
                if (!this.isEdit) {
                    this.changeNdsOption();
                }

                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            }, this);
        },

        onRender() {
            this.bind();
            this.initializeControls();
            this.changeNdsOption();
            this.setNdsSumVisibility();

            this.listenTo(this.model, `change:IncludeNds`, () => {
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            });
           
            return this;
        },

        changeNdsOption() {
            const nds20PercentDate = dateHelper(`2019-01-01`);
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const ndsAfter2026 = dateHelper(`2026-01-01`);
            const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);
            const previous = dateHelper(this.model.previous(`Date`), `DD.MM.YYYY`).year();
            const current = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`).year();
            this.setNdsSumVisibility();

            if (current === previous) {
                return;
            }

            if (documentDate.isBefore(ndsUsn2025Date) && this.model.get(`IsUsn`)) {
                this.model.set({ IncludeNds: false });
            }
            
            if (documentDate.isSameOrAfter(nds20PercentDate) && documentDate.isBefore(ndsAfter2026)) {
                this.model.set({ NdsType: common.Data.BankAndCashNdsTypes.Nds20 });
            }

            if (documentDate.isBefore(nds20PercentDate)) {
                this.model.set({ NdsType: common.Data.BankAndCashNdsTypes.Nds18 });
            }

            if (documentDate.isSameOrAfter(ndsAfter2026) && this.model.get(`IsOsno`)) {
                this.model.set({ NdsType: common.Data.BankAndCashNdsTypes.Nds22 });
            }

            if (documentDate.isSameOrAfter(ndsUsn2025Date) && this.model.get(`IsUsn`)) {
                this.model.set({ NdsType: convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)] });
                this.model.set({ IncludeNds: true });
            }

            this.setNdsSumVisibility();

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

        changeNds({ value }) {
            this.model.set(`NdsType`, value);
        },

        initializeControls() {
            this.initKontragentWithFirmAutocomplete();
            this.setMaxSumTooltip();
            
            this.$(`[data-type=float]`).decimalMask();
            this.$(`select`).change();

            const { model } = this;
            model.on(`change:IncludeNds change:Date`, this.showNdsMessage, this);
            this.showNdsMessage();
            this.$(`[data-bind=ProjectNumber]`).projectAutocomplete(model, {
                direction: model.get(`Direction`)
            });
        },
        
        initKontragentWithFirmAutocomplete() {
            const { model } = this;

            this.$(`[data-bind=KontragentName]`).saleKontragentWaybillAutocomplete({
                onSelect(selected) {
                    model.set({
                        KontragentId: selected.object.KontragentId || selected.object.Id,
                        KontragentName: selected.object.Name
                    });
                },
                clean() {
                    model.unset(`KontragentId`);
                },
                onBlur() {
                    if (!model.get(`KontragentName`)) {
                        model.unset(`KontragentId`);
                    }
                },
                url: Cash.Data.KontragentAutocomplete
            });
        },
        
        bindings: {
            '#ndsSumContainer': {
                observe: `IncludeNds`,
                visible: true
            }
        }
    });
    
    cash.Views.outgoingOther = otherPayment.extend({
        template: `#OutgoingOtherTemplate`
    });
    
    cash.Views.incomingOther = otherPayment.extend({
        template: `#IncomingOtherTemplate`,

        initialize() {
            otherPayment.prototype.initialize.call(this);
            const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current();
            const isUsn = taxSystem.isUsn();
            this.model.set(`IsUsn`, isUsn);
            this.model.on(`change:Date`, this.renderTaxationSystemRow, this);
            this.model.on(`change:TaxationSystemType`, this.renderPatentSelect, this);
            this.isOoo = (new common.FirmRequisites()).get(`IsOoo`);
            this.isEdit = this.model.get(`Id`);

            const taxationSystemType = this.model.get(`TaxationSystemType`);
            const direction = this.model.get(`Direction`);

            if (direction === Direction.Incoming && taxationSystemType === TaxationSystemType.Patent && isUsn) {
                this.model.set(`TaxationSystemType`, TaxationSystemType.Usn);
            }

            this.listenTo(this.model, `change:IncludeNds`, () => {
                const newRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
                this.model.set(`NdsType`, newRate);
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
            });

            this.listenTo(this.model, `change:CurrentRate`, () => {
                if (!this.isEdit) {
                    const newRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
                    this.model.set(`NdsType`, newRate);
                }
                
                renderNdsDropdown({ model: this.model, changeNds: this.changeNds.bind(this) });
                renderNdsWarningIcon({
                    model: this.model,
                    changeNdsFromAccPolicy: this.setNdsFromAccPolicy.bind(this)
                });
                this.checkNdsWarningIcon();
            });

            this.listenTo(this.model, `change:NdsType`, () => {
                this.checkNdsWarningIcon();
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
        },

        regions: {
            taxationSystemTypeSelect: `.js-taxationSystemType`,
            patentSelect: `.js-patent`
        },

        behaviors() {
            return {
                OperationBills: {
                    behaviorClass: operationBillsBehavior
                }
            };
        },

        onRender() {
            this.bind();
            this.initializeControls();
            this.listenTo(this.model, `change:Date`, () => {
                if (!this.isEdit) {
                    this.changeNdsOption();
                }
            });
            this.model.on(`change:OperationType`, this.changeNdsOption, this);
            this.listenTo(this.model, `change:IncludeNds`, this.setNdsSumVisibility());
            this.listenTo(this.model, `change:Date`, this.setNdsSumVisibility());
            this.renderTaxationSystemRow();
            this.setNdsSumVisibility();
            this.checkNdsWarningIcon();

            return this;
        },

        renderTaxationSystemRow() {
            const isIpOsno = this.options.taxSystem.IsOsno && !this.isOoo;

            if (!isIpOsno) {
                return;
            }

            const select = new TaxationSystemSelect({
                model: this.model
            });
            const region = this.getRegion(`taxationSystemTypeSelect`);

            region && region.show(select);
            this.renderPatentSelect();
        },

        changeNds({ value }) {
            this.model.set(`NdsType`, value);
        },

        checkNdsWarningIcon() {
            const isUsn = this.model.get(`IsUsn`);
            const ndsUsn2025Date = dateHelper(`2025-01-01`);
            const documentDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`);
            const icon = this.$(`#ndsWarningIconMountPoint`);
            const currentRate = this.model.get(`CurrentRate`);
            const ndsType = this.model.get(`NdsType`);
    
            if (currentRate === convertFinanceToAccPolNdsType[ndsType] || !isUsn || documentDate.isBefore(ndsUsn2025Date) || this.model.get(`Direction`) === Direction.Outgoing) {
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

        renderPatentSelect() {
            const region = this.getRegion(`patentSelect`);
            const date = toDate(this.model.get(`Date`));

            if (this.model.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
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
            } else {
                this.unsetPatent();
            }
        },

        unsetPatent() {
            const region = this.getRegion(`patentSelect`);

            this.model.unset(`PatentId`);
            region && region.empty();
        }
    });
})(Cash, Common);
