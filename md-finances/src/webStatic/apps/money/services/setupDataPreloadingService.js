import { get } from '@moedelo/frontend-core-v2/helpers/restHttpClient';

const setupDataPreloadingService = {
    async getSetupDataPreloading() {
        return new Promise(async resolve => {
            if (this.dataSetup) {
                resolve(this.dataSetup);

                return;
            }

            if (!this.loading && !this.dataSetup) {
                this.loading = true;
                this.dataSetup = (await get(`/Finances/Data/Setup`)).data;
                this.loading = false;

                resolve(this.dataSetup);

                return;
            }

            const interval = setInterval(() => {
                if (this.dataSetup) {
                    clearInterval(interval);
                    resolve(this.dataSetup);
                }
            }, 50);
        });
    },

    async getRegistrationInService() {
        const data = await this.getSetupDataPreloading();

        return data?.RegistrationInService;
    },

    async getAccessRuleFlags() {
        const data = await this.getSetupDataPreloading();

        return data?.AccessRuleFlags;
    },

    async getAccessToMoneyEdit() {
        const data = await this.getSetupDataPreloading();

        return data?.AccessRuleFlags?.HasAccessToMoneyEdit;
    },

    async getAccessToPostings() {
        const data = await this.getSetupDataPreloading();

        return data?.AccessRuleFlags?.HasAccessToPostings;
    },

    async getImportMessages() {
        const data = await this.getSetupDataPreloading();

        return data?.ImportMessages;
    },

    async getIntegrationErrors() {
        const data = await this.getSetupDataPreloading();

        return data?.IntegrationErrors;
    },

    async getLastClosedDate() {
        const data = await this.getSetupDataPreloading();

        return data?.LastClosedDate;
    }
};

export default setupDataPreloadingService;
