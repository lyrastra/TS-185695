import {
    observable, action, computed, toJS,
    makeObservable
} from 'mobx';
import sessionStorageHelper from '@moedelo/frontend-core-v2/helpers/sessionStorageHelper';
import currencyHelper from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import OperationModel from '../stores/models/OperationModel';
import MoneyOperationService from '../../../services/newMoney/moneyOperationService';
import SortColumnEnum from '../../../enums/newMoney/SortColumnEnum';
import SortTypeEnum from '../../../enums/newMoney/SortTypeEnum';
import storage from '../../../helpers/newMoney/storage';
import { getApproveByIds, getInitialDate } from '../../../services/approvedService';
import { isSettlement } from '../../../helpers/MoneyOperationHelper';

const rubCurrency = 643;

class TableStore {
    @observable operations = [];
    @observable loading = true;
    @observable loaded = false;
    @observable hasOperations = false;
    @observable totalCount = 0;
    @observable tableCount = 0;
    @observable startBalance = 0;
    @observable incomingCount = 0;
    @observable incomingBalance = 0;
    @observable outgoingCount = 0;
    @observable outgoingBalance = 0;
    @observable endBalance = 0;
    @observable incomingDate = ``;
    @observable outgoingDate = ``;
    @observable currency = rubCurrency;
    @observable sortColumn;
    @observable sortType;
    @observable counter = 20;
    @observable relatedDocsArray = [];
    @observable operationsGroupedByCurrency = [];
    @observable bankBalance = null;
    @observable canShowBankTurnoversAndBalances = false;
    @observable isOutsourceUser = false;
    @observable approvedOperations = [];
    @observable isOutsourceUserOrEmployee = false;

    allSelected = false;

    constructor() {
        makeObservable(this);
        this.sortColumn = this.getSort().sortColumn || SortColumnEnum.Date;
        this.sortType = this.getSort().sortType || SortTypeEnum.Desc;
    }

    @action loadOperations = async (options = { reset: false, counterUsed: false, tableCount: 0 }, filter = {}) => {
        const {
            sortColumn, sortType, counter, tableCount
        } = this;
        const offset = options.reset ? 0 : this.operations.length;
        const count = options.reset && !options.counterUsed ? Math.max(tableCount, storage.get(`tableCounter`) || counter) : counter;

        if (options.reset) {
            this.setData({ operations: [], loading: true, loaded: false });
        }

        const request = Object.assign({
            offset,
            count: options.tableCount || count,
            sortType,
            sortColumn
        }, filter);

        const multiCurrencyData = await MoneyOperationService.getOperations(request);
        const userData = await userDataService.get();

        const data = multiCurrencyData.Summaries;
        let operations = multiCurrencyData.Operations;
        const totalCount = multiCurrencyData.TotalCount;

        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser, IsFirmOnService
        } = userData;

        this.isOutsourceUser = (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
        this.isOutsourceUserOrEmployee = IsProfOutsourceUser || IsFirmOnService;

        if (this.isOutsourceUser) {
            const InitialDate = await getInitialDate();

            const Ids = operations.reduce((acc, item) => {
                const isValidDate = dateHelper(InitialDate).isSameOrBefore(dateHelper(item.Date));

                if (isSettlement(item.OperationType) && isValidDate) {
                    acc.push(item.DocumentBaseId);
                }

                return acc;
            }, []);

            const result = await getApproveByIds({ Ids });

            const approvedOperations = result.data;

            operations = operations.map(operation => {
                const isApproved = toJS(approvedOperations).find(el => el.DocumentBaseId === operation.DocumentBaseId)?.IsApproved;

                const isShowApprove = isApproved !== undefined;

                return { ...operation, isApproved, isShowApprove };
            });
        }

        this.bankBalance = multiCurrencyData.BankBalance;
        this.canShowBankTurnoversAndBalances = !!(request.sourceId && (request.startDate || request.endDate));

        let parsedOperations = this.parseOperationsToModels(operations);

        if (request.sortColumn === SortColumnEnum.Sum) {
            parsedOperations = this.groupByCurrency(data, parsedOperations);
        }

        if (options.reset) {
            this.operations = parsedOperations;
        } else {
            this.operations.push(...parsedOperations);
        }

        this.loading = false;
        this.loaded = true;

        this.hasOperations = data.some(res => res.HasOperations);
        this.totalCount = totalCount;
        this.tableCount = this.operationsListLength;

        this.incomingCount = data.reduce((sum, res) => sum + res.IncomingCount, 0);
        this.outgoingCount = data.reduce((sum, res) => sum + res.OutgoingCount, 0);
        this.relatedDocsArray.length = 0;
        this.operationsGroupedByCurrency = [];

        if (data && data.length) {
            const resp = data[0];
            this.incomingDate = resp.IncomingDate;
            this.outgoingDate = resp.OutgoingDate;
            this.startBalance = resp.StartBalance;
            this.endBalance = resp.EndBalance;
            this.incomingBalance = resp.IncomingBalance;
            this.outgoingBalance = resp.OutgoingBalance;
            this.currency = resp.Currency || rubCurrency;
        }

        if (data.length > 1) {
            this.operationsGroupedByCurrency = this.mapMultiCurrencyResponse(data);
        }
    }

    @action setData({ operations, loading, loaded }) {
        this.operations = operations;
        this.loading = loading;
        this.loaded = loaded;
    }

    @action setOperation(operations) {
        this.operations.replace(operations);
    }

    @action uncheckAllOperation() {
        this.operations.forEach(operation => operation.uncheck());
        this.allSelected = false;
    }

    @action setCounter(counter) {
        this.counter = counter;
    }

    @action toggleSelectAll(checked) {
        this.operations.forEach(o => {
            const operation = o;

            operation.Checked = checked;
        });
        this.handleAllSelectedFlag(checked);
    }

    @action handleAllSelectedFlag = checked => {
        this.allSelected = checked;
    }

    @computed get operationsListLength() {
        return this.operations.length;
    }

    @computed get checkedOperationList() {
        return this.operations.filter(o => o.Checked);
    }

    mapMultiCurrencyResponse(operationsGropedByCurrency) {
        return operationsGropedByCurrency.map(operation => {
            return {
                currency: operation.Currency,
                currencySign: currencyHelper.getSymbolByCode(operation.Currency),
                startBalance: operation.StartBalance,
                endBalance: operation.EndBalance,
                incomingBalance: operation.IncomingBalance,
                outgoingBalance: operation.OutgoingBalance,
                ...operation
            };
        });
    }

    parseOperationsToModels(operationsList) {
        return operationsList.map(operation => {
            return new OperationModel(this, operation);
        });
    }

    getSort() {
        const sortObj = sessionStorageHelper.get(`moneyMainTableSort`);

        return sortObj ? JSON.parse(sortObj) : {};
    }

    groupByCurrency = (data, parsedOperations) => {
        const currencies = data.map(d => d.Currency);
        let sortedOperations = [];
        currencies.forEach(currency => {
            const currencyOperations = parsedOperations.filter(po => po.Currency === currency);
            sortedOperations = sortedOperations.concat(currencyOperations);
        });

        return sortedOperations;
    }
}

export default TableStore;
