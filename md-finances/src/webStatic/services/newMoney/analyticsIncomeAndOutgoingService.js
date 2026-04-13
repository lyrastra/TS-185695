import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { downloadPost, download } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import allPeriod from '../../resources/periodResource';
import PeriodTypeEnum from '../../enums/newMoney/AnalyticsPeriodTypeEnum';

export function downloadUsnReport(periods = [], year) {
    const data = periods.length ? getPeriods(periods, year) : getPeriodsByYear(year);

    mrkStatService.sendEventWithoutInternalUser(`skachat_otchet_analitiki_stranitsa_dengi_click_button`);

    return downloadPost(`/Finances/UsnReports/GetIncomeExpense`, data);
}

export function downloadPatentReport(patentId) {
    mrkStatService.sendEventWithoutInternalUser({
        event: `skachat_otchet_analitiki_po_patentu_stranitsa_dengi_click_button`,
        st5: patentId
    });

    return download(`/Finances/PatentReports/GetIncome`, { patentId });
}

function getPeriods(periods, year) {
    return periods.map(item => {
        const period = getPeriodItem(item);

        if (period.type === PeriodTypeEnum.Quarter) {
            return getQuarterRange(period.dateValue, year);
        }

        return getMonthRange(period.dateValue, year);
    });
}

function getPeriodsByYear(year) {
    return [{
        StartDate: dateHelper(`${year}`, `YYYY`).startOf(`year`).format(`YYYY-MM-DD`),
        EndDate: dateHelper(`${year}`, `YYYY`).endOf(`year`).format(`YYYY-MM-DD`)
    }];
}

function getPeriodItem(dateValue) {
    return Object.values(allPeriod).find(({ value }) => value === dateValue) || {};
}

function getQuarterRange(quarter, year) {
    const startDate = dateHelper(`${year}`, `YYYY`).quarter(quarter).startOf(`quarter`).format(`YYYY-MM-DD`);
    const endDate = dateHelper(`${year}`, `YYYY`).quarter(quarter).endOf(`quarter`).format(`YYYY-MM-DD`);

    return { StartDate: startDate, EndDate: endDate };
}

function getMonthRange(month, year) {
    const startDate = dateHelper(`${year}-${month}`, `YYYY-MM`).startOf(`month`).format(`YYYY-MM-DD`);
    const endDate = dateHelper(`${year}-${month}`, `YYYY-MM`).endOf(`month`).format(`YYYY-MM-DD`);

    return { StartDate: startDate, EndDate: endDate };
}

export default {
    downloadUsnReport,
    downloadPatentReport
};
