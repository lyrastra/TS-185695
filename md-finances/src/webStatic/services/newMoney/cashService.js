import { download, get } from '@moedelo/frontend-core-v2/helpers/httpClient';
import requisitesService from '../requisitesService';

const cashService = {
    downloadImportFile({
        cashId, startDate, endDate, format, timeout
    }) {
        if (!cashId || !startDate || !endDate || !format) {
            throw new Error(`Проверьте передаваемые параметры`);
        }

        return download(`/Accounting/FirmCash/CashbookReport?cashId=${cashId}&startDate=${startDate}&endDate=${endDate}&showTitles=true&fileType=${format}`, { timeout });
    },

    hasCashOperationInPeriod({ cashId, startDate, endDate }) {
        if (!cashId || !startDate || !endDate) {
            throw new Error(`Проверьте передаваемые параметры`);
        }

        return get(`/Accounting/FirmCash/HasCashOperationInPeriod?cashId=${cashId}&startDate=${startDate}&endDate=${endDate}`);
    },

    getRegistrationDate() {
        return requisitesService.get(`/Finances/Data/Setup`);
    }
};

export default cashService;
