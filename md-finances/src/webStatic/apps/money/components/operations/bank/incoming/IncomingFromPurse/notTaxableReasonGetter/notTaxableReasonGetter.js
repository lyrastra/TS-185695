import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {} }) {
        const { IsOsno, IsUsn, IsEnvd } = taxationSystem;

        if (!IsOsno && !IsUsn && IsEnvd) {
            return notTaxableMessages.envd;
        }

        if (IsOsno) {
            return notTaxableMessages.osno;
        }

        return notTaxableMessages.simple;
    }
};
