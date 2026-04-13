import React from 'react';
import classnames from 'classnames/bind';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import remove from '@moedelo/frontend-core-react/icons/remove.m.svg';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Period from '../Period';
import TradingObjectDropdown from '../TradingObjectDropdown';
import PatentDropdown from '../../../../../commonComponents/PatentDropdown';
import TaxesAndFeesTypeDropdown from '../TaxesAndFeesTypeDropdown';
import Kbk from '../Kbk';
import GapLine from '../GapLine';
import commonStyles from '../../commonStyles.m.less';
import Postings from './components/Postings';
import CurrencyInvoices from '../CurrencyInvoices';
import Sum from './components/Sum';

import style from './style.m.less';

const cn = classnames.bind({ style, commonStyles });

const SubTax = observer(props => {
    const {
        store, onDelete, index, isLast
    } = props;
    const { patentSelectVisible } = store;

    const getExtraRows = () => {
        return <React.Fragment>
            <TradingObjectDropdown operationStore={store} />
            {patentSelectVisible && <div className={grid.row}>
                <PatentDropdown className={grid.col_9} operationStore={store} />
            </div>}
        </React.Fragment>;
    };

    return <div className={style.subTaxContainer}>
        <div className={cn(grid.row, style.topRow)}>
            <div className={cn(grid.col_9, style.withTooltip)}>
                <div className={style.flexGrow}>
                    <TaxesAndFeesTypeDropdown operationStore={store} />
                </div>
                <Tooltip
                    width={320}
                    position={Position.top}
                    content={`Выберите вид налога или взноса, который уплатили в бюджет. Например, если платили налог УСН, то нужно выбрать из списка "Единый налог при применении упрощенной системы налогообложения".`}
                />
            </div>
            <div className={grid.col_1} />
            <div className={style.periodWrapper}>
                <Period
                    tooltipText={`Выберите верный период, это важно для расчета налогов и взносов в сервисе. Например, взносы за сотрудников и НДФЛ платят ежемесячно, для них период "МС", для квартальных налогов выбирают "КВ", для годового налога или фиксированных взносов за ИП период "ГД"`}
                    label={`Период`}
                    operationStore={store}
                    isInline
                />
            </div>
        </div>
        {getExtraRows()}
        <div className={grid.row}>
            <div className={cn(grid.col_9, style.withTooltip)}>
                <div className={style.flexGrow}>
                    <Kbk label={`КБК`} operationStore={store} />
                </div>
                <Tooltip
                    width={320}
                    position={Position.top}
                    content={`Выберите КБК налога, взноса, штрафа,пени. Например, если платили налог УСН, то нужно выбрать КБК соответствующий вашему объекту, доходы или доходы минус расходы.`}
                />
            </div>
            <div className={grid.col_1} />
            <div className={grid.col_3}>
                <Sum operationStore={store} label={`Сумма`} />
            </div>
        </div>
        <CurrencyInvoices store={store} />
        <Postings operationStore={store} />
        {index > 0 && <IconButton
            className={style.subTaxDeleteButton}
            icon={remove}
            onClick={onDelete}
        />}
        {!isLast && <GapLine left={-40} bottom={-10} />}
    </div>;
});

SubTax.propTypes = {
    store: PropTypes.object.isRequired,
    index: PropTypes.number,
    onDelete: PropTypes.func,
    isLast: PropTypes.bool
};

export default SubTax;
