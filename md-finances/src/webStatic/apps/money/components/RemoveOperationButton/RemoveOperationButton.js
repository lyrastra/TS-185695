import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames/bind';
import Link from '@moedelo/frontend-core-react/components/Link/Link';
import Button from '@moedelo/frontend-core-react/components/buttons/Button/Button';
import { Type } from '@moedelo/frontend-core-react/components/buttons/enums';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper/svgIconHelper';
import { observer } from 'mobx-react';
import MoneyOperationService from '../../../../services/newMoney/moneyOperationService';
import ClosedDocumentsStatusEnum from '../../../../enums/ClosedDocumentsStatusEnum';
import RemoveOperationDialog from './RemoveOperationDialog';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class RemoveOperationButton extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isOpened: false,
            deleteStarted: false
        };
        this.commonDataStore = props.commonDataStore;
    }

    onDialogElementClick = () => {
        if (this.state.isOpened) {
            this.removeDialog();
        }
    }

    onClickRemoveButton = () => {
        this.props.onClick(this.props.operations)
            .then(() => {
                this.removeDialog();
                this.setState({ deleteStarted: false });
            });
    }

    getText = () => {
        const status = ClosedDocumentsStatusEnum;
        const { dialogType } = this.props;

        if (dialogType < status.Partly) {
            return <p>Вы уверены, что хотите удалить эти операции?</p>;
        } else if (dialogType > status.Partly) {
            return <p>
                Операции в закрытом периоде нельзя удалить.
                Вы можете <Link text="открыть период" onClick={this.openClosedPeriod} />
            </p>;
        }

        return <p>
            <strong>Внимание! </strong>
            Будут удалены только документы из открытого периода.
        </p>;
    }

    getHeaders = () => {
        return this.props.dialogType !== ClosedDocumentsStatusEnum.Completely ? `Удаление` : `Удаление невозможно`;
    }

    getDate = () => {
        return new Date(this.props.operations[0].Date);
    }

    openClosedPeriod = () => {
        this.removeDialog();

        return MoneyOperationService.openClosedPeriod(this.getDate())
            .then(() => {
                window.location.reload();
            });
    }

    removeDialog = () => {
        this.setState({
            isOpened: false
        });
    }

    openDialog = () => {
        setTimeout(() => {
            this.setState({
                isOpened: true
            });
        }, 150);
    }

    notClosedStatus = () => {
        return this.props.dialogType !== ClosedDocumentsStatusEnum.Completely;
    }

    render() {
        const disabled = !this.commonDataStore.hasAccessToMoneyEdit;
        const { loadingAccessToMoney } = this.commonDataStore;

        const { isOpened } = this.state;

        return (
            <div className={cn(`removeButtonContainer`)}>
                <Button
                    onClick={this.openDialog}
                    type={Type.Panel}
                    disabled={disabled}
                    loading={loadingAccessToMoney}
                >
                    { svgIconHelper.getJsx({ name: `remove` }) }
                    Удалить
                </Button>
                <RemoveOperationDialog
                    width={`307px`}
                    header={this.getHeaders()}
                    description={this.getText}
                    visible={isOpened}
                    disabled={this.notClosedStatus}
                    onButtonClick={this.onClickRemoveButton}
                    onClose={this.onDialogElementClick}
                />
            </div>
        );
    }
}

RemoveOperationButton.propTypes = {
    onClick: PropTypes.func,
    dialogType: PropTypes.number,
    commonDataStore: PropTypes.object.isRequired,
    operations: PropTypes.arrayOf(PropTypes.object)
};

export default RemoveOperationButton;
