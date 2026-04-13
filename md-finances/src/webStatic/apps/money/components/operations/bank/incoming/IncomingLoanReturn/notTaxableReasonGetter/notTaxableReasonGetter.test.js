import expect from 'expect';
import notTaxableReasonGetter from '../notTaxableReasonGetter';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

const envdTaxationSystem = {
    IsUsn: false,
    IsOsno: false,
    IsEnvd: true
};

const usnTaxationSystem = {
    IsUsn: true,
    IsOsno: false,
    IsEnvd: true,
    UsnType: 1
};

const osnoTaxationSystem = {
    IsUsn: false,
    IsOsno: true,
    IsEnvd: true
};

describe(`notTaxableReasonGetter for OutgoingLoanRepayment operation`, () => {
    it(`not taxable for Envd`, () => {
        const operationData = {
            taxationSystem: envdTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.simple);
    });

    it(`not taxable for Osno`, () => {
        const operationData = {
            taxationSystem: osnoTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.simple);
    });

    it(`not taxable for Usn 6`, () => {
        const operationData = {
            taxationSystem: usnTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.simple);
    });
});
