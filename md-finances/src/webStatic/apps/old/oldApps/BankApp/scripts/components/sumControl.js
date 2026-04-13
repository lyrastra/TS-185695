/* global Bank, Marionette */

import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';

(function() {
    Bank.SumControl = Marionette.ItemView.extend({
        template: `#SumControl`,

        events: {
            'blur [data-bind=Sum]': `formatSum`
        },

        bindings: {
            '[data-bind=Sum]': {
                observe: `Sum`,
                onSet: value => {
                    if (!value) {
                        return 0;
                    }
        
                    return toFloat(value, true);
                },
                onGet: value => toAmountString(value),
                setOptions: { validate: true }
            }
        },

        formatSum() {
            this.$(`[data-bind=Sum]`).val(toAmountString(this.model.get(`Sum`)));
        },

        onRender() {
            this.stickit();
            this.$(`[data-bind=Sum]`).decimalMask();
        }
    });
}());
