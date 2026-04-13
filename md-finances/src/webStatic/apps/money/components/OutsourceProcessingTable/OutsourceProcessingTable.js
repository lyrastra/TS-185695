import React from 'react';
import PropTypes from 'prop-types';
import { observer, inject } from 'mobx-react';
import classnames from 'classnames/bind';
import TableOverFooter from '@moedelo/frontend-core-react/components/table/TableOverFooter';
import Link from '@moedelo/frontend-core-react/components/Link/Link';
import { pluralNoun } from '@moedelo/frontend-core-v2/helpers/PluralHelper';
import TableLoader from '@moedelo/frontend-core-react/components/table/TableLoader';
import TableRow from '@moedelo/frontend-core-react/components/table/TableRow';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import Checkbox from '@moedelo/frontend-core-react/components/Checkbox';
import gridStyle from '@moedelo/frontend-core-v2/styles/grid.m.less';
import TableUnderHeader from '@moedelo/frontend-core-react/components/table/TableUnderHeader';
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
import Filter from './components/Filter';
import FilterTagsHelper from '../../../../helpers/newMoney/FilterTagsHelper';
import FilterTags from '../FilterTags';
import style from './style.m.less';

const cn = classnames.bind(style);
const pageCount = 20;

let checkedAll = false;

@inject(`massChangeTaxSystemStore`)
@observer
class OutsourceProcessingTable extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            dialogType: ClosedDocumentsStatusEnum.NoMatter,
            onDeleteDialogShown: false,
            singleOperationForDelete: null,
            isFilterVisible: false,
            filters: {}
        };
        this.store = props.store;
        this.commonDataStore = props.commonDataStore;
        this.massChangeTaxSystemStore = props.massChangeTaxSystemStore;
    }

    onClickRow = operation => {
        storage.save(`Scroll`, window.scrollY);
        storage.save(`outsourceProcessingOperation`, true);

        this.props.onClickRow(operation);
    };

    onClickSpoiler = () => {
        const { toggleTable } = this.props;

        toggleTable && toggleTable(`outsourceProcessing`);
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

    getSpoiler = () => {
        const disabledButton = !this.commonDataStore.hasAccessToMoneyEdit;
        const { totalCount, isOpen, operations } = this.store;
        const { dialogType } = this.state;
        const checkedCount = this.store.checkedOperationList.length;
        const allSelected = operations.length > 0 && operations.length === checkedCount;

        if (!this.store.checkedOperationList.length) {
            const tagsCount = FilterTagsHelper.getTags({ filter: this.state.filters })?.length;
            const text = tagsCount || `Фильтры`;

            return <div className={style.headerWrapper}>
                <Spoiler
                    spoilerType={SpoilerType.OutsourceProcessing}
                    operationStatusText={``}
                    statusText={`Операции в обработке`}
                    onClickOpenSpoiler={this.onClickSpoiler}
                    totalCount={totalCount}
                    isOpen={isOpen}
                    disabledButton={disabledButton}
                    hasFilterButton={false}
                    onClickButton={() => {
                        this.setState({ isFilterVisible: !this.state.isFilterVisible });
                    }}
                    buttonText={text}
                    icon={`filter`}
                />
                {this.state.isFilterVisible && <Filter
                    moneySourceStore={this.props.moneySourceStore}
                    closeFilter={() => {
                        this.setState({ isFilterVisible: false });
                    }}
                    filters={this.state.filters}
                    setFilters={value => this.setState({ filters: value }, () => {
                        this.loadOperations({ reset: true });
                    })}
                />}
            </div>;
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
                <DownloadOperationButton
                    checkedOperations={this.store.checkedOperationList}
                    dropped={false}
                />
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
            className={cn(`tableBody`, { tableBodyClosed: !isOpen })}
        >
            {this.underHeader()}
            {!loaded && this.loader()}
            {(!operations?.length && loaded) && this.renderEmpty()}
            <OperationList
                operations={operations}
                tableCount={tableCount}
                onSelect={this.onSelectRow}
                onClick={this.onClickRow}
                onDelete={this._showRemoveOperationsDialog}
                commonDataStore={this.commonDataStore}
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
                    }
                }

                this.setState({ operationsForDeleting });
                this.checkClosedPeriod(dialogTypes);
            });
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

                this.props.onChangeList && this.props.onChangeList({ reset: true, filter: this.state.filters });
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

                    onChangeList && onChangeList({ reset: true, filter: this.state.filters });
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
        return this.store.loadOperations({ ...options, filter: this.state.filters });
    };

    loader = () => {
        return <TableLoader />;
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

    clearTag = tags => {
        const filter = Object.assign({}, this.state.filters);

        tags.forEach(tag => delete filter[tag]);

        this.setState({ filters: filter }, () => {
            this.loadOperations({ reset: true });
        });
    };

    underHeader = () => {
        const { filters } = this.state;
        const tags = FilterTagsHelper.getTags({
            filter: filters,
            clearTag: clearTag => this.clearTag(clearTag)
        });

        if (tags.length) {
            return <TableUnderHeader>
                <FilterTags onClearFilter={() => {
                    this.setState({ filters: {} }, () => {
                        this.loadOperations({ reset: true });
                    });
                }}
                >{tags}</FilterTags>
            </TableUnderHeader>;
        }

        return null;
    };

    renderEmpty = () => {
        return <TableRow>
            Список операций пуст
        </TableRow>;
    };

    render() {
        const { totalCount } = this.store;

        if (!totalCount && !Object.values(this.state.filters).length) {
            return null;
        }

        return (
            <div className={cn(`table`)}>
                {this.getSpoiler()}
                {this.getTableBody()}
                {this.removeConfirmationModal()}
            </div>
        );
    }
}

OutsourceProcessingTable.propTypes = {
    onClickRow: PropTypes.func,
    store: PropTypes.object.isRequired,
    commonDataStore: PropTypes.object.isRequired,
    onChangeList: PropTypes.func,
    toggleTable: PropTypes.func,
    massChangeTaxSystemStore: PropTypes.object,
    moneySourceStore: PropTypes.object
};

export default OutsourceProcessingTable;
