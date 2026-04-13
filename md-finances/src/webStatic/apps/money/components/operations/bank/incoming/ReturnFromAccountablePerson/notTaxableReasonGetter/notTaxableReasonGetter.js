import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {} }) {
        if (taxationSystem.IsOsno) {
            return notTaxableMessages.osno;
        }

        if (taxationSystem.IsUsn) {
            return notTaxableMessages.simple;
        }

        return notTaxableMessages.envd;
    }
};

