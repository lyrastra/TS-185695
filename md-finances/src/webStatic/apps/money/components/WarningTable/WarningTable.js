import React from 'react';
import { toJS } from 'mobx';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import Link from '@moedelo/frontend-core-react/components/Link/Link';
import { observer } from 'mobx-react';
import { pluralNoun } from '@moedelo/frontend-core-v2/helpers/PluralHelper';
import TableOverFooter from '@moedelo/frontend-core-react/components/table/TableOverFooter';
import TableLoader from '@moedelo/frontend-core-react/components/table/TableLoader';
import storage from '../../../../helpers/newMoney/storage';
import RemoveOperationDialog from '../../components/RemoveOperationButton/RemoveOperationDialog';
import Row from './components/Row';
import style from './style.m.less';
import SpoilerType from '../Spoiler/SpoilerType';
import Spoiler from '../Spoiler/Spoiler';
import ClosedDocumentsStatusEnum from '../../../../enums/ClosedDocumentsStatusEnum';
import MoneyOperationService from '../../../../services/newMoney/moneyOperationService';
import { getEditUrlHash } from '../../../../helpers/newMoney/operationUrlHelper';

const cn = classnames.bind(style);
const pageCount = 20;

@observer
export default class WarningTable extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            dialogType: ClosedDocumentsStatusEnum.NoMatter,
            onDeleteDialogShown: false,
            singleOperationForDelete: null
        };
        this.store = props.store;
        this.commonDataStore = props.commonDataStore;
    }

    onDialogElementClick = () => {
        storage.save(`Scroll`, window.scrollY);

        this.setState({
            onDeleteDialogShown: false
        });
    };

    onClickRow = operation => {
        storage.save(`Scroll`, window.scrollY);

        this.props.onClickRow(operation);
    };

    onClickShowMoreButton = () => {
        if (!this.store.loading) {
            storage.save(`Scroll`, window.scrollY);

            return this.loadOperations();
        }

        return false;
    };

    onChangeList = (options = { resetStore: false }) => {
        const { onChangeList } = this.props;
        let reset = true;

        if (options.resetStore) {
            reset = false;
            this.store.resetStore();
        }

        onChangeList && onChangeList({ reset });
    };

    getSpoiler = () => {
        const { totalCount, isOpen } = this.store;
        const disabledButton = !this.commonDataStore.hasAccessToMoneyEdit;

        return <Spoiler
            spoilerType={SpoilerType.Warning}
            onClickButton={() => {
                this._showRemoveOperationsDialog(null);
            }}
            operationStatusText={`Не проведены`}
            statusText={`Нераспознанные операции`}
            onClickOpenSpoiler={this.toggleTable}
            totalCount={totalCount}
            isOpen={isOpen}
            icon={`remove`}
            buttonText={`Удалить`}
            disabledButton={disabledButton}
            manual={{
                manualLink: `https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dengi-prof/neraspoznannye-operacii`,
                manualLinkText: `Как исправить нераспознанные операции?`
            }}
        />;
    };

    getHeaders = () => {
        return `Удаление`;
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

    toggleTable = () => {
        const { toggleTable } = this.props;

        toggleTable && toggleTable(`warning`);
    };

    deleteOperation = (operation, callback = null) => {
        storage.save(`Scroll`, window.scrollY);

        const id = operation.DocumentBaseId;

        this.store.deleteOperation(id).then(() => {
            this.onChangeList();
            callback && callback();

            NotificationManager.show({
                message: `Нераспознанная операция успешно удалена`,
                type: `success`,
                duration: 4000
            });
        });
    };

    _deleteOperationsHandle = () => {
        const { singleOperationForDelete } = this.state;

        if (singleOperationForDelete) {
            this.deleteOperation(singleOperationForDelete, () => {
                this.setState({
                    singleOperationForDelete: null,
                    onDeleteDialogShown: false
                });
            });
        } else {
            this.deleteAllOperations();
        }
    };

    deleteAllOperations = () => {
        storage.save(`Scroll`, window.scrollY);
        storage.save(`warningTableIsOpen`, false);

        this.store.deleteAllOperations().then(() => {
            const { closeTable } = this.props;

            closeTable && closeTable(`warning`);

            this.onChangeList({ resetStore: true });

            NotificationManager.show({
                message: `Все нераспознанные операции были удалены.`,
                type: `success`,
                duration: 4000
            });

            this.setState({ onDeleteDialogShown: false });
        });
    };

    mergeOperation = operation => {
        storage.save(`Scroll`, window.scrollY);

        this.store.mergeOperation(operation).then(() => {
            this.onChangeList();

            NotificationManager.show({
                message: `Дата операции в сервисе успешно обновлена`,
                type: `success`,
                duration: 4000
            });
        });
    };

    importOperation = operation => {
        storage.save(`Scroll`, window.scrollY);

        this.store.importOperation(operation).then(() => {
            this.onChangeList();

            NotificationManager.show({
                message: `Операция успешно сохранена в сервисе`,
                type: `success`,
                duration: 4000
            });
        });
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

    loadOperationsCount = () => {
        this.store.loadOperationsCount();
    };

    loadOperations = (options = { reset: false }) => {
        return this.store.loadOperations(options);
    };

    _getDescription = () => {
        const status = ClosedDocumentsStatusEnum;
        const { dialogType, singleOperationForDelete } = this.state;
        const questionText = `Вы уверены, что хотите удалить ${singleOperationForDelete ? `эту операцию` : `все нераспознанные операции`}?`;

        if (dialogType < status.Partly) {
            return <p>{questionText}</p>;
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

    _showRemoveOperationsDialog = (singleOperationForDelete = null) => {
        storage.save(`Scroll`, window.scrollY);
        const operationsList = singleOperationForDelete ? [singleOperationForDelete] : this.store.operations;

        this.setDialogTypes(operationsList);

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
        this.onDialogElementClick();

        return MoneyOperationService.openClosedPeriod(this._getDate())
            .then(() => {
                this.setState({
                    singleOperationForDelete: null
                });
                window.location.reload();
            });
    };

    _getDate = () => {
        return new Date(this.state.singleOperationForDelete.Date);
    };

    removeConfirmationModal = () => {
        return (
            <RemoveOperationDialog
                header={this.getHeaders()}
                visible={this.state.onDeleteDialogShown}
                description={this._getDescription}
                disabled={this._notClosedStatus}
                onButtonClick={this._deleteOperationsHandle}
                onClose={this.onDialogElementClick}
            />
        );
    };

    operationsContent = () => {
        const {
            operations,
            operationsLoaded,
            isOpen
        } = this.store;

        if (operations.length) {
            return (
                <ul className={cn(`unrecognized-ops-table`, { 'unrecognized-ops-table--open': operationsLoaded && isOpen })}>
                    {operations.map(operation => {
                        const op = { ...toJS(operation) };
                        const operationEditHash = `#${getEditUrlHash(operation)}`;

                        if (op.BaseOperation) {
                            return (
                                <div
                                    className={cn(`op__duplicate`)}
                                    key={getUniqueId(`wopContainer_${op.DocumentBaseId}`)}
                                >
                                    <div className={style.linkRow}>
                                        <a href={operationEditHash}>
                                            <Row
                                                key={getUniqueId(`wop_${op.DocumentBaseId}`)}
                                                data={op}
                                                onDelete={this._showRemoveOperationsDialog}
                                                onClickRow={this.onClickRow}
                                                onImport={() => {
                                                    this.importOperation(op);
                                                }}
                                                onMerge={() => {
                                                    this.mergeOperation(op);
                                                }}
                                            />
                                        </a>
                                    </div>
                                    <Row
                                        isOriginal
                                        key={getUniqueId(`wopBase_${op.BaseOperation.DocumentBaseId}`)}
                                        data={op.BaseOperation}
                                        onDelete={this._showRemoveOperationsDialog}
                                        onClickRow={() => {
                                        }}
                                        onImport={() => {
                                            this.importOperation(op);
                                        }}
                                        onMerge={() => {
                                            this.mergeOperation(op);
                                        }}
                                    />
                                </div>);
                        }

                        return <div className={style.linkRow}>
                            <a href={operationEditHash} className={style.rowLink}>
                                <Row
                                    key={getUniqueId(`wop_${op.DocumentBaseId}`)}
                                    data={op}
                                    onDelete={this._showRemoveOperationsDialog}
                                    onClickRow={this.onClickRow}
                                    onImport={() => {
                                        this.importOperation(op);
                                    }}
                                    onMerge={() => {
                                        this.mergeOperation(op);
                                    }}
                                />
                            </a>
                        </div>;
                    })}
                </ul>
            );
        }

        return null;
    };

    tableOverFooter = () => {
        const {
            totalCount,
            operationsLoadedCount,
            loading
        } = this.store;

        return (
            <TableOverFooter
                className={cn(`tableOverFooter`)}
                showButton={operationsLoadedCount < totalCount}
                buttonText={`Показать еще ${pluralNoun(Math.min(totalCount - operationsLoadedCount, pageCount), `операцию`, `операции`, `операций`, true)}`}
                text={`Отображено: ${operationsLoadedCount} из ${pluralNoun(totalCount, `операции`, `операций`, `операций`, true)}`}
                onClick={this.onClickShowMoreButton}
                buttonLoading={loading}
            />
        );
    };

    render() {
        const { totalCount, loading, isOpen } = this.store;

        if (totalCount) {
            return (
                <div className={cn(`unrecognized-operations`)}>
                    {this.removeConfirmationModal()}

                    {this.getSpoiler()}

                    {loading && <TableLoader />}

                    {this.operationsContent()}

                    {isOpen && this.tableOverFooter()}
                </div>
            );
        }

        return null;
    }
}

WarningTable.propTypes = {
    onClickRow: PropTypes.func,
    commonDataStore: PropTypes.object.isRequired,
    store: PropTypes.object,
    onChangeList: PropTypes.func,
    toggleTable: PropTypes.func,
    closeTable: PropTypes.func
};
