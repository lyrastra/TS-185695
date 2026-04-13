import { showDirectPaymentModal } from '@moedelo/frontend-common-v2/apps/bankIntegration/helpers/directPaymentHelper';

export const showDirectPaymentInstructionsAsync = partnerId => {
    return showDirectPaymentModal({
        flagName: `dontShowFinancesDirectPaymentModal`,
        partnerId
    });
};

export default { showDirectPaymentInstructionsAsync };
