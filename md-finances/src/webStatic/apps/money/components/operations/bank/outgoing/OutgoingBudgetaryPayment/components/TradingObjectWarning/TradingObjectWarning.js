import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Link from '@moedelo/frontend-core-react/components/Link';
import P from '@moedelo/frontend-core-react/components/P';

@observer
class TradingObjectWarning extends React.Component {
    constructor() {
        super();

        this.state = { isAdditionalTextShown: false };
    }

    toggleAdditionalText = () => {
        this.setState(({ isAdditionalTextShown }) => {
            return {
                isAdditionalTextShown: !isAdditionalTextShown
            };
        });
    }

    renderAdditionalText = () => {
        if (!this.state.isAdditionalTextShown) {
            return null;
        }

        return (
            <Fragment>
                <P>
                    {`– при уплате торгового сбора рекомендуем уточнить в налоговой инспекции код ОКТМО,
                    указываемый в платежном поручении. Это связано с тем , что в ряде случаев код ОКТМО
                    зависит от места расположения торгового объекта. По умолчанию выставлен ОКТМО налоговой
                    инспекции, которая указана в реквизитах;`}
                </P>
                <P>
                    {`– внимательно проверьте данные, отраженные в блоке "Налоговый учет"
                    и при необходимости скорректируйте их. Это связано с тем, что при
                    учете торгового сбора при расчете налогов есть ряд особенностей.`}
                </P>
            </Fragment>
        );
    }

    render() {
        if (!this.props.operationStore.isTradingFee) {
            return null;
        }

        return (
            <div className={grid.col_24}>
                <P>Внимание! Рекомендуем тщательно проверить <Link text={`следующую информацию`} type={`modal`} onClick={this.toggleAdditionalText} />:</P>
                {this.renderAdditionalText()}
            </div>
        );
    }
}

TradingObjectWarning.propTypes = {
    operationStore: PropTypes.object
};

export default TradingObjectWarning;
