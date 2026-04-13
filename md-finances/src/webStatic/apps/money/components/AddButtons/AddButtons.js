import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import classnames from 'classnames/bind';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import Loader from '@moedelo/frontend-core-react/components/Loader';
import style from './style.m.less';

const cn = classnames.bind(style);

@observer
class AddButtons extends React.Component {
    constructor(props) {
        super(props);

        this.store = props.store;
        this.commonDataStore = props.commonDataStore;
    }

    _isButtonDisabled() {
        const activeList = this.store.list.filter(item => item.IsActive);
        const isArchiveSource = this.store.isArchiveSource();

        return isArchiveSource || !activeList.length || !this.commonDataStore.hasAccessToMoneyEdit;
    }

    render() {
        const { onClickIncomingButton, onClickOutgoingButton } = this.props;
        const { loading } = this.store;
        const disabled = this._isButtonDisabled();

        const { loadingAccessToMoney } = this.commonDataStore;

        if (loading || loadingAccessToMoney) {
            return <section className={cn(`loaderSection`)}>
                <Loader className={cn(`loader`)} />
            </section>;
        }

        return <Fragment>
            <Button
                onClick={onClickIncomingButton}
                disabled={disabled}
            >
                {svgIconHelper.getJsx({ name: `plus` })}
                Поступление
            </Button>
            <Button
                onClick={onClickOutgoingButton}
                disabled={disabled}
            >
                {svgIconHelper.getJsx({ name: `plus` })}
                Списание
            </Button>
        </Fragment>;
    }
}

AddButtons.propTypes = {
    onClickIncomingButton: PropTypes.func.isRequired,
    onClickOutgoingButton: PropTypes.func.isRequired,
    store: PropTypes.object.isRequired,
    commonDataStore: PropTypes.object.isRequired
};

export default AddButtons;
