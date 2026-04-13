import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { Transition } from 'react-transition-group';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import DropdownList from '@moedelo/frontend-core-react/components/dropdown/DropdownList';
import { getUrlWithId } from '@moedelo/frontend-core-v2/helpers/companyId';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import DocumentTypeEnum from '../../../../../../../../enums/DocumentTypeEnum';
import EditDocumentUrlsEnum from '../../../../../../../../enums/EditDocumentUrlsEnum';
import EditOperationUrlsEnum from '../../../../../../../../enums/EditOperationUrlsEnum';
import style from './style.m.less';
import OperationTablesService from '../../../../../../../../services/newMoney/operationTablesService';
import Store from '../../../../../../stores/TableStore';
import storage from '../../../../../../../../helpers/newMoney/storage';

const cn = classnames.bind(style);
const transitionDuration = 275;
const timeout = {
    enter: transitionDuration,
    exit: transitionDuration
};

let currentOperationId = null;
let requestWasNotSending = true;

class RelatedDocs extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            visible: false,
            loading: false,
            isRequestLoading: false
        };

        this.store = new Store();
    }

    getDocumentName = operationType => {
        switch (operationType) {
            case DocumentTypeEnum.PaymentOrder:
                return `Платежное поручение`;
            case DocumentTypeEnum.IncomingCashOrder:
                return `ПКО`;
            case DocumentTypeEnum.OutcomingCashOrder:
                return `РКО`;
            case DocumentTypeEnum.Bill:
                return `Счет`;
            case DocumentTypeEnum.Statement:
                return `Акт`;
            case DocumentTypeEnum.Waybill:
                return `Накладная`;
            case DocumentTypeEnum.Invoice:
                return `Счет-фактура`;
            case DocumentTypeEnum.RetailReport:
                return `Отчет о розничной продаже`;
            case DocumentTypeEnum.MiddlemanReport:
                return `Отчет посредника`;
            case DocumentTypeEnum.AccountingAdvanceStatement:
                return `Авансовый отчет`;
            case DocumentTypeEnum.Upd:
            case DocumentTypeEnum.SalesUpd:
                return `УПД`;
            case DocumentTypeEnum.Contract:
                return `Договор`;
            case DocumentTypeEnum.InventoryCard:
                return `Инвентарная карточка ОС`;
            default:
                return `Документ`;
        }
    }

    getRelatedDocsDropdown = () => {
        const { loading } = this.state;
        const relatedDocsArray = this.store.relatedDocsArray.slice();

        if (loading) {
            return <div className={cn(`relatedDocs__dropdown__loader`)}>
                <Loader className={cn(`relatedDocs__loader`)} />
            </div>;
        }

        const data = [
            <div>
                {relatedDocsArray.map(document => {
                    const url = this.getRelatedDocUrl(document);

                    return (
                        <a key={getUniqueId(`relDoc_${document.value.id}_`)} href={url} onClick={() => this.clickOperation(url)} className={cn(`relatedDocs__item`)}>
                            <div className={cn(`relatedDocs__title`)}>{document.text}</div>
                            <div className={cn(`relatedDocs__date`)}>{document.description}</div>
                        </a>
                    );
                })}
            </div>
        ];

        return <Transition in={this.state.visible} timeout={timeout}>
            {
                status => {
                    if (status === `exited`) {
                        return null;
                    }

                    return <DropdownList
                        data={data}
                        onClick={() => {}}
                        className={cn(`relatedDocs__dropdown`, `fade`, `fade-${status}`)}
                    />;
                }
            }

        </Transition>;
    }

    getRelatedDocUrl = operation => {
        const { operationDirection } = this.props;
        const { id, type } = operation.value;
        const salesUrl = `/AccDocuments/Sales#documents`;
        const buyUrl = `/AccDocuments/Buy#documents`;

        let finansesUrlRoot = `/Finances#edit`;
        let documentsUrlRoot = operationDirection === Direction.Outgoing
            ? buyUrl
            : salesUrl;

        switch (type) {
            case DocumentTypeEnum.PaymentOrder: {
                finansesUrlRoot += `${EditOperationUrlsEnum.PaymentOrder}/${id}`;

                return getUrlWithId(finansesUrlRoot);
            }

            case DocumentTypeEnum.IncomingCashOrder: {
                finansesUrlRoot += `${EditOperationUrlsEnum.CashOrder}/${id}`;

                return getUrlWithId(finansesUrlRoot);
            }

            case DocumentTypeEnum.OutcomingCashOrder: {
                finansesUrlRoot += `${EditOperationUrlsEnum.CashOrder}/${id}`;

                return getUrlWithId(finansesUrlRoot);
            }

            case DocumentTypeEnum.Waybill: {
                documentsUrlRoot += `${EditDocumentUrlsEnum.Waybill}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.Bill: {
                return getUrlWithId(`${documentsUrlRoot}${salesUrl}${EditDocumentUrlsEnum.Bill}/${id}`);
            }

            case DocumentTypeEnum.Statement: {
                documentsUrlRoot += `${EditDocumentUrlsEnum.Statement}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.Invoice: {
                documentsUrlRoot += `${EditDocumentUrlsEnum.Invoice}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.RetailReport: {
                documentsUrlRoot += `${EditDocumentUrlsEnum.RetailReport}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.MiddlemanReport: {
                documentsUrlRoot += `${EditDocumentUrlsEnum.MiddlemanReport}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.AccountingAdvanceStatement: {
                documentsUrlRoot += `${EditDocumentUrlsEnum.AdvanceStatement}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.Upd: {
                documentsUrlRoot += `${EditDocumentUrlsEnum.Upd}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.Contract: {
                documentsUrlRoot = `${EditDocumentUrlsEnum.Contract}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            default:
                return null;
        }
    }

    setTableCount = location => {
        storage.save(`Scroll`, window.scrollY);

        return storage.save(`tableData`, {
            tableCount: this.props.tableCount,
            location
        });
    }

    showRelatedDocs = e => {
        const { operationId } = this.props;
        const { relatedDocsArray } = this.store;

        e.stopPropagation();
        e.preventDefault();

        if (relatedDocsArray.length && currentOperationId === operationId) {
            return this.setState({ visible: true });
        }

        this.setState({
            loading: true,
            isRequestLoading: true
        });

        relatedDocsArray.length = 0;

        if (requestWasNotSending) {
            requestWasNotSending = false;

            return OperationTablesService.getRelatedOperations(`Finances/Money/Operations/${this.props.operationId}/LinkedDocuments`)
                .then(result => {
                    result.map(operation => {
                        const operationData = {
                            text: `${this.getDocumentName(operation.Type)} №${operation.Number}`,
                            value: {
                                type: operation.Type,
                                id: operation.Id
                            },
                            description: dateHelper(operation.Date).format(`DD.MM.YYYY`)
                        };

                        return relatedDocsArray.push(operationData);
                    });

                    requestWasNotSending = true;

                    if (!this.state.isRequestLoading) {
                        relatedDocsArray.length = 0;

                        return null;
                    }

                    this.setState({ loading: false, visible: true });

                    currentOperationId = operationId;

                    return null;
                });
        }

        return null;
    }

    cancelLocationReplace = e => {
        e.stopPropagation();
        e.preventDefault();
    }

    clickOperation = url => {
        this.setTableCount(url);
        NavigateHelper.push(url);
    }

    handleClickOutside = () => {
        const { visible, loading } = this.state;

        visible && this.setState({
            visible: false
        });

        if (loading) {
            this.setState({
                loading: false,
                isRequestLoading: false
            });
        }
    }

    render() {
        return <div
            role="button"
            className={cn(`relatedDocs`)}
            onClick={this.cancelLocationReplace}
            onMouseLeave={this.handleClickOutside}
            onKeyPress={() => {}}
            tabIndex="-1"
        >
            <button onMouseEnter={this.showRelatedDocs} className={cn(`relatedDocs__button`)}>
                {svgIconHelper.getJsx({ name: `clip`, className: cn(`relatedDocs__icon`) })}
                {this.props.count}
            </button>
            { this.getRelatedDocsDropdown() }
        </div>;
    }
}

RelatedDocs.propTypes = {
    count: PropTypes.number,
    operationId: PropTypes.number,
    operationDirection: PropTypes.number,
    tableCount: PropTypes.number
};

export default RelatedDocs;
