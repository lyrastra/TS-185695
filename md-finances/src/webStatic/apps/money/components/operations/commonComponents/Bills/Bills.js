import React from 'react';
import PropTypes from 'prop-types';
import { toJS } from 'mobx';
import { observer, inject } from 'mobx-react';
import classnames from 'classnames/bind';
import Link from '@moedelo/frontend-core-react/components/Link';
import DocumentRow from './DocumentRow';
import style from './style.m.less';

const cn = classnames.bind(style);

@inject(`operationStore`)
@observer
class Bills extends React.Component {
    onAddDocument = () => {
        const list = this.getBills();
        list.push({});
        this.props.operationStore.setBills(list);
    };

    onChangeItem = (value) => {
        const items = this.getBills().map(item => {
            if (item.key === value.key) {
                return { ...item, ...value };
            }

            return item;
        });
        this.props.operationStore.setBills(items);
    };

    onDeleteRow = (value) => {
        const items = this.getBills().filter(item => item.key !== value.key);
        this.props.operationStore.setBills(items);
    };

    getBills = () => {
        return toJS(this.props.operationStore.model.Bills) || [];
    };

    renderHeader = () => {
        if (!this.getBills().length) {
            return null;
        }

        return (
            <div className={cn(style.documentsHead)}>
                <div className={cn(style.documentCol, style.documentNumber)}>По счету</div>
                <div className={cn(style.documentCol, style.sum)}>Сумма</div>
                <div className={cn(style.documentCol, style.remove)} />
            </div>
        );
    };

    renderDocumentsList = () => {
        const documents = this.getBills();

        if (!documents.length) {
            return null;
        }

        return (
            <div className={cn(style.documentsList)}>
                {documents.map((document) => {
                    return (
                        <DocumentRow
                            key={document.key}
                            value={document}
                            onChange={this.onChangeItem}
                            onDelete={this.onDeleteRow}
                        />
                    );
                })}
            </div>
        );
    };

    render() {
        const { canEdit, canAddBill } = this.props.operationStore;

        if (!canEdit && this.getBills().length === 0) {
            return null;
        }

        return (
            <div className={cn(style.documentsWrapper, this.props.className)}>
                {this.renderHeader()}
                {this.renderDocumentsList()}
                { this.props.operationStore.validationState.BillsSum &&
                    <div className={cn(style.error)}>
                        { this.props.operationStore.validationState.BillsSum }
                    </div>
                }
                {canEdit && canAddBill && <div className={cn(style.documentsControls)}>
                    <Link
                        text={`+ Cчет`}
                        onClick={this.onAddDocument}
                    />
                </div>}
            </div>
        );
    }
}

Bills.propTypes = {
    className: PropTypes.string,
    operationStore: PropTypes.object
};

export default Bills;
