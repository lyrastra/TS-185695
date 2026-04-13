import KudirNameResource from '../resources/KudirNameResource';
import KudirTypeResource from '../resources/KudirTypeResource';

export const mapKudirs = kudirs => {
    return kudirs.map(kudir => ({
        ...kudir,
        value: kudir.Type,
        text: KudirNameResource[kudir.Type]
    }));
};

export const mapPeriods = periods => {
    return periods.map(period => ({
        ...period,
        text: `за ${period.Year} год`,
        value: period
    })).reverse();
};

export const mapParams = params => {
    return {
        ...params,
        ReportType: KudirTypeResource[params.ReportType]
    };
};

export default {
    mapKudirs,
    mapPeriods,
    mapParams
};
