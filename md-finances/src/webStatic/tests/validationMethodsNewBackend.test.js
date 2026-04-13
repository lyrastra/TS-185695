import {
    validateKbk,
    validatePeriodDate,
    validateUin
} from '../apps/money/components/operations/validation/validationMethodsNewBackend';
import CalendarTypesEnum from '../enums/newMoney/budgetaryPayment/BudgetaryCalendarTypesEnum';

describe(`validationMethodsNewBackend Uin test`, () => {
    it(`Uin false is valid`, () => {
        const expected = validateUin({ Uin: false });

        expect(expected).toEqual(true);
    });

    it(`Uin empty is valid`, () => {
        const expected = validateUin({});

        expect(expected).toEqual(true);
    });

    it(`Uin empty string is valid`, () => {
        const expected = validateUin({ Uin: `` });

        expect(expected).toEqual(true);
    });

    it(`Uin string value zero is valid`, () => {
        const expected = validateUin({ Uin: `0` });

        expect(expected).toEqual(true);
    });

    it(`Uin less than required length is not valid`, () => {
        const expected = validateUin({ Uin: `123` });

        expect(expected).toEqual(false);
    });

    it(`Uin 25 zeros in a row is not valid`, () => {
        const expected = validateUin({ Uin: `0000000000000000000000000` });

        expect(expected).toEqual(false);
    });

    it(`Uin 20 zeros in a row is not valid`, () => {
        const expected = validateUin({ Uin: `00000000000000000000` });

        expect(expected).toEqual(false);
    });

    it(`Uin 20 characters is valid`, () => {
        const expected = validateUin({ Uin: `01234567890000000020` });

        expect(expected).toEqual(true);
    });

    it(`Uin 25 characters is valid`, () => {
        const expected = validateUin({ Uin: `0123456789012345678900025` });

        expect(expected).toEqual(true);
    });
});

describe(`validationMethodsNewBackend KBK test`, () => {
    it(`KBK empty is not valid`, () => {
        const expected = validateKbk({});

        expect(expected).toEqual(false);
    });

    it(`KBK empty string is not valid`, () => {
        const Kbk = { Number: `` };
        const expected = validateKbk({ Kbk });

        expect(expected).toEqual(false);
    });

    it(`KBK less than required length is not valid`, () => {
        const Kbk = { Number: `123` };
        const expected = validateKbk({ Kbk });

        expect(expected).toEqual(false);
    });

    it(`KBK is valid`, () => {
        const Kbk = { Number: `12345678901234567890` };
        const expected = validateKbk({ Kbk });

        expect(expected).toEqual(true);
    });

    it(`KBK more than required length is not valid`, () => {
        const Kbk = { Number: `123456789012345678901234567890` };
        const expected = validateKbk({ Kbk });

        expect(expected).toEqual(false);
    });
});

describe(`validationMethodsNewBackend Period Date test`, () => {
    it(`Period empty is valid`, () => {
        const expected = validatePeriodDate({});

        expect(expected).toEqual(true);
    });

    it(`Period type mount is valid`, () => {
        const Period = { Type: CalendarTypesEnum.Month, Date: `25.10.2019` };
        const expected = validatePeriodDate({ Period });

        expect(expected).toEqual(true);
    });

    it(`Period type date and date is empty is not valid`, () => {
        const Period = { Type: CalendarTypesEnum.Date };
        const expected = validatePeriodDate({ Period });

        expect(expected).toEqual(false);
    });

    it(`Period type date and date null is not valid`, () => {
        const Period = { Type: CalendarTypesEnum.Date, Date: null };
        const expected = validatePeriodDate({ Period });

        expect(expected).toEqual(false);
    });

    it(`Period type date and date broken format is not valid`, () => {
        const Period = { Type: CalendarTypesEnum.Date, Date: `2019-11-25` };
        const expected = validatePeriodDate({ Period });

        expect(expected).toEqual(false);
    });

    it(`Period type date and date is valid`, () => {
        const Period = { Type: CalendarTypesEnum.Date, Date: `25.11.2019` };
        const expected = validatePeriodDate({ Period });

        expect(expected).toEqual(true);
    });
});
