import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import TaxationSystemTypeEnum from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

export default {
    get({ taxationSystem = {}, TaxationSystemType }) {
        const {
            IsUsn, UsnType, IsEnvd, IsOsno
        } = taxationSystem;

        if (TaxationSystemType === TaxationSystemTypeEnum.Envd || (!IsOsno && !IsUsn && IsEnvd)) {
            return notTaxableMessages.envd;
        }

        if ((IsUsn || (IsUsn && IsEnvd)) && UsnType === UsnTypeEnum.Profit) {
            return notTaxableMessages.simple;
        }

        return null;
    }
};
