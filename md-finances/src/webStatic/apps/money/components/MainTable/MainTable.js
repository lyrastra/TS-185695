import React from 'react';
import * as mobx from 'mobx';
import { observer, inject } from 'mobx-react';
import Sticky from 'react-sticky-el';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import Guide from '@moedelo/frontend-common-v2/apps/bankIntegration/components/Guide';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import ButtonDropdown from '@moedelo/frontend-core-react/components/buttons/ButtonDropdown/ButtonDropdown';
import Checkbox from '@moedelo/frontend-core-react/components/Checkbox';
import Link from '@moedelo/frontend-core-react/components/Link/Link';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import TableHeader from '@moedelo/frontend-core-react/components/table/TableHeader';
import SortTag from '@moedelo/frontend-core-react/components/SortTag';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import TableOverFooter from '@moedelo/frontend-core-react/components/table/TableOverFooter';
import TableRow from '@moedelo/frontend-core-react/components/table/TableRow';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup/ElementsGroup';
import sessionStorageHelper from '@moedelo/frontend-core-v2/helpers/sessionStorageHelper';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper/svgIconHelper';
import gridStyle from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { pluralNoun } from '@moedelo/frontend-core-v2/helpers/PluralHelper';
import TableUnderHeader from '@moedelo/frontend-core-react/components/table/TableUnderHeader';
import { Type } from '@moedelo/frontend-core-react/components/buttons/enums';
import whiteLabelHelper from '@moedelo/frontend-common-v2/apps/marketing/helpers/whiteLabelHelper';
import WhiteLabelName from '@moedelo/frontend-enums/mdEnums/WhiteLabelName';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import SmsConfirmModal from '@moedelo/frontend-common-v2/apps/bankIntegration/components/SmsConfirmModal';
import Row from './components/Row';
import OperationList from '../OperationsList';
import MoneyOperationService from '../../../../services/newMoney/moneyOperationService';
import { deleteRentPaymentOrders } from '../../../../services/newMoney/newMoneyOperationService';
import SortColumnEnum from '../../../../enums/newMoney/SortColumnEnum';
import SortTypeEnum from '../../../../enums/newMoney/SortTypeEnum';
import BankIntegrationStatusCodeEnum from '../../../../enums/BankIntegrationStatusCodeEnum';
import Filter from './components/Filter';
import { toQueryString } from '../../../../helpers/newMoney/utils';
import { paymentOrderOperationResources } from '../../../../resources/MoneyOperationTypeResources';
import { canCopyOperation } from '../../../../helpers/MoneyOperationHelper';
import EmptyFilteredTable from './components/EmptyFilteredTable';
import FilterTags from '../FilterTags';
import DownloadOperationButton from './../DownloadOperationButton';
import RemoveOperationButton from '../RemoveOperationButton';
import RemoveOperationDialog from '../RemoveOperationButton/RemoveOperationDialog';
import DownloadOperationsButtonsList from '../DownloadOperationsButtonsList';
import CopyOperationButton from '../CopyOperationButton';
import FilterTagsHelper from '../../../../helpers/newMoney/FilterTagsHelper';
import {
    isAllCanBeSent as isAllOperationsCanBeSent, isAnyDownloadable as isAnyOperationDownloadable, downloadOperations, sendToBank, onApproveOperation
} from '../../../../helpers/newMoney/operationActionsHelper';
import ClosedDocumentsStatusEnum from '../../../../enums/ClosedDocumentsStatusEnum';
import mrkStatServiceHelper from '../../../../helpers/newMoney/mrkStatServiceHelper';
import storage from '../../../../helpers/newMoney/storage';
import MassTaxSystemChangeModal from '../MassTaxSystemChangeModal';
import autoPayIcon from '../../../../icons/autoPay.m.svg';
import { handleSendPaymentError } from '../../../../helpers/newMoney/sendToBankHelper';
import style from './style.m.less';
import ApproveOperationButton from '../ApproveOperationButton/ApproveOperationButton';
import { setApproveByIds } from '../../../../services/approvedService';

const cn = classnames.bind(style);
const counterData = [
    {
        value: 20,
        text: `20`
    },
    {
        value: 50,
        text: `50`
    },
    {
        value: 100,
        text: `100`
    },
    {
        value: 300,
        text: `300`
    }
];

let componentWasLoadedFirstTime = true;
let checkedAll = false;
const operationsCanSendOnceNum = 21;
const defaultTableCount = 20;

