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
    IsEnvd: false
};

const osnoEnvdTaxationSystem = {
    IsUsn: false,
    IsOsno: true,
    IsEnvd: true
};

describe(`notTaxableReasonGetter for TransferToOtherAccount operation`, () => {
    it(`not taxable for Envd`, () => {
        const operationData = {
            taxationSystem: envdTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.envd);
    });

    it(`not taxable for Osno`, () => {
        const operationData = {
            taxationSystem: osnoTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.osnoOutgoing);
    });

    it(`not taxable for Osno+Envd`, () => {
        const operationData = {
            taxationSystem: osnoEnvdTaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.osnoOutgoing);
    });

    it(`taxable for Usn6`, () => {
        const operationData = {
            taxationSystem: usn6TaxationSystem
        };

        expect(notTaxableReasonGetter.get(operationData)).toEqual(notTaxableMessages.usn6);
    });
});
