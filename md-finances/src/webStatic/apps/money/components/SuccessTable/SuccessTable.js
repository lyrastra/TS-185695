import React from 'react';
import classnames from 'classnames/bind';
import TableOverFooter from '@moedelo/frontend-core-react/components/table/TableOverFooter';
import Link from '@moedelo/frontend-core-react/components/Link/Link';
import { pluralNoun } from '@moedelo/frontend-core-v2/helpers/PluralHelper';
import TableLoader from '@moedelo/frontend-core-react/components/table/TableLoader';
import { observer, inject } from 'mobx-react';
import PropTypes from 'prop-types';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import Checkbox from '@moedelo/frontend-core-react/components/Checkbox';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import { Type } from '@moedelo/frontend-core-react/components/buttons/enums';
import gridStyle from '@moedelo/frontend-core-v2/styles/grid.m.less';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import storage from '../../../../helpers/newMoney/storage';
import RemoveOperationDialog from '../../components/RemoveOperationButton/RemoveOperationDialog';
import OperationList from '../OperationsList';
import Row from './components/Row';
import SpoilerType from '../Spoiler/SpoilerType';
import Spoiler from '../Spoiler/Spoiler';
import ClosedDocumentsStatusEnum from '../../../../enums/ClosedDocumentsStatusEnum';
import MoneyOperationService from '../../../../services/newMoney/moneyOperationService';
import DownloadOperationButton from '../DownloadOperationButton';
import RemoveOperationButton from '../RemoveOperationButton';
import MassTaxSystemChangeModal from '../MassTaxSystemChangeModal';
import autoPayIcon from '../../../../icons/autoPay.m.svg';
import { onApproveOperation } from '../../../../helpers/newMoney/operationActionsHelper';
import { setApproveByIds } from '../../../../services/approvedService';
import ApproveOperationButton from '../ApproveOperationButton/ApproveOperationButton';

import style from './style.m.less';

const cn = classnames.bind(style);
const pageCount = 20;
const time = 350;

let checkedAll = false;

