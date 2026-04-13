/* eslint-disable object-curly-newline */
import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';
import { get as restGet } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import SubcontoType from '../../enums/newMoney/SubcontoTypeEnum';
import SyntheticAccountCodesEnum from '../../enums/SyntheticAccountCodesEnum';

export function subcontoAutocomplete({ date, type, accountCode, kontragentSubcontoId = -1, query = ``, count = 5 }) {
    switch (type) {
        case SubcontoType.Cashes:
            return accountCode === SyntheticAccountCodesEnum._50_01
                ? getMainCashSubcontoAutocomplete({ query, count })
                : getOtherCashSubcontoAutocomplete({ query, count });
        case SubcontoType.SpecialSettlementAccount:
            return getSubcontosByTypeAndCode({ type, accountCode, query, count });
        case SubcontoType.SpecialSettlementAccountForDigitalRuble:
            return getSubcontosByTypeAndCode({ type, accountCode, query, count });
        case SubcontoType.Kontragent:
            return getSubcontosAutocomplete({ type, query, count });
        case SubcontoType.Securities:
        case SubcontoType.Worker:
        case SubcontoType.AppointmentOfTrustFunds:
            return getSubcontosAutocompleteForPostings({ type, query, count });
        case SubcontoType.Contract:
            return getContractSubcontoAutocomplete({ type, kontragentSubcontoId, query, count });
        case SubcontoType.CostItems:
            return getSubcontosAutocompleteForPostings({ type, query, count });
        case SubcontoType.OtherIncomeOrOutgo:
            return getSubcontosAutocompleteForPostings({ type, query, count });
        case SubcontoType.SeparateDivision:
            return getSubcontosAutocompleteForPostings({ type, query, count });
        case SubcontoType.EnforcementDocuments:
            return getSubcontosAutocompleteForPostings({ type, query, count });
        case SubcontoType.Kbk:
            return accountCode === SyntheticAccountCodesEnum._68_10
                ? getSubcontosAutocompleteForPostings({ date, type, accountCode, query, count })
                : getKbkAutocomplateBySubconto({ date, type, accountCode, query, count });
        default:
            return getRequisitesSubcontoAutocomplete({ type, query, count });
    }
}

export function subcontoLevelForAccount({ settlementAccountId, syntheticAccountTypeId }) {
    const query = `settlementAccountId=${settlementAccountId}&syntheticAccountTypeId=${syntheticAccountTypeId}`;

    return get(`/Accounting/ChartOfAccounts/GetSubcontoLevelForAccount?${query}`)
        .then(({ List }) => List.sort((a, b) => a.Level - b.Level));
}

function getMainCashSubcontoAutocomplete({ query, count }) {
    return get(`/Accounting/Subcontos/GetMainCashSubcontoAutocomplete?query=${query}&count=${count}`)
        .then(({ List }) => List);
}

function getRequisitesSubcontoAutocomplete({ type, query, count }) {
    return get(`/Requisites/SettlementAccounts/SubcontoAutocomplete?type=${type}&query=${query}&count=${count}`)
        .then(({ List }) => List);
}

function getOtherCashSubcontoAutocomplete({ query, count }) {
    return get(`/Accounting/Subcontos/GetOtherCashSubcontoAutocomplete?query=${query}&count=${count}`)
        .then(({ List }) => List);
}

function getSubcontosByTypeAndCode({ type, accountCode, query, count }) {
    return get(`/Accounting/Subcontos/GetSubcontosByTypeAndCode?type=${type}&accountCode=${accountCode}&query=${query}&count=${count}`)
        .then(({ List }) => List);
}

export function getSubcontosAutocomplete({ type, query, count }) {
    return get(`/Accounting/Subcontos/GetSubcontosAutocomplete?type=${type}&query=${query}&count=${count}`)
        .then(({ List }) => List);
}

function getSubcontosAutocompleteForPostings({ date, type, accountCode, query, count }) {
    return get(`/Accounting/Subcontos/GetSubcontosAutocompleteForPostings?type=${type}&accountCode=${accountCode}&date=${date}&query=${query}&count=${count}`)
        .then(({ List }) => List);
}

export function getContractSubcontoAutocomplete({ kontragentSubcontoId = -1, type, query, count }) {
    return get(`/Contract/Autocomplete/SubcontoAutocomplete?kontragentSubcontoId=${kontragentSubcontoId}&type=${type}&query=${query}&count=${count}`)
        .then(({ List }) => List);
}

export function getSubaccount() {
    return restGet(`/Accounting/api/v1/subaccount`);
}

function getKbkAutocomplateBySubconto({ date, type, accountCode, query, count }) {
    return get(`/Accounting/Subcontos/GetKbkAutocomplateBySubconto?date=${date}&type=${type}&accountCode=${accountCode}&query=${query}&count=${count}&&period=null`)
        .then(({ List }) => List);
}

export default { subcontoAutocomplete, subcontoLevelForAccount, getSubaccount };
