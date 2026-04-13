import UsnType from '@moedelo/frontend-enums/mdEnums/UsnType';
import notTaxableReasonGetter from './notTaxableReasonGetter';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

const envdTaxationSystem = {
    IsUsn: false,
    IsOsno: false,
    IsEnvd: true
};

const usn15TaxationSystem = {
    IsUsn: true,
    UsnType: UsnType.ProfitAndOutgo,
    IsOsno: false,
    IsEnvd: false
};

const usn15AndEnvdTaxationSystem = {
    IsUsn: true,
    UsnType: UsnType.ProfitAndOutgo,
    IsOsno: false,
    IsEnvd: true
};

const osnoTaxationSystem = {
    IsUsn: false,
    IsEnvd: false,
    IsOsno: true
};

const osnoAndEnvdTaxationSystem = {
    IsUsn: false,
    IsEnvd: true,
    IsOsno: true
};

describe(`notTaxableReasonGetter for GoodsPaidByCreditCard operation`, () => {
    it(`not taxable for Envd`, () => {
        const operationData = {
            taxationSystem: envdTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.envd);
    });

    it(`taxable for Usn15`, () => {
        const operationData = {
            taxationSystem: usn15TaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(null);
    });

    it(`taxable for Usn15 And Envd`, () => {
        const operationData = {
            taxationSystem: usn15AndEnvdTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(null);
    });

    it(`taxable for Osno`, () => {
        const operationData = {
            taxationSystem: osnoTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(null);
    });

    it(`taxable for Osno And Envd`, () => {
        const operationData = {
            taxationSystem: osnoAndEnvdTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(null);
    });
});
