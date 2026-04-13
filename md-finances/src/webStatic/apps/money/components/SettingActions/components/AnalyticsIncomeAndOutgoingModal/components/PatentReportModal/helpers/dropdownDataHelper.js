import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import patentMapper from '../../../../../../../../../mappers/patentMapper';

export const getPatents = ({ patentList, year }) => {
    const currentPatents = patentList.filter(patent => dateHelper(patent.StartDate, `DD.MM.YYYY`).year() === year);

    return patentMapper.mapPatentsToDropDown(currentPatents);
};

export const getYears = patentList => {
    const years = [...new Set(patentList.map(patent => dateHelper(patent.StartDate, `DD.MM.YYYY`).year()))];

    return years.map(y => {
        return {
            text: y,
            value: y
        };
    });
};
