import { showModal } from '@moedelo/frontend-core-react/helpers/confirmModalHelper';
import { deleteRuleAsync } from '../services/paymentImportRulesService';

/**
 * @param {number} id удаляемого правила
 * @returns {Promise<number>} выполнит обещание с id удаленного правила
 */
export function deletePaymentImportRule(id) {
    return new Promise(resolve => {
        showModal({
            header: `Удаление`,
            children: `Вы уверены, что хотите удалить это правило?`,
            confirmButtonText: `Удалить`,
            onConfirm: () => {
                deleteRuleAsync(id).then(() => resolve(id));
            }
        });
    });
}

export default { deletePaymentImportRule };
