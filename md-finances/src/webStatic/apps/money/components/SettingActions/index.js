import React from 'react';
import PropTypes from 'prop-types';
import additionalAction from '@moedelo/frontend-core-react/icons/additionalAction.m.svg';
import scenarioSectionResource from '@moedelo/frontend-common-v2/apps/onboardingScenario/resources/scenarioSectionResource';
import Dropdown, { Type, DropdownPosition } from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import { metrics, sendMetric } from '../../../paymentImportRules/helpers/metricsHelper';
import ImportSettingsModal from './components/ImportSettingsModal';
import { getReports } from './components/AnalyticsIncomeAndOutgoingModal/helpers/dropdownDataHelper';
import AnalyticsIncomeAndOutgoingModal
    from './components/AnalyticsIncomeAndOutgoingModal';

const SettingActionsDropdown = ({ isUsn, registrationDate, patentList }) => {
    const [importSettingsModalVisible, setImportSettingsVisibility] = React.useState(false);
    const [analyticsModalVisible, setAnalyticsModalVisible] = React.useState(false);
    const [report, setReport] = React.useState(null);

    const onAnalyticsReportTypeClick = value => {
        setReport(value);
        setAnalyticsModalVisible(true);
    };

    const getSettingsDropdownData = () => {
        const data = [];

        data.push({
            onClick: () => {
                sendMetric(metrics.money_operations_settings_cog_icon_button_click);
                setImportSettingsVisibility(true);
            },
            text: `Настройки импорта`
        });

        if (isUsn) {
            data.push(...getReports(patentList, onAnalyticsReportTypeClick));
        }

        return data;
    };

    return <React.Fragment>
        <ImportSettingsModal isVisible={importSettingsModalVisible} onClose={setImportSettingsVisibility} />
        <AnalyticsIncomeAndOutgoingModal
            visible={analyticsModalVisible}
            report={report}
            registrationDate={registrationDate}
            patentList={patentList}
            onCloseDialog={() => setAnalyticsModalVisible(false)}
        />

        <Dropdown
            type={Type.icon}
            dropdownPosition={DropdownPosition.right}
            icon={additionalAction}
            onSelect={() => {
            }}
            data={getSettingsDropdownData()}
        />
        <div id={`tip_${scenarioSectionResource.Finances}_3`} />
        <div id={`tip_${scenarioSectionResource.Finances}_4`} />
    </React.Fragment>;
};

SettingActionsDropdown.propTypes = {
    isUsn: PropTypes.bool,
    registrationDate: PropTypes.string,
    patentList: PropTypes.arrayOf(PropTypes.shape({}))
};

export default React.memo(SettingActionsDropdown);
