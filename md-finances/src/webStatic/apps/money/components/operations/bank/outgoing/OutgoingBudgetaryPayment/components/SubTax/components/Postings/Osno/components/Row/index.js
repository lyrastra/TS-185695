import classnames from 'classnames/bind';
import React from 'react';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import Dropdown, { Type as DropdownType } from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import Input, { TextAlign } from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import style from './style.m.less';

const cn = classnames.bind(style);

const Row = props => {
    const {
        transferType,
        transferKind,
        normalizedCostType,
        readOnly,
        posting
    } = props;
    const {
        Sum,
        SumError,
        Type,
        Kind,
        NormalizedCostType,
        LinkedDocument
    } = posting;

    const canEdit = !LinkedDocument && !readOnly;
    const sum = toFinanceString(Sum) || ``;

    const onChangeSum = ({ value }) => {
        const { onChange } = props;

        onChange({ ...posting, Sum: value });
    };

    const onChangeType = ({ value }) => {
        props.onChange({
            ...posting,
            Type: value,
            Kind: null,
            NormalizedCostType: null
        });
    };

    const onChangeKind = ({ value }) => {
        props.onChange({
            ...posting,
            Kind: value,
            NormalizedCostType: null
        });
    };

    const onChangeNormalizedCostType = ({ value }) => {
        props.onChange({ ...posting, NormalizedCostType: value });
    };

    const setDefaultValues = () => {
        if (Type === null) {
            onChangeType({ value: transferType[0].value });
        }

        if (Kind === null) {
            onChangeKind({ value: transferKind[0].value });
        }

        if (NormalizedCostType === null) {
            onChangeNormalizedCostType({ value: normalizedCostType[0].value });
        }
    };

    React.useEffect(() => setDefaultValues());

    if (Type === null || Kind === null || NormalizedCostType === null) {
        return null;
    }

    return <div className={cn(style.row, grid.row)}>
        <div className={cn(style.sum, grid.col_3)}>
            <Input
                label={`Расход в ОСНО`}
                value={sum}
                decimalLimit={2}
                allowDecimal
                textAlign={TextAlign.left}
                type={InputType.number}
                onBlur={onChangeSum}
                error={!!SumError}
                message={SumError}
                showAsText={!canEdit}
            />
            {sum && <React.Fragment>&nbsp;₽</React.Fragment>}
        </div>
        <div className={grid.col_1} />
        <Dropdown
            className={cn(grid.col_6, `taxPosting_type`)}
            onSelect={onChangeType}
            label={`Тип`}
            data={transferType}
            value={Type}
            type={DropdownType.input}
            showAsText={!canEdit || !(transferType.length > 1)}
            width={250}
        />
        <div className={grid.col_1} />
        <Dropdown
            className={cn(grid.col_5, `taxPosting_kind`)}
            onSelect={onChangeKind}
            label={`Вид`}
            data={transferKind}
            value={Kind}
            type={DropdownType.input}
            showAsText={!canEdit || !(transferKind.length > 1)}
            width={250}
        />
        <Dropdown
            className={cn(grid.col_9, `taxPosting_normalizedCostType`)}
            onSelect={onChangeNormalizedCostType}
            label={`Нормируемый`}
            data={normalizedCostType}
            value={NormalizedCostType}
            type={DropdownType.input}
            showAsText={!canEdit || !(normalizedCostType.length > 1)}
            width={400}
        />
        <div className={grid.col_1} />
    </div>;
};

Row.propTypes = {
    posting: PropTypes.object,
    readOnly: PropTypes.bool,
    onChange: PropTypes.func,
    transferType: PropTypes.arrayOf(PropTypes.object),
    transferKind: PropTypes.arrayOf(PropTypes.object),
    normalizedCostType: PropTypes.arrayOf(PropTypes.object)
};

export default Row;
