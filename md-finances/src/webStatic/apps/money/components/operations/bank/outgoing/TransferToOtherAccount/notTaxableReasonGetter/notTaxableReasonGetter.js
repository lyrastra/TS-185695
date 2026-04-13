import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {} }) {
        const {
            IsUsn, UsnType, IsEnvd, IsOsno
        } = taxationSystem;

        if (IsEnvd && !IsOsno && !IsUsn) {
            return notTaxableMessages.envd;
        }

        if ((IsUsn || (IsUsn && IsEnvd)) && UsnType === UsnTypeEnum.Profit) {
            return notTaxableMessages.usn6;
        }

        if ((IsUsn || (IsUsn && IsEnvd)) && UsnType === UsnTypeEnum.ProfitAndOutgo) {
            return notTaxableMessages.usn;
        }

        if (IsOsno) {
            return notTaxableMessages.osnoOutgoing;
        }

        return null;
    }
};
