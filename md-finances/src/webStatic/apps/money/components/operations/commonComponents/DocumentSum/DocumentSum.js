import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import style from './style.m.less';

const cn = classnames.bind(style);

const DocumentSum = ({
    value, label, error, onChange, textAlign, showAsText, className, unit
}) => {
    if (showAsText) {
        if (!value) {
            return null;
        }

        return <div className={cn(style.noWrap)}>
            <Input
                value={value}
                label={label}
                type={InputType.number}
                unit={unit}
                showAsText
                decimalLimit={2}
                allowDecimal
            />
        </div>;
    }

    return <div className={className}>
        <Input
            value={value}
            onBlur={onChange}
            decimalLimit={2}
            allowDecimal
            type={InputType.number}
            textAlign={textAlign}
            label={label}
            error={!!error}
            message={error}
            unit={unit}
        />
    </div>;
};

DocumentSum.defaultProps = {
    textAlign: `left`,
    unit: `₽`
};

DocumentSum.propTypes = {
    value: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
    label: PropTypes.string,
    onChange: PropTypes.func,
    error: PropTypes.string,
    textAlign: PropTypes.string,
    showAsText: PropTypes.bool,
    className: PropTypes.string,
    unit: PropTypes.string
};

export default DocumentSum;
