import classnames from 'classnames/bind';
import React from 'react';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import IconButton from '@moedelo/frontend-core-react/components/IconButton/IconButton';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import directionPostings from '../../../../../../../../../../resources/newMoney/directionPostings';
import style from './style.m.less';

const cn = classnames.bind(style);

class Row extends React.Component {
    componentDidMount() {
        this.setDefaultValues();
    }

    componentDidUpdate() {
        this.setDefaultValues();
    }

    onChangeDirection = ({ value }) => {
        this.props.onChange({
            ...this.props.posting,
            Direction: value,
            Type: null,
            Kind: null,
            NormalizedCostType: null
        });
    };

    onChangeSum = ({ value }) => {
        this.props.onChange({ ...this.props.posting, Sum: value });
    };

    onChangeType = ({ value }) => {
        this.props.onChange({
            ...this.props.posting,
            Type: value,
            Kind: null,
            NormalizedCostType: null
        });
    };

    onChangeKind = ({ value }) => {
        this.props.onChange({
            ...this.props.posting,
            Kind: value,
            NormalizedCostType: null
        });
    };

    onChangeNormalizedCostType = ({ value }) => {
        this.props.onChange({ ...this.props.posting, NormalizedCostType: value });
    };

    onDelete = () => {
        this.props.onDelete(this.props.posting);
    };

    setDefaultValues = () => {
        const {
            transferType,
            transferKind,
            normalizedCostType,
            posting
        } = this.props;

        const {
            Type,
            Kind,
            NormalizedCostType
        } = posting;

        if (Type === null) {
            this.onChangeType({ value: transferType[0].value });
        }

        if (Kind === null) {
            this.onChangeKind({ value: transferKind[0].value });
        }

        if (NormalizedCostType === null) {
            this.onChangeNormalizedCostType({ value: normalizedCostType[0].value });
        }
    };

    render() {
        const {
            transferType,
            transferKind,
            normalizedCostType,
            posting
        } = this.props;

        const {
            Direction,
            Sum,
            SumError,
            Type,
            Kind,
            NormalizedCostType,
            LinkedDocument
        } = posting;

        const canEdit = !LinkedDocument && !this.props.readOnly;
        const sum = !Sum ? `` : toAmountString(Sum);

        if (Type === null || Kind === null || NormalizedCostType === null) {
            return null;
        }

        return <div className={cn(style.row, grid.row)}>
            <Dropdown
                className={grid.col_3}
                onSelect={this.onChangeDirection}
                data={directionPostings}
                value={Direction}
                type={`link`}
                showAsText={!canEdit || !this.props.canChangeDirection}
            />
            <div className={cn(style.sum, grid.col_3)}>
                <Input
                    value={sum}
                    decimalLimit={2}
                    allowDecimal
                    textAlign="right"
                    type={InputType.number}
                    onBlur={this.onChangeSum}
                    error={!!SumError}
                    message={SumError}
                    showAsText={!canEdit}
                />
                {sum && <React.Fragment>&nbsp;₽</React.Fragment>}
            </div>
            <Dropdown
                className={cn(grid.col_5, `taxPosting_type`)}
                onSelect={this.onChangeType}
                data={transferType}
                value={Type}
                type={`input`}
                showAsText={!canEdit || !(transferType.length > 1)}
                width={250}
            />
            <Dropdown
                className={cn(grid.col_5, `taxPosting_kind`)}
                onSelect={this.onChangeKind}
                data={transferKind}
                value={Kind}
                type={`input`}
                showAsText={!canEdit || !(transferKind.length > 1)}
                width={250}
            />
            <Dropdown
                className={cn(grid.col_11, `taxPosting_normalizedCostType`)}
                onSelect={this.onChangeNormalizedCostType}
                data={normalizedCostType}
                value={NormalizedCostType}
                type={`input`}
                showAsText={!canEdit || !(normalizedCostType.length > 1)}
                width={400}
            />
            <div className={cn(style.delete, grid.col_1)}>
                {!canEdit ||
                    <IconButton
                        onClick={this.onDelete}
                        size={`small`}
                        icon={`clear`}
                        className={style.deleteBtn}
                    />
                    }
            </div>
        </div>;
    }
}

Row.propTypes = {
    posting: PropTypes.object,
    readOnly: PropTypes.bool,
    onChange: PropTypes.func,
    onDelete: PropTypes.func,
    transferType: PropTypes.arrayOf(PropTypes.object),
    transferKind: PropTypes.arrayOf(PropTypes.object),
    normalizedCostType: PropTypes.arrayOf(PropTypes.object),
    canChangeDirection: PropTypes.bool
};

export default Row;
