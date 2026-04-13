import React from 'react';
import classnames from 'classnames/bind';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import PropTypes from 'prop-types';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import style from './style.m.less';

const cn = classnames.bind(style);

class Day extends React.Component {
    onChangeDate = ({ value }) => {
        this.props.onChange(value);
    };

    render() {
        const {
            value, errorMessage, store
        } = this.props;

        return <div className={cn(`component`)}>
            <Input
                placeholder="Дата"
                error={!!errorMessage}
                message={errorMessage}
                value={value}
                type={InputType.date}
                onChange={this.onChangeDate}
                minDate={dateHelper(store.minDate).format(`DD.MM.YYYY`)}
            />
        </div>;
    }
}

Day.propTypes = {
    value: PropTypes.string,
    onChange: PropTypes.func.isRequired,
    errorMessage: PropTypes.string,
    store: PropTypes.object
};

export default Day;
