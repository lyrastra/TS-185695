import expect from 'expect';
import notTaxableReasonGetter from '../notTaxableReasonGetter';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

const usn = {
    IsUsn: true,
    IsOsno: false,
    IsEnvd: false
};

const usnEnvd = {
    IsUsn: true,
    IsOsno: false,
    IsEnvd: true
};

const osno = {
    IsUsn: false,
    IsOsno: true,
    IsEnvd: false
};

const osnoEnvd = {
    IsUsn: false,
    IsOsno: true,
    IsEnvd: true
};

const envd = {
    IsUsn: false,
    IsOsno: false,
    IsEnvd: true
};

describe(`notTaxableReasonGetter for IncomeFromCommissionAgent operation`, () => {
    it(`not taxable for Usn`, () => {
        const operationData = {
            taxationSystem: usn
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.simple);
    });

    it(`not taxable for UsnEnvd`, () => {
        const operationData = {
            taxationSystem: usnEnvd
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.simple);
    });

    it(`not taxable for Osno`, () => {
        const operationData = {
            taxationSystem: osno
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.osno);
    });

    it(`not taxable for OsnoEnvd`, () => {
        const operationData = {
            taxationSystem: osnoEnvd
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.osno);
    });

    it(`not taxable for Envd`, () => {
        const operationData = {
            taxationSystem: envd
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.envd);
    });
});
