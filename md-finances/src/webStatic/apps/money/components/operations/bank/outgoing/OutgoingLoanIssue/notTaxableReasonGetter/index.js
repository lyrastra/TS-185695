import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {} }) {
        const {
            IsUsn, UsnType, IsEnvd, IsOsno
        } = taxationSystem;

        if (IsEnvd && !IsUsn && !IsOsno) {
            return notTaxableMessages.envd;
        }

        if ((IsUsn && UsnType === UsnTypeEnum.ProfitAndOutgo)) {
            return notTaxableMessages.usn6;
        }

        return notTaxableMessages.notTaxable;
    }
};

