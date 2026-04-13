import React from 'react';
import { observer } from 'mobx-react';
import cn from 'classnames';
import PropTypes from 'prop-types';
import info from '@moedelo/frontend-core-react/icons/info.m.svg';
import plus from '@moedelo/frontend-core-react/icons/plus.m.svg';
import { getJsx } from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import Link, { Size } from '@moedelo/frontend-core-react/components/Link';
import P from '@moedelo/frontend-core-react/components/P';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';

import BankTile from './components/BankTile';

import style from './style.m.less';

function BankIntegrationContent({ store }) {
    const renderNotification = () => {
        if (store.isDisabled) {
            return null;
        }

        return <div className={cn(grid.row, style.notification)}>
            <div className={grid.col_1}>
                {getJsx({ file: info, className: style.notification__icon })}
            </div>
            <div className={grid.col_23}>
                Нет счета в интересующем банке? <Link size={Size.small} onClick={store.onRequisitesLinkClick}>Открыть</Link> на льготных условиях
            </div>
        </div>;
    };

    const renderMessage = () => {
        const getLink = (type = ``) => <Link onClick={() => store.onMarketplaceLinkClick(type)}>
            Перейти к тарифам
        </Link>;

        if (store.hasLimit) {
            return <P className={style.message}>
                <b>Вы достигли лимита по подключениям.</b> Чтобы подключать больше банков смените тариф. {getLink(`limit`)}
            </P>;
        }

        if (store.hasUpsale) {
            return <P className={style.message}>
                <b>Вам доступны 2 интеграции.</b> Чтобы подключать больше банков смените тариф. {getLink(`upsale`)}
            </P>;
        }

        return null;
    };

    return <div>
        {renderMessage()}
        <div className={style.tiles__container}>
            {store.turnedOnBanks.map(bank => <BankTile
                isTurnedOn
                store={store}
                {...bank}
            />)}
            {store.accessibleBanks.map(bank => <BankTile
                isTurnedOn={false}
                store={store}
                {...bank}
            />)}
            {/* eslint-disable-next-line jsx-a11y/click-events-have-key-events,jsx-a11y/interactive-supports-focus */}
            <div
                role={`button`}
                onClick={store.onAddSettlementAccountClick}
                className={cn(style.settlement__tile, { [style.disabled]: store.isAddButtonDisabled })}
            >
                {getJsx({ file: plus, className: style.plus__icon })}
                Добавить счёт
            </div>
        </div>
        {renderNotification()}
    </div>;
}

BankIntegrationContent.propTypes = {
    store: PropTypes.object
};

export default observer(BankIntegrationContent);
