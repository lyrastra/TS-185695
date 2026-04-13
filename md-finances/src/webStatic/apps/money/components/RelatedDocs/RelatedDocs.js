import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { Transition } from 'react-transition-group';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import DropdownList from '@moedelo/frontend-core-react/components/dropdown/DropdownList';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import { getUrlWithId } from '@moedelo/frontend-core-v2/helpers/companyId';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Tooltip, { Position as PositionEnum } from '@moedelo/frontend-core-react/components/Tooltip';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import Link, { Size as LinkSize } from '@moedelo/frontend-core-react/components/Link';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import { getSymbolByCode } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import scenarioSectionResource from '@moedelo/frontend-common-v2/apps/onboardingScenario/resources/scenarioSectionResource';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import DocumentTypeEnum from '../../../../enums/DocumentTypeEnum';
import EditDocumentUrlsEnum from '../../../../enums/EditDocumentUrlsEnum';
import EditOperationUrlsEnum from '../../../../enums/EditOperationUrlsEnum';
import OperationTablesService from '../../../../services/newMoney/operationTablesService';
import Store from '../../stores/TableStore';
import PrimaryDocsStatusEnum from '../../../../enums/PrimaryDocsStatusEnum';
import storage from '../../../../helpers/newMoney/storage';
import MoneyOperationTypeResources, { paymentOrderOperationResources, cashOrderOperationResources } from '../../../../resources/MoneyOperationTypeResources';
import relatedDocsManualLinkResource from './resources/relatedDocsManualLinkResource';
import style from './style.m.less';

const cn = classnames.bind(style);
const transitionDuration = 275;
const timeout = {
    enter: transitionDuration,
    exit: transitionDuration
};

let currentOperationId = null;
let requestWasNotSending = true;

