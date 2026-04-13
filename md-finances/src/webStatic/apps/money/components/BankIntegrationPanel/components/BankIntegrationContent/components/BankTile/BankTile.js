import React from 'react';
import { observer } from 'mobx-react';
import cn from 'classnames';
import PropTypes from 'prop-types';
import { getIconJsxByPartnerId } from '@moedelo/frontend-common-v2/apps/finances/helpers/bankIconsHelper';
import { getJsx } from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import Button, { Color, Size } from '@moedelo/frontend-core-react/components/buttons/Button';
import success from '@moedelo/frontend-core-react/icons/success.m.svg';

import style from './style.m.less';

function BankTile(props) {
    const {
        store,
        isTurnedOn,
        IntegrationPartner,
        Name
    } = props;

    const getTileClassnames = () => {
        return cn(
            style.bank__info,
            {
                [style.disabled]: isTurnedOn ? store.isTurnedOnTileDisabled : store.isTileDisabled
            }
        );
    };

    const renderButton = () => {
        if (isTurnedOn) {
            return <Button
                className={style.fake__button}
                color={Color.White}
                size={Size.Small}
                disabled={store.isTurnedOnTileDisabled}
                onClick={() => {}}
            >
                Подключен
                {getJsx({ file: success, className: style.turnedOn__icon })}
            </Button>;
        }

        return <Button
            color={Color.Blue}
            size={Size.Small}
            disabled={store.isTileDisabled}
            onClick={() => store.onClickTurnIntegration(IntegrationPartner)}
        >
            Подключить
        </Button>;
    };

    return <div className={cn(style.tile, { [style.turnedOn]: isTurnedOn })}>
        <div className={getTileClassnames()}>
            {getIconJsxByPartnerId({ id: IntegrationPartner, className: style.bank__icon })}
            <div title={Name} className={style.bank__name}>{Name}</div>
        </div>
        {renderButton()}
    </div>;
}

BankTile.propTypes = {
    store: PropTypes.object,
    IntegrationPartner: PropTypes.number,
    Name: PropTypes.string,
    isTurnedOn: PropTypes.bool
};

export default observer(BankTile);
