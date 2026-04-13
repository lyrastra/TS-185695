import React from 'react';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import classnames from 'classnames/bind';
import Input, { TextAlign } from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import style from './style.m.less';

const cn = classnames.bind(style);

const Row = props => {
    const {
        posting,
        disableSum,
        disableDescription,
        readOnly,
        sumLabel
    } = props;
    const {
        Sum,
        Description,
        DescriptionError,
        SumError,
        LinkedDocument
    } = posting;

    const canEdit = !LinkedDocument && !readOnly;
    const sum = toFinanceString(Sum) || ``;

    const onChangeSum = ({ value }) => {
        props.onChange({ ...posting, Sum: value });
    };

    const onChangeDescription = ({ value }) => {
        props.onChange({ ...posting, Description: value });
    };

    return <div className={cn(style.row, grid.row)}>
        <div className={cn(style.sum, grid.col_3)}>
            <Input
                label={sumLabel}
                value={sum}
                decimalLimit={2}
                textAlign={TextAlign.left}
                onBlur={onChangeSum}
                error={!!SumError}
                type={InputType.number}
                message={SumError}
                showAsText={!canEdit || disableSum}
                allowDecimal
                allowNegative
            />
            {sum && <React.Fragment>&nbsp;₽</React.Fragment>}
        </div>
        <div className={grid.col_1} />
        {!disableDescription && (
            <div className={cn(style.description, grid.col_18)}>
                <Input
                    label={`Комментарий`}
                    value={Description}
                    showAsText={!canEdit}
                    onChange={onChangeDescription}
                    error={!!DescriptionError}
                    message={DescriptionError}
                />
            </div>
        )}
    </div>;
};

Row.propTypes = {
    posting: PropTypes.object,
    readOnly: PropTypes.bool,
    onChange: PropTypes.func,
    disableDescription: PropTypes.bool,
    disableSum: PropTypes.bool,
    sumLabel: PropTypes.string
};

export default Row;
