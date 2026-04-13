import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {}, isIp }) {
        const { IsUsn, IsEnvd, IsOsno } = taxationSystem;

        if (IsOsno && isIp) {
            return null;
        }

        if (!IsUsn && (IsOsno || IsEnvd)) {
            return notTaxableMessages.simple;
        }

        return null;
    }
};
