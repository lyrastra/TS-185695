import notTaxableReasonGetter from './notTaxableReasonGetter';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

const envdTaxationSystem = {
    IsUsn: false,
    IsOsno: false,
    IsEnvd: true
};

const osnoTaxationSystem = {
    IsUsn: false,
    IsOsno: true,
    IsEnvd: true
};

describe(`notTaxableReasonGetter for ReturnToBuyerStore operation`, () => {
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
});
