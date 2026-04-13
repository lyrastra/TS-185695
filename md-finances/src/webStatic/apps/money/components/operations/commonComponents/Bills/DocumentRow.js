import React from 'react';
import PropTypes from 'prop-types';
import { inject } from 'mobx-react';
import classnames from 'classnames/bind';
import { toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
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
        const { canEdit } = this.props.operationStore;
        const document = mapLinkedDocumentsItem(this.props.value);

        return (
            <div className={cn(style.documentRow)}>
                <DocumentAutocomplete
                    className={cn(style.documentCol, style.documentNumber)}
                    value={document}
                    onChange={this.onChangeDocument}
                    showAsText={!canEdit}
                />
                <div className={cn(style.documentCol, style.sum)}>
                    <DocumentSum
                        onChange={this.onChangeSum}
                        textAlign="right"
                        value={document.Sum}
                        showAsText={!canEdit}
                        className={cn(style.currency)}
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

function mapLinkedDocumentsItem(doc) {
    return {
        ...doc,
        Sum: doc.Sum ? toAmountString(doc.Sum) : ``
    };
}

DocumentRow.propTypes = {
    value: PropTypes.object,
    onChange: PropTypes.func,
    onDelete: PropTypes.func,
    operationStore: PropTypes.object
};

export default DocumentRow;
