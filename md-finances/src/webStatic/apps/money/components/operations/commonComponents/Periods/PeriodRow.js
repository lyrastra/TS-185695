import React from 'react';
import { observer, inject } from 'mobx-react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import PeriodAutocomplete from './PeriodAutocomplete';
import style from './style.m.less';
import Nds from '../Nds';

const cn = classnames.bind(style);

@inject(`operationStore`)
@observer
class PeriodRow extends React.Component {
    onChangePeriod = ({ model }) => {
        this.props.operationStore.setPeriod(this.props.value, model);
        this.props.onChange(model);
    };

    onChangeSum = ({ value }) => {
        const { operationStore } = this.props;
        operationStore.setSum({ sum: value, period: this.props.value });
    };

    onClickRemove = () => {
        this.props.onDelete(this.props.value);
    };

    getErrorMessage = () => {
        const { operationStore, value } = this.props;
        const { validationState, model } = operationStore;

        if (validationState.SumPeriod && value.Sum <= 0) {
            return validationState.SumPeriod;
        }

        if (validationState.DefaultSumPeriod && value.Sum > value.DefaultSum) {
            // если ОС создано в Остатках и изменяем первый период, тогда игнорим проверку
            if (model.FixedAsset?.IsRentRemains && value.Id === model.FirstPeriodId) {
                return ``;
            }

            return validationState.DefaultSumPeriod;
        }

        return ``;
    };

    render() {
        const { operationStore, value } = this.props;
        const {
            canEdit, model, sumCurrencySymbol, setNdsType, setNdsSum, ndsTypes, setIncludeNds, hasNds, validationState
        } = operationStore;

        const periods = model.Periods;
        const showNds = periods?.length <= 1;
        const errMsg = this.getErrorMessage();
        const sumStr = value?.Sum > 0 ? toAmountString(value.Sum) : ``;

        return (
            <div className={cn(grid.row)}>
                <div className={grid.col_9}>
                    <PeriodAutocomplete
                        value={value}
                        onChange={this.onChangePeriod}
                        showAsText={!canEdit}
                    />
                </div>
                <div className={grid.col_1} />
                <div className={grid.col_3}>
                    <Input
                        readOnly={!value || value.Description === ``}
                        onBlur={this.onChangeSum}
                        label={`Сумма`}
                        value={sumStr}
                        decimalLimit={2}
                        allowDecimal
                        type={InputType.number}
                        textAlign={`left`}
                        error={errMsg.length !== 0}
                        message={errMsg}
                        showAsText={!canEdit}
                        unit={sumCurrencySymbol}
                        className={cn(style.currency)}
                    />
                </div>
                {showNds &&
                    <div className={grid.col_11}>
                        <div className={cn(grid.row, style.periodRow)}>
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
                                ndsSumClassName={cn(grid.col_7)}
                                qaNdsSumClassName="qa-inputNdsSum"
                            />
                        </div>
                    </div>
                }
                {canEdit && !showNds &&
                <div className={grid.col_1} >
                    <IconButton
                        onClick={this.onClickRemove}
                        size={`small`}
                        icon={`clear`}
                        className={style.removeBtn}
                    />
                </div>
                }
            </div>
        );
    }
}

PeriodRow.propTypes = {
    value: PropTypes.object,
    onChange: PropTypes.func,
    onDelete: PropTypes.func,
    operationStore: PropTypes.object
};

export default PeriodRow;
