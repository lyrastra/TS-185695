import firmFlagService from '@moedelo/frontend-common-v2/services/firmFlagService';
import showNewPaymentMethodModal from '../../commonComponents/NewPaymentMethodModal';
import NewPaymentMethodPartner from '../../../../../../enums/newMoney/NewPaymentMethodPartner';

export const getPaymentModalInfo = async ({ currentSettlementAccount }) => {
    /*
        Определение наличия модалки интеграции у контрагента
    */
    const { IntegrationPartner } = currentSettlementAccount;
    const hasModal = Object.keys(NewPaymentMethodPartner).includes(`${IntegrationPartner}`);
    
    /*
         Определение была ли уже показана модалка
    */
    
    const flagName = `newPaymentMethod_${IntegrationPartner}`;
    const isFlagEnabled = await firmFlagService.isEnabled({ name: flagName });
    /*
        Определение надо ли показывать модалку
     */
    const isNeedToShowModal = !isFlagEnabled && hasModal;

    return {
        isNeedToShowModal,
        IntegrationPartner,
        flagName
    };
};

/**
 * Проверяет условия и показывает модальное окно выбора способа оплаты,
 * если это необходимо для указанного направления и счёта.
 */

export const checkAndShowPaymentModal = async ({ currentSettlementAccount }) => {
    const { isNeedToShowModal, IntegrationPartner, flagName } = await getPaymentModalInfo({ currentSettlementAccount });

    if (isNeedToShowModal) {
        showNewPaymentMethodModal({ header: NewPaymentMethodPartner[IntegrationPartner], IntegrationPartner, flagName });
    }
};

export default {
    getPaymentModalInfo,
    checkAndShowPaymentModal
};
