import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import TabGroup from '@moedelo/frontend-core-react/components/TabGroup';
import Year from '@moedelo/frontend-core-react/components/calendar/Year';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Month from './../Month';
import Period from '../../../../Period';
import Day from './../Day';
import date from '../../../../../../../helpers/date';
import style from './style.m.less';
import CommonPeriodTypeEnum from '../../../../../../../enums/newMoney/CommonPeriodTypeEnum';

const cn = classnames.bind(style);

class DateFilter extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;

        const { startDate, endDate } = this.store;

        this.store.periodType = definePeriodType(startDate, endDate);
    }

    onChangePeriod = ({ startDate, endDate }) => {
        this.store.setDate({ startDate, endDate });
    };

    onSelectPeriodType = ({ value }) => {
        this.store.setPeriod({ periodType: value });
    };

    onChangeYear = ({ year }) => {
        this.store.setDate({
            startDate: date.toString(date.firstDayOfYear(year)),
            endDate: date.toString(date.lastDayOfYear(year))
        });
    };

    onChangeMonth = ({ year, month, quarter }) => {
        const startDate = quarter ? date.firstDayOfQuarter(year, quarter) : new Date(year, month - 1, 1);
        const endDate = quarter ? date.lastDayOfQuarter(year, quarter) : date.lastDayOfMonth(new Date(year, month - 1, 1));

        this.store.setDate({
            startDate: date.toString(startDate),
            endDate: date.toString(endDate)
        });
    };

    onChangeDay = curDate => {
        this.store.setDate({ startDate: curDate, endDate: curDate });
    };

    dateFilter = () => {
        const today = new Date();
        const {
            minDate,
            periodType,
            valueByPeriod,
            validationMessage,
            oldDateMessage
        } = this.store;
        const min = minDate.getFullYear();
        const max = date.lastDayOfYear(today.getFullYear());

        switch (periodType) {
            case CommonPeriodTypeEnum.Year:
                return <div className={cn(`row`)}>
                    <div className={cn(`year`)}>
                        <Year
                            min={min}
                            max={today.getFullYear()}
                            value={valueByPeriod}
                            onChange={this.onChangeYear}
                            error={!!oldDateMessage}
                            message={oldDateMessage}
                        />
                    </div>
                </div>;
            case CommonPeriodTypeEnum.Month:
                return <Month
                    minDate={minDate}
                    maxDate={max}
                    value={valueByPeriod}
                    onChange={this.onChangeMonth}
                    errorMessage={oldDateMessage}
                />;

            case CommonPeriodTypeEnum.Period: {
                const errorMessage = {
                    ...validationMessage,
                    common: validationMessage.common || oldDateMessage
                };

                return <Period
                    value={valueByPeriod}
                    errorMessage={errorMessage}
                    onChange={this.onChangePeriod}
                    store={this.store}
                />;
            }

            case CommonPeriodTypeEnum.Day:
                return <Day
                    value={valueByPeriod}
                    errorMessage={validationMessage || oldDateMessage}
                    onChange={this.onChangeDay}
                    store={this.store}
                />;
            default:
                return null;
        }
    };

    tabs = [
        { text: `Все`, value: CommonPeriodTypeEnum.All },
        { text: `Год`, value: CommonPeriodTypeEnum.Year },
        { text: `Месяц`, value: CommonPeriodTypeEnum.Month },
        { text: `День`, value: CommonPeriodTypeEnum.Day },
        { text: `Период`, value: CommonPeriodTypeEnum.Period }
    ];

    render() {
        return <div className={cn(`dateFilterWrapper`, this.props.className)}>
            <div className={style.tabs}>
                <TabGroup
                    tabs={this.tabs}
                    currentTab={this.store.periodType}
                    onChange={this.onSelectPeriodType}
                />
            </div>
            {this.dateFilter()}
        </div>;
    }
}

DateFilter.propTypes = {
    className: PropTypes.string,
    store: PropTypes.object
};

export default observer(DateFilter);

function definePeriodType(startDate, endDate) {
    if (date.isEmpty(startDate) && date.isEmpty(endDate)) {
        return CommonPeriodTypeEnum.All;
    }

    if (date.isYear(startDate, endDate)) {
        return CommonPeriodTypeEnum.Year;
    }

    if (date.isQuarter(startDate, endDate) || date.isMonth(startDate, endDate)) {
        return CommonPeriodTypeEnum.Month;
    }

    if (dateHelper(startDate, `DD.MM.YYYY`).isSame(dateHelper(endDate, `DD.MM.YYYY`), `day`)) {
        return CommonPeriodTypeEnum.Day;
    }

    return CommonPeriodTypeEnum.Period;
}
