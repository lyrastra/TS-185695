import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get(taxationSystemType, { taxationSystem: { IsOsno, IsUsn, IsEnvd } }) {
        if (taxationSystemType === TaxationSystemType.Envd || (!IsOsno && !IsUsn && IsEnvd)) {
            return notTaxableMessages.envd;
        }

        return ``;
    }
};
