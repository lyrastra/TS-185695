import React from 'react';
import { observer } from 'mobx-react';
import PropTypes from 'prop-types';
import ButtonDropdown from '@moedelo/frontend-core-react/components/buttons/ButtonDropdown';
import Button, { Color } from '@moedelo/frontend-core-react/components/buttons/Button';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';

import style from './style.m.less';

@observer
class KudirDownloadButton extends React.Component {
    constructor(props) {
        super(props);
        this.data = props.data;
        this.canShowDropdown = props.canShowDropdown;
        this.selectReportType = props.selectReportType;
    }

    onTypeSelect = e => {
        this.selectReportType(e.value);
    }

    render() {
        if (this.canShowDropdown) {
            return <ButtonDropdown
                mainButton={this.data?.[0]}
                data={this.data}
                onSelect={this.onTypeSelect}
                color={Color.White}
                className={style.button}
            >
                {svgIconHelper.getJsx({ name: `download` })}
                Книга доходов и расходов
            </ButtonDropdown>;
        }

        return <Button
            color={Color.White}
            onClick={this.onTypeSelect}
            className={style.button}
        >
            {svgIconHelper.getJsx({ name: `download` })}
            Книга доходов и расходов
        </Button>;
    }
}

KudirDownloadButton.propTypes = {
    data: PropTypes.arrayOf({}),
    canShowDropdown: PropTypes.bool,
    selectReportType: PropTypes.func
};

export default KudirDownloadButton;

