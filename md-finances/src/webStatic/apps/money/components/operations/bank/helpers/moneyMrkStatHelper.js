import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import { changeOperationTypeEvent } from '../../../../../../resources/mrkStatEventResources';

export function sendChangeOperationTypeEvent(model) {
    const { oldOperationType, OperationType, DocumentBaseId } = model;

    mrkStatService.sendEventWithoutInternalUser({
        event: changeOperationTypeEvent,
        st5: JSON.stringify({
            oldOperationType,
            operationType: OperationType,
            documentBaseId: DocumentBaseId
        })
    });
}

export default {
    sendChangeOperationTypeEvent
};
