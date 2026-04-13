import React from 'react';
import PropTypes from 'prop-types';
import { toJS } from 'mobx';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import Link from '@moedelo/frontend-core-react/components/Link';
import DocumentRow from './DocumentRow';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class Documents extends React.Component {
    onAddDocument = () => {
        const list = this.getDocuments();
        list.push({});
        this.updateDocuments(list);
    };

    onChangeItem = value => {
        const items = this.getDocuments().map(item => {
            if (item.key === value.key) {
                return { ...item, ...value };
            }

            return item;
        });
        this.updateDocuments(items);
    };

    onDeleteRow = value => {
        const items = this.getDocuments().filter(item => item.key !== value.key);
        this.updateDocuments(items);
    };

    getDocuments = () => {
        return toJS(this.props.operationStore.model.Documents) || [];
    };

    updateDocuments = list => {
        this.props.operationStore.setDocuments(list);
        this.props.operationStore.switchOffAutoSetDocuments();
    };

    renderDocuments = () => {
        const { canEdit, canAddDocument } = this.props.operationStore;

        if (!canEdit && this.getDocuments().length === 0) {
            return null;
        }

        return (
            <div className={cn(style.documentsWrapper, this.props.className)}>
                {this.renderHeader()}
                {this.renderDocumentsList()}
                {this.renderErrors()}
                {canEdit && canAddDocument && <div className={cn(style.documentsControls)}>
                    <Link text={`+ Документ`} onClick={this.onAddDocument} />
                </div> }
            </div>
        );
    };

    renderHeader = () => {
        if (!this.getDocuments().length) {
            return null;
        }

        return (
            <div className={cn(style.documentsHead)}>
                <div className={cn(style.documentCol, style.documentNumber)}>Документ</div>
                <div className={cn(style.documentCol, style.documentDate)}>Дата</div>
                <div className={cn(style.documentCol, style.documentSum)}>Сумма</div>
                <div className={cn(style.documentCol, style.documentPaid)}>Уже оплачено</div>
                <div className={cn(style.documentCol, style.documentToPay)}>К оплате</div>
                <div className={cn(style.documentCol, style.remove)} />
            </div>
        );
    };

    renderDocumentsList = () => {
        const documents = this.getDocuments();

        if (!documents.length) {
            return null;
        }

        return (
            <div className={cn(style.documentsList)}>
                {documents.map(document => {
                    return (
                        <DocumentRow
                            key={document.key}
                            value={document}
                            onChange={this.onChangeItem}
                            onDelete={this.onDeleteRow}
                            autocomplete={this.props.autocomplete}
                        />
                    );
                })}
            </div>
        );
    };

    renderErrors = () => {
        const { DocumentsSum, DocumentsCount } = this.props.operationStore.validationState;
        const errors = [DocumentsSum, DocumentsCount].filter(errorString => errorString?.length);

        if (!errors.length) {
            return null;
        }

        return errors.map(errorString => (
            <div className={cn(style.error)}>
                {errorString}
            </div>
        ));
    }

    render() {
        return (
            <React.Fragment>
                { this.renderDocuments() }
            </React.Fragment>
        );
    }
}

Documents.propTypes = {
    className: PropTypes.string,
    operationStore: PropTypes.shape({
        model: PropTypes.object.isRequired,
        validationState: PropTypes.object.isRequired,
        setDocuments: PropTypes.func.isRequired,
        switchOffAutoSetDocuments: PropTypes.func.isRequired,
        canEdit: PropTypes.bool.isRequired,
        canAddDocument: PropTypes.bool.isRequired
    }),
    autocomplete: PropTypes.func.isRequired
};

export default Documents;
