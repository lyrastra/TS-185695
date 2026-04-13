import { get as restGet, download } from '@moedelo/frontend-core-v2/helpers/restHttpClient';
import { post, get } from '@moedelo/frontend-core-v2/helpers/httpClient';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';

export const getKudirInfo = async () => {
    return restGet(`Reports/api/v1/Available/Kudir`);
};

export const downloadOsnoReport = async params => {
    return download(`/KudirOsno/api/v1/Download/Get`, params);
};

export const downloadReport = async params => {
    const file = await post(`/Rpt/Reports/CreateReport`, params);
    const link = document.createElement(`a`);
    link.href = file;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};

export const sendEvent = params => {
    return mrkStatService.sendEvent(params);
};

export const getPeriodWarnings = async period => {
    return get(`/Accounting/ClosingPeriodWizard/CheckPeriod`, period);
};

export const sendKudirEmail = async data => {
    return post(`/Rpt/Reports/SendKudirByEmail`, data);
};

export default {
    getKudirInfo,
    downloadReport,
    downloadOsnoReport,
    sendEvent,
    getPeriodWarnings,
    sendKudirEmail
};
