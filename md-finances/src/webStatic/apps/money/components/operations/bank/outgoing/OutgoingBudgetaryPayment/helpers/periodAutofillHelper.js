import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import CalendarTypesEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryCalendarTypesEnum';

const {
    _68_02, _68_04, _68_06, _68_07, _68_08, _68_09, _68_12, _68_13
} = SyntheticAccountCodesEnum;
const quarterAccountCodes = [_68_02, _68_04, _68_06, _68_07, _68_08, _68_09, _68_12, _68_13];
const quarterKbkNameParts = [`Налог (по доходам ИП)`];
const yearKbkNameParts = [`Фикс. взносы`];

function isKbkPartMatches(arr, kbkName) {
    return arr.some(kbkPart => kbkName.includes(kbkPart));
}

export function getPeriodByKbkOrAccountCode(accountCode, kbkName) {
    if (quarterAccountCodes.includes(accountCode) || isKbkPartMatches(quarterKbkNameParts, kbkName)) {
        return CalendarTypesEnum.Quarter;
    } else if (isKbkPartMatches(yearKbkNameParts, kbkName)) {
        return CalendarTypesEnum.Year;
    }

    return CalendarTypesEnum.Month;
}

export default {};