@inject(`massChangeTaxSystemStore`)
@observer
class SuccessTable extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            dialogType: ClosedDocumentsStatusEnum.NoMatter,
            onDeleteDialogShown: false,
            singleOperationForDelete: null
        };
        this.store = props.store;
        this.commonDataStore = props.commonDataStore;
        this.massChangeTaxSystemStore = props.massChangeTaxSystemStore;
    }

    onClickOKButton = e => {
        storage.save(`Scroll`, window.scrollY);

        e.stopPropagation();
        e.preventDefault();

        setTimeout(this.removeTable, time);
    };

    onClickRow = operation => {
        storage.save(`Scroll`, window.scrollY);
        storage.save(`successOperation`, true);

        this.props.onClickRow(operation);
    };

    onClickSpoiler = () => {
        const { toggleTable } = this.props;

        toggleTable && toggleTable(`success`);
    };

    onClickShowMoreButton = () => {
        checkedAll = false;

        storage.save(`Scroll`, window.scrollY);

        this.store.loading = true;
        this.loadOperations();
    };

    onSelectRow = () => {
        const { checkedOperationList } = this.store;
        storage.save(`Scroll`, window.scrollY);

        this.massChangeTaxSystemStore.setCheckedOperations(checkedOperationList);
        this.setDialogTypes(checkedOperationList);
    };

    onChangeSelectAll = ({ checked }) => {
        storage.save(`Scroll`, window.scrollY);

        this.store.toggleSelectAll(checked);
        this.massChangeTaxSystemStore.setCheckedOperations(this.store.checkedOperationList);
        this.setDialogTypes(this.store.checkedOperationList);
    };

    onDialogElementClick = () => {
        storage.save(`Scroll`, window.scrollY);

        this.setState({
            onDeleteDialogShown: false,
            singleOperationForDelete: null
        });
    };

    onApprove = ({ Id }) => {
        onApproveOperation({ Id });
        this.props.onChangeList && this.props.onChangeList({ reset: true });
    }

    setMassApprove = ({ Ids }) => {
        setApproveByIds({ Ids });
        this.props.onChangeList && this.props.onChangeList({ reset: true });
    }

    getSpoiler = () => {
        const disabledButton = !this.commonDataStore.hasAccessToMoneyEdit;
        const { totalCount, isOpen, operations } = this.store;
        const { dialogType } = this.state;
        const checkedCount = this.store.checkedOperationList.length;
        const allSelected = operations.length > 0 && operations.length === checkedCount;
        const checkedOperations = this.store.checkedOperationList;
        const isDisableApproved = checkedOperations.every(el => el.isApproved !== false);
        const isCanMassApprove = !checkedOperations.every(el => el.isApproved === undefined);

        if (!this.store.checkedOperationList.length) {
            return <Spoiler
                spoilerType={SpoilerType.Success}
                onClickButton={this.onClickOKButton}
                operationStatusText={`Проведено в учете`}
                statusText={`Обработано успешно`}
                onClickOpenSpoiler={this.onClickSpoiler}
                totalCount={totalCount}
                isOpen={isOpen}
                icon={`success`}
                buttonText={`Ок`}
                disabledButton={disabledButton}
            />;
        }

        return <div className={cn(`tableHead`)}>
            <div className={cn(gridStyle.col_1, `cell`, `checkbox__label`)}>
                <Checkbox
                    ref={ref => {
                        this.checkAll = ref;
                    }}
                    onChange={this.onChangeSelectAll}
                    checked={allSelected || checkedAll}
                    indeterminate={!allSelected && this.store.checkedOperationList.length}
                />
            </div>
            <div
                className={cn(gridStyle.col_2, `cell`, `checkedCount`)}
            >Выбрано: {this.store.checkedOperationList.length}</div>
            <div className={cn(gridStyle.col_14, `cell`, `cell__action`)}>
                <RemoveOperationButton
                    operations={this.store.checkedOperationList}
                    onClick={this.removeRows}
                    dialogType={dialogType}
                    commonDataStore={this.commonDataStore}
                />
                {this.renderMassTaxSystemChangeButton()}
                <DownloadOperationButton
                    checkedOperations={this.store.checkedOperationList}
                    dropped={false}
                />
                {isCanMassApprove && <ApproveOperationButton
                    isDisableApproved={isDisableApproved}
                    setMassApprove={this.setMassApprove}
                    checkedOperations={this.store.checkedOperationList}
                />}
            </div>
            <div className={cn(gridStyle.col_7, `cell`)} />
        </div>;
    };

    getTableBody = () => {
        const {
            loaded,
            operations,
            tableCount,
            totalCount,
            isOpen
        } = this.store;

        return <div
            ref={c => {
            this.tableBody = c;
        }}
            className={cn(`tableBody`, { 'tableBody--closed': !isOpen })}
        >
            {!loaded && this.loader()}
            <OperationList
                operations={operations}
                tableCount={tableCount}
                onSelect={this.onSelectRow}
                onClick={this.onClickRow}
                onDelete={this._showRemoveOperationsDialog}
                commonDataStore={this.commonDataStore}
                onApprove={this.onApprove}
                Row={Row}
            />
            <TableOverFooter
                className={cn(`tableOverFooter`)}
                showButton={tableCount !== 0 && tableCount < totalCount}
                buttonText={`Показать еще ${pluralNoun(Math.min(totalCount - tableCount, pageCount), `операцию`, `операции`, `операций`, true)}`}
                text={`Отображено: ${tableCount} из ${pluralNoun(totalCount, `операции`, `операций`, `операций`, true)}`}
                onClick={this.onClickShowMoreButton}
                buttonLoading={false}
            />
        </div>;
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

    uncheckOperations = () => {
        this.setState({
            singleOperationForDelete: null
        });
        this.store.uncheckAllOperation();
    };

    removeRows = () => {
        storage.save(`Scroll`, window.scrollY);

        const checkedOperations = this.state.operationsForDeleting;
        const operationIds = checkedOperations.map(o => {
            return o.DocumentBaseId;
        });

        return MoneyOperationService.deleteOperation(operationIds)
            .then(() => {
                NotificationManager.show({
                    message: checkedOperations.length > 1 ? `Документы удалены` : `Документ удален`,
                    type: `info`,
                    duration: 2000
                });

                this.props.onChangeList && this.props.onChangeList({ reset: true });
            });
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

    _deleteOperationsHandle = () => {
        const { singleOperationForDelete } = this.state;
        const { onChangeList } = this.props;

        if (singleOperationForDelete) {
            MoneyOperationService.deleteOperation([singleOperationForDelete.DocumentBaseId])
                .then(() => {
                    this.onDialogElementClick();

                    onChangeList && onChangeList({ reset: true });
                });
        }
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

    _openClosedPeriod = () => {
        this.onDialogElementClick();

        return MoneyOperationService.openClosedPeriod(this._getDate())
            .then(() => {
                window.location.reload();
            });
    };

    _getDate = () => {
        return new Date(this.state.singleOperationForDelete.Date);
    };

    _notClosedStatus = () => {
        return this.state.dialogType !== ClosedDocumentsStatusEnum.Completely;
    };

    removeConfirmationModal = () => {
        return (
            <RemoveOperationDialog
                header={this._getHeaders()}
                visible={this.state.onDeleteDialogShown}
                description={this._getDescription}
                disabled={this._notClosedStatus}
                onButtonClick={this._deleteOperationsHandle}
                onClose={this.onDialogElementClick}
            />
        );
    };

    loadOperations = (options = { reset: false }) => {
        return this.store.loadOperations(options);
    };

    loader = () => {
        return <TableLoader />;
    };

    removeTable = () => {
        storage.save(`Scroll`, window.scrollY);
        storage.save(`successTableIsOpen`, false);

        const { onChangeList, closeTable } = this.props;
        this.store.operations = [];
        this.store.loaded = true;
        this.store.isOKClicked = true;

        return this.store.approveOperations()
            .then(() => {
                closeTable && closeTable(`success`);
                onChangeList && onChangeList({ reset: true });

                NotificationManager.show({
                    message: `Операции перенесены в основную таблицу`,
                    type: `success`,
                    duration: 4000
                });
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

    renderMassTaxSystemChangeButton = () => {
        const { canShowChangeButton, setModalVisibility } = this.massChangeTaxSystemStore;

        if (!canShowChangeButton || !this.commonDataStore.hasAccessToMoneyEdit) {
            return null;
        }

        return (
            <Button
                onClick={() => setModalVisibility(true)}
                type={Type.Panel}
            >
                {svgIconHelper.getJsx({ file: autoPayIcon })}Сменить СНО
            </Button>
        );
    };

    render() {
        const { totalCount } = this.store;

        if (!totalCount) {
            return null;
        }

        return (
            <div className={cn(`table`)}>
                {this.getSpoiler()}
                {this.getTableBody()}
                {this.removeConfirmationModal()}
                <MassTaxSystemChangeModal onSuccess={this.uncheckOperations} />
            </div>
        );
    }
}

SuccessTable.propTypes = {
    onClickRow: PropTypes.func,
    store: PropTypes.object.isRequired,
    commonDataStore: PropTypes.object.isRequired,
    onChangeList: PropTypes.func,
    toggleTable: PropTypes.func,
    closeTable: PropTypes.func,
    massChangeTaxSystemStore: PropTypes.object
};

export default SuccessTable;
