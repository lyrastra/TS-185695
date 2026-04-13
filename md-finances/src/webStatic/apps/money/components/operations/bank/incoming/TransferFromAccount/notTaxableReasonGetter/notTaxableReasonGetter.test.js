import notTaxableReasonGetter from './notTaxableReasonGetter';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

const envdTaxationSystem = {
    IsUsn: false,
    IsOsno: false,
    IsEnvd: true
};

const usnTaxationSystem = {
    IsUsn: true,
    IsOsno: false,
    IsEnvd: true
};

const osnoTaxationSystem = {
    IsUsn: false,
    IsOsno: true,
    IsEnvd: true
};

describe(`notTaxableReasonGetter for IncomingFromAnotherAccount operation`, () => {
    it(`not taxable for Envd`, () => {
        expect(notTaxableReasonGetter.get(envdTaxationSystem)).toEqual(notTaxableMessages.envd);
    });

    it(`not taxable for Osno`, () => {
        expect(notTaxableReasonGetter.get(osnoTaxationSystem)).toEqual(notTaxableMessages.osno);
    });

    it(`not taxable for Usn`, () => {
        expect(notTaxableReasonGetter.get(usnTaxationSystem)).toEqual(notTaxableMessages.usn);
    });
});