let tipPortalParent;

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

    onManualLinkClick = linkType => {
        mrkStatService.sendEventWithoutInternalUser({
            event: `related_docs_${linkType}_manual_click`
        });

        NavigateHelper.open(relatedDocsManualLinkResource[linkType]);
    }

    onClickManualLinkForPaymentToSupplier = () => {
        mrkStatService.sendEventWithoutInternalUser({
            event: `related_docs_manual_click_link`,
            st5: this.props.operationType
        });

        NavigateHelper.open(`https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dokumenty-prof`);
    }

    getDocumentText = operation => {
        if (operation.Type === DocumentTypeEnum.PaymentReserve) {
            return this.getDocumentName(operation.Type);
        }

        return `${this.getDocumentName(operation.Type)} №${operation.Number}`;
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
            case DocumentTypeEnum.ReceiptStatement:
                return `Акт приема-передачи`;
            case DocumentTypeEnum.SalesCurrencyInvoice:
            case DocumentTypeEnum.PurchasesCurrencyInvoice:
                return `Инвойс`;
            case DocumentTypeEnum.Ukd:
                return `УКД`;
            case DocumentTypeEnum.PaymentReserve:
                return `Резерв`;
            default:
                return `Документ`;
        }
    }

    getRelatedDocsDropdown = () => {
        const {
            primaryDocsStatus, count, uncoveredSum, currency, operationId
        } = this.props;
        const { loading } = this.state;
        const relatedDocsArray = this.store.relatedDocsArray.slice();

        if (loading) {
            return <div className={cn(`relatedDocs__dropdown__loader`)}>
                <Loader className={cn(`relatedDocs__loader`)} />
            </div>;
        }

        const data = [
            <div>
                {(count > 0 && primaryDocsStatus === PrimaryDocsStatusEnum.UnpaidPrimaryDocs && uncoveredSum > 0) &&
                    <div className={cn(`relatedDocs__header`)}>
                        Не хватает закрывающих<br />документов на {toFinanceString(uncoveredSum)} {getSymbolByCode(currency)}
                    </div>
                }
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
                        width={`auto`}
                        className={cn(`relatedDocs__dropdown`, `fade`, `fade-${status}`)}
                        parentId={operationId}
                        fixOverflow
                    />;
                }
            }
        </Transition>;
    }

    getRelatedDocsTooltipText = () => {
        const { operationType, operationDirection } = this.props;

        const directionLinkPart = operationDirection === Direction.Outgoing ? `Buy` : `Sale`;

        const operationsWithOtherTooltip = [
            MoneyOperationTypeResources.PaymentOrderPaymentToSupplier.value,
            MoneyOperationTypeResources.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods.value,
            MoneyOperationTypeResources.CashOrderOutgoingPaymentSuppliersForGoods.value,
            MoneyOperationTypeResources.PurseOperationIncome.value,
            MoneyOperationTypeResources.PaymentOrderIncomingPaymentForGoods.value,
            MoneyOperationTypeResources.PaymentOrderIncomingCurrencyPaymentFromBuyer.value,
            MoneyOperationTypeResources.CashOrderIncomingPaymentForGoods.value
        ];

        if (operationsWithOtherTooltip.includes(operationType)) {
            return <React.Fragment>
                Привяжите к платежу первичный документ. Такой <Link
                    size={LinkSize.small}
                    onClick={this.onClickManualLinkForPaymentToSupplier}
                > документ подтверждает</Link> сделку.
                Для оказания услуг — акт или УПД, для торговли — накладная или УПД.
            </React.Fragment>;
        }

        return <React.Fragment>
            Первичный документ подтверждает окончание сделки. Для оказанных услуг это <Link onClick={() => this.onManualLinkClick(`Statement${directionLinkPart}Link`)}>акт</Link> или&nbsp;
            <Link onClick={() => this.onManualLinkClick(`Upd${directionLinkPart}Link`)}>УПД</Link>, при отгруженных
            товарах это <Link onClick={() => this.onManualLinkClick(`Invoice${directionLinkPart}Link`)}>накладная</Link> или&nbsp;
            <Link onClick={() => this.onManualLinkClick(`Upd${directionLinkPart}Link`)}>УПД</Link>.
        </React.Fragment>;
    };

    getRelatedDocUrl = operation => {
        const { operationDirection, operationType } = this.props;
        const { id, type } = operation.value;
        const salesUrl = `/Sales#documents`;
        const buyUrl = `/Buy#documents`;

        let finansesUrlRoot = `/Finances#edit`;
        let appUrlRoot = `/AccDocuments`;
        let documentsUrlRoot = appUrlRoot;

        documentsUrlRoot += operationDirection === Direction.Outgoing
            ? buyUrl
            : salesUrl;

        documentsUrlRoot = operationType === paymentOrderOperationResources.PaymentOrderIncomingMediationFee.value
            || operationType === paymentOrderOperationResources.PaymentOrderOutgoingReturnToBuyer.value
            || operationType === cashOrderOperationResources.CashOrderOutgoingReturnToBuyer.value
            ? appUrlRoot + salesUrl
            : documentsUrlRoot;

        switch (type) {
            case DocumentTypeEnum.InventoryCard:
                return getUrlWithId(`/AccDocuments/Estate#fixedAssets/linked/${id}`);

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
                return getUrlWithId(`${appUrlRoot}${salesUrl}${EditDocumentUrlsEnum.Bill}/${id}`);
            }

            case DocumentTypeEnum.Statement: {
                documentsUrlRoot += `${EditDocumentUrlsEnum.Statement}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.ReceiptStatement: {
                return `${getUrlWithId(`${EditDocumentUrlsEnum.ReceiptStatement}/${id}`)}#/`;
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
                appUrlRoot += `${EditDocumentUrlsEnum.AdvanceStatement}/${id}`;

                return getUrlWithId(appUrlRoot);
            }

            case DocumentTypeEnum.Upd: {
                documentsUrlRoot += `${EditDocumentUrlsEnum.Upd}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.SalesUpd: {
                documentsUrlRoot += `${EditDocumentUrlsEnum.Upd}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.Contract: {
                documentsUrlRoot = `${EditDocumentUrlsEnum.Contract}/${id}`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.SalesCurrencyInvoice: {
                documentsUrlRoot = `${EditDocumentUrlsEnum.SalesCurrencyInvoice}/${id}/View`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.PurchasesCurrencyInvoice: {
                documentsUrlRoot = `${EditDocumentUrlsEnum.PurchasesCurrencyInvoice}/${id}/Edit`;

                return getUrlWithId(documentsUrlRoot);
            }

            case DocumentTypeEnum.Ukd: {
                return getUrlWithId(`/Docs/Sales/Ukd/${id}?tab=preview`);
            }

            // при клике на Резерв переходим в платеж
            case DocumentTypeEnum.PaymentReserve: {
                const reserveRedirectUrl = `${finansesUrlRoot}${EditOperationUrlsEnum.PaymentOrder}/${this.props.operationId}`;

                return getUrlWithId(reserveRedirectUrl);
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

    getRelatedDocsIcon = () => {
        const { primaryDocsStatus, count } = this.props;
        const hasLinkedDocuments = count > 0;
        const className = cn(`relatedDocs__icon`, {
            valid: primaryDocsStatus === PrimaryDocsStatusEnum.PaidPrimaryDocs,
            invalid: primaryDocsStatus === PrimaryDocsStatusEnum.UnpaidPrimaryDocs
        });

        return <IconButton
            size={`small`}
            icon={`clip`}
            onClick={e => hasLinkedDocuments && this.showRelatedDocs(e)}
            className={className}
        />;
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
                            text: this.getDocumentText(operation),
                            value: {
                                type: operation.Type,
                                id: operation.Id
                            },
                            description: operation.Type === DocumentTypeEnum.PaymentReserve
                                ? null
                                : dateHelper(operation.Date).format(`DD.MM.YYYY`)
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

    renderRelatedDocsIcon = () => {
        const { primaryDocsStatus, count } = this.props;
        const hasLinkedDocuments = count > 0;

        if (!tipPortalParent) {
            tipPortalParent = document.createElement(`div`);
            document.body.appendChild(tipPortalParent);
        }

        if (primaryDocsStatus !== PrimaryDocsStatusEnum.UnpaidPrimaryDocs || (primaryDocsStatus === PrimaryDocsStatusEnum.UnpaidPrimaryDocs && hasLinkedDocuments)) {
            return this.getRelatedDocsIcon();
        }

        return (
            <Tooltip
                usePortal
                parentForPortal={tipPortalParent}
                content={this.getRelatedDocsTooltipText()}
                position={PositionEnum.leftTop}
                width={350}
                shift={-10}
            >
                {this.getRelatedDocsIcon()}
            </Tooltip>
        );
    }

    renderRelatedDocs = () => {
        const {
            primaryDocsStatus, count, isMainTable, operationId
        } = this.props;
        const hasLinkedDocuments = count > 0;

        return (
            <div className={`relatedDocs__container`} id={operationId}>
                <div
                    role="button"
                    className={cn(`relatedDocs`)}
                    onClick={this.cancelLocationReplace}
                    onMouseLeave={this.handleClickOutside}
                    onKeyPress={() => {}}
                    tabIndex="-1"
                >
                    <div
                        className={cn(`relatedDocs__button`, {
                            fullyPaid: primaryDocsStatus === PrimaryDocsStatusEnum.PaidPrimaryDocs,
                            unPaid: primaryDocsStatus === PrimaryDocsStatusEnum.UnpaidPrimaryDocs
                        })}
                    >
                        {this.renderRelatedDocsIcon()}
                        {hasLinkedDocuments && count}
                    </div>
                    {hasLinkedDocuments && this.getRelatedDocsDropdown()}
                </div>
                {isMainTable && <div
                    id={`tip_${scenarioSectionResource.Finances}_5`}
                    onClick={e => e.stopPropagation()}
                    role={`presentation`}
                />}
            </div>
        );
    }

    render() {
        return this.renderRelatedDocs();
    }
}

RelatedDocs.defaultProps = {
    primaryDocsStatus: PrimaryDocsStatusEnum.CantHaveAnyDocs,
    uncoveredSum: 0,
    isMainTable: false
};

RelatedDocs.propTypes = {
    count: PropTypes.number,
    operationId: PropTypes.number,
    operationDirection: PropTypes.number,
    tableCount: PropTypes.number,
    primaryDocsStatus: PropTypes.number,
    uncoveredSum: PropTypes.number,
    operationType: PropTypes.number,
    currency: PropTypes.number,
    isMainTable: PropTypes.bool
};

export default RelatedDocs;
