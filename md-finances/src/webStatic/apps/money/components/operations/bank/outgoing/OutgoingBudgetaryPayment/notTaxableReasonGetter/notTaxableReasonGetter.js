import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import BudgetaryAccountTypeEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryAccountTypeEnum';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';
import renderOsnoTradingObjectAdditionsWarning from './OsnoTradingObjectAdditionsWarning';

export default {
    get({
        kbk,
        isIp,
        accountCode,
        accountType,
        taxationSystem = {},
        isTradingFee = false,
        isIpOsno
    }) {
        const {
            IsUsn, UsnType, IsEnvd, IsOsno
        } = taxationSystem;
        const kbkTaxAgentId = 50;
        const isTaxesAccountType = accountType === BudgetaryAccountTypeEnum.Taxes.value;
        const isFeeAccountType = accountType === BudgetaryAccountTypeEnum.Fees.value;
        const isTaxAgent = kbk.Id === kbkTaxAgentId;

        if (isIpOsno && isTradingFee) {
            return renderOsnoTradingObjectAdditionsWarning();
        }

        if (
            IsOsno && isIp &&
                (
                    (isTaxesAccountType && isTaxAgent && accountCode === SyntheticAccountCodesEnum._68_01) ||
                    isFeeAccountType
                )
        ) {
            return null;
        }

        if (!IsUsn && !IsOsno && IsEnvd) {
            return notTaxableMessages.envd;
        }

        if (IsOsno && !isTradingFee) {
            return notTaxableMessages.osnoOutgoing;
        }

        if (IsUsn && (UsnType === UsnTypeEnum.Profit) && !isTradingFee) {
            return notTaxableMessages.usn6;
        }

        if (accountCode === SyntheticAccountCodesEnum._68_69) {
            return notTaxableMessages.simple;
        }

        return null;
    }
};
