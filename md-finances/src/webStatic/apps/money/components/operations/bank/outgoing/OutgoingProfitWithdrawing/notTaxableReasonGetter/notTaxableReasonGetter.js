import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {}, hasLoanSum = false }) {
        const { IsUsn, UsnType } = taxationSystem;

        if ((IsUsn && UsnType === UsnTypeEnum.ProfitAndOutgo) && hasLoanSum) {
            return ``;
        }

        return notTaxableMessages.simple;
    }
};

