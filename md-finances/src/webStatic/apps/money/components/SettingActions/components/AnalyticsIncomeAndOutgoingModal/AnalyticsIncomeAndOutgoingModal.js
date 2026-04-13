import React from 'react';
import PropTypes from 'prop-types';
import ReportEnum from './enums/ReportEnum';
import UsnReportModal from './components/UsnReportModal/UsnReportModal';
import PatentReportModal from './components/PatentReportModal';

const AnalyticsIncomeAndOutgoingModal = ({
    report, onCloseDialog, registrationDate, patentList, visible
}) => {
    if (report === ReportEnum.Usn) {
        return <UsnReportModal visible={visible} onClose={onCloseDialog} registrationDate={registrationDate} />;
    }

    if (report === ReportEnum.Patent) {
        return <PatentReportModal visible={visible} onClose={onCloseDialog} patentList={patentList} />;
    }

    return null;
};

AnalyticsIncomeAndOutgoingModal.propTypes = {
    visible: PropTypes.bool,
    onCloseDialog: PropTypes.func,
    registrationDate: PropTypes.number,
    report: PropTypes.oneOf(Object.values(ReportEnum)),
    patentList: PropTypes.arrayOf(PropTypes.object)
};

export default AnalyticsIncomeAndOutgoingModal;
