import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import requisitesService from '@moedelo/frontend-common-v2/apps/requisites/services/requisitesService';
import firmFlagService from '@moedelo/frontend-common-v2/services/firmFlagService';
import {
    requestReconciliationByHand
} from '@moedelo/frontend-common-v2/apps/finances/components/ReconciliationByHandDialog/services/reconciliationService';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import VoteForm from '@moedelo/frontend-common-v2/apps/marketing/components/VoteForm';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import H1 from '@moedelo/frontend-core-react/components/headers/H1';
import CollapsibleInformation from '@moedelo/frontend-core-react/components/CollapsibleInformation';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { withNotificationAsync } from '@moedelo/frontend-common-v2/helpers/notificationHelper';
import BalanceReconciliationDialog from './components/BalanceReconciliationDialog/BalanceReconciliationDialog';
import ReconciliationInstructionContent from './components/ReconciliationInstructionContent';
import balanceReconciliationService from '../../../../services/newMoney/balanceReconciliationService';
import BalanceReconciliationMessagesEnum from '../../../../enums/newMoney/BalanceReconciliationMessagesEnum';
import BalanceReconciliationStatusEnum from '../../../../enums/newMoney/BalanceReconciliationStatusEnum';
import storage from '../../../../helpers/newMoney/storage';
import OpenYearPeriodModal from './components/OpenYearPeriodModal';
import style from './style.m.less';

const cn = classnames.bind(style);

class BalanceReconciliation extends React.Component {
    constructor(props) {
        super(props);

        this.onCancelMrkEvent = `otmena_avtosverki_blok_otchet_avtosverki_stranitsa_dengi_click_button`;
        this.onConfirmMrkEvent = `podtverzhdenie_avtosverki_blok_otchet_avtosverki_stranitsa_dengi_click_button`;
        this.onFeedbackSendEvent = `otzyv_o_sverke_blok_otchet_avtosverki_stranitsa_dengi_send_feedback`;
        this.feedBackFirmFlagId = `balanceReconciliationFeedback`;

        this.state = {
            isOpenYearPeriodModalShown: false,
            isNewReconciliationPending: false,
            excessOperations: null,
            missingOperations: null,
            excessSalaryOperations: null,
            missingSalaryOperations: null,
            isFeedbackDialogShown: false,
            loading: true,
            disabled: false,
            isEnabledFirmFlag: false,
            sessionId: props.guid || ``,
            sourceId: null,
            FinancialResultLastClosedPeriod: null,
            status: null,
            date: ``,
            excludeOperationsIds: []
        };
    }

    async componentDidMount() {
        const { FinancialResultLastClosedPeriod } = await requisitesService.get();

        this.setState({ FinancialResultLastClosedPeriod });

        await this.loadReconciliationData();
    }

    async componentDidUpdate(prevProps) {
        if (prevProps.guid !== this.props.guid) {
            await this.loadReconciliationData();
        }
    }

    onConfirm = () => {
        const {
            missingOperations, excessOperations, loading, sessionId, isEnabledFirmFlag
        } = this.state;
        const data = {
            missingOperations: [],
            excessOperations: [],
            sessionId
        };

        !loading && this.setState({
            loading: true
        });

        missingOperations.length && missingOperations.forEach(operation => {
            data.missingOperations.push(operation.Id);
        });

        excessOperations.length && excessOperations.forEach(operation => {
            data.excessOperations.push(operation.Id);
        });

        balanceReconciliationService.produceReconciliation(data)
            .then(() => this.loadReconciliationData())
            .then(() => {
                this.setState({
                    loading: false
                });

                isEnabledFirmFlag && NotificationManager.show({
                    message: BalanceReconciliationMessagesEnum.changesAccepted,
                    type: `success`,
                    duration: 4000
                });
            })
            .catch(() => {
                NotificationManager.show({
                    message: BalanceReconciliationMessagesEnum.requestError,
                    type: `error`,
                    duration: 4000
                });
            })
            .then(() => {
                !isEnabledFirmFlag && this.toggleFeedbackDialog();
            });

        mrkStatService.sendEventWithoutInternalUser(this.onConfirmMrkEvent);
    }

