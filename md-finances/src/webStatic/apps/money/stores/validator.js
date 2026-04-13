import CurrencyCodes from '@moedelo/frontend-enums/mdEnums/CurrencyCode';
import { isValidChecksum } from '@moedelo/frontend-common-v2/apps/requisites/helpers/settlementAccountHelper';
import SettlementAccountTypeEnum from '../../../enums/newMoney/SettlementAccountTypeEnum';

const validCurrencyCodes = Object.keys(CurrencyCodes);

export default {
    validateSecondSettlementAccount({
        SecondSettlementAccount,
        Bik,
        SettlementAccount,
        SettlementAccountType,
        settlementAccounts
    }) {
        if (!SecondSettlementAccount) {
            return `Заполните счёт`;
        }

        if (SecondSettlementAccount.length < 20) {
            return `Счет должен состоять из 20 знаков`;
        }

        if (SecondSettlementAccount === SettlementAccount) {
            return `Счет не должен быть равен ${SettlementAccountType === SettlementAccountTypeEnum.Currency ? `валютному` : `транзитному`}`;
        }

        const currencyCode = SecondSettlementAccount.substring(5, 8);

        if (!validCurrencyCodes.includes(currencyCode)) {
            return `Не определена валюта счета, проверьте его корректность`;
        }

        const currencyCodeMainAccount = SettlementAccount.substring(5, 8);

        if (currencyCode !== currencyCodeMainAccount) {
            return `Валюты счетов должны совпадать`;
        }

        if (isExists({ SecondSettlementAccount, settlementAccounts })) {
            return `Такой счёт уже существует`;
        }

        /** для тестовых данных не проверяем контрольную сумму */
        const isTester = SecondSettlementAccount.substring(0, 5) === `12345`;

        if (!isTester && Bik && Bik.length && !isValidChecksum({ account: SecondSettlementAccount, bik: Bik })) {
            return `Неверная контрольная сумма`;
        }

        return ``;
    }
};

function isExists({ SecondSettlementAccount, settlementAccounts = [] }) {
    return settlementAccounts.some(account => account.Number === SecondSettlementAccount);
}
