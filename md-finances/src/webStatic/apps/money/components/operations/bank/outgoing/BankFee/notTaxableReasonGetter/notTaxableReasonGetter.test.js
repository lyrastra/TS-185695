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

describe(`notTaxableReasonGetter for WarrantBankFeeIsDeducted operation`, () => {
    it(`not taxable for Envd`, () => {
        const operationData = {
            taxationSystem: envdTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.envd);
    });

    it(`taxable for Usn6`, () => {
        const operationData = {
            taxationSystem: usn6TaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.simple);
    });
});
