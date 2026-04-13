import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import dateHelper, { DateFormat } from '@moedelo/frontend-core-v2/helpers/dateHelper';

export async function getNextNumber(date, settlementAccountId) {
    const args = {
        year: dateHelper(date, DateFormat.ru).year(),
        settlementAccountId
    };

    const { data } = await get(`/MoneyNumeration/api/v1/PaymentOrderNumeration/NextNumber`, args);

    return data;
}

export default { getNextNumber };
