import React from 'react';
import { observer, Provider } from 'mobx-react';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import { getUrlWithId } from '@moedelo/frontend-core-v2/helpers/companyId';
import PageLoader from '../../components/PageLoader';
import CashOperationDialog from './components/CashOperationDialog';
import PurseOperation from './components/PurseOperation';
import MoneySourceTypeDialog from './components/MoneySourceTypeDialog';
import MoneySourceType from '../../enums/MoneySourceType';
import {
    getDefaultFilter, initFilter, toObject, toQueryString
} from '../../helpers/newMoney/utils';
import storage from '../../helpers/newMoney/storage';
import MoneySourceStore from './stores/MoneySourceStore';
import MoneyRouter from './MoneyRouter';
import AsyncComponent from '../../components/AsyncComponent';
import { removeFalsyFields } from '../../helpers/newMoney/filterHelper';
import moneySourceHelper from '../../helpers/newMoney/moneySourceHelper';
import MoneyOperationWrapper from './MoneyOperationWrapper';

@observer
class Money extends React.Component {
    constructor(props) {
        super(props);
        const unexpectedHashRegexList = [/edit/gi, /add/gi, /copy/gi, /viewReconciliationResults/gi];

        const { hash } = window.location;
        const filter = initFilter();

        this.state = {
            currentSource: {},
            userIsPaid: true,
            isLoading: true,
            userInfo: {
                isOoo: false,
                userEmail: ``
            }
        };

        this.moneySourceStore = new MoneySourceStore();

        if (!unexpectedHashRegexList.some(regex => hash.match(regex))) {
            window.location = `#${toQueryString(filter)}`;
        }
    }

    async componentDidMount() {
        const {
            IsTrial, IsPaid, IsExpired, IsOoo, Email
        } = await userDataService.get();
        this.setState({
            userIsPaid: (IsTrial || IsPaid) && !IsExpired,
            isLoading: false,
            userInfo: {
                isOoo: IsOoo,
                userEmail: Email
            }
        });

        storage.save(`tableData`, {});
        storage.save(`Scroll`, 0);
    }

    onSaveOperation = () => {
        const { referrer } = document;

        if (referrer.search(/\/Marketplace/gi) !== -1) {
            NavigateHelper.push(`/Finances`);

            return;
        }

        if (localStorage.getItem(`referrerUrl`) === `finances`) {
            this.setCurrentSource();

            const { backHash } = toObject(window.location.href.split(`#`)[0]);

            if (backHash && referrer.search(/sales/gi) !== -1) {
                NavigateHelper.push(backHash ? `${referrer}#${backHash}` : referrer);
            } else {
                this.goBack();
            }
        } else {
            const current = window.location.href;
            const urlToBack = sessionStorage.getItem(`urlToBack`);

            if (urlToBack) {
                sessionStorage.removeItem(`urlToBack`);

                NavigateHelper.replace(urlToBack);

                return;
            }

            NavigateHelper.back();

            setTimeout(() => {
                if (window.location.href === current) {
                    NavigateHelper.push(`/Finances`);
                }
            }, 1000);
        }
    };

    onSaveSettlementOperation = operation => {
        this.updateFilter(operation.SettlementAccountId);
        this.onSaveOperation();
    };

    onDeleteOperation = () => {
        this.goBack();
    };

    onCloseDialog = () => {
        localStorage.removeItem(`referrerUrl`);
        window.history.back();
    };

    setCurrentSource = (filter = storage.get(`filter`) || getDefaultFilter()) => {
        const currentSource = moneySourceHelper.getCurrentSource(this.moneySourceStore.sourceList, filter.sourceId);

        currentSource.Id !== undefined && this.setState({
            currentSource: moneySourceHelper.getCurrentSource(this.moneySourceStore.sourceList, filter.sourceId)
        });
    };

    getSourceFromFilter = () => {
        const filter = storage.get(`filter`) || {};
        const sourceType = parseInt(filter.sourceType, 10);
        const sourceId = parseInt(filter.sourceId, 10);

        if (Object.values(MoneySourceType).includes(sourceType) && sourceId) {
            return {
                Type: sourceType,
                Id: Number.isNaN(sourceId) ? null : sourceId
            };
        }

        return null;
    };

