import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';

let loading;

export default {
    sendFilterRequest(requestArgs) {
        if (!loading) {
            loading = true;
            setTimeout(() => {
                mrkStatService.sendEvent({
                    event: `finances_table_filter`,
                    st5: JSON.stringify(requestArgs)
                });
                loading = false;
            }, 300);
        }
    }
};
