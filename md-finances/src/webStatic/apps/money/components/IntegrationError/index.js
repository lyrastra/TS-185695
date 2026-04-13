import React from 'react';
import classnames from 'classnames/bind';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import NotificationType from '@moedelo/frontend-core-react/components/Notification/enums/NotificationType';
import companyId from '@moedelo/frontend-core-v2/helpers/companyId';
import gridStyle from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Link from '@moedelo/frontend-core-react/components/Link';

import integrationErrorService from '../../../../services/newMoney/integrationErrorService';
import partnerData from './resources/partnerUrls';
import style from './style.m.less';

const cn = classnames.bind(style);

// Компонент показывает непрочитанные ошибки интеграций в виде уведомлений.
// После закрытия уведомления ошибка помечается как прочитанная и удаляется из локального состояния.
class IntegrationError extends React.Component {
    constructor() {
        super();

        this.state = {
            errors: []
        };
    }

    // При монтировании запрашиваем ошибки интеграций для текущей компании.
    // Обновляем state только если ошибки есть, чтобы избежать лишнего рендера.
    async componentDidMount() {
        const id = companyId.getId();
        const errors = await integrationErrorService.get(id);

        if (errors.length) {
            this.setState({ errors });
        }
    }

    // Обработчик закрытия уведомления:
    // 1) отправляет в сервис id ошибок для пометки как прочитанных;
    // 2) удаляет конкретную ошибку из локального списка отображения.
    onErrorClose = (err) => {
        integrationErrorService.setReedState(err.ErrorIds);

        this.setState((prevState) => ({
            errors: prevState.errors.filter((item) => item !== err)
        }));
    }

    // Возвращает данные партнера для ссылки в тексте ошибки (если партнер известен).
    // Формат ответа: { url, messageUrl } или null, если партнер не найден.
    getUrlForPartner = (IntegrationPartnerId) => {
        const partner = partnerData[IntegrationPartnerId];

        if (partner) {
            const { getUrl, messageUrl } = partner;

            return {
                url: getUrl?.(),
                messageUrl
            };
        }

        return null;
    };

    // Формирует содержимое уведомления.
    // Если нужна ссылка и URL доступен, возвращается JSX с текстом и ссылкой;
    // иначе возвращается только текст сообщения.
    getMessage = (error) => {
        if (!error) { return null; }

        const { IntegrationPartnerId, NeedLink, Message } = error;
        const urlPartner = this.getUrlForPartner(IntegrationPartnerId);
        const shouldRenderLink = NeedLink && urlPartner?.url;

        if (shouldRenderLink) {
            return (
                <div className={cn(style.message__wrapper)}>
                    <div>{Message}</div>
                    <Link
                        href={urlPartner.url}
                        target={`_blank`}
                        noCompanyId
                    >{urlPartner.messageUrl}</Link>
                </div>
            );
        }

        return Message;
    }

    // Рендерит список панелей ошибок на основе текущего state.errors.
    renderErrors() {
        return this.state.errors.map(err => (
            <NotificationPanel
                key={err.ErrorIds.join(`,`)}
                className={gridStyle.row}
                type={err.IntegrationNotificationErrorType || NotificationType.warning}
                onClose={() => this.onErrorClose(err)}
            >
                {this.getMessage(err)}
            </NotificationPanel>
        ));
    }

    // Если ошибок нет, компонент ничего не рендерит.
    render() {
        return this.state.errors.length ? this.renderErrors() : null;
    }
}

export default IntegrationError;
