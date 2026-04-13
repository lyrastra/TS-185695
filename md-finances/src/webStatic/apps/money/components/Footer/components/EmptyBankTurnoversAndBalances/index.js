import React from 'react';
import TableFooter from '@moedelo/frontend-core-react/components/table/TableFooter';
import style from './style.m.less';

const EmptyBankTurnoversAndBalances = () => {
    return <TableFooter className={style.wrapper}>
        По данному расчетному счету нет данных за выбранный период
    </TableFooter>;
};

export default EmptyBankTurnoversAndBalances;
