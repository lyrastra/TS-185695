import notTaxableReasonGetter from './notTaxableReasonGetter';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

const envdTaxationSystem = {
    IsUsn: false,
    IsOsno: false,
    IsEnvd: true
};

const usn6TaxationSystem = {
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

describe(`notTaxableReasonGetter for BudgetaryPayment operation`, () => {
    it(`not taxable for Envd`, () => {
        const operationData = {
            taxationSystem: envdTaxationSystem,
            kbk: {}
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.envd);
    });

    it(`not taxable for Osno`, () => {
        const operationData = {
            taxationSystem: osnoTaxationSystem,
            kbk: {}
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.osnoOutgoing);
    });

    it(`taxable for Usn6`, () => {
        const operationData = {
            taxationSystem: usn6TaxationSystem,
            kbk: {}
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.usn6);
    });
});