@inject(`massChangeTaxSystemStore`)
@observer
class MainTable extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;
        this.commonDataStore = props.commonDataStore;
        this.warningTableStore = props.warningTableStore;
        this.successTableStore = props.successTableStore;
        this.moneySourceStore = props.moneySourceStore;
        this.massChangeTaxSystemStore = props.massChangeTaxSystemStore;

        this.state = {
            operationsForDeleting: [],
            operationsSentReport: {},
            needToSignOperationsIds: [],
            sentOperationsReportElement: null,
            singleOperationForDelete: null,
            dialogType: ClosedDocumentsStatusEnum.NoMatter,
            filter: this.props.filter,
            isAllCanBeSent: false,
            isAnyDownloadable: false,
            onDeleteDialogShown: false,
            getIntegrationModalShown: false,
            operationsSentNeedSmsModalShown: false,
            operationSentToSignModalShown: false,
            sendOperationsPending: false
        };
        this.store.counter = storage.get(`tableCounter`) || counterData[0].value;
    }

    async componentDidMount() {
        const tableData = storage.get(`tableData`);
        const options = {
            reset: true
        };
        const checkedOperations = this.getCheckedOperations();

        if (checkedOperations.length) {
            this.setOperationsConstraints(checkedOperations);
        }

        if (!componentWasLoadedFirstTime) {
            setTimeout(() => {
                this.props.onAddOperation({ withoutLoading: true });
            }, 200);
        }

        if (tableData && tableData.location) {
            options.tableCount = tableData.tableCount;
            tableData.location = null;
            storage.save(`tableData`, tableData);
        }

        componentWasLoadedFirstTime = false;

        window.scrollTo(0, storage.get(`Scroll`));
    }

    componentDidUpdate() {
        window.scrollTo(0, storage.get(`Scroll`));
    }

    onApprove = async ({ Id }) => {
        await onApproveOperation({ Id });

        this.reload({ filter: storage.get(`filter`) });
    }

    onClickDateHeaderCell = () => {
        storage.save(`Scroll`, window.scrollY);

        this.setSort(SortColumnEnum.Date);
    };

    onClickKontragentHeaderCell = () => {
        storage.save(`Scroll`, window.scrollY);

        this.setSort(SortColumnEnum.Kontragent);
    };

    onClickSumHeaderCell = () => {
        storage.save(`Scroll`, window.scrollY);

        this.setSort(SortColumnEnum.Sum);
    };

    onChangeSelectAll = ({ checked }) => {
        storage.save(`Scroll`, window.scrollY);

        this.store.toggleSelectAll(checked);
        this.setOperationsForMassChangeTaxSystem();
        this.setOperationsConstraints(this.store.checkedOperationList);
        this.setDialogTypes(this.store.checkedOperationList);
    };

    onClickRow = operation => {
        const { operationsListLength } = this.store;
        const tableData = storage.get(`tableData`);
        const tableCount = operationsListLength < defaultTableCount ? defaultTableCount : operationsListLength;

        storage.save(`tableData`, { ...tableData, tableCount });
        storage.save(`Scroll`, window.scrollY);

        this.props.onClickRow(operation);
    };

    onClickShowMoreButton = () => {
        checkedAll = false;

        storage.save(`Scroll`, window.scrollY);

        this.setState({ loading: true });
        this.loadOperations();
    };

    onSelectRow = () => {
        const { checkedOperationList, handleAllSelectedFlag } = this.store;

        storage.save(`Scroll`, window.scrollY);

        this.setOperationsForMassChangeTaxSystem();
        this.setOperationsConstraints(checkedOperationList);
        this.setDialogTypes(checkedOperationList);
        handleAllSelectedFlag();
    };

    setOperationsConstraints = checkedOperations => {
        const { currentSource } = this.props;
        const isAllCanBeSent = isAllOperationsCanBeSent(checkedOperations, currentSource);
        const isAnyDownloadable = isAnyOperationDownloadable(checkedOperations);

        this.setState({
            isAllCanBeSent,
            isAnyDownloadable
        });
    };

    setDialogTypes = operations => {
        const operationsForDeleting = operations;
        const dialogTypes = [];

        return MoneyOperationService.getLastClosedPeriod()
            .then(date => {
                for (let i = 0; i < operations.length; i += 1) {
                    const oDate = new Date(operations[i].Date);

                    if (this.isNoClosedDocumentsStatus(oDate.getFullYear(), oDate.getMonth(), oDate.getDate(), date)) {
                        dialogTypes.push(ClosedDocumentsStatusEnum.No);
                    } else {
                        dialogTypes.push(ClosedDocumentsStatusEnum.Completely);
                        delete operationsForDeleting[i];
                    }
                }

                this.setState({ operationsForDeleting });
                this.checkClosedPeriod(dialogTypes);
            });
    };

    getCheckedOperations = (operationsList = this.store.operations) => {
        return operationsList.filter(o => {
            return o.Checked;
        });
    };

    setSort = sortColumn => {
        const { operations, sortType } = this.store;

        this.store.sortType = this.store.sortColumn !== sortColumn || sortType === SortTypeEnum.Desc ? SortTypeEnum.Asc : SortTypeEnum.Desc;
        this.store.sortColumn = sortColumn;

        this.setState({
            loaded: !operations.length,
            loading: !!operations.length
        }, () => {
            sessionStorageHelper.set(`moneyMainTableSort`, JSON.stringify({
                sortColumn,
                sortType: this.store.sortType
            }));

            if (operations.length) {
                this.loadOperations({ reset: true });
            }
        });
    };

    getFilter(options) {
        let obj = options.filter || this.props.filter;

        if (obj.query) {
            obj = { ...obj, query: decodeURIComponent(obj.query) };
        }

        return obj;
    }

    setTableCounter = counter => {
        checkedAll = false;

        storage.save(`Scroll`, window.scrollY);
        storage.save(`tableCounter`, counter.value);

        if (counter.value !== this.store.counter) {
            this.store.setCounter(counter.value);
            this.loadOperations({ reset: true, counterUsed: true });
        }
    };

    setOperationsForMassChangeTaxSystem = () => {
        this.massChangeTaxSystemStore.setCheckedOperations(this.store.checkedOperationList);
    }

    setMassApprove = ({ Ids }) => {
        setApproveByIds({ Ids });
        this.props.onChangeList && this.props.onChangeList({ reset: true });
    }

    getRemovingIds = checkedOperations => {
        const removingOperationIds = [];
        const removingRentPaymentRowIds = [];

        checkedOperations.forEach(co => {
            if (co.OperationType === paymentOrderOperationResources.RentPayment.value) {
                removingRentPaymentRowIds.push(co.DocumentBaseId);
            } else {
                removingOperationIds.push(co.DocumentBaseId);
            }
        });

        return { removingOperationIds, removingRentPaymentRowIds };
    };

    uncheckOperations = () => {
        this.setState({
            singleOperationForDelete: null,
            operationsSentReport: {}
        });
        this.store.uncheckAllOperation();
    };

    reload = ({ filter }) => {
        this.loadOperations({ reset: true, filter });
    };

    loadOperations = (options = { reset: false }) => {
        const filter = this.getFilter(options);

        return this.store.loadOperations(options, filter);
    };

    sortClasses = sortColumn => {
        const { sortType } = this.store;

        return {
            activeSort: this.store.sortColumn === sortColumn,
            asc: sortType === SortTypeEnum.Asc,
            desc: sortType === SortTypeEnum.Desc
        };
    };

    sendOperationsToSign = () => {
        const { needToSignOperationsIds } = this.state;
        const requestString = JSON.stringify({
            action: `bookkeepingBulkSign`,
            arguments: needToSignOperationsIds.toString()
        });

        window.top.postMessage(requestString, `*`);

        this.setState({
            needToSignOperationsIds: [],
            sentOperationsReportElement: null
        });
        this._closeOperationsModals();
    };

    removeConfirmationModal = () => {
        return (
            <RemoveOperationDialog
                header={this._getHeaders()}
                visible={this.state.onDeleteDialogShown}
                description={this._getDescription}
                disabled={this._notClosedStatus}
                onButtonClick={this.removeRows}
                onClose={this._closeOperationsModals}
            />
        );
    };

    operationsToBankErrorModal = () => {
        const { operationsSentReport } = this.state;
        const { successLength, errorsList } = operationsSentReport;

        return (
            <Modal
                onClose={this._closeOperationsModals}
                header={`Отчет об отправке`}
                visible
                width={`307px`}
            >
                {successLength &&
                    <div>Успешно отправлено: {pluralNoun(successLength, `операции`, `операций`, `операций`, true)}</div>}
                {errorsList.length && <div>Не удалось
                    отправить: {pluralNoun(errorsList.length, `операции`, `операций`, `операций`, true)}</div>}
                <ElementsGroup className={cn(`sentReportControls`)}>
                    <Link text={`Закрыть`} onClick={this._closeOperationsModals} />
                </ElementsGroup>
            </Modal>
        );
    };

    operationsToBankNeedSmsModal = () => {
        const { operationsSentReport } = this.state;
        const { PhoneMask } = operationsSentReport;
        const { currentSource } = this.props;

        return <SmsConfirmModal
            visible
            phoneMasked={PhoneMask}
            source={currentSource}
            onConfirm={this.sendToBank}
            onClose={this._closeOperationsModals}
        />;
    };

    operationsToBankSentToSignModal = () => {
        const { sentOperationsReportElement } = this.state;

        return (
            <Modal
                onClose={this._closeOperationsModals}
                header={`Отчет об отправке`}
                visible
                width={`400px`}
            >
                {sentOperationsReportElement}
                <ElementsGroup className={cn(`sentReportControls`)}>
                    <Button onClick={this.sendOperationsToSign}>Продолжить</Button>
                </ElementsGroup>
            </Modal>
        );
    };

    parseSentOperationsList = async response => {
        const { List = [] } = response;
        const successOperationsIds = [];
        const success = [];
        const error = [];

        List.forEach(operation => {
            const date = new Date(operation.Date);
            const dateString = dateHelper(date).format(`DD.MM.YYYY`);
            const operationElement = <div
                className={cn(`reportOperation`)}
            >{`№${operation.Number} от ${dateString} на сумму ${operation.Sum} ₽`}</div>;

            if (operation.IsSuccess) {
                successOperationsIds.push(operation.ExternalId);
                success.push(operationElement);
            } else {
                error.push(operationElement);
            }
        });

        const reportElement = <div className={cn(`beforeSignReport`)}>
            {success.length > 0 &&
                <div className={cn(`beforeSignReport__block`)}>
                    <div className={cn(`title`, `incoming`)}>Отправлено успешно:</div>
                    {success.map(operation => {
                        return operation;
                    })}
                </div>}
            {error.length > 0 &&
                <div className={cn(`beforeSignReport__block`)}>
                    <div className={cn(`title`, `outgoing`)}>Не отправлено:</div>
                    {error.map(operation => {
                        return operation;
                    })}
                </div>}
        </div>;

        this.setState({
            sentOperationsReportElement: reportElement,
            needToSignOperationsIds: successOperationsIds
        });

        return null;
    };

    downloadOperations = (operations, format) => {
        downloadOperations(operations, format);
    };

    sendToBank = (operations = this.getCheckedOperations()) => {
        const { currentSource } = this.props;
        const { sendOperationsPending } = this.state;

        if (currentSource) {
            if (currentSource.HasActiveIntegration && currentSource.CanSendPaymentOrder && !sendOperationsPending) {
                NotificationManager.show({
                    message: `Отправка платежных поручений...`,
                    type: `info`,
                    duration: 2000
                });

                this.setState({
                    sendOperationsPending: true
                });

                sendToBank(mobx.toJS(operations))
                    .then(response => {
                        this.showSendingToBankResult(response);

                        if (response.StatusCode !== BankIntegrationStatusCodeEnum.NeedSms) {
                            this.uncheckOperations();
                        }

                        setTimeout(() => {
                            this.setState({
                                sendOperationsPending: false
                            });
                        }, 500);
                    })
                    .catch(() => {
                        this.uncheckOperations();
                        NotificationManager.show({
                            title: `Ошибка!`,
                            message: `Произошла неизвестная ошибка.`,
                            type: `error`,
                            duration: 4000
                        });
                    });
            }

            if (currentSource.HasAvaliableIntegration) {
                this.setState({
                    getIntegrationModalShown: true
                });
            }
        }
    };

    showSendingToBankResult = response => {
        switch (response.StatusCode) {
            case BankIntegrationStatusCodeEnum.Ok:
                this._closeOperationsModals();
                NotificationManager.show({
                    message: `Отправлено успешно`,
                    type: `success`,
                    duration: 4000
                });

                break;
            case BankIntegrationStatusCodeEnum.Error:
                handleSendPaymentError(response);

                break;
            case BankIntegrationStatusCodeEnum.Partially:
                this.setState({
                    operationsSentPartiallyErrorModalShown: true,
                    operationsSentReport: response
                });

                break;
            case BankIntegrationStatusCodeEnum.NeedSms:
                this.setState({
                    operationsSentNeedSmsModalShown: true,
                    operationsSentReport: response
                });

                break;
            case BankIntegrationStatusCodeEnum.NeedToSign:
                this.parseSentOperationsList(response)
                    .then(() => {
                        this.setState({
                            operationSentToSignModalShown: true,
                            operationsSentReport: response
                        });
                    })
                    .catch(err => {
                        throw new Error(err);
                    });

                break;
            default:
                NotificationManager.show({
                    message: `Неизвестный статус`,
                    type: `warning`,
                    duration: 4000
                });
        }
    };

    tableHeader = () => {
        const { operations } = this.store;
        const { filter } = this.props;
        const { dialogType, isAllCanBeSent, isAnyDownloadable } = this.state;
        const checkedCount = operations.filter(o => o.Checked).length;
        const checkedOperations = this.getCheckedOperations();
        const canCopy = checkedCount === 1 && canCopyOperation({ ...checkedOperations[0], ...this.commonDataStore }) && this.commonDataStore.hasAccessToMoneyEdit;
        const allSelected = operations.length > 0 && operations.length === checkedCount;
        const isDisableApproved = checkedOperations.every(el => el.isApproved !== false);
        const isCanMassApprove = !checkedOperations.every(el => el.isApproved === undefined);
        const noneSelected = checkedCount === 0;
        const isIntegrationAvailable = this.isIntegrationAvailable();
        const options = {
            allSelected: operations.length > 0 && operations.length === checkedCount,
            noneSelected: checkedCount === 0,
            checkedCount: checkedOperations.length,
            checkedOperations,
            isAnyDownloadable,
            isAllCanBeSent,
            dialogType,
            canCopy,
            filter,
            isCanMassApprove,
            isDisableApproved
        };
        let component = null;

        if (noneSelected) {
            component = this.renderNoSelectedOperationsControls(allSelected, noneSelected, filter);
        }

        if (!noneSelected && !isIntegrationAvailable) {
            component = this.renderNoIntegrationControls(options);
        }

        if (!noneSelected && isIntegrationAvailable) {
            component = this.renderHaveIntegrationControls(options);
        }

        return (
            <Sticky mode={`top`} stickyClassName={style.stickyHeader} style={{ zIndex: 2, position: `relative` }}>
                <TableHeader>
                    {component}
                </TableHeader>
            </Sticky>
        );
    };

    isIntegrationAvailable = () => {
        const { currentSource } = this.props;

        return currentSource && ((currentSource.CanSendPaymentOrder && currentSource.HasActiveIntegration) || currentSource.HasAvaliableIntegration);
    };

    underHeader = () => {
        const { filter } = this.props;
        const tags = FilterTagsHelper.getTags({
            filter,
            clearTag: clearTag => this.clearTag(clearTag)
        });

        if (tags.length) {
            mrkStatServiceHelper.sendFilterRequest(filter); // В случае изменения данного куска оповестить lebedev@moedelo.org

            return <TableUnderHeader>
                <FilterTags onClearFilter={this.clearFilter}>{tags}</FilterTags>
            </TableUnderHeader>;
        }

        return null;
    };

    removeRows = () => {
        checkedAll = false;

        storage.save(`Scroll`, window.scrollY);

        const checkedOperations = this.state.operationsForDeleting;
        const { removingOperationIds, removingRentPaymentRowIds } = this.getRemovingIds(checkedOperations);
        const removingTasks = [];

        if (removingRentPaymentRowIds.length) {
            removingTasks.push(deleteRentPaymentOrders(removingRentPaymentRowIds));
        }

        if (removingOperationIds.length) {
            removingTasks.push(MoneyOperationService.deleteOperation(removingOperationIds));
        }

        return Promise.all(removingTasks)
            .then(() => {
                NotificationManager.show({
                    message: checkedOperations.length > 1 ? `Документы удалены` : `Документ удален`,
                    type: `info`,
                    duration: 2000
                });
                this._closeOperationsModals();

                this.props.onChangeList && this.props.onChangeList({ reset: true });
            })
            .catch(this.errorAtDeletingOperations);
    };

    errorAtDeletingOperations = () => {
        let supportPhone = `8 800 200 77 27`;
        let supportEmail = `support@moedelo.org`;

        if (whiteLabelHelper.isWhiteLabel() && whiteLabelHelper.getWhiteLabel() === WhiteLabelName.delo) {
            const whiteLabelData = whiteLabelHelper.getWhiteLabelData();
            supportPhone = whiteLabelData.supportPhone;
            supportEmail = whiteLabelData.supportEmail;
        }

        const errorMessage = `Ошибка при удалении. Позвоните нам по телефону ${supportPhone} или <a href='mailto:${supportEmail}'>напишите</a>, и мы исправим ее в максимально короткие сроки.`;

        NotificationManager.show({
            message: errorMessage,
            type: `error`,
            duration: 5000
        });

        this._closeOperationsModals();
    };

    clearTag = tags => {
        const filter = Object.assign({}, this.props.filter);

        tags.forEach(tag => delete filter[tag]);

        storage.save(`filter`, filter);
        storage.save(`Scroll`, window.scrollY);

        NavigateHelper.push(toQueryString(filter));

        this.reload({ filter });
    };

    clearFilter = () => {
        const filter = {};

        [`sourceType`, `sourceId`, `query`].forEach(tag => {
            this.props.filter[tag] && (filter[tag] = this.props.filter[tag]);
        });

        storage.save(`filter`, filter);
        storage.save(`Scroll`, window.scrollY);

        NavigateHelper.push(toQueryString(filter));

        this.reload({ filter });
    };

    applyFilter = (filter = {}) => {
        const filterParam = { ...this.props.filter, ...filter };

        const filterString = Object.entries(filterParam).reduce((acc, val) => {
            const [key, value] = val;
            const isZeroArrayValue = Array.isArray(value) && value.length === 1 && value[0] === 0;

            if (!isZeroArrayValue && value) {
                if (Array.isArray(value)) {
                    acc[key] = [];

                    for (let i = 0; i < value.length; i += 1) {
                        acc[key].push(value[i]);
                    }
                } else {
                    acc[key] = value;
                }
            }

            return acc;
        }, {});

        NavigateHelper.push(toQueryString(filterString));

        this.reload({ filter: filterString });
    };

    isNoClosedDocumentsStatus = (year, month, day, date) => {
        if (year > date.getFullYear()
            || (year === date.getFullYear()
                && month > date.getMonth())
            || (year === date.getFullYear()
                && month === date.getMonth()
                && day > date.getDate())) {
            return true;
        }

        return ``;
    };

    checkClosedPeriod = dialogTypes => {
        const average = dialogTypes.reduce((a, b) => a + b, 0) / dialogTypes.length;
        const status = ClosedDocumentsStatusEnum;
        let dialogType = status.NoMatter;

        if (average === status.No) {
            dialogType = status.No;
        } else if (average === status.Completely) {
            dialogType = status.Completely;
        } else {
            dialogType = status.Partly;
        }

        this.setState({ dialogType });
    };

    _getHeaders = () => {
        return `Удаление`;
    };

    _getDescription = () => {
        const status = ClosedDocumentsStatusEnum;
        const { dialogType } = this.state;

        if (dialogType < status.Partly) {
            return <p>Вы уверены, что хотите удалить эту операцию?</p>;
        } else if (dialogType > status.Partly) {
            return <p>
                Операции в закрытом периоде нельзя удалить.
                Вы можете <Link text="открыть период" onClick={this._openClosedPeriod} />
            </p>;
        }

        return <p>
            <strong>Внимание! </strong>
            Будут удалены только документы из открытого периода.
        </p>;
    };

    _closeOperationsModals = () => {
        this.setState({
            onDeleteDialogShown: false,
            operationsSentPartiallyErrorModalShown: false,
            operationsSentNeedSmsModalShown: false,
            getIntegrationModalShown: false,
            operationSentToSignModalShown: false
        });
    };

    _showRemoveOperationsDialog = (singleOperationForDelete = null) => {
        storage.save(`Scroll`, window.scrollY);

        this.setDialogTypes([singleOperationForDelete]);

        setTimeout(() => {
            this.setState({
                onDeleteDialogShown: true,
                singleOperationForDelete
            });
        }, 150);
    };

    _notClosedStatus = () => {
        return this.state.dialogType !== ClosedDocumentsStatusEnum.Completely;
    };

    _openClosedPeriod = () => {
        this._closeOperationsModals();

        return MoneyOperationService.openClosedPeriod(this._getDate())
            .then(() => {
                NavigateHelper.reload();
            });
    };

    _getDate = () => {
        return new Date(this.state.singleOperationForDelete.Date);
    };

    renderMassTaxSystemChangeButton = () => {
        const { canShowChangeButton, setModalVisibility, loading } = this.massChangeTaxSystemStore;

        if (!canShowChangeButton || !this.commonDataStore.hasAccessToMoneyEdit) {
            return null;
        }

        return (
            <Button
                onClick={() => setModalVisibility(true)}
                type={Type.Panel}
                disabled={loading}
            >
                {svgIconHelper.getJsx({ file: autoPayIcon })}Сменить СНО
            </Button>
        );
    }

    renderFilter = () => {
        return (
            <div className={cn(gridStyle.col_8)}>
                <Filter
                    moneySourceStore={this.moneySourceStore}
                    search={this.applyFilter}
                    value={this.props.filter}
                    onClearFilter={this.clearFilter}
                />
            </div>
        );
    }

    renderNoSelectedOperationsControls = (allSelected, noneSelected) => {
        return (
            <div className={cn(`tableHead`)}>
                <div className={cn(gridStyle.col_1)}>
                    <Checkbox
                        ref={ref => {
                            this.checkAll = ref;
                        }}
                        onChange={this.onChangeSelectAll}
                        checked={allSelected || checkedAll}
                        indeterminate={!allSelected && !noneSelected}
                    />
                </div>
                <div className={cn(gridStyle.col_3)}>
                    <SortTag
                        onSort={this.onClickDateHeaderCell}
                        direction={this.store.sortColumn === SortColumnEnum.Date ? this.store.sortType : null}
                    >
                        Дата
                    </SortTag>
                </div>
                <div className={cn(gridStyle.col_6)}>
                    <SortTag
                        onSort={this.onClickKontragentHeaderCell}
                        direction={this.store.sortColumn === SortColumnEnum.Kontragent ? this.store.sortType : null}
                    >
                        Контрагент
                    </SortTag>
                </div>
                <div className={cn(gridStyle.col_4, style.priceCell)}>
                    <SortTag
                        onSort={this.onClickSumHeaderCell}
                        direction={this.store.sortColumn === SortColumnEnum.Sum ? this.store.sortType : null}
                        align="right"
                    >
                        Сумма
                    </SortTag>
                </div>
                <div className={cn(gridStyle.col_2)} />
                {this.renderFilter()}
            </div>
        );
    };

    renderNoIntegrationControls = options => {
        return (
            <div className={cn(`tableHead`)}>
                <div className={cn(gridStyle.col_1, `checkbox__label`)}>
                    <Checkbox
                        onChange={this.onChangeSelectAll}
                        checked={options.allSelected || checkedAll}
                        indeterminate={!options.allSelected && !options.noneSelected}
                    />
                </div>
                <div className={cn(gridStyle.col_2, `checkedCount`)}>Выбрано: {options.checkedCount}</div>
                <div className={cn(gridStyle.col_13, `cell__action`)}>
                    <RemoveOperationButton
                        operations={options.checkedOperations}
                        onClick={this.removeRows}
                        dialogType={options.dialogType}
                        commonDataStore={this.commonDataStore}
                    />
                    {this.renderMassTaxSystemChangeButton()}
                    {options.isAnyDownloadable && <DownloadOperationButton
                        checkedOperations={options.checkedOperations}
                        dropped={false}
                    />}
                    {options.canCopy &&
                        <CopyOperationButton
                            copyOperation={() => {
                                return this.props.copyOperation(options.checkedOperations[0]);
                            }}
                        />}
                    {options.isCanMassApprove && <ApproveOperationButton
                        isDisableApproved={options.isDisableApproved}
                        setMassApprove={this.setMassApprove}
                        checkedOperations={options.checkedOperations}
                    />}
                </div>
                {this.renderFilter()}
            </div>
        );
    };

    renderHaveIntegrationControls = options => {
        const is1CDisabled = false;
        const moreButtons = [
            []
        ];

        if (options.isAnyDownloadable) {
            moreButtons.push({
                content: <DownloadOperationsButtonsList
                    operation={options.checkedOperations}
                    onDownload={this.downloadOperations}
                    is1CDisabled={is1CDisabled}
                />
            });
        }

        if (options.canCopy) {
            moreButtons.push({
                content: <CopyOperationButton
                    wide
                    copyOperation={() => {
                        return this.props.copyOperation(options.checkedOperations[0]);
                    }}
                />
            });
        }

        return (
            <div className={cn(`tableHead`)}>
                <div className={cn(gridStyle.col_1, `checkbox__label`)}>
                    <Checkbox
                        onChange={this.onChangeSelectAll}
                        checked={options.allSelected || checkedAll}
                        indeterminate={!options.allSelected && !options.noneSelected}
                    />
                </div>
                <div className={cn(gridStyle.col_3, `checkedCount`)}>Выбрано: {options.checkedCount}</div>
                <div className={cn(gridStyle.col_12, `cell__action`)}>
                    <RemoveOperationButton
                        operations={options.checkedOperations}
                        onClick={this.removeRows}
                        dialogType={options.dialogType}
                        commonDataStore={this.commonDataStore}
                    />
                    {((options.checkedCount < operationsCanSendOnceNum) && options.isAllCanBeSent) && <Button
                        onClick={() => {
                            this.sendToBank(options.checkedOperations);
                        }}
                        type={Type.Panel}
                    >{svgIconHelper.getJsx({ name: `sendToBank` })}Отправить в банк</Button>}
                    {this.renderMassTaxSystemChangeButton()}
                    {options.isCanMassApprove && <ApproveOperationButton
                        isDisableApproved={options.isDisableApproved}
                        setMassApprove={this.setMassApprove}
                        checkedOperations={options.checkedOperations}
                    />}
                    {options.isAllCanBeSent && moreButtons.length > 1 && <ButtonDropdown
                        data={moreButtons}
                        onSelect={() => {}}
                        type={Type.Panel}
                        dropdownWidth={155}
                        disabled={false}
                    >
                        Еще
                    </ButtonDropdown>}
                    {!options.isAllCanBeSent && options.isAnyDownloadable && <DownloadOperationButton
                        checkedOperations={options.checkedOperations}
                        dropped={false}
                    />}
                    {!options.isAllCanBeSent && options.canCopy && <CopyOperationButton
                        copyOperation={() => {
                            return this.props.copyOperation(options.checkedOperations[0]);
                        }}
                    />}
                </div>
                {this.renderFilter()}
            </div>
        );
    };

    render() {
        const {
            loading,
            loaded,
            operations,
            tableCount,
            totalCount,
            counter,
            isOutsourceUserOrEmployee
        } = this.store;

        const { filter, copyOperation, currentSource = {} } = this.props;
        const { IntegrationPartner } = currentSource;
        const {
            operationsSentPartiallyErrorModalShown, onDeleteDialogShown, operationsSentNeedSmsModalShown, getIntegrationModalShown, operationSentToSignModalShown
        } = this.state;

        if (!loaded) {
            return <div className={cn(`tableLoader`)}>
                <Loader className={cn(`loader`)} />
            </div>;
        }

        if (!operations.length && loaded) {
            return <div className={cn(`table`)} id={`mainTable`}>
                {this.tableHeader()}
                {this.underHeader()}
                <EmptyFilteredTable
                    filter={filter}
                />
            </div>;
        }

        return <div className={cn(`table`)} id={`mainTable`}>
            {this.tableHeader()}
            {this.underHeader()}
            <div className={cn(`tableBody`)}>
                <OperationList
                    operations={operations}
                    tableCount={tableCount}
                    copyOperation={copyOperation}
                    onSelect={this.onSelectRow}
                    onClick={this.onClickRow}
                    onDelete={this._showRemoveOperationsDialog}
                    onSendToBank={this.sendToBank}
                    commonDataStore={this.commonDataStore}
                    onApprove={this.onApprove}
                    Row={Row}
                    isHistoryButtonVisible={isOutsourceUserOrEmployee}
                />
            </div>
            {loading &&
                <TableRow>
                    <div className={cn(`tableLoader`)}>
                        <Loader className={cn(`loader`)} />
                    </div>
                </TableRow>
            }
            <TableOverFooter
                className={cn(`tableOverFooter`)}
                showButton={tableCount < totalCount}
                buttonText={`Показать еще ${pluralNoun(Math.min(totalCount - tableCount, (storage.get(`tableCounter`) || counter)), `операцию`, `операции`, `операций`, true)}`}
                text={`Отображено: ${tableCount} из ${pluralNoun(totalCount, `операции`, `операций`, `операций`, true)}`}
                onClick={this.onClickShowMoreButton}
                buttonLoading={false}
                showCounter={tableCount <= totalCount && tableCount >= counterData[0].value}
                counterData={counterData}
                counterText="Показывать по"
                onSelect={this.setTableCounter}
                counterValue={storage.get(`tableCounter`) || counter}
            />

            {onDeleteDialogShown && this.removeConfirmationModal()}
            {operationsSentPartiallyErrorModalShown && this.operationsToBankErrorModal()}
            {operationsSentNeedSmsModalShown && this.operationsToBankNeedSmsModal()}
            {operationSentToSignModalShown && this.operationsToBankSentToSignModal()}
            {getIntegrationModalShown && <Guide bank={IntegrationPartner} onClose={this._closeOperationsModals} />}
            <MassTaxSystemChangeModal onSuccess={this.uncheckOperations} />
        </div>;
    }
}

MainTable.propTypes = {
    filter: PropTypes.object,
    onClickRow: PropTypes.func,
    onAddOperation: PropTypes.func,
    onChangeList: PropTypes.func,
    copyOperation: PropTypes.func,
    moneySourceStore: PropTypes.object,
    store: PropTypes.object.isRequired,
    commonDataStore: PropTypes.object.isRequired,
    warningTableStore: PropTypes.object,
    successTableStore: PropTypes.object,
    currentSource: PropTypes.object,
    massChangeTaxSystemStore: PropTypes.object
};

MainTable.defaultProps = {
    filter: {}
};

export default MainTable;
