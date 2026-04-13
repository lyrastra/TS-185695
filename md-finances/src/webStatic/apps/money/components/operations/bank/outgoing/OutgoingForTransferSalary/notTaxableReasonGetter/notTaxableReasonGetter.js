import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';
import ContractTypesEnum from '../../../../../../../../enums/newMoney/ContractTypesEnum';

export default {
    get({ taxationSystem = {}, UnderContract = false, isIp }) {
        const {
            IsUsn, UsnType, IsEnvd, IsOsno
        } = taxationSystem;

        if (IsOsno && isIp) {
            return null;
        }

        if (IsUsn && (UsnType === UsnTypeEnum.Profit) && UnderContract === ContractTypesEnum.Dividends.value) {
            return notTaxableMessages.simple;
        }

        if (IsUsn && (UsnType === UsnTypeEnum.Profit)) {
            return notTaxableMessages.usn6;
        }

        if (IsUsn && (UsnType === UsnTypeEnum.ProfitAndOutgo) &&
            [ContractTypesEnum.Employment.value, ContractTypesEnum.GPD.value, ContractTypesEnum.SalaryProject.value,
                ContractTypesEnum.GPDBySalaryProject.value, ContractTypesEnum.DividendsBySalaryProject.value].includes(UnderContract)) {
            return notTaxableMessages.onClosingOfMonth;
        }

        if (IsUsn && (UsnType === UsnTypeEnum.ProfitAndOutgo) && UnderContract === ContractTypesEnum.Dividends.value) {
            return notTaxableMessages.simple;
        }

        if (!IsUsn && !IsOsno && IsEnvd) {
            return notTaxableMessages.envd;
        }

        if (IsOsno) {
            return notTaxableMessages.osnoOutgoing;
        }

        return null;
    }
};
