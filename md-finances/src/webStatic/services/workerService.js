import { get } from '@moedelo/frontend-core-v2/helpers/httpClient';
import { get as restGet } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import notificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';

export default {
    async autocomplete({
        query,
        date,
        onlyInStaff = false,
        withIpAsWorker = false
    }) {
        const args = {
            date,
            onlyInStaff,
            query,
            count: 5,
            withIpAsWorker
        };

        const { List } = await get(`/Payroll/Workers/GetWorkersAutocomplete`, args);

        return List;
    },

    async getEmployeeById(employeeId) {
        try {
            const result = await restGet(`/payroll/api/v1/Employee/${employeeId}`);

            return result;
        } catch (e) {
            notificationManager.show({
                type: `error`,
                message: `Произошла непредвиденная ошибка`
            });

            return null;
        }
    }
};
