import React, { Fragment } from 'react';
import renderHtml from 'react-render-html';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import ImportStatusEnum from '@moedelo/frontend-common-v2/apps/finances/enums/ImportStatusEnum';
import ImportHelper from '@moedelo/frontend-common-v2/apps/finances/helpers/ImportHelper';
import IntegrationPartnerEnum from '@moedelo/frontend-enums/mdEnums/IntegrationPartner';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Link from '@moedelo/frontend-core-react/components/Link';
import Tooltip, { EventType as TooltipEventType } from '@moedelo/frontend-core-react/components/Tooltip';
import notificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import { Color } from '@moedelo/frontend-core-react/components/buttons/enums';
import classnames from 'classnames/bind';
import style from './style.m.less';
import MoneySourceType from '../../../../enums/MoneySourceType';
import UploadButton from '../UploadButton';
import CashDownloadButton from '../CashDownloadButton/CashDownloadButton';
import BankUpdateButton from '../BankUpdateButton/BankUpdateButton';
import NewCheckingAccountDialog from './components/NewCheckingAccountDialog';
import AddCurrencyAccountContent from './components/AddCurrencyAccountContent';
import { getDefaultFilter } from '../../../../helpers/newMoney/utils';
import { showPurseImportErrorModal } from '../PurseImportErrorModal';
import KudirDownloadButton from './components/KudirDownloadButton/KudirDownloadButton';

const cn = classnames.bind(style);

