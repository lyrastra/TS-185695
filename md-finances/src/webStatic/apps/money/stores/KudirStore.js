import {
    observable, action, computed, runInAction, makeObservable
} from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import localStorageHelper from '@moedelo/frontend-core-v2/helpers/localStorageHelper';
import NotificationManager, { NotificationType } from '@moedelo/frontend-core-react/helpers/notificationManager';
import {
    downloadOsnoReport, downloadReport, getKudirInfo, getPeriodWarnings, sendEvent,
    sendKudirEmail
} from '../services/kudirService';
import { mapKudirs, mapParams, mapPeriods } from '../mappers/kudirMapper';
import { showKudirDownloadModal } from '../components/ActionSource/components/KudirDownloadModal';
import KudirEnum from '../enums/KudirEnum';
import defaultParams from './models/KudirModel';
import WarningHelper from '../helpers/WarningHelper';
import UsnSizeEnum from '../enums/UsnSizeEnum';
import defaultWarning from './models/KudirWarningModel';
import KudirTypeResource from '../resources/KudirTypeResource';

class KudirStore {
    @observable availableReports = []
    @observable hideCreateButton = true
    @observable currentPeriods = []
    @observable params = defaultParams
    @observable isIpUsn15 = false
    @observable periodWarnings = defaultWarning
    @observable isModalVisible = false
    @observable isWarningLoading = false
    @observable isAfterLoading = true

    constructor(props) {
        makeObservable(this);

        this.isOoo = props.userInfo.isOoo;
        this.isOsno = props.isOsno;
        this.userEmail = props.userInfo.userEmail;
        this.isUsn = props.isUsn;
        this.usnSize = props.usnSize;

        this.init();
    }

    init = async () => {
        await this.getKudirInfo();
        await this.getAccountingPolicy();
        this.openModalFromLocalStorage();
    }

    getKudirInfo = async () => {
        const reports = await getKudirInfo();

        this.setCurrentPeriods(reports[0]?.Periods);
        this.changeParams({ ReportType: reports[0]?.Type });

        const mappedKudirs = mapKudirs(reports);
        this.setKudirInfo({ availableReports: mappedKudirs });
    }

    selectReportType = reportType => {
        sendEvent({
            event: `vybor_tipa_kudir_click_dropdown`,
            st5: this.params.ReportType
        });

        if (reportType) {
            this.findPeriods(reportType);
            this.changeParams({ ReportType: reportType });
        }

        this.openModal();
    }

    openModal = async () => {
        const mappedPeriod = mapPeriods(this.currentPeriods);
        this.setModalVisible(true);
        showKudirDownloadModal({ store: this, periods: mappedPeriod });
        this.isAfterLoading = false;
    }

    findPeriods = reportType => {
        const currentReport = this.availableReports.find(report => report.value === reportType);
        this.setCurrentPeriods(currentReport.Periods);
    }

    downloadReport = () => {
        sendEvent({
            event: `skachivanie_kudir_modal_click_button`,
            st5: this.params.ReportType
        });
        const paramsForServer = mapParams(this.params);

        if (this.params.ReportType === KudirEnum.KudirOsno) {
            downloadOsnoReport(paramsForServer);

            return;
        }

        downloadReport(paramsForServer);
    }

    changePeriod = period => {
        if (!this.isAfterLoading) {
            sendEvent({
                event: `smena_perioda_kudir_modal_click_dropdown`,
                st5: period.Year
            });
        }

        this.changeParams({ ...period });
        this.getPeriodWarnings();
    }

    onRecalculate = async () => {
        sendEvent({ event: `pereracshitat_kudir_modal_click_button` });

        const response = await sendKudirEmail({ Year: this.params.Year, ReportType: KudirTypeResource[this.params.ReportType] });

        if (response !== true) {
            NotificationManager.show({
                message: `Что-то пошло не так`,
                type: NotificationType.error,
                duration: 4000
            });

            return;
        }

        NotificationManager.show({
            message: `Запрос успешно отправлен.`,
            type: NotificationType.success,
            duration: 4000
        });
    }

    onCloseModal = () => {
        this.setModalVisible(false);
        sendEvent({ event: `zakrytie_kudir_modal_click_button` });
        this.isAfterLoading = true;
    }

    openModalFromLocalStorage = () => {
        const lsKey = `openKudirModal`;
        const canOpenModal = localStorageHelper.get(lsKey);
        localStorageHelper.remove(lsKey);

        if (!canOpenModal) {
            return;
        }

        this.selectReportType(KudirEnum.KudirUsn);
    }

    @action changeParams = updatedPart => {
        this.params = { ...this.params, ...updatedPart };
    }

    @action setKudirInfo = ({ availableReports }) => {
        runInAction(() => {
            this.availableReports = availableReports;
        });
    }

    @action setCurrentPeriods = periods => {
        this.currentPeriods = periods;
    }

   @action getAccountingPolicy = () => {
       this.isIpUsn15 = this.isUsn && this.usnSize === UsnSizeEnum[15] && !this.isOoo;
   }

   @action getPeriodWarnings = async () => {
       if (!this.canShowRecalculate) {
           this.periodWarnings = defaultWarning;

           return;
       }

       this.isWarningLoading = true;
       const startDate = dateHelper().startOf(`year`).format(`DD.MM.YYYY`);
       const endDate = dateHelper().endOf(`year`).format(`DD.MM.YYYY`);
       const period = { startDate, endDate };

       const periodWarnings = await getPeriodWarnings(period);

       const wizard = {
           Year: this.params.Year,
           PeriodNumber: this.params.Number,
           IsProfit: false,
           IsSimpleMode: false
       };

       const warnings = WarningHelper.build({ wizard, additionalData: periodWarnings, period });

       this.periodWarnings = warnings;
       this.isWarningLoading = false;
   }

   @action setModalVisible = value => {
       this.isModalVisible = value;
   }

    @computed get isKudirButtonVisible() {
       return (!!this.availableReports.length && !(this.isOoo && this.isOsno));
   }

    @computed get canShowDropdown() {
        return this.availableReports.length > 1;
    }

    @computed get canShowRecalculate() {
        const year = dateHelper().year();

        return year === this.params.Year && this.isIpUsn15 && this.params.ReportType === KudirEnum.KudirUsn;
    }
}

export default KudirStore;
