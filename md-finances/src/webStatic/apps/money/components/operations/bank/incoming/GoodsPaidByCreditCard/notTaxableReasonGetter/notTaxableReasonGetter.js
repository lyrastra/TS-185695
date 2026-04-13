import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {} }) {
        if (taxationSystem.IsEnvd && !taxationSystem.IsUsn && !taxationSystem.IsOsno) {
            return notTaxableMessages.envd;
        }

        return null;
    }
};
