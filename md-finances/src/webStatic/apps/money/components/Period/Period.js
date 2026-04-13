import React from 'react';
import classnames from 'classnames/bind';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import PropTypes from 'prop-types';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

class Period extends React.Component {
    onChangeStartDate = ({ value }) => {
        this.onChange(value, this.props.value.endDate);
    }

    onChangeEndDate = ({ value }) => {
        this.onChange(this.props.value.startDate, value);
    }

    onChange(startDate, endDate) {
        this.props.onChange({ startDate, endDate });
    }

    getCommonErrorMessage() {
        const { errorMessage = {} } = this.props;

        if (errorMessage.common) {
            return <div className={cn(`errorMessage`)}>{errorMessage.common}</div>;
        }

        return null;
    }

    render() {
        const { value, errorMessage = {}, store } = this.props;
        const { startDate, endDate } = value;
        const startDateErrorMessage = errorMessage.startDate;
        const endDateErrorMessage = errorMessage.endDate;
        const commonErrorMessage = errorMessage.common;

        return <div className={cn(`component`, this.props.className)}>
            <div className={cn(`inputSection`)}>
                <Input
                    placeholder="Начало"
                    value={startDate}
                    type={InputType.date}
                    error={!!startDateErrorMessage || !!commonErrorMessage}
                    message={startDateErrorMessage}
                    onChange={this.onChangeStartDate}
                    minDate={store && dateHelper(store.minDate).format(`DD.MM.YYYY`)}
                />
                <div className={cn(`separator`)}>–</div>
                <Input
                    placeholder="Конец"
                    value={endDate}
                    error={!!endDateErrorMessage || !!commonErrorMessage}
                    message={endDateErrorMessage}
                    type={InputType.date}
                    calendarAlignment="right"
                    onChange={this.onChangeEndDate}
                    minDate={store && dateHelper(store.minDate).format(`DD.MM.YYYY`)}
                />
            </div>
            {this.getCommonErrorMessage()}
        </div>;
    }
}

Period.defaultProps = {
    value: {}
};

Period.propTypes = {
    className: PropTypes.string,
    value: PropTypes.objectOf(PropTypes.string),
    onChange: PropTypes.func,
    errorMessage: PropTypes.object,
    store: PropTypes.object
};

export default Period;
