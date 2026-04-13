import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import operationTaxesEnum from '../../enums/newMoney/operationTaxesEnum';

const operationTaxesHelper = {
    getTax: ({ TaxType = null, Sum = null } = {}) => {
        if (TaxType && Sum !== 0 && operationTaxesEnum[TaxType]) {
            return `${operationTaxesEnum[TaxType]}: ${toAmountString(Math.abs(Sum))}`;
        }

        return false;
    }
};

export default operationTaxesHelper;