@observer
class ActionSource extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
        this.paymentImportStore = props.paymentImportStore;
        this.moneySourceStore = props.moneySourceStore;
        this.commonDataStore = props.commonDataStore;
        this.kudirStore = props.kudirStore;
    }

    _renderBankSourceAction = sourceFilter => {
        const filter = sourceFilter.sourceId !== undefined ? sourceFilter : getDefaultFilter();
        const sourceIdFromFilter = parseInt(filter.sourceId, 10);
        const selectedSource = this.moneySourceStore.getBySourceId(sourceIdFromFilter);
        const sourceType = parseInt(filter.sourceType, 10) || MoneySourceType.All;
        const isYandexKassa = (selectedSource?.IntegrationPartner === IntegrationPartnerEnum.YandexKassa || selectedSource?.IntegrationPartner === IntegrationPartnerEnum.UKassa);
        const isPurse = sourceType === MoneySourceType.Purse;
        const canShowAnyButton = [MoneySourceType.SettlementAccount, MoneySourceType.Purse, MoneySourceType.All].includes(sourceType) || isYandexKassa;

        if (!canShowAnyButton) {
            return null;
        }

        if (this.moneySourceStore.loading) {
            return <section className={cn(`loaderSection`)}>
                <Loader className={cn(`loader`)} />
            </section>;
        }

        const disabledByAccess = !this.commonDataStore.hasAccessToMoneyEdit;
        const isArchiveSource = this.moneySourceStore.isArchiveSource();
        const list = this.moneySourceStore.sourceList;
        const sourceTypeFromFilter = parseInt(filter.sourceType, 10);
        const selectedSourceAllAndNotVtb24 = list.some(i => i.IntegrationPartner === IntegrationPartnerEnum.Vtb24Bank) && sourceTypeFromFilter === MoneySourceType.All;

        const canRequestMovementList = sourceTypeFromFilter === MoneySourceType.All ? list.some(i => i.CanRequestMovementList) : selectedSource.CanRequestMovementList;
        const hasActiveIntegration = sourceTypeFromFilter === MoneySourceType.All ? list.some(i => i.HasActiveIntegration) : selectedSource.HasActiveIntegration;
        const hasUnprocessedRequests = sourceTypeFromFilter === MoneySourceType.All ? list.some(i => i.HasUnprocessedRequests) : selectedSource.HasUnprocessedRequests;
        const canShowBankUpdateButton = (!isPurse || isYandexKassa) && !selectedSourceAllAndNotVtb24 && canRequestMovementList && hasActiveIntegration;

        if (!selectedSource || !list.length) {
            return null;
        }

        const defaultButtonText = `Обновить из ${isYandexKassa ? `ПС` : `банка`}`;
        const accept = isPurse ? `.xls,.xlsx` : `.txt`;
        const options = isPurse ? {
            kontragentId: sourceIdFromFilter,
            isPurse
        } : {};
        const onChange = fileInput => this._onChangeUploadButton(fileInput, options);

        if (canShowBankUpdateButton) {
            return <BankUpdateButton
                filter={filter}
                source={selectedSource}
                isArchiveSource={isArchiveSource || disabledByAccess}
                onInputFileChange={onChange}
                statementSent={hasUnprocessedRequests}
                commonDataStore={this.commonDataStore}
                store={this.paymentImportStore}
                moneySourceStore={this.moneySourceStore}
                defaultButtonText={defaultButtonText}
                accept={accept}
            />;
        }

        return this._renderUploadFile(options);
    }

    _renderUploadFile = options => {
        const disabled = !this.commonDataStore.hasAccessToMoneyEdit;
        const isArchiveSource = this.moneySourceStore.isArchiveSource();
        const { loading, importLoading } = this.paymentImportStore;

        if (this.commonDataStore.loadingAccessToMoney) {
            return <section className={cn(`loaderSection`)}>
                <Loader className={cn(`loader`)} />
            </section>;
        }

        if (disabled) {
            return null;
        }

        const accept = options?.isPurse ? `.xls,.xlsx` : `.txt`;
        const text = options?.isPurse ? `Отчёт` : `Выписка`;
        const onChange = fileInput => this._onChangeUploadButton(fileInput, options);

        return <Tooltip event={TooltipEventType.hover} content={`Размер файла не более 1 Мб.`} width={200}>
            <UploadButton
                loading={loading || importLoading}
                onChange={onChange}
                icon={`plus`}
                disabled={isArchiveSource}
                color={Color.White}
                className={cn(`uploadButton`)}
                accept={accept}
            >
                {text}
            </UploadButton>
        </Tooltip>;
    }

    _showNotification = options => {
        notificationManager.show(options);
    }

    _validateFileExtension(fileInput, options) {
        if (!options?.isPurse) {
            return null;
        }

        if (!/(.xlsx?)$/.test(fileInput?.files?.[0]?.name)) {
            return {
                type: `error`,
                duration: 4000,
                message: `Не удалось загрузить файл. Пожалуйста, убедитесь, что был выбран файл в формате xls, xlsx.`
            };
        }

        return null;
    }

    _onChangeUploadButton = (fileInput, options) => {
        const files = fileInput;
        let needToReloadTables = true;

        const fileExtensionError = this._validateFileExtension(fileInput, options);

        if (fileExtensionError) {
            files.value = ``;

            this._showNotification(fileExtensionError);

            return;
        }

        if (!this._isActualFileSize(files)) {
            this._showNotification(ImportHelper.getImportBubbleMessage(ImportStatusEnum.sizeLimit));
            needToReloadTables && this._updateTables();
            this.paymentImportStore.resetImport();

            return;
        }

        this.paymentImportStore.uploadFile(files, options)
            .then(response => {
                files.value = ``;

                if (response.Status === ImportStatusEnum.wrongFile && options?.isPurse) {
                    showPurseImportErrorModal(response?.ExData?.Errors);

                    return;
                }

                if (
                    response.Status !== ImportStatusEnum.newSettlement &&
                    response.Status !== ImportStatusEnum.archiveSettlement &&
                    response.Status !== ImportStatusEnum.wrongOperations &&
                    response.Status !== ImportStatusEnum.inProcess
                ) {
                    this._showNotification(ImportHelper.getImportBubbleMessage(response.Status));
                }

                if (response.Status === ImportStatusEnum.success) {
                    this.moneySourceStore.loadList();
                    this._setImportedAccount(response);
                }

                if (response.Status === ImportStatusEnum.inProcess) {
                    needToReloadTables = false;
                    this.paymentImportStore.toggleBigFileNotificationDialogVisibility();
                }
            })
            .catch(() => {
                this._showNotification(ImportHelper.getImportBubbleMessage());
                needToReloadTables && this._updateTables();
                this.paymentImportStore.resetImport();
            });
    }

    _isActualFileSize(files) {
        const file = files.files[0];
        const sizeLimit = 1024 * 1024;

        return file.size <= sizeLimit;
    }

    _resetImport = () => {
        this.paymentImportStore.resetImport();
    }

    _onConfirmDialog = () => {
        if (!this.paymentImportStore.isValid()) {
            return;
        }

        this.paymentImportStore.confirmImport()
            .then(response => {
                if (response.Status === ImportStatusEnum.success) {
                    this._setImportedAccount(response);
                    this._showNotification(ImportHelper.getImportBubbleMessage(response.Status));
                }

                if (response.Status === ImportStatusEnum.inProcess) {
                    this.paymentImportStore.toggleBigFileNotificationDialogVisibility();
                }
            })
            .catch(() => {
                this._showNotification(ImportHelper.getImportBubbleMessage());
                this._updateTables();
                this.paymentImportStore.resetImport();
            });
    }

    _setImportedAccount = response => {
        const { onImportSuccess } = this.props;
        const sourceType = MoneySourceType.SettlementAccount;
        const sourceId = response.ExData.SettlementAccountId;

        onImportSuccess && onImportSuccess({ reset: true, newAccount: { sourceId, sourceType } });
    }

    _updateTables = () => {
        const { onImportSuccess } = this.props;

        onImportSuccess && onImportSuccess({ reset: true });
    }

    _renderBigFileNotificationDialog = () => {
        const { toggleBigFileNotificationDialogVisibility } = this.paymentImportStore;

        return (
            <Modal
                header={`Внимание!`}
                width={`550px`}
                onClose={toggleBigFileNotificationDialogVisibility}
                className={`qa-dialogImportBigPayment`}
                visible
            >
                <p>Импорт произойдет в фоновом режиме. По окончании импорта операции появятся в таблице.</p>
                <ElementsGroup>
                    <Link
                        text={`Закрыть`}
                        onClick={toggleBigFileNotificationDialogVisibility}
                        className={`qa-closeText`}
                    />
                </ElementsGroup>
            </Modal>
        );
    }

    _renderConfirmDialog = () => {
        const { isCurrencyAccount, moneyAccess, importMessage: { header, text } } = this.paymentImportStore;

        if (isCurrencyAccount && !moneyAccess.CanEditCurrencyAccount) {
            notificationManager.show({
                type: `error`,
                message: `Недостаточно прав для создания счета в иностранной валюте, обратитесь в тех. поддержку`
            });

            setTimeout(() => this._resetImport(), 0);

            return null;
        }

        return (
            <Modal
                header={header}
                width={`350px`}
                onClose={this._resetImport}
                visible
            >
                {isCurrencyAccount ? <AddCurrencyAccountContent store={this.paymentImportStore} /> : <p>{renderHtml(text)}</p>}
                <NewCheckingAccountDialog onClose={this._resetImport} onConfirm={this._onConfirmDialog} store={this.paymentImportStore} />
            </Modal>
        );
    }

    render() {
        const { isConfirmDialogShown, isBigFileNotificationDialogShown } = this.paymentImportStore;
        const { filter } = this.props;

        if (filter) {
            const sourceType = parseInt(filter.sourceType, 10) || MoneySourceType.All;

            return <Fragment>
                {this._renderBankSourceAction(filter)}
                {sourceType === MoneySourceType.Cash && <CashDownloadButton filter={filter} />}
                {this.kudirStore?.isKudirButtonVisible &&
                <KudirDownloadButton data={this.kudirStore?.availableReports} canShowDropdown={this.kudirStore?.canShowDropdown} selectReportType={this.kudirStore?.selectReportType} />}
                {isConfirmDialogShown && this._renderConfirmDialog()}
                {isBigFileNotificationDialogShown && this._renderBigFileNotificationDialog()}
            </Fragment>;
        }

        return null;
    }
}

ActionSource.propTypes = {
    moneySourceStore: PropTypes.object,
    paymentImportStore: PropTypes.object,
    kudirStore: PropTypes.object,
    commonDataStore: PropTypes.object.isRequired,
    filter: PropTypes.object,
    onImportSuccess: PropTypes.func
};

export default ActionSource;

