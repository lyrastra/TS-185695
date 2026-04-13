import React from 'react';
import classnames from 'classnames/bind';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import PropTypes from 'prop-types';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import style from './style.m.less';
import date from '../../../../../../../helpers/date';

const cn = classnames.bind(style);

class Month extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            year: this.props.value.year
        };
    }

    dateFormat = `DD.MM.YYYY`;

    quarter(quarterNumber) {
        const isQuarterSelected = this.props.value.quarter === quarterNumber && this.props.value.year === this.state.year;

        const className = {
            quarter: true,
            active: isQuarterSelected,
            disabled: this.quarterDisabled(quarterNumber),
            'qa-quarter': true,
            'qa-selectedQuarter': isQuarterSelected
        };

        return <div role="button" tabIndex={-1} onKeyDown={() => {}} className={cn(className)} onClick={() => this.selectQuarter(quarterNumber)}>
            <span>{quarterNumber} кв</span>
        </div>;
    }

    month(monthNumber) {
        const months = [`янв`, `фев`, `мар`, `апр`, `май`, `июн`, `июл`, `авг`, `сен`, `окт`, `ноя`, `дек`];
        const quarterNumber = Math.ceil(monthNumber / 3);
        const active = (this.props.value.quarter === quarterNumber || this.props.value.month === monthNumber) && this.props.value.year === this.state.year;
        const className = {
            month: true,
            active,
            disabled: this.monthDisabled(monthNumber),
            'qa-month': true,
            'qa-selectedMonth': active
        };

        return <div role="button" tabIndex={-1} onKeyDown={() => {}} className={cn(className)} onClick={() => this.selectMonth(monthNumber)}>{months[monthNumber - 1]}</div>;
    }

    selectQuarter = (quarter) => {
        if (this.quarterDisabled(quarter)) {
            return;
        }

        this.props.onChange({
            quarter,
            year: this.state.year
        });
    }

    quarterDisabled = (quarter) => {
        return dateHelper(date.lastDayOfQuarter(this.state.year, quarter)).isBefore(dateHelper(this.props.minDate, `DD.MM.YYYY`));
    }

    monthDisabled = (month) => {
        return dateHelper(new Date(this.state.year, month, 0)).isBefore(dateHelper(this.props.minDate, `DD.MM.YYYY`));
    }

    selectMonth = (month) => {
        if (this.monthDisabled(month)) {
            return;
        }

        this.props.onChange({
            month,
            year: this.state.year
        });
    }

    prevYear = () => {
        const year = this.state.year - 1;
        const min = this.props.minDate.getFullYear();

        if (min > year) {
            return;
        }

        this.setState({ year });
    }

    nextYear = () => {
        const year = this.state.year + 1;
        const max = this.props.maxDate.getFullYear();

        if (max < year) {
            return;
        }

        this.setState({ year });
    }

    render() {
        const minYear = this.props.minDate.getFullYear();
        const maxYear = this.props.maxDate.getFullYear();

        return <div className={style.component}>
            <div className={style.period}>
                <span role="button" tabIndex={-1} onKeyDown={() => {}} onClick={this.prevYear}>
                    {svgIconHelper.getJsx({ name: `dropdownArrow`, className: cn({ left: true, disabled: minYear >= this.state.year }) })}
                </span>
                <span className={cn(`label`, `qa-selectedYear`)}>{this.state.year}</span>
                <span role="button" tabIndex={-1} onKeyDown={() => {}} onClick={this.nextYear}>
                    {svgIconHelper.getJsx({ name: `dropdownArrow`, className: cn({ right: true, disabled: maxYear <= this.state.year }) })}
                </span>
            </div>
            <div className={style.calendar}>
                {this.quarter(1)}
                {this.month(1)}
                {this.month(2)}
                {this.month(3)}
                {this.quarter(2)}
                {this.month(4)}
                {this.month(5)}
                {this.month(6)}
                {this.quarter(3)}
                {this.month(7)}
                {this.month(8)}
                {this.month(9)}
                {this.quarter(4)}
                {this.month(10)}
                {this.month(11)}
                {this.month(12)}
            </div>
            <div className={style.errorMessage}>{this.props.errorMessage}</div>
        </div>;
    }
}

Month.defaultProps = {
    value: { year: dateHelper().year() }
};

Month.propTypes = {
    value: PropTypes.objectOf(PropTypes.number),
    minDate: PropTypes.instanceOf(Date),
    maxDate: PropTypes.instanceOf(Date),
    onChange: PropTypes.func,
    errorMessage: PropTypes.string
};

export default Month;
