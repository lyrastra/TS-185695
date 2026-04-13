import React from 'react';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import PropTypes from 'prop-types';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import { Type } from '@moedelo/frontend-core-react/components/buttons/enums';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper/svgIconHelper';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import changelog from '@moedelo/frontend-core-react/icons/changelog.m.svg';
import navigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import { downloadOperations } from '../../../../helpers/newMoney/operationActionsHelper';
import DownloadOperationsButtonsList from '../DownloadOperationsButtonsList';
import LinkEventStopper from './components/LinkEventStopper';
import autoPayIcon from '../../../../icons/autoPay.m.svg';
import storage from '../../../../helpers/newMoney/storage';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class OperationAdditionalActions extends React.Component {
    constructor(props) {
        super(props);

        /* для красной таблицы стор для смены сно не передается */
        this.massChangeTaxSystemStore = props.massChangeTaxSystemStore;
        this.operation = props.operation;
        this.props = props;
        this.state = {
            downloading: false,
            isDDClicked: false,
            isApproved: this.operation?.isApproved || false,
            isShowApproveBtn: this.operation?.isShowApprove && !this.operation?.isApproved && !this.props.isWarningTable && !this.props.isOutsourceProcessingTable
        };
    }

    componentDidUpdate() {
        if (this.state.isDDClicked) {
            this.setState({ isDDClicked: false });
        }
    }

    onHistoryButtonClick = () => {
        navigateHelper.open(`/changelog?entityId=${this.operation.DocumentBaseId}`);
        this._onDDClick();
    };

    _onDDClick = () => {
        this.setState({
            isDDClicked: !this.state.isDDClicked
        });
    };

    _downloadOperation = (operation, format) => {
        const { downloading } = this.state;

        if (!downloading) {
            this.setState({
                downloading: true
            });

            return downloadOperations([operation], format)
                .then(() => {
                    setTimeout(() => {
                        this.setState({
                            downloading: false
                        });
                    }, 300);
                });
        }

        return null;
    }

    _handleChangeTaxSystem = () => {
        const { massChangeTaxSystemStore } = this;

        massChangeTaxSystemStore && massChangeTaxSystemStore.setCheckedOperations([this.operation]);
    }

    _getDropdownData = () => {
        const {
            isDownloadable, is1CDisabled, canCopy, operation, onDelete, onSendToBank, isHistoryButtonVisible, canRemove
        } = this.props;

        const { canShowChangeButton, setModalVisibility } = this.massChangeTaxSystemStore || {};
        const data = [];

        if (isDownloadable) {
            data.push({
                content: <LinkEventStopper>
                    <DownloadOperationsButtonsList
                        operation={operation}
                        onDownload={this._downloadOperation}
                        is1CDisabled={is1CDisabled}
                    />
                </LinkEventStopper>
            });
        }

        data.push({
            content: <LinkEventStopper>
                {canShowChangeButton && <Button
                    className={cn(`button_wide`)}
                    type={Type.Panel}
                    onClick={() => setModalVisibility(true)}
                >
                    {svgIconHelper.getJsx({ file: autoPayIcon })}Сменить СНО
                </Button>}
                {onSendToBank && <Button
                    className={cn(`button_wide`)}
                    type={Type.Panel}
                    onClick={this._sendToBank}
                >
                    {svgIconHelper.getJsx({ name: `sendToBank` })}Отправить в банк
                </Button>}
                {canCopy && <Button
                    className={cn(`button_wide`)}
                    type={Type.Panel}
                    onClick={this._makeOperationCopy}
                >
                    {svgIconHelper.getJsx({ name: `copy` })}Копировать
                </Button>}
                {canRemove && <Button
                    className={cn(`button_wide`)}
                    onClick={() => { onDelete && onDelete(operation); }}
                    type={Type.Panel}
                >
                    {svgIconHelper.getJsx({ name: `remove` })}Удалить
                </Button>}
                {this.state.isShowApproveBtn && <Button
                    className={cn(`button_wide`)}
                    onClick={this._onApprove}
                    type={Type.Panel}
                >
                    {svgIconHelper.getJsx({ name: `reports` })}Обработать
                </Button>}
                {isHistoryButtonVisible && <Button
                    className={cn(`button_wide`)}
                    type={Type.Panel}
                    onClick={this.onHistoryButtonClick}
                >
                    {svgIconHelper.getJsx({ file: changelog })}История изменений
                </Button>}
            </LinkEventStopper>
        });

        data.push([]);

        return data;
    }

    _sendToBank = () => {
        const { onSendToBank, operation } = this.props;

        storage.save(`Scroll`, window.scrollY);

        onSendToBank && onSendToBank([operation]);
    }

    _makeOperationCopy = () => {
        const { copyOperation, operation } = this.props;

        mrkStatService.sendEventWithoutInternalUser(`copy_tablitsa_click_button`);

        return copyOperation && copyOperation(operation);
    };

    _onApprove = async () => {
        const { onApprove } = this.props;
        onApprove && onApprove(({ Id: this.operation.DocumentBaseId }));
        this._onDDClick();
        this.setState({ isShowApproveBtn: false });
    }

    render() {
        if (this.state.isDDClicked) {
            return null;
        }

        return (
            <div className={cn(`additionalActions__container`, { hidden: this.operation.isChecked })}>
                <Dropdown
                    width={200}
                    dropdownPosition={`right`}
                    data={this._getDropdownData()}
                    type={`dots`}
                    onSelect={() => {}}
                    onClick={this._handleChangeTaxSystem}
                    fixOverflow
                />
            </div>
        );
    }
}

OperationAdditionalActions.propTypes = {
    isDownloadable: PropTypes.bool,
    is1CDisabled: PropTypes.bool,
    canCopy: PropTypes.bool,
    copyOperation: PropTypes.func,
    operation: PropTypes.object,
    onDelete: PropTypes.func,
    onApprove: PropTypes.func,
    canRemove: PropTypes.bool,
    onSendToBank: PropTypes.oneOfType([
        PropTypes.func,
        PropTypes.bool
    ]),
    massChangeTaxSystemStore: PropTypes.object,
    isWarningTable: PropTypes.bool,
    isOutsourceProcessingTable: PropTypes.bool,
    isHistoryButtonVisible: PropTypes.bool
};

export default OperationAdditionalActions;

