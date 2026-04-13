import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import _ from 'underscore';
import { getNdsOptions } from '../../../../../helpers/ndsHelper';
import { convertAccPolToFinanceNdsType } from '../../../../../../../resources/ndsFromAccPolResource';
import { osnoTaxPostingsForServer } from '../../../../../../../mappers/taxPostingsMapper';

const { Money, Bank, Common } = window;

Bank.Views.PayersControl = Marionette.CompositeView.extend({
    template: `#PayersControl`,
    className: `payers`,
    defaults: {
        IncludeNds: false,
        ProjectNumber: `Основной договор`
    },

    childViewContainer: `tbody`,

    initialize() {
        this.model.on(`change:IncludeNds`, this.setDefaultNdsType, this);
        this.model.on(`change:NdsType change:IncludeNds change:Sum`, this.setNdsSum, this.model);
        this.model.on(`change:NdsType change:IncludeNds`, this.setNdsSumVisibility, this);

        _.each(this.defaults, (val, attr) => {
            this.model.set(attr, this.model.get(attr) || val);
        }, this);
    },

    setDefaultNdsType() {
        const nds20PercentDate = dateHelper(`2019-01-01`);
        const ndsUsnAfter2025 = dateHelper(`2025-01-01`);
        const ndsAfter2026 = dateHelper(`2026-01-01`);
        const documentDate = dateHelper(this.model.get(`Date`));

        if (documentDate.isSameOrAfter(ndsAfter2026) && !this.model.get(`IsUsn`)) {
            this.model.set(`NdsType`, Common.Data.BankAndCashNdsTypes.Nds22);
        } else if (documentDate.isSameOrAfter(ndsUsnAfter2025) && this.model.get(`IsUsn`)) {
            const currrentRate = convertAccPolToFinanceNdsType[this.model.get(`CurrentRate`)];
            this.model.set(`NdsType`, currrentRate);
        } else if (documentDate.isSameOrAfter(nds20PercentDate)) {
            this.model.set(`NdsType`, Common.Data.BankAndCashNdsTypes.Nds20);
        } else {
            this.model.set(`NdsType`, Common.Data.BankAndCashNdsTypes.Nds18);
        }
    },

    setNdsSum() {
        if (!this.get(`IncludeNds`)) {
            this.set(`NdsSum`, 0);

            return;
        }

        const ndsType = this.get(`NdsType`);

        if (ndsType === Common.Data.BankAndCashNdsTypes.Empty) {
            this.set(`NdsSum`, null);

            return;
        }

        if (!Number.isFinite(ndsType)) {
            return;
        }

        const ndsSum = Common.Utils.NdsConverter.toPercent({
            value: this.get(`Sum`),
            type: ndsType,
            typeEnum: Common.Data.BankAndCashNdsTypes
        });

        this.set(`NdsSum`, ndsSum);
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

    onRender() {
        this.bind();
        this.hideNdsControl();
        this.sum = this.createSumControl();
        this.bill = this.createBillControl();
        this.kontragent = this.createKontragentControl();
        this.setNdsSumVisibility();
        this.$(`select`).change();
        this.$(`[data-bind=ProjectNumber]`).projectForPurseAutocomplete(this.model);
    },

    onBeforeDestroy() {
        this.kontragent.destroy();
    },

    createBillControl() {
        return new Bank.BillControl({
            el: this.$(`[data-bind=BillNumber]`),
            model: this.model
        });
    },

    createKontragentControl() {
        return new Bank.PurseKontragentControl({
            el: this.$(`[data-bind=KontragentName]`).parent(),
            model: this.model,
            params: { withPaymentAgents: false }
        }).render();
    },

    hideNdsControl() {
        const selectedDate = dateHelper(this.model.get(`Date`), `DD.MM.YYYY`).toDate();

        const ts = new Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
        const taxSystem = ts.Current(selectedDate);
        const isOoo = (new Common.FirmRequisites()).get(`IsOoo`);
        const row = this.$(`#NdsCheckbox`).closest(`.ndsContainer`);

        if (!taxSystem.get(`IsOsno`) || isOoo) {
            row.hide();
        } else {
            row.show();
        }
    },
    createSumControl() {
        return new Bank.SumControl({
            el: this.$(`[data-bind=Sum]`).closest(`.mdCol-3`),
            model: this.model,
            template: false
        }).render();
    },
    isValid() {
        return this.model.isValid(true);
    },

    bindings: {
        '[data-bind=ProjectNumber]': `ProjectNumber`,
        '#ndsSumContainer': {
            observe: `IncludeNds`,
            visible: true
        },
        '[data-bind=Date]': {
            observe: `NdsType`,
            selectOptions: {
                collection() {
                    return getNdsOptions({
                        date: this.model.get(`Date`),
                        isUsn: this.model.get(`IsUsn`),
                        isOutgoing: this.model.get(`Direction`) === Direction.Outgoing
                    });
                }
            }
        },
        '[data-bind=NdsType]': {
            observe: `NdsType`,
            selectOptions: {
                collection() {
                    return getNdsOptions({
                        date: this.model.get(`Date`),
                        isUsn: this.model.get(`IsUsn`),
                        isOutgoing: this.model.get(`Direction`) === Direction.Outgoing
                    });
                }
            }
        },
        '[data-bind=BillNumber]': {
            observe: `BillNumber`
        }
    }
});

