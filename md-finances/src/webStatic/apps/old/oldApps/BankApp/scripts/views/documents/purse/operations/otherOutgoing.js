const { Bank, Common } = window;

Bank.Views.OtherOutgoingPurseOperation = Marionette.ItemView.extend({
    template: `#OtherOutgoingPurseOperation`,

    bindings: {
        '[data-bind=ProjectNumber]': `ProjectNumber`
    },

    defaults: {
        ProjectNumber: `Основной договор`
    },

    initialize() {
        this.model.set(`PostingsAndTaxMode`, Common.Data.ProvidePostingType.ByHand);
        !this.model.get(`Id`) && this.model.set(`ProvideInAccounting`, true);

        Object.entries(this.defaults).forEach(([attr, val]) => this.model.set(attr, this.model.get(attr) || val));
    },

    onRender() {
        this.stickit();
        this.$(`[data-bind=ProjectNumber]`).projectForPurseAutocomplete(this.model, { withPaymentAgents: true });

        this.kontragent = this.createKontragentControl();
        this.sum = this.createSumControl();
    },

    createKontragentControl() {
        return new Bank.PurseKontragentControl({
            el: this.$(`[data-bind=KontragentName]`).parent(),
            model: this.model,
            params: { withPaymentAgents: true }
        }).render();
    },

    createSumControl() {
        return new Bank.SumControl({
            el: this.$(`[data-control=sum]`),
            model: this.model
        }).render();
    },

    onDestroy() {
        this.model.unset(`Sum`);
        this.model.unset(`KontragentName`);
        this.model.unset(`ProjectNumber`);

        // OSNO-4164 Для операций отличных от "Прочее" должно быть 0 (Auto)
        this.model.set(`PostingsAndTaxMode`, Common.Data.ProvidePostingType.Auto);
    }
});

