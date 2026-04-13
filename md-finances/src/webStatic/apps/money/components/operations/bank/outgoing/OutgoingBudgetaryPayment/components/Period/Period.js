import React, { Fragment } from 'react';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import * as PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import Tooltip, { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import budgetaryDateHelper from '../../../../../../../../../helpers/newMoney/budgetaryPayment/budgetaryDateHelper';
import commonStyle from '../../commonStyles.m.less';
import CalendarTypesEnum from '../../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryCalendarTypesEnum';

const commonCn = classnames.bind(commonStyle);
const defaultTooltipText = `Укажите период, за который уплачиваются налог (сбор), пени, штрафы`;

@observer
class Period extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.operationStore;
    }

    renderSingleCalendar = () => {
        return (
            <Input
                label={`Период (107)`}
                value={this.store.model.Period.Date}
                onChange={this.store.setPeriodDate}
                minDate={`01.01.2011`}
                type={InputType.date}
                showAsText={!this.store.canEdit}
                error={!!this.store.validationState.Period}
                message={this.store.validationState.Period}
            />
        );
    }

    renderTripleCalendar = () => {
        const { Type, Year } = this.store.model.Period;
        const { isInline, label, tooltipText } = this.props;
        const {
            canShowSecondAutocomplete,
            canShowYearAutocomplete,
            getDataForSecondAutocomplete,
            getValueForSecondAutocomplete,
            setPeriodCalendarType,
            setSecondAutocompleteValue,
            setPeriodYear,
            canEdit
        } = this.store;
        const columnGrid = isInline ? grid.col_6 : grid.col_4;
        const smallColumnGrid = isInline ? grid.col_3 : grid.col_2;

        return (
            <Fragment>
                <div className={commonCn(columnGrid, commonStyle.paddingRight20)}>
                    <Dropdown
                        label={label}
                        value={Type}
                        data={budgetaryDateHelper.getCalendarTypesForDropdown()}
                        onSelect={setPeriodCalendarType}
                        showAsText={!canEdit}
                        className={`periodType`}
                        allowEmpty
                    />
                </div>
                {canShowSecondAutocomplete && <div className={commonCn(columnGrid, commonStyle.paddingRight20)}>
                    <Dropdown
                        value={getValueForSecondAutocomplete}
                        data={getDataForSecondAutocomplete}
                        onSelect={setSecondAutocompleteValue}
                        showAsText={!canEdit}
                        className={`periodSecond`}
                        allowEmpty
                    />
                </div>}
                {canShowYearAutocomplete && <div className={smallColumnGrid}>
                    <Dropdown
                        value={Year}
                        data={this.store.yearsList}
                        onSelect={setPeriodYear}
                        showAsText={!canEdit}
                        className={`periodYear`}
                        allowEmpty
                    />
                </div>}
                <Tooltip
                    width={320}
                    position={Position.top}
                    content={tooltipText || defaultTooltipText}
                />
            </Fragment>
        );
    }

    renderPeriod = () => {
        if (!this.store.model.Period) {
            return <Loader height={48} />;
        }

        return this.store.model.Period.Type === CalendarTypesEnum.Date ? this.renderSingleCalendar() : this.renderTripleCalendar();
    }

    render() {
        return (
            <div className={commonCn(grid.row, commonStyle.endItems)} >
                {this.renderPeriod()}
            </div>
        );
    }
}

Period.defaultProps = {
    isInline: false,
    label: `Период (107)`,
    tooltipText: ``
};

Period.propTypes = {
    operationStore: PropTypes.object,
    isInline: PropTypes.bool,
    label: PropTypes.string,
    tooltipText: PropTypes.oneOfType([PropTypes.string, PropTypes.node])
};

export default Period;
