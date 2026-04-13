import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import { getTaxSystemsForAccounting } from '@moedelo/frontend-common-v2/apps/requisites/helpers/taxationSystemTypeHelper';
import { toDate } from '@moedelo/frontend-core-v2/helpers/converter';
import { isSupportDoubleTaxationSystemAccounting } from '../../../../../helpers/MoneyOperationHelper';
import template from './template.hbs';

export default Marionette.ItemView.extend({
    template,

    initialize(options) {
        this.model = options.model;
        this.taxationSystems = null;
        this.model.on(`change:Date`, this.setTaxSystems, this);
    },

    async onRender() {
        if (!this.taxationSystems) {
            await this.setTaxSystems();
        }

        if (this.isDestroyed) {
            return;
        }

        this.bind();
        this.updateTaxationSystemTypeValue();
        this.$(`[data-bind=TaxationSystemType]`).change();
    },

    async setTaxSystems() {
        const firmTaxSystem = await taxationSystemService.getTaxSystem(toDate(this.model.get(`Date`)));
        const activePatents = await taxationSystemService.getActivePatents(toDate(this.model.get(`Date`)));
        const operationType = this.model.get(`OperationType`) || this.model.get(`PurseOperationType`);
        this.taxationSystems = getTaxSystemsForAccounting(
            firmTaxSystem.getType(),
            {
                hasPatents: activePatents?.length > 0,
                withDoubleTypes: isSupportDoubleTaxationSystemAccounting(operationType)
            }
        ).map(ts => ({ value: ts.type, label: ts.text }));
    },

    updateTaxationSystemTypeValue() {
        const value = this.model.get(`TaxationSystemType`);

        if (!this.taxationSystems.some(ts => ts.value === value)) {
            this.model.set({
                TaxationSystemType: this.taxationSystems?.length > 0
                    ? this.taxationSystems[0].value
                    : null
            });
        }
    },

    bindings: {
        'select[data-bind=TaxationSystemType]': {
            observe: `TaxationSystemType`,
            selectOptions: {
                collection() {
                    return this.taxationSystems;
                }
            }
        }
    }
});
