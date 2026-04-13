import whiteLabelHelper from '@moedelo/frontend-common-v2/apps/marketing/helpers/whiteLabelHelper';
import IntegrationPartner from '@moedelo/frontend-enums/mdEnums/IntegrationPartner';

import { getHostName } from '@moedelo/frontend-core-v2/helpers/urlHelper';

// Идентификаторы партнеров, которые участвуют в формировании ссылок для ошибок интеграции.
const sberBank = IntegrationPartner.SberBank;
const alfaBank = IntegrationPartner.AlfaBankSsoNew;
const pointBank = IntegrationPartner.PointBank;

// Текст ссылки, который показывается для партнеров, где требуется подписание.
// Пример отображения в UI: "Перейти на подписание.".
const signMessageUrl = `Перейти на подписание.`;

// Партнеры, для которых ссылка ведет на endpoint инициации подписания.
// Пример: [IntegrationPartner.AlfaBankSsoNew, IntegrationPartner.PointBank].
const signedPartners = [alfaBank, pointBank];

// Ссылка на инструкцию для Сбера зависит от white-label окружения.
// Пример результата:
// - WL: https://www.moedelo.org/.../sberbank-wl
// - не WL: https://www.moedelo.org/.../sbbol-oferta
const getSberBankIntegrationUrl = () => {
    const baseUrl = `https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rekvizity/integratsiya-s-bankami-prof/banki-partnyory-prof`;

    return whiteLabelHelper.isSberWl()
        ? `${baseUrl}/sberbank-wl`
        : `${baseUrl}/sbbol-oferta`;
};

// Формирует URL для редиректа на страницу подписания/инициации интеграции банка.
// Пример:
// hostName = "https://restapi.moedelo.org/", partner = 123
// результат = "https://restapi.moedelo.org/Sso/Uri/v1/RedirectInitIntegrationUrlForPartner?partner=123"
const getBankIntegrationUrl = (partner) => {
    const subDomain = `restapi`;
    const hostName = getHostName(subDomain);
    const endpoint = `Sso/Uri/v1/RedirectInitIntegrationUrlForPartner`;

    if (!hostName) {
        throw new Error(`Имя хоста не определено.`);
    }

    return `${hostName}${endpoint}?partner=${partner}`;
};

// Возвращает описание партнера в формате, который ожидает IntegrationError.
// Пример результата:
// {
//   getUrl: () => "https://restapi.../RedirectInitIntegrationUrlForPartner?partner=123",
//   messageUrl: "Перейти на подписание."
// }
const createBankPartnerData = (partner) => ({
    getUrl: () => getBankIntegrationUrl(partner),
    messageUrl: signMessageUrl
});

// Строит объект вида { [partnerId]: { getUrl, messageUrl } } для партнеров с подписанием.
// Пример ключа: partnerData[IntegrationPartner.AlfaBankSsoNew].
const signedPartnerData = signedPartners.reduce((acc, partner) => ({
    ...acc,
    [partner]: createBankPartnerData(partner)
}), {});

// Общий справочник данных для отображения ссылок в уведомлениях об ошибках интеграции.
// Ключ — IntegrationPartnerId, значение — объект с функцией получения URL и текстом ссылки.
// Пример использования:
// const info = partnerData[IntegrationPartner.SberBank];
// info.getUrl(); // "https://www.moedelo.org/..."
// info.messageUrl; // "Инструкция."
const partnerData = {
    [sberBank]: {
        getUrl: getSberBankIntegrationUrl,
        messageUrl: `Инструкция.`
    },
    ...signedPartnerData
};

export default partnerData;
