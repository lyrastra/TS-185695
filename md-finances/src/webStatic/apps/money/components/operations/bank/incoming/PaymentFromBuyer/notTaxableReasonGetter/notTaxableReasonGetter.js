import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {}, taxationSystemType, isOoo }) {
        if (taxationSystem.IsOsno && isOoo) {
            return notTaxableMessages.osno;
        }

        if ((taxationSystem.IsEnvd && !taxationSystem.IsUsn) ||
            taxationSystemType === TaxationSystemType.Envd
        ) {
            return notTaxableMessages.envd;
        }

        return null;
    }
};
