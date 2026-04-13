import {
    observable, action, toJS, makeObservable
} from 'mobx';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import OperationTablesService from '../../../services/newMoney/operationTablesService';
import storage from '../../../helpers/newMoney/storage';
import { importOperation, mergeOperation } from '../../../services/newMoney/newMoneyOperationService';
import { getApproveByIds, getInitialDate } from '../../../services/approvedService';
import { isSettlement } from '../../../helpers/MoneyOperationHelper';

class WarningTableStore {
    filter = storage.get(`filter`);

    pageCount = 20;

    @observable totalCount = 0;

    @observable operations = [];

    @observable operationsLoadedCount = 0;

    @observable loading = false;

    @observable isOpen = false;

    @observable operationsLoaded = false;

    @observable sourceId = this.filter ? this.filter.sourceId : 0;

    @observable sourceType = this.filter ? this.filter.sourceType : 0;

    constructor() {
        makeObservable(this);
    }

    @action loadOperationsCount() {
        this.resetStore();

        return OperationTablesService.getWarningOperationsCount(this.sourceId, this.sourceType)
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
            this.operationsLoaded = false;
        }

        const { Operations, TotalCount } = await OperationTablesService.getWarningOperationsList(offset, count, this.sourceId, this.sourceType);
        let warningOperations = Operations;

        const userData = await userDataService.get();
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = userData;

        this.isOutsourceUser = (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;

        if (this.totalCount !== TotalCount) {
            this.totalCount = TotalCount;
        }

        if (this.totalCount && !this.operationsLoaded) {
            this.operationsLoaded = true;
        }

        if (warningOperations && warningOperations.length) {
            if (this.isOutsourceUser) {
                const InitialDate = await getInitialDate();

                const Ids = warningOperations.reduce((acc, item) => {
                    const isValidDate = dateHelper(InitialDate).isSameOrBefore(dateHelper(item.Date));

                    if (isSettlement(item.OperationType) && isValidDate) {
                        acc.push(item.DocumentBaseId);
                    }

                    return acc;
                }, []);

                const result = await getApproveByIds({ Ids });

                const approvedOperations = result.data;

                warningOperations = warningOperations.map(operation => {
                    const isApproved = toJS(approvedOperations).find(el => el.DocumentBaseId === operation.DocumentBaseId)?.IsApproved;

                    const isShowApprove = isApproved !== undefined;

                    return { ...operation, isApproved, isShowApprove };
                });
            }

            this.operations = this.operations.concat(warningOperations);
            this.operationsLoadedCount = this.operations.length;
            storage.save(`warningOperations`, this.operations.map(op => ({ documentBaseId: op.DocumentBaseId, operationType: op.OperationType })));
        }

        this.loading = false;
    }

    @action deleteAllOperations() {
        const { sourceType, sourceId } = this;

        return OperationTablesService.deleteAllOperations({ sourceType, sourceId });
    }

    @action deleteOperation(operationId) {
        return OperationTablesService.deleteOperation(operationId);
    }

    @action importOperation(operation) {
        return importOperation(operation.DocumentBaseId);
    }

    @action mergeOperation(operation) {
        return mergeOperation(operation.DocumentBaseId);
    }

    @action resetStore() {
        this.totalCount = 0;
        this.operations = [];
        this.operationsLoadedCount = 0;
        this.loading = false;
        this.isOpen = false;
        this.operationsLoaded = false;
    }
}

export default WarningTableStore;
