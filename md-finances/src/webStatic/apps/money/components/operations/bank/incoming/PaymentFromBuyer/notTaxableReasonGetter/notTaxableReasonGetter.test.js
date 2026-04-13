import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
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

describe(`notTaxableReasonGetter for PaymentFromBuyer operation`, () => {
    it(`not taxable for Envd`, () => {
        const operationData = {
            taxationSystem: envdTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.envd);
        expect(notTaxableReasonGetter.get({ taxationSystemType: TaxationSystemType.Envd })).toEqual(notTaxableMessages.envd);
    });

    it(`not taxable for Osno`, () => {
        const operationData = {
            taxationSystem: osnoTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.osno);
    });

    it(`taxable for Usn`, () => {
        const operationData = {
            taxationSystem: usnTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(null);
    });
});
