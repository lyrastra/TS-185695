import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {} }) {
        const { IsUsn, UsnType } = taxationSystem;

        if (IsUsn && UsnType === UsnTypeEnum.ProfitAndOutgo) {
            return notTaxableMessages.usnRentPaymentOnClosingOfMonth;
        }

        return notTaxableMessages.simple;
    }
};

