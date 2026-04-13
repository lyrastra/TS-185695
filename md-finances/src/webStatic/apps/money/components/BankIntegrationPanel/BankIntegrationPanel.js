import React from 'react';
import cn from 'classnames';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import CollapsiblePanel from '@moedelo/frontend-core-react/components/CollapsiblePanel';
import Arrow, { ArrowDirection } from '@moedelo/frontend-core-react/components/Arrow';
import bank from '@moedelo/frontend-core-react/icons/bank.m.svg';
import { getJsx } from '@moedelo/frontend-core-react/helpers/svgIconHelper';

import BankIntegrationStore from './store/BankIntegrationStore';
import BankIntegrationContent from './components/BankIntegrationContent';

import style from './style.m.less';

function BankIntegrationPanel({ refreshSourceList }) {
    // eslint-disable-next-line react-hooks/exhaustive-deps
    const store = React.useMemo(() => new BankIntegrationStore({ refreshSourceList }), []);

    const renderCount = () => {
        if (!store.needToShowCount) {
            return null;
        }

        return <div className={style.tag}>+{store.availableIntegrationsCount}</div>;
    };

    return <CollapsiblePanel
        opened={store.opened}
        loading={store.loading}
        onOpen={store.openPanel}
        onClose={store.closePanel}
        className={style.panel__container}
        panelClassName={style.panel}
        contentClassName={style.content}
        content={<BankIntegrationContent store={store} />}
    >
        {getJsx({ file: bank, className: style.bank__icon })}
        <div className={cn(style.panel__title, { [style.panel__title__notification]: store.needToShowNotification })}>
            Подключить банк
        </div>
        {renderCount()}
        <Arrow
            direction={store.opened ? ArrowDirection.Up : ArrowDirection.Down}
            className={style.arrow}
        />
    </CollapsiblePanel>;
}

BankIntegrationPanel.propTypes = {
    refreshSourceList: PropTypes.func
};

export default observer(BankIntegrationPanel);
