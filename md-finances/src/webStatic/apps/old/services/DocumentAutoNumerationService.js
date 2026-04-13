import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { getNextNumber } from '../../../services/Bank/paymentOrderService';

class DocumentAutoNumerationService {
    constructor({ model }) {
        if (model.get(`action`) === `import`) {
            return;
        }

        this.model = model;

        this.model.on(`change:Date`, this.onDateChange, this);
        this.model.on(`change:SettlementAccountId`, this.updateDocNumber, this);
    }

    onDateChange(model, value) {
        const dateFormat = `DD.MM.YYYY`;
        const newDate = dateHelper(value, dateFormat);
        const oldDate = dateHelper(model.previous(`Date`), dateFormat);

        if (!newDate.isSame(oldDate, `year`)) {
            this.updateDocNumber();
        }
    }

    updateDocNumber() {
        if (this.model.get(`Id`)) {
            this.destroy();
        }

        const date = this.model.get(`Date`);
        const settlementId = this.model.get(`SettlementAccountId`);

        getNextNumber(date, settlementId).then(response => this.model.set(`Number`, response.Value));
    }

    destroy() {
        this.model && this.model.off(null, null, this);
    }
}

export default DocumentAutoNumerationService;
