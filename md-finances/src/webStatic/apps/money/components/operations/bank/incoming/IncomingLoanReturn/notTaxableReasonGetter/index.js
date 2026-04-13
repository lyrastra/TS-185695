import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {}, hasLoanSum = false, isIp }) {
        const { IsUsn, IsOsno } = taxationSystem;

        if (IsUsn && hasLoanSum) {
            return ``;
        }

        if (IsOsno && isIp) {
            return ``;
        }

        return notTaxableMessages.simple;
    }
};

