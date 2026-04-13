import {
    observable, action, computed, toJS, runInAction, makeObservable
} from 'mobx';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import OperationTablesService from '../../../services/newMoney/operationTablesService';
import OperationModel from '../stores/models/OperationModel';
import storage from '../../../helpers/newMoney/storage';
import { getApproveByIds, getInitialDate } from '../../../services/approvedService';
import { isSettlement } from '../../../helpers/MoneyOperationHelper';

class OutsourceProcessingTableStore {
    filter = storage.get(`filter`);

    pageCount = 20;

    allSelected = false;

    @observable operations = [];

    @observable totalCount = 0;

    @observable tableCount = 0;

    @observable loading = false;

    @observable loaded = false;

    @observable isOpen = storage.get(`outsourceProcessingIsOpen`);

    @observable isOKClicked = false;

    @observable sourceId = this.filter ? this.filter.sourceId : 0;

    @observable sourceType = this.filter ? this.filter.sourceType : 0;

    constructor() {
        makeObservable(this);
    }

    @action loadOperationsCount() {
        this.resetStore();

        return OperationTablesService.getOutsourceProcessingOperationsCount(this.sourceId, this.sourceType)
            .then(response => {
                this.totalCount = response;
            });
    }

    @action setMoneySource(sourceId, sourceType) {
        this.sourceId = Number(sourceId) || null;
        this.sourceType = Number(sourceType) || null;
    }

    @action loadOperations = async (options = { reset: false }) => {
        const { operations } = this;

        const offset = options.reset ? 0 : operations.length;
        const count = options.reset ? Math.max(operations.length, this.pageCount) : this.pageCount;

        this.loading = true;

        if (options.reset) {
            this.operations = [];
            this.loaded = false;
        }

        const { Operations, TotalCount } = await OperationTablesService.getOutsourceProcessingOperationsList(offset, count, this.sourceId, this.sourceType, options.filter);

        let outsourceOperations = Operations;
        const userData = await userDataService.get();
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = userData;

        this.isOutsourceUser = (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;

        if (this.isOutsourceUser) {
            const InitialDate = await getInitialDate();

            const Ids = outsourceOperations.reduce((acc, item) => {
                const isValidDate = dateHelper(InitialDate).isSameOrBefore(dateHelper(item.Date));

                if (isSettlement(item.OperationType) && isValidDate) {
                    acc.push(item.DocumentBaseId);
                }

                return acc;
            }, []);

            const result = await getApproveByIds({ Ids });

            const approvedOperations = result.data;

            outsourceOperations = outsourceOperations.map(operation => {
                const isApproved = toJS(approvedOperations).find(el => el.DocumentBaseId === operation.DocumentBaseId)?.IsApproved;

                const isShowApprove = isApproved !== undefined;

                return { ...operation, isApproved, isShowApprove };
            });
        }

        runInAction(() => {
            const parsedOperations = this.parseOperationsToModels(outsourceOperations);

            if (options.reset) {
                this.operations = parsedOperations;
            } else {
                this.operations.push(...parsedOperations);
            }

            this.tableCount = this.operations.length;
            this.totalCount = TotalCount;
            this.loaded = true;
            this.loading = false;
        });
    }

    @action resetStore() {
        this.operations = [];
        this.totalCount = 0;
        this.tableCount = 0;
        this.loading = true;
        this.loaded = false;
        this.isOpen = false;
        this.isOKClicked = false;
    }

    @action toggleSelectAll(checked) {
        this.operations.forEach(o => {
            const operation = o;

            operation.Checked = checked;
        });

        this.allSelected = checked;
    }

    @action setOperation(operations) {
        this.operations.replace(operations);
    }

    @action uncheckAllOperation() {
        this.operations.forEach(operation => operation.uncheck());
    }

    @computed get checkedOperationList() {
        return this.operations.filter(o => o.Checked);
    }

    parseOperationsToModels(operationsList) {
        return operationsList.map(operation => {
            return new OperationModel(this, operation);
        });
    }
}

export default OutsourceProcessingTableStore;
