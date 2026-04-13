import React from 'react';
import PropTypes from 'prop-types';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Modal from '@moedelo/frontend-core-react/components/Modal';
import ElementsGroup from '@moedelo/frontend-core-react/components/ElementsGroup';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import Link from '@moedelo/frontend-core-react/components/Link';
import closedPeriodService from './../../../../../../services/closedPeriodService';
import style from './style.m.less';

class ClosedPeriodDialog extends React.Component {
    constructor() {
        super();

        this.state = { loaded: false, process: false };
    }

    componentDidMount = async () => {
        const { EndDate, StartDate } = await closedPeriodService.getClosedPeriod(this.props.date);
        this.setState({
            EndDate, StartDate, loaded: true
        });
    };

    getTitle = () => {
        const start = dateHelper(this.state.StartDate, `DD.MM.YYYY`).format(`MMMM YYYY`);
        const end = dateHelper(this.state.EndDate, `DD.MM.YYYY`).format(`MMMM YYYY`);

        if (start === end) {
            return start;
        }

        return `${start} — ${end}`;
    };

    openPeriod = async () => {
        this.setState({ process: true });
        await closedPeriodService.openPeriod(this.props.date);
        window.location.reload();
    };

    render() {
        if (!this.state.loaded) {
            return null;
        }

        return <Modal
            width={`375px`}
            header={this.getTitle()}
            onClose={this.props.onClose}
            canClose
            visible
        >
            Будут удалены все записи в бухгалтерском и налоговом учете, созданные при закрытии месяца.
            <ElementsGroup className={style.buttons}>
                <Button onClick={this.openPeriod} loading={this.state.process}>
                    {svgIconHelper.getJsx({ name: `lock` })}
                    Открыть период
                </Button>
                <Link onClick={this.props.onClose}>
                    Отмена
                </Link>
            </ElementsGroup>
        </Modal>;
    }
}

ClosedPeriodDialog.propTypes = {
    date: PropTypes.string,
    onClose: PropTypes.func
};

export default ClosedPeriodDialog;
