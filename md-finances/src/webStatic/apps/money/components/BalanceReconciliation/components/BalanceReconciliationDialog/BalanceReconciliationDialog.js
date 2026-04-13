import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { Transition } from 'react-transition-group';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import Arrow from '@moedelo/frontend-core-react/components/Arrow';
import Link from '@moedelo/frontend-core-react/components/Link/Link';
import TableRow from '@moedelo/frontend-core-react/components/table/TableRow';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup/ElementsGroup';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import { Position } from '@moedelo/frontend-core-react/components/Tooltip';
import svgIconHelper, { getJsx } from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import info from '@moedelo/frontend-core-react/icons/info.m.svg';
import success from '@moedelo/frontend-core-react/icons/success.m.svg';
import error from '@moedelo/frontend-core-react/icons/error.m.svg';
import { pluralNoun } from '@moedelo/frontend-core-v2/helpers/PluralHelper';
import { toFinanceString } from '@moedelo/frontend-core-v2/helpers/converter';
import TabGroup from '@moedelo/frontend-core-react/components/TabGroup';
import P from '@moedelo/frontend-core-react/components/P';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import BalanceReconciliationStatusEnum from '../../../../../../enums/newMoney/BalanceReconciliationStatusEnum';
import balanceReconciliationService from '../../../../../../services/newMoney/balanceReconciliationService';
import ColorEnum from '../../enums/ColorEnum';
import StatusUIEnum from '../../enums/StatusUIEnum';
import style from '../../style.m.less';

const cn = classnames.bind(style);
const ListTypeEnum = {
    delete: `Delete`,
    insert: `Insert`,
    salaryDelete: `SalaryDelete`,
    salaryInsert: `SalaryInsert`
};
const TabsEnum = {
    Edit: 0,
    View: 1
};

