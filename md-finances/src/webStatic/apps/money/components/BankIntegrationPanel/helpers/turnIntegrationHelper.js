import {
    turnIntegrationDialog
} from '@moedelo/frontend-common-v2/apps/bankIntegration/helpers/turnIntegrationHelper';
import Logger from '@moedelo/frontend-core-v2/helpers/logger/index';
import ChatHelper from '@moedelo/chatbot/src/Helpers/ChatHelper';

export function showIntegrationDialog(partnerId, onSubmit) {
    if (!partnerId) {
        Logger.error(`Необходим partnerId`);

        return;
    }

    turnIntegrationDialog({
        isOn: false,
        partnerId,
        onSubmit,
        showChat: ChatHelper.showChat
    });
}

export default {
    showIntegrationDialog
};
