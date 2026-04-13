/* global Bank, BankApp, Common */

import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

// eslint-disable-next-line func-names
(function(bank) {
    // eslint-disable-next-line no-param-reassign
    bank.Views.CommissionHoldPurseOperation = Marionette.LayoutView.extend({
        template: `#CommissionHoldPurseOperation`,

        regions: {
            taxationSystemTypeSelect: `.js-taxationSystemType`,
            patentSelect: `.js-patent`
        },

        initialize() {
            const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current();
            this.isUsn = taxSystem.isUsn();
            const ndsRates = Common.Utils.CommonDataLoader.NdsRates;
            this.model.set(`IsUsn`, this.isUsn);
            this.model.set({ NdsRates: ndsRates });
            this.setCurrentRate();

            this.listenTo(this.model, `change:Date`, () => {
                this.setCurrentRate();
            });
        },

        onRender() {
            this.stickit();

            this.sum = this.createSumControl();
            this.ndsBlock = this.createNdsBlock();
        },

        setCurrentRate() {
            if (!this.isUsn) {
                return;
            }

            const date = this.model.get(`Date`);
            const ndsRates = this.model.get(`NdsRates`);
            const currrentRateFromAccPol = ndsRates?.find(r => dateHelper(date).isBetween(r.StartDate, r.EndDate, undefined, `[]`))?.Rate;

            this.model.set(`CurrentRate`, currrentRateFromAccPol || Common.Data.BankAndCashNdsTypes.None);
        },

        createSumControl() {
            return new bank.SumControl({
                el: this.$(`[data-control=sum]`),
                model: this.model
            }).render();
        },

        createNdsBlock() {
            return new bank.NdsControl({
                el: this.$(`[data-control=ndsBlock]`),
                model: this.model,
                replaceElement: true
            }).render();
        },

        onDestroy() {
            this.model.unset(`Sum`);
            this.model.unset(`TaxationSystemType`);
        },

        behaviors() {
            return {
                TaxationSystemChoiceBehavior: {
                    behaviorClass: BankApp.Views.TaxationSystemChoiceBehavior
                }
            };
        }
    });
}(Bank));