    handleUserIsPaid() {
        return !this.state.userIsPaid && NavigateHelper.push(getUrlWithId(`/Pay`));
    }

    cashOperation = operation => {
        this.handleUserIsPaid();

        return <AsyncComponent
            component={<CashOperationDialog
                value={operation}
                onSave={this.onSaveOperation}
                onDelete={this.onDeleteOperation}
            />}
            stubComponent={<PageLoader />}
            loader={() => import(`../old/index`)}
        />;
    };

    purseOperation = operation => {
        this.handleUserIsPaid();

        return <AsyncComponent
            component={<PurseOperation
                value={operation}
                onSave={this.onSaveOperation}
                onDelete={this.onDeleteOperation}
            />}
            stubComponent={<PageLoader />}
            loader={() => import(`../old/index`)}
        />;
    };

    moneyOperation = options => {
        !options.SkipPaymentChecking && this.handleUserIsPaid();

        return (
            <Provider moneySourceStore={this.moneySourceStore}>
                <MoneyOperationWrapper
                    options={options}
                    onSave={this.onSaveSettlementOperation}
                    onDelete={this.goBack}
                    onCancel={this.goBack}
                />
            </Provider>
        );
    };

    goBack = () => {
        localStorage.removeItem(`referrerUrl`);

        const filter = storage.get(`filter`) || getDefaultFilter();

        NavigateHelper.push(toQueryString(removeFalsyFields(filter)));
    };

    updateFilter = sourceId => {
        const filter = storage.get(`filter`);

        if ((filter && sourceId) && (+filter.sourceId !== 0)) {
            if (filter.sourceId !== sourceId) {
                storage.save(`Scroll`, 0);
            }

            const filterData = { ...filter, sourceId };

            storage.save(`filter`, filterData);
        }
    };

    addIncoming = source => {
        const incomingSource = source || this.getSourceFromFilter();

        if (incomingSource) {
            const id = incomingSource.Id || ``;

            switch (incomingSource.Type) {
                case MoneySourceType.SettlementAccount:
                    NavigateHelper.replace(`add/incoming/settlement/${id}`);

                    break;
                case MoneySourceType.Cash:
                    NavigateHelper.replace(`add/incoming/cash/${id}`);

                    break;
                case MoneySourceType.Purse:
                    NavigateHelper.replace(`add/incoming/purse/${id}`);

                    break;
            }
        }

        return <MoneySourceTypeDialog
            sourceList={this.moneySourceStore.sourceList}
            onSelect={src => this.addIncoming(src)}
            onClose={this.onCloseDialog}
        />;
    };

    addOutgoing = source => {
        const outgoingSource = source || this.getSourceFromFilter();

        if (outgoingSource) {
            const id = outgoingSource.Id || ``;

            switch (outgoingSource.Type) {
                case MoneySourceType.SettlementAccount:
                    NavigateHelper.replace(`add/outgoing/settlement/${id}`);

                    break;
                case MoneySourceType.Cash:
                    NavigateHelper.replace(`add/outgoing/cash/${id}`);

                    break;
                case MoneySourceType.Purse:
                    NavigateHelper.replace(`add/outgoing/purse/${id}`);

                    break;
            }
        }

        return <MoneySourceTypeDialog
            sourceList={this.moneySourceStore.sourceList}
            onSelect={src => this.addOutgoing(src)}
            onClose={this.onCloseDialog}
        />;
    };

    render() {
        if (this.state.isLoading) {
            return null;
        }

        return <MoneyRouter
            moneyOperation={this.moneyOperation}
            cashOperation={this.cashOperation}
            purseOperation={this.purseOperation}
            moneySourceStore={this.moneySourceStore}
            addIncoming={this.addIncoming}
            addOutgoing={this.addOutgoing}
            userInfo={this.state.userInfo}
        />;
    }
}

export default Money;

