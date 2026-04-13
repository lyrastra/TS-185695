import UnifiedAccountsResource from '../resources/UnifiedAccountsResource';
import AccountsResource2023 from '../resources/AccountsResource2023';
import AccountsResource from '../resources/AccountsResource';
import { isUnifiedBP } from './checkHelper';

export function getDefaultCalendarType(type) {
    const unifiedAccount = UnifiedAccountsResource.find(({ Code }) => Code === type) || UnifiedAccountsResource[0];

    return unifiedAccount.DefaultCalendarType;
}

export function getDefaultPeriod(type) {
    return new window.Bank.Models.Tools.TripleCalendar({
        CalendarType: getDefaultCalendarType(type)
    }).toJSON();
}

export function getDefaultEmptySubPayment() {
    return {
        Sum: null, AccountCode: null, PatentId: null, Period: getDefaultPeriod(), KbkId: null
    };
}

export function getDefaultSubPayment() {
    const AccountCode = getDefaultTypeId();

    return {
        Sum: null,
        AccountCode: getDefaultTypeId(),
        PatentId: null,
        Period: getDefaultPeriod(AccountCode),
        KbkId: null
    };
}

export function getDefaultSubPaymentList(data) {
    if (!isUnifiedBP(data)) {
        return [];
    }

    return [getDefaultSubPayment()];
}

function getDefaultTypeId() {
    return UnifiedAccountsResource[0].Code;
}

export function getDefaultKbkId(kbks, value) {
    return (kbks.find(kbk => kbk.Id === value) || kbks[0])?.Id;
}

export function getDefaultAccountCode(isUnifiedEnabled) {
    if (isUnifiedEnabled) {
        return AccountsResource2023[0].Code;
    }

    return AccountsResource[0].Code;
}
