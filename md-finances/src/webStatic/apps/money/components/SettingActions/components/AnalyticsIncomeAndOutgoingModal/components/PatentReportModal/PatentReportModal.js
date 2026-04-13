import React from 'react';
import PropTypes from 'prop-types';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import { downloadPatentReport } from '../../../../../../../../services/newMoney/analyticsIncomeAndOutgoingService';
import DownloadModal from '../DownloadModal';
import { getPatents, getYears } from './helpers/dropdownDataHelper';

const PatentReportModal = ({ visible, patentList, onClose }) => {
    const [patentId, setPatentId] = React.useState(null);
    const [year, setYear] = React.useState(null);

    const onSelectPatent = ({ value }) => {
        setPatentId(value);
    };

    const onSelectYear = ({ value }) => {
        setYear(value);
    };

    const onDownload = () => {
        return downloadPatentReport(patentId);
    };

    return <DownloadModal
        visible={visible}
        header={`Аналитика по доходам`}
        download={onDownload}
        onClose={onClose}
    >
        <div className={grid.row}>
            <div className={grid.col_8}>
                <Dropdown
                    data={getYears(patentList)}
                    onSelect={onSelectYear}
                    value={year}
                    className={`selectYear`}
                />
            </div>
            <div className={grid.col_16}>
                <Dropdown
                    width={200}
                    data={getPatents({ patentList, year })}
                    onSelect={onSelectPatent}
                    placeholder="Выберите патент"
                    value={patentId}
                    className={`selectPatent`}
                />
            </div>
        </div>
    </DownloadModal>;
};

PatentReportModal.propTypes = {
    visible: PropTypes.bool,
    patentList: PropTypes.arrayOf(PropTypes.object),
    onClose: PropTypes.func
};

export default PatentReportModal;