    onCancel = () => {
        const { sessionId, isEnabledFirmFlag } = this.state;

        this.setState({
            loading: true
        });

        balanceReconciliationService.cancelReconciliation(sessionId)
            .then(() => this.loadReconciliationData())
            .catch(() => {
                NotificationManager.show({
                    message: BalanceReconciliationMessagesEnum.noChangesAccepted,
                    type: `error`,
                    duration: 4000
                });
            })
            .then(() => {
                !isEnabledFirmFlag && this.toggleFeedbackDialog();
            });

        mrkStatService.sendEventWithoutInternalUser(this.onCancelMrkEvent);
    }

    onDeleteFromMissing = Id => {
        const { missingOperations, excludeOperationsIds } = this.state;
        const newList = missingOperations.filter && missingOperations.filter(operation => {
            return operation.Id !== Id;
        });

        this.setState({
            missingOperations: newList,
            excludeOperationsIds: [...excludeOperationsIds, Id]
        });
    }

    onDeleteFromExcess = Id => {
        const { excessOperations, excludeOperationsIds } = this.state;
        const newList = excessOperations.filter && excessOperations.filter(operation => {
            return operation.Id !== Id;
        });

        this.setState({
            excessOperations: newList,
            excludeOperationsIds: [...excludeOperationsIds, Id]
        });
    }

    async onFeedbackSend({ message, rating, self }) {
        const eventObj = {
            event: self.onFeedbackSendEvent,
            st5: rating,
            st6: message
        };

        new Promise(resolve => {
            mrkStatService.sendEventWithoutInternalUser(eventObj);
            resolve();
        })
            .then(() => {
                firmFlagService.enable({ name: self.feedBackFirmFlagId });

                NotificationManager.show({
                    message: `Спасибо! Ваше сообщение отправлено.`,
                    type: `success`,
                    duration: 4000
                });
            })
            .catch(() => {
                NotificationManager.show({
                    message: `Произошла ошибка.`,
                    type: `error`,
                    duration: 4000
                });
            })
            .then(() => {
                self.toggleFeedbackDialog();
            });
    }

    onRequestNewReconciliation = async () => {
        const yearOfLastClosedPeriod = dateHelper(this.state.FinancialResultLastClosedPeriod).year();
        const hasClosedPeriodInCurrentYear = yearOfLastClosedPeriod && dateHelper().year() === yearOfLastClosedPeriod;

        if (hasClosedPeriodInCurrentYear) {
            this.setState({ isOpenYearPeriodModalShown: true });

            return;
        }

        await this.requestNewReconciliation();
    }

    requestNewReconciliation = async () => {
        const { sourceId: sourceIdFromFilter } = storage.get(`filter`) || {};
        const settlementAccountId = this.state.sourceId || sourceIdFromFilter;

        this.setState({ isNewReconciliationPending: true });

        await withNotificationAsync({
            func: async () => {
                const { Status, SessionId } = await requestReconciliationByHand(settlementAccountId);

                if (!Status || !SessionId) {
                    NavigateHelper.reload();

                    return;
                }

                NavigateHelper.push(`/Finances#viewReconciliationResults/${SessionId}`);
                this.setState({ isNewReconciliationPending: false });
            },
            errorMessage: `Не удалось запросить сверку. Повторите запрос или обратитесь в техническую поддержку: <a href="mailto:support@moedelo.org">support@moedelo.org</a>`
        });
    };

    loadReconciliationData = async () => {
        const { sourceId: settlementAccountId } = storage.get(`filter`) || {};
        const { guid } = this.props;

        await balanceReconciliationService.checkBalance({ guid, settlementAccountId })
            .then(response => this.handleBalanceResponse(response))
            .catch(({ status }) => this.setState({ status }))
            .then(() => this.setState({ loading: false }));
    }

    handleVariances = ({ data = {} }) => {
        const {
            SessionId, ExcessOperations, MissingOperations, Status, Currency
        } = data;
        const excessSalaryOperations = [];
        const missingSalaryOperations = [];
        const excessOperations = ExcessOperations.filter(o => extractSalaryOperations(o, excessSalaryOperations));
        const missingOperations = MissingOperations.filter(o => extractSalaryOperations(o, missingSalaryOperations));

        firmFlagService.isEnabled({ name: this.feedBackFirmFlagId })
            .then(isEnabledFirmFlag => {
                this.setState({
                    sessionId: SessionId,
                    status: Status,
                    currency: Currency,
                    excessOperations,
                    missingOperations,
                    excessSalaryOperations,
                    missingSalaryOperations,
                    isEnabledFirmFlag
                });
            });
    }

