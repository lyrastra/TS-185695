/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { getOperationTypeByDirectionAndLegalTypeAndSource } from '../../../../../../../helpers/MoneyOperationHelper';
import LegalType from '../../../../../../../enums/LegalTypeEnum';
import MoneySourceType from '../../../../../../../enums/MoneySourceType';
import { cashOrderOperationResources } from '../../../../../../../resources/MoneyOperationTypeResources';

(function(cash) {
    cash.Views.outgoingOperationList = (function() {
        const legalType = window._preloading.IsOoo ? LegalType.Ooo : LegalType.Ip;
        const hide = [ cashOrderOperationResources.UnifiedCashOrderBudgetaryPayment ];

        const operations = getOperationTypeByDirectionAndLegalTypeAndSource(Direction.Outgoing, legalType, MoneySourceType.Cash, hide);

        function getByType(type) {
            const items = _.where(operations, { value: type });
            if (!items.length) {
                throw Error('Undefined cash operation type - ', type);
            }
            return items[0];
        }

        return {
            collection: operations,
            getDefaultType() {
                return operations[0].value;
            },
            getRelatedView(type) {
                return getByType(type).view;
            },

            getDescription(type) {
                return getByType(type).text;
            }
        };
    }());
}(Cash));
