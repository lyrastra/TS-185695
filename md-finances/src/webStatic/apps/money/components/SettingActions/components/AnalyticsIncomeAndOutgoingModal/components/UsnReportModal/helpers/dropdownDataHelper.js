import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { months, quarter } from '../../../../../../../../../resources/periodResource';

export const getYears = registrationDate => {
    const minYear = dateHelper(registrationDate, `YYYY-MM-DD`).year();
    const maxYear = dateHelper().year();

    return Array.from(new Array((maxYear - minYear) + 1))
        .map((item, index) => {
            const year = maxYear - index;

            return { value: year, text: year };
        });
};

export const getPeriods = () => {
    return [quarter, months];
};