class BalanceReconciliationDialog extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isAutoDeleteListOpen: true,
            isAutoInsertListOpen: true,
            isSalaryDeleteListOpen: true,
            isSalaryInsertListOpen: true,
            isSalaryOpen: true,
            confirmDialogVisible: false,
            canShowControls: false,
            currentTab: TabsEnum.Edit
        };
    }

    componentDidUpdate(prevProps) {
        if (prevProps.status !== this.props.status) {
            this.setState({
                canShowControls: this.props.status === BalanceReconciliationStatusEnum.Ready
            });
        }
    }

    onConfirm = () => {
        const { onConfirm } = this.props;

        onConfirm && onConfirm();
    }

    onClose = () => {
        const { onCancel } = this.props;

        this.closeConfirmCancelDialog();

        onCancel && onCancel();
    }

    onDelete = ({ Id, type }) => {
        const {
            onDeleteFromMissing,
            onDeleteFromExcess
        } = this.props;

        if (type === ListTypeEnum.insert) {
            onDeleteFromMissing && onDeleteFromMissing(Id);
        } else {
            onDeleteFromExcess && onDeleteFromExcess(Id);
        }
    }

    getOperationsDataList = ({ type, list }) => {
        const { loading, status, currency } = this.props;

        return list.map(operation => {
            const readOnly = status === BalanceReconciliationStatusEnum.Completed || [ListTypeEnum.salaryDelete, ListTypeEnum.salaryInsert].includes(type);
            let sumClass = `operationSumGreen`;
            let plusMinus = `+`;

            if (operation.IsOutgoing) {
                sumClass = `operationSumRed`;
                plusMinus = `–`;
            }

            const sumString = `${plusMinus} ${toFinanceString(operation.Sum)} ${getSymbol(currency)}`;

            return (
                <TableRow key={`${type}_${operation.Id}`} onClickRow={() => {}} className={cn(`operation`)}>
                    <div className={grid.col_4}>
                        {dateHelper(operation.Date).format(`DD.MM.YYYY`)}
                    </div>
                    <div className={cn(grid.col_7, `operationKontragent`)} title={operation.KontragentName}>
                        {operation.KontragentName}
                    </div>
                    <div className={cn(grid.col_7, `operationDescription`)} title={operation.Description}>
                        {operation.Description}
                    </div>
                    <div className={cn(grid.col_5, `operationSum`, sumClass)}>
                        {sumString}
                    </div>
                    {!readOnly && <div className={cn(grid.col_2, `operationDelete`)}>
                        <Link disabled={loading} onClick={() => this.onDelete({ Id: operation.Id, type })}>
                            { svgIconHelper.getJsx({ name: `clear`, className: cn(`operationDeleteIcon`) }) }
                        </Link>
                    </div>}
                </TableRow>
            );
        });
    }

    getToggleLinkText = type => {
        switch (type) {
            case ListTypeEnum.salaryInsert:
                return `Есть в банке, нет в сервисе`;
            case ListTypeEnum.salaryDelete:
                return `Есть в сервисе, нет в банке`;
            case ListTypeEnum.insert:
                return `Загрузить из банка`;
            case ListTypeEnum.delete:
                return `Удалить лишние`;
            default: return ``;
        }
    }

    getControlLink = ({ type, length, isOpen }) => {
        const linkText = this.getToggleLinkText(type);

        return (
            <div className={style.controlLink}>
                <Link
                    color={[ListTypeEnum.delete, ListTypeEnum.salaryDelete].includes(type) && `red`}
                    type={`modal`}
                    onClick={() => this.toggleList(type)}
                >
                    {linkText}
                </Link>
                <span className={cn(`operationsCount`, `operationsCount${type}`)}>{length}</span>
                <Arrow
                    direction={isOpen ? `up` : `down`}
                    className={cn(`dropDownLink`, `dropDownLink${type}`)}
                    onClick={() => this.toggleList(type)}
                />
            </div>
        );
    }

    getMessage = ({ type, length }) => {
        switch (type) {
            case ListTypeEnum.insert: {
                const missingPluralIntro = pluralNoun(length, `отсутствует`, `отсутствуют`, `отсутствуют`);
                const missingPluralText = pluralNoun(length, `операция, она будет загружена`, `операции, они будут загружены`, `операций, они будут загружены`);

                return `В сервисе ${missingPluralIntro} ${length} ${missingPluralText}`;
            }

            case ListTypeEnum.delete: {
                const findWord = pluralNoun(length, `Найдена`, `Найдены`, `Найдены`);
                const partOfMessage = pluralNoun(length, `лишняя операция в сервисе, она будет удалена`, `лишние операции в сервисе, они будут удалены`, `лишних операций в сервисе, они будут удалены`);

                return `${findWord} ${length} ${partOfMessage}`;
            }

            default: return ``;
        }
    }

    getReadyMessage = () => {
        const editableOperationsExists = this.isEditableOperationExists();
        const readOnlyOperationExists = this.isReadonlyOperationExists();

        if (editableOperationsExists && readOnlyOperationExists) {
            return (
                <Fragment>
                    {`На вкладке "Импортировать/удалить" Вы можете ознакомиться и отредактировать списки операций, и внести изменения, нажав кнопку Подтвердить.`}<br />
                    {`На вкладке "Просмотреть" отражены операции по выплатам физ. лицам. Внести изменения нужно самостоятельно в разделе "Деньги".`}
                </Fragment>
            );
        }

        if (editableOperationsExists && !readOnlyOperationExists) {
            return `Вы можете ознакомиться и отредактировать списки операций, и внести изменения, нажав кнопку Подтвердить.`;
        }

        if (!editableOperationsExists && readOnlyOperationExists) {
            return `На вкладке "Просмотреть" Вы можете ознакомиться со списками операций по выплатам физ. лицам. Внести изменения нужно самостоятельно в разделе "Деньги".`;
        }

        return ``;
    }

    isEditableOperationExists = () => {
        const { excessOperations, missingOperations } = this.props;

        return excessOperations?.length > 0 || missingOperations?.length > 0;
    }

    isReadonlyOperationExists = () => {
        const { excessSalaryOperations, missingSalaryOperations } = this.props;

        return excessSalaryOperations?.length > 0 || missingSalaryOperations?.length > 0;
    }

    toggleList = type => {
        const {
            isAutoDeleteListOpen, isAutoInsertListOpen, isSalaryDeleteListOpen, isSalaryInsertListOpen
        } = this.state;
        let nextState = {};

        switch (type) {
            case ListTypeEnum.delete: {
                nextState = {
                    isAutoDeleteListOpen: !isAutoDeleteListOpen
                };

                break;
            }

            case ListTypeEnum.insert: {
                nextState = {
                    isAutoInsertListOpen: !isAutoInsertListOpen
                };

                break;
            }

            case ListTypeEnum.salaryDelete: {
                nextState = {
                    isSalaryDeleteListOpen: !isSalaryDeleteListOpen
                };

                break;
            }

            case ListTypeEnum.salaryInsert: {
                nextState = {
                    isSalaryInsertListOpen: !isSalaryInsertListOpen
                };

                break;
            }
        }

        this.setState(nextState);
    }

    downloadXlsReconciliation = async () => {
        const { sessionId, excludeOperationsIds } = this.props;
        mrkStatService.sendEventWithoutInternalUser({
            event: `download_xls_bank_reconciliation_page_click_link`
        });

        await balanceReconciliationService.downloadReconciliationXls({ sessionId, excludeOperationsIds });
    }

    openCancelConfirmDialog = () => {
        this.setState({ confirmDialogVisible: true });
    }

    closeConfirmCancelDialog = () => {
        this.setState({ confirmDialogVisible: false });
    }

    isLoading = () => {
        const {
            loading, missingSalaryOperations, status
        } = this.props;

        if (status === BalanceReconciliationStatusEnum.NotFound) {
            return false;
        }

        return loading || missingSalaryOperations === null;
    }

    renderOperationLists = () => {
        switch (this.state.currentTab) {
            case 0: {
                const {
                    missingOperations,
                    excessOperations
                } = this.props;
                const { isAutoInsertListOpen, isAutoDeleteListOpen } = this.state;

                if (missingOperations.length || excessOperations.length) {
                    return (
                        <Fragment>
                            { this.renderList({ type: ListTypeEnum.insert, list: missingOperations, isOpen: isAutoInsertListOpen }) }
                            { this.renderList({ type: ListTypeEnum.delete, list: excessOperations, isOpen: isAutoDeleteListOpen }) }
                        </Fragment>
                    );
                }

                return <P className={style.textResult}>Нет операций.</P>;
            }

            case 1: {
                const { excessSalaryOperations, missingSalaryOperations } = this.props;
                const { isSalaryInsertListOpen, isSalaryDeleteListOpen } = this.state;

                if (!excessSalaryOperations.length && !missingSalaryOperations.length && this.state.currentTab === TabsEnum.View) {
                    this.setState({
                        currentTab: TabsEnum.Edit
                    });

                    return null;
                }

                return (
                    <Fragment>
                        { this.renderList({ type: ListTypeEnum.salaryInsert, list: missingSalaryOperations, isOpen: isSalaryInsertListOpen }) }
                        { this.renderList({ type: ListTypeEnum.salaryDelete, list: excessSalaryOperations, isOpen: isSalaryDeleteListOpen }) }
                    </Fragment>
                );
            }

            default: return null;
        }
    }

    renderConfirmationModal = () => {
        return (
            <Modal
                header={`Вы точно хотите закрыть сверку с банком?`}
                onClose={this.onClose}
                canClose={false}
                width={`575px`}
                visible={this.state.confirmDialogVisible}
            >
                <p>Предлагаемые изменения не будут внесены в сервис. Вы уверены?</p>

                <ElementsGroup>
                    <Button onClick={this.onClose}>Подтвердить</Button>
                    <Link onClick={this.closeConfirmCancelDialog}>Отмена</Link>
                </ElementsGroup>
            </Modal>
        );
    }

    renderControls = () => {
        const { currentTab, canShowControls } = this.state;

        if (currentTab === TabsEnum.View || !canShowControls) {
            return null;
        }

        const { loading, disabled } = this.props;

        return (
            <ElementsGroup className={style.controls}>
                <Button loading={loading} disabled={disabled} onClick={this.onConfirm}>Подтвердить</Button>
                <Link disabled={loading || disabled} onClick={this.openCancelConfirmDialog}>Отмена</Link>
            </ElementsGroup>
        );
    }

    renderList = ({ type, list = [], isOpen }) => {
        if (!list?.length) {
            return null;
        }

        const msg = this.getMessage({ type, length: list.length });

        return (
            <div className={cn(`containerItem`)}>
                {this.getControlLink({ type, length: list.length, isOpen })}
                <Transition in={isOpen} timeout={0}>
                    {state => {
                        return (
                            <div className={cn(`operationsContainer`, `operationsContainer${state}`)}>
                                {msg.length > 0 && <div className={cn(`operationsContainerDescription`)}>{msg}</div>}
                                <ul className={cn(`operationsList`)}>
                                    {this.getOperationsDataList({ type, list })}
                                </ul>
                            </div>
                        );
                    }}

                </Transition>
            </div>
        );
    }

    renderTabGroup = () => {
        const { missingSalaryOperations, excessSalaryOperations } = this.props;
        const tooltipMessage = this.getReadyMessage();
        const tabs = [
            { text: `Импортировать/удалить`, value: 0 }
        ];

        if ([...missingSalaryOperations, ...excessSalaryOperations].length) {
            tabs.push({ text: `Просмотреть`, value: 1 });
        }

        if (tooltipMessage) {
            tabs.push({
                text: svgIconHelper.getJsx({ file: info, className: cn(style.icon, style.tooltipIcon) }),
                value: -1,
                tooltip: { content: tooltipMessage, position: Position.top, width: 400 },
                disabled: true
            });
        }

        return (
            <div className={grid.wrapper}>
                <div className={cn(grid.row, `tabGroupContainer`)}>
                    <div className={cn(grid.col_19)}>
                        <TabGroup
                            tabs={tabs}
                            onChange={({ currentTab }) => this.setState({ currentTab })}
                            currentTab={this.state.currentTab}
                        />
                    </div>
                    <div className={cn(grid.col_5, `downloadLinkContainer`)}>
                        <a onClick={this.downloadXlsReconciliation} className={style.downloadXlsLink} role="presentation">
                            Скачать результаты сверки <span className={style.downloadXlsLinkIcon}>xls</span>
                        </a>
                    </div>
                </div>
            </div>
        );
    }

    renderStatus = (status, message) => {
        const statusIcons = {
            [StatusUIEnum.Success]: success,
            [StatusUIEnum.Warning]: info,
            [StatusUIEnum.Error]: error
        };

        return <div className={classnames(style.withLabelWrapper)}>
            <div className={style.iconWrapper}>{getJsx({ file: statusIcons[status], color: ColorEnum[status], className: style.icon })}</div>
            <P><b>{message}</b></P>
        </div>;
    }

    renderContent = () => {
        const { status } = this.props;
        const isAnyOperationExists = this.isReadonlyOperationExists() || this.isEditableOperationExists();
        const notFoundMessage = `Расхождений с банком не найдено.`;

        if (this.isLoading()) {
            return (
                <div className={style.loaderContainer}>
                    <Loader width={150} />
                </div>
            );
        }

        switch (status) {
            case BalanceReconciliationStatusEnum.Ready: {
                const msg = this.getReadyMessage();
                const variancesMsg = isAnyOperationExists ?
                    <Fragment>У Вас имеются расхождения в данных банка и сервиса «Мое Дело».<br />{msg}</Fragment> :
                    notFoundMessage;

                return (
                    <Fragment>
                        {this.renderStatus(isAnyOperationExists ? StatusUIEnum.Error : StatusUIEnum.Success, variancesMsg)}
                        {this.renderTabGroup()}
                        {this.renderOperationLists()}
                        {this.renderControls()}
                    </Fragment>
                );
            }

            case BalanceReconciliationStatusEnum.Completed: {
                const completeMessage = isAnyOperationExists ?
                    `Сверка произведена, Вы можете ознакомиться с обработанными операциями.` :
                    notFoundMessage;

                return (
                    <Fragment>
                        {this.renderStatus(StatusUIEnum.Success, completeMessage)}
                        {this.renderTabGroup()}
                        {this.renderOperationLists()}
                    </Fragment>
                );
            }

            case BalanceReconciliationStatusEnum.InProgress: {
                return this.renderStatus(StatusUIEnum.Warning, `Сверка с банком в процессе. Результаты поступят в ближайшее время. Зайдите позже или обновите страницу.`);
            }

            case BalanceReconciliationStatusEnum.Error: {
                return this.renderStatus(StatusUIEnum.Error, `При проведении сверки произошла ошибка.`);
            }

            case BalanceReconciliationStatusEnum.None: {
                return this.renderStatus(StatusUIEnum.Warning, `На данный момент результатов сверки нет.`);
            }

            case BalanceReconciliationStatusEnum.NotFound: {
                return this.renderStatus(StatusUIEnum.Warning, `По данному расчетному счету сверок не проводилось. Вы можете запустить сверку вручную в разделе "Деньги" или нажав на кнопку "Запросить сверку"`);
            }

            case BalanceReconciliationStatusEnum.FatalError: {
                return this.renderStatus(StatusUIEnum.Error, `Произошла неизвестная ошибка. Попробуйте обновить страницу или обратитесь в тех поддержку.`);
            }

            default: return null;
        }
    }

    render() {
        return (
            <Fragment>
                <div className={cn(`container`)}>
                    {this.renderContent()}
                </div>
                {this.renderConfirmationModal()}
            </Fragment>
        );
    }
}

BalanceReconciliationDialog.propTypes = {
    excessOperations: PropTypes.arrayOf(PropTypes.object),
    missingOperations: PropTypes.arrayOf(PropTypes.object),
    excessSalaryOperations: PropTypes.arrayOf(PropTypes.object),
    missingSalaryOperations: PropTypes.arrayOf(PropTypes.object),
    currency: PropTypes.string,
    onConfirm: PropTypes.func,
    onCancel: PropTypes.func,
    onDeleteFromExcess: PropTypes.func,
    onDeleteFromMissing: PropTypes.func,
    status: PropTypes.oneOf(BalanceReconciliationStatusEnum),
    loading: PropTypes.bool,
    disabled: PropTypes.bool,
    sessionId: PropTypes.number,
    excludeOperationsIds: PropTypes.arrayOf(PropTypes.number)
};

export default BalanceReconciliationDialog;
