import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Link from '@moedelo/frontend-core-react/components/Link';
import P from '@moedelo/frontend-core-react/components/P';

@observer
class TradingObjectAdditionslWarning extends React.Component {
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
                    {`Плательщики УСН при использовании объекта налогообложения
                     "доходы" не включают сумму торгового сбора в расходы, однако,
                      они вправе уменьшить на сумму уплаченного торгового сбора
                      единый налог при УСН с учетом следующих особенностей.`}
                </P>
                <P>
                    {`Единый налог при УСН (авансовый платеж по нему) за отчетный
                     (налоговый) период можно уменьшить на сумму торгового сбора,
                      уплаченного в течение этого налогового (отчетного) периода.`}
                </P>
                <P>
                    {`Если ведется несколько видов деятельности, то на сумму
                     уплаченного торгового сбора можно уменьшить только ту часть
                      налога (авансового платежа), которая сама рассчитана именно
                       по виду деятельности, по которому уплачивается сбор.`}
                </P>
                <P>
                    {`Уменьшение можно сделать только в случае, если торговый
                     сбор уплачен в бюджет того же региона, в который зачисляется
                      единый налог (авансовый платеж) при УСН. Например, ИП, который проживает
                       в Московской области, но ведет торговую деятельность только
                        в г. Москве, не вправе уменьшить единый налог при УСН на сумму
                         торгового сбора. Это связано с тем, что торговый сбор зачисляется
                         в бюджет г. Москвы, а единый налог при УСН (авансовые платежи по нему)
                          – в бюджет Московской области по месту жительства ИП.`}
                </P>
                <P>
                    {`Вычет нельзя применить если налогоплательщик не представил в отношении
                     объекта, по которому уплачен торговый сбор, уведомление о постановке
                     на учет в качестве плательщика торгового сбора. Вместе с тем несвоевременное
                      представление уведомления не лишит права на уменьшение суммы налога (авансового платежа).`}
                </P>
                <P>
                    {`Сумма, отраженная в поле "Расход" блока налоговый учет уменьшит сумму
                     налога (авансового платежа по нему), попадая в раздел IV КУДИР.
                      При необходимости с учетом вышеописанных особенностей применения
                       вычета на сумму торгового сбора, измените сумму, отраженную в поле "Расход".`}
                </P>
            </Fragment>
        );
    }

    render() {
        if (!this.props.operationStore.canShowTradingFeeAdditionalWarning) {
            return null;
        }

        return (
            <div className={grid.col_24}>
                <P>При уменьшении суммы налога на торговый сбор необходимо учитывать <Link text={`следующее`} type={`modal`} onClick={this.toggleAdditionalText} />:</P>
                {this.renderAdditionalText()}
            </div>
        );
    }
}

TradingObjectAdditionslWarning.propTypes = {
    operationStore: PropTypes.object
};

export default TradingObjectAdditionslWarning;
