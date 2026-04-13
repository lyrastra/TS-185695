import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ IsOsno, IsEnvd, IsUsn }) {
        if (IsEnvd && !IsOsno && !IsUsn) {
            return notTaxableMessages.envd;
        }

        if (IsOsno) {
            return notTaxableMessages.osno;
        }

        return notTaxableMessages.usn;
    }
};
