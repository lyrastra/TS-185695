import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import DirectionEnum from '@moedelo/frontend-enums/mdEnums/Direction';
import scenarioSectionResource
    from '@moedelo/frontend-common-v2/apps/onboardingScenario/resources/scenarioSectionResource';
import MoneyOperationTypeResources from '../resources/MoneyOperationTypeResources';

const getTaxTipId = (taxType, direction) => {
    if (taxType !== TaxationSystemType.Usn) {
        return null;
    }

    switch (direction) {
        case DirectionEnum.Outgoing:
            return `tip_${scenarioSectionResource.Finances}_6`;
        case DirectionEnum.Incoming:
            return `tip_${scenarioSectionResource.Finances}_7`;
        default:
            return null;
    }
};

const getOperationTipId = (operationType, sum) => {
    const isFixedAssetsPayment = (operationType === MoneyOperationTypeResources.PaymentOrderPaymentToSupplier.value && sum > 100000);

    if (isFixedAssetsPayment) {
        return `tip_${scenarioSectionResource.Finances}_8`;
    }

    return null;
};

export default { getTaxTipId, getOperationTipId };
