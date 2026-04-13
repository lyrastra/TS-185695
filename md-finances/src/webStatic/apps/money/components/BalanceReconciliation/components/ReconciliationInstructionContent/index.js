import React from 'react';
import P from '@moedelo/frontend-core-react/components/P';
import Link from '@moedelo/frontend-core-react/components/Link';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import navigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { post } from '@moedelo/frontend-core-v2/helpers/httpClient';

const links = {
    manual: `https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dengi-prof/sverka-s-bankom`,
    howToFillStocks: `https://www.moedelo.org/manual/ostatki`,
    howToFixOperations: `https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dengi-prof/neraspoznannye-operacii`
};

const eventName = `on_reconciliation_instruction_link_click`;
const localStorageKey = `reconciliation_instruction_outsource_help_key`;
const dateFormat = `DD.MM.YYYY HH.mm`;

const isRequestSent = () => {
    const now = dateHelper();
    const localRequestTime = localStorage.getItem(localStorageKey);

    if (!localRequestTime) {
        return false;
    }

    const requestTimeWithDelay = dateHelper(localRequestTime, dateFormat, true).add(3, `hour`);

    return !now.isAfter(requestTimeWithDelay);
};

const ReconciliationInstructionContent = () => {
    const [isSent, setIsSent] = React.useState(false);

    React.useEffect(() => setIsSent(isRequestSent()), []);

    const onClickLink = (href, linkId = ``) => {
        mrkStatService.sendEventWithoutInternalUser({ event: eventName, st5: linkId });

        navigateHelper.open(href, { useRawUrl: true });
    };

    const onOutsourceLinkClick = async () => {
        mrkStatService.sendEvent({
            event: `reconciliation_outsource_request_help_link_click`
        });

        await post(`/Main/MailRequest/WrongBalance`);

        localStorage.setItem(localStorageKey, dateHelper().format(dateFormat));
        setIsSent(true);
    };

    return <React.Fragment>
        <P>
            Автосверка проверит операции в бухгалтерии и банке и покажет недостающие или лишние операции.<br />
            Функционал запускается автоматически раз в неделю.<br />
            <Link onClick={() => onClickLink(links.manual, `reconciliationManual`)}>Инструкция</Link>
        </P>

        <P>
            <b>Для правильного остатка по счету рекомендуем:</b>
        </P>

        <P>
            <b>1.</b> Проведите новую сверку с банком, если текущая не актуальна.
        </P>

        <P>
            <b>2.</b> Проверьте корректность остатка по расчетному счету в Реквизитах- <Link href={`/Requisites?accountingBalances`}>Остатки</Link> и проверьте корректно ли указан остаток по расчетному счету.<br />
            Если Вы не заполняли остатки, то пропустите данный шаг.<br />
            <Link onClick={() => onClickLink(links.howToFillStocks, `howToFillStocks`)}>Как заполнить остатки?</Link>
        </P>

        <P>
            <b>3.</b> Проверьте нераспознанные операции.<br />
            <Link onClick={() => onClickLink(links.howToFixOperations, `howToFixOperations`)}>Как исправить нераспознанные операции?</Link>
        </P>

        <P>
            <b>4.</b> Доверьтесь специалисту.<br />

            {!isSent && <React.Fragment>
                Доверьте решение этого вопроса эксперту (услуга платная - 3000 руб.).<br />
                <b><Link onClick={onOutsourceLinkClick}>Оставить заявку</Link></b>.
            </React.Fragment>}

            {isSent && <b>Заявка отправлена.</b>}
        </P>
    </React.Fragment>;
};

export default ReconciliationInstructionContent;
