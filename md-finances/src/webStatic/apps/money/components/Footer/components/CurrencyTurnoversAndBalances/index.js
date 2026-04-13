import React from 'react';
import classnames from 'classnames/bind';
import Sticky from 'react-sticky-el';
import style from './style.m.less';
import Table from './components/Table';
import Collapse from './components/Collapse';

const cn = classnames.bind(style);

const CurrencyTurnoversAndBalances = props => {
    return (
        <Sticky mode="bottom" stickyClassName={cn(`fixed`)} positionRecheckInterval={30} style={{ zIndex: 2 }}>
            <Collapse toggler={`Обороты и остатки`} togglerClass={cn(style.footerCollapseToggle)}>
                <div className={style.footerCollapseBody}>
                    <Table {...props} />
                </div>
            </Collapse>
        </Sticky>
    );
};

CurrencyTurnoversAndBalances.propTypes = Table.propTypes;

export default CurrencyTurnoversAndBalances;
