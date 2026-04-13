import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {} }) {
        const { IsUsn, IsEnvd, IsOsno } = taxationSystem;

        if (!IsUsn && !IsOsno && IsEnvd) {
            return notTaxableMessages.envd;
        }

        return notTaxableMessages.simple;
    }
};