    handleBalanceResponse = response => {
        const { data: { Status, InitialDate = ``, SettlementAccount = {} } } = response;
        const momentDate = dateHelper(InitialDate);
        const date = momentDate.isValid() ? momentDate.format(`DD.MM.YYYY`) : ``;

        this.setState({
            status: Status,
            name: SettlementAccount?.Name,
            sourceId: SettlementAccount?.Id,
            number: SettlementAccount?.Number,
            date
        }, () => this.handleVariances(response));
    }

    toggleFeedbackDialog({ self = this } = {}) {
        const { isFeedbackDialogShown } = self.state;

        self.setState({
            isFeedbackDialogShown: !isFeedbackDialogShown
        });
    }

    renderHead = () => {
        const { date } = this.state;

        return <React.Fragment>
            <div className={cn(grid.row, style.reconciliationHeader)}>
                <div className={grid.col_20}>
                    <H1>Результаты сверки с банком по счёту {date.length > 0 && ` от ${date}`}</H1>
                    <div className={style.subtext}>
                        {this.state.name && `${this.state.name} —`}
                        {this.state.number && `№ ${this.state.number}`}
                    </div>
                </div>
                <div className={cn(grid.col_4, style.colFlex, style.reconciliationButton)}>
                    <Button
                        loading={this.state.isNewReconciliationPending}
                        onClick={this.onRequestNewReconciliation}
                        disabled={(this.state.status && this.state.status === BalanceReconciliationStatusEnum.InProgress) || this.state.loading}
                    >
                        Запросить сверку
                    </Button>
                </div>
            </div>
            <CollapsibleInformation
                header={`Рекомендации по расхождению данных`}
                panelClassName={style.instruction}
                contentClassName={style.instructionContent}
                content={<ReconciliationInstructionContent />}
            />
            <OpenYearPeriodModal
                isShow={this.state.isOpenYearPeriodModalShown}
                sendRequest={() => {
                    this.setState({ isOpenYearPeriodModalShown: false });

                    this.requestNewReconciliation();
                }}
            />
        </React.Fragment>;
    }

    render() {
        const {
            loading,
            disabled,
            excessOperations,
            missingOperations,
            currency,
            excessSalaryOperations,
            missingSalaryOperations,
            isFeedbackDialogShown,
            isEnabledFirmFlag,
            status,
            sessionId,
            excludeOperationsIds
        } = this.state;
        const {
            onCancel,
            onConfirm,
            onDeleteFromExcess,
            onDeleteFromMissing,
            onFeedbackSend,
            toggleFeedbackDialog
        } = this;

        return (
            <React.Fragment>
                {this.renderHead()}
                <BalanceReconciliationDialog
                    excessOperations={excessOperations}
                    missingOperations={missingOperations}
                    excessSalaryOperations={excessSalaryOperations}
                    missingSalaryOperations={missingSalaryOperations}
                    currency={currency}
                    onCancel={onCancel}
                    onConfirm={onConfirm}
                    onDeleteFromExcess={onDeleteFromExcess}
                    onDeleteFromMissing={onDeleteFromMissing}
                    status={status}
                    loading={loading}
                    disabled={disabled}
                    sessionId={sessionId}
                    excludeOperationsIds={excludeOperationsIds}
                />
                <VoteForm
                    headerText={`Оцените функцию проверки`}
                    onDone={({ message, rating }) => onFeedbackSend({ message, rating, self: this })}
                    onClose={() => toggleFeedbackDialog({ self: this })}
                    open={!isEnabledFirmFlag && isFeedbackDialogShown}
                />
            </React.Fragment>
        );
    }
}

function extractSalaryOperations(operation, salaryArray) {
    if (operation.IsSalary) {
        salaryArray.push(operation);

        return false;
    }

    return operation;
}

BalanceReconciliation.propTypes = {
    guid: PropTypes.string
};

export default BalanceReconciliation;
