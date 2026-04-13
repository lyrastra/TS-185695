import React from 'react';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import classnames from 'classnames/bind';
import Input from '@moedelo/frontend-core-react/components/Input';
import InputType from '@moedelo/frontend-core-react/components/Input/enums/Type';
import IconButton from '@moedelo/frontend-core-react/components/IconButton/IconButton';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import directionPostings from '../../../../../../../../../../resources/newMoney/directionPostings';
import style from './style.m.less';

const cn = classnames.bind(style);

class Row extends React.Component {
    onChangeDirection = ({ value }) => {
        this.props.onChange({ ...this.props.posting, Direction: value });
    };

    onChangeSum = ({ value }) => {
        this.props.onChange({ ...this.props.posting, Sum: value });
    };

    onChangeDescription = ({ value }) => {
        this.props.onChange({ ...this.props.posting, Description: value });
    };

    onDelete = () => {
        this.props.onDelete(this.props.posting);
    };

    render() {
        const {
            posting,
            disableSum,
            disableDescription
        } = this.props;
        const {
            Direction,
            Sum,
            Description,
            DescriptionError,
            SumError,
            LinkedDocument
        } = posting;

        const canEdit = !LinkedDocument && !this.props.readOnly;
        const sum = toAmountString(Sum) || ``;

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
                    textAlign="right"
                    onBlur={this.onChangeSum}
                    error={!!SumError}
                    type={InputType.number}
                    message={SumError}
                    showAsText={!canEdit || disableSum}
                    allowDecimal
                    allowNegative
                />
                {sum && <React.Fragment>&nbsp;₽</React.Fragment>}
            </div>
            {!disableDescription && (
                <div className={cn(style.description, grid.col_18)}>
                    <Input
                        value={Description}
                        showAsText={!canEdit}
                        onBlur={this.onChangeDescription}
                        error={!!DescriptionError}
                        message={DescriptionError}
                    />
                </div>
            )}
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
    canChangeDirection: PropTypes.bool,
    onChange: PropTypes.func,
    onDelete: PropTypes.func,
    disableDescription: PropTypes.bool,
    disableSum: PropTypes.bool
};

export default Row;
