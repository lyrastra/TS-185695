import React from 'react';
import { inject } from 'mobx-react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import DocumentAutocomplete from './DocumentAutocomplete';
import DocumentSum from './../DocumentSum';

import style from './style.m.less';

const cn = classnames.bind(style);

@inject(`operationStore`)
class DocumentRow extends React.Component {
    onChangeDocument = ({ value }) => {
        this.props.onChange({ ...this.props.value, ...value, Sum: `` });
    };

    onChangeSum = ({ value }) => {
        this.props.onChange({ ...this.props.value, Sum: value });
    };

    onClickRemove = () => {
        this.props.onDelete(this.props.value);
    };

    render() {
        const { canEdit, sumCurrencySymbol } = this.props.operationStore;
        const document = this.props.value;

        return (
            <div className={cn(style.documentRow)}>
                <DocumentAutocomplete
                    className={cn(style.documentCol, style.documentNumber)}
                    value={document}
                    onChange={this.onChangeDocument}
                    showAsText={!canEdit}
                    autocomplete={this.props.autocomplete}
                />
                <div className={cn(style.documentCol, style.documentDate)}>{document.Date}</div>
                <div className={cn(style.documentCol, style.documentSum)}>
                    <DocumentSum
                        showAsText
                        value={document.DocumentSum}
                        className={cn(style.currency)}
                        unit={sumCurrencySymbol}
                    />
                </div>
                <div className={cn(style.documentCol, style.documentPaid)}>
                    <DocumentSum
                        showAsText
                        value={document.PaidSum}
                        className={cn(style.currency)}
                        unit={sumCurrencySymbol}
                    />
                </div>
                <div className={cn(style.documentCol, style.documentToPay)}>
                    <DocumentSum
                        onChange={this.onChangeSum}
                        textAlign="right"
                        value={document.Sum}
                        showAsText={!canEdit}
                        className={cn(style.currency)}
                        unit={sumCurrencySymbol}
                    />
                </div>
                <div className={cn(style.documentCol, style.remove)} >
                    {canEdit &&
                    <IconButton
                        onClick={this.onClickRemove}
                        size={`small`}
                        icon={`clear`}
                    />
                    }
                </div>
            </div>
        );
    }
}

DocumentRow.propTypes = {
    value: PropTypes.object,
    onChange: PropTypes.func,
    onDelete: PropTypes.func,
    operationStore: PropTypes.object,
    autocomplete: PropTypes.func.isRequired
};

export default DocumentRow;
