import React from 'react';
import PropTypes from 'prop-types';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import P, { Size as ParagraphSize } from '@moedelo/frontend-core-react/components/P';
import Link, { Size as LinkSize } from '@moedelo/frontend-core-react/components/Link';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import requisitesService from '@moedelo/frontend-common-v2/apps/requisites/services/requisitesService';
import { downloadUsnReport } from '../../../../../../../../services/newMoney/analyticsIncomeAndOutgoingService';
import { getPeriods, getYears } from './helpers/dropdownDataHelper';
import DownloadModal from '../DownloadModal';

const UsnReportModal = ({ visible, onClose, registrationDate }) => {
    const [year, setYear] = React.useState(null);
    const [periods, setPeriods] = React.useState([]);
    const [isUsn15, setUsn15] = React.useState(false);

    React.useEffect(() => {
        const getTaxationSystem = async () => {
            const [{ IsUsn, UsnType }, { IsOoo }] = await Promise.all([taxationSystemService.getTaxSystem(), requisitesService.get()]);

            setUsn15(!IsOoo && IsUsn && UsnType === UsnTypeEnum.ProfitAndOutgo);
        };

        getTaxationSystem();
    }, []);

    const onSelectPeriod = ({ selected }) => {
        const result = selected.map(item => {
            return item.value;
        });

        setPeriods(result);
    };

    const onSelectYear = ({ value }) => {
        setYear(value);
    };

    const onDownload = () => {
        downloadUsnReport(periods, year);
    };

    const onClickKudirLink = () => {
        mrkStatService.sendEventWithoutInternalUser({
            event: `pereraschet_kudir_analytic_modal_click_link`
        });

        NavigateHelper.open(`/Rpt/App/Reports?showKudir=true`);
    };

    return (<DownloadModal
        visible={visible}
        header={`Аналитика по доходам и расходам (в рамках УСН)`}
        download={onDownload}
        onClose={onClose}
    >
        <div className={grid.row}>
            <div className={grid.col_16}>
                <Dropdown
                    width={200}
                    data={getPeriods()}
                    onSelect={onSelectPeriod}
                    placeholder="Выберите несколько"
                    multiple
                    value={periods}
                    className={`selectPeriod`}
                />
            </div>
            <div className={grid.col_8}>
                <Dropdown
                    data={getYears(registrationDate)}
                    onSelect={onSelectYear}
                    value={year}
                    className={`selectYear`}
                />
            </div>
        </div>
        {isUsn15 && <div className={grid.row}>
            <P size={ParagraphSize.small}>Расчет ознакомительный, для точного расчета <Link onClick={onClickKudirLink} size={LinkSize.small}>перерассчитайте КУДиР</Link></P>
        </div>}
    </DownloadModal>);
};

UsnReportModal.propTypes = {
    visible: PropTypes.bool,
    onClose: PropTypes.func,
    registrationDate: PropTypes.number
};

export default UsnReportModal;
