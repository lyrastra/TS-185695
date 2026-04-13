import React from 'react';
import PropTypes from 'prop-types';
import { toJS } from 'mobx';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import Link from '@moedelo/frontend-core-react/components/Link';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import PeriodRow from './PeriodRow';
import style from './style.m.less';
import Nds from '../Nds';

const cn = classnames.bind(style);

@observer
class Periods extends React.Component {
    onAddPeriod = () => {
        const list = this.getPeriods();
        list.push({
            Id: getUniqueId(),
            Sum: ``,
            Description: ``,
            PaymentType: null
        });
        this.updatePeriods(list);
    };

    onChangeItem = value => {
        const items = this.getPeriods().map(item => {
            if (item.Id === value.Id) {
                return { ...item, ...value };
            }

            return item;
        });
        this.updatePeriods(items);
    };

    onDeleteRow = value => {
        const items = this.getPeriods().filter(item => item.Id !== value.Id);
        this.updatePeriods(items);
    };

    getPeriods = () => {
        return toJS(this.props.operationStore.model.Periods) || [];
    };

    updatePeriods = list => {
        this.props.operationStore.setPeriods(list);
    };

    renderPeriodsList = () => {
        const periods = this.getPeriods();

        return periods.map(period => {
            return (
                <PeriodRow
                    key={period.Id}
                    value={period}
                    onChange={this.onChangeItem}
                    onDelete={this.onDeleteRow}
                />
            );
        });
    };

    renderPaymentElements = () => {
        const { operationStore } = this.props;
        const {
            model,
            canEdit,
            sumCurrencySymbol,
            validationState,
            setNdsType,
            setNdsSum,
            ndsTypes,
            setIncludeNds,
            hasNds
        } = operationStore;
        const totalSum = model.TotalSum > 0 ? toAmountString(model.TotalSum) : ``;

        return <React.Fragment>
            <div className={grid.col_3}>
                <div className={style.currency}>
                    <Input
                        value={totalSum}
                        decimalLimit={2}
                        allowDecimal
                        type={InputType.number}
                        textAlign={`left`}
                        label={`К оплате`}
                        error={!!validationState.TotalSum}
                        message={validationState.TotalSum}
                        unit={sumCurrencySymbol}
                        showAsText={!canEdit}
                        readOnly
                    />
                </div>
            </div>
            <div className={grid.col_11}>
                <div className={cn(grid.row, style.periodRow, style.additionalPeriodRow)}>
                    <Nds
                        NdsSum={model.NdsSum}
                        IncludeNds={model.IncludeNds}
                        setNdsType={setNdsType}
                        setNdsSum={setNdsSum}
                        ndsTypes={ndsTypes}
                        NdsType={model.NdsType}
                        setIncludeNds={setIncludeNds}
                        hasNds={hasNds}
                        canEdit={canEdit}
                        validationState={validationState?.NdsSum}
                        nds={model.NdsType}
                        switchTop={20}
                        switchClassName={grid.col_12}
                        ndsTypeClassName={grid.col_5}
                        ndsSumClassName={grid.col_7}
                        qaNdsSumClassName="qa-inputNdsSum"
                    />
                </div>
            </div>
        </React.Fragment>;
    };

    renderFooter = () => {
        const { operationStore } = this.props;
        const { model, canEdit } = operationStore;

        const showForPayment = model.Periods?.length > 1;

        return <div className={grid.row}>
            <div className={grid.col_9}>
                { canEdit && <Link
                    text={`+ Период`}
                    onClick={this.onAddPeriod}
                /> }
            </div>
            <div className={grid.col_1} />
            {showForPayment && this.renderPaymentElements() }
        </div>;
    };

    render() {
        return <React.Fragment>
            <div className={cn(style.periodWrapper, this.props.className)}>
                {this.renderPeriodsList()}
            </div>
            {this.renderFooter()}
        </React.Fragment>;
    }
}

Periods.propTypes = {
    className: PropTypes.string,
    operationStore: PropTypes.shape({
        model: PropTypes.object.isRequired,
        validationState: PropTypes.object.isRequired,
        setPeriods: PropTypes.func.isRequired,
        TaxationSystem: PropTypes.object.isRequired,
        setSum: PropTypes.func.isRequired,
        canEdit: PropTypes.boolean,
        sumCurrencySymbol: PropTypes.object,
        setNdsType: PropTypes.func,
        setNdsSum: PropTypes.func,
        ndsTypes: PropTypes.arrayOf(PropTypes.object),
        setIncludeNds: PropTypes.func,
        hasNds: PropTypes.bool
    })
};

export default Periods;
