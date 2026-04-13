import React from 'react';
import cn from 'classnames';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import H1 from '@moedelo/frontend-core-react/components/headers/H1';
import svgIconHelper from '@moedelo/frontend-core-react/helpers/svgIconHelper';
import Button from '@moedelo/frontend-core-react/components/buttons/Button';
import navigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import Link from '@moedelo/frontend-core-react/components/Link';
import style from './style.m.less';
import TablePaymentImportRules from './components/TablePaymentImportRules';
import { metrics, sendMetric } from '../../helpers/metricsHelper';

const ListOfRulesPage = () => {
    return <section>
        <header className={cn(grid.wrapper)}>
            <div className={cn(grid.row, grid.rowLarge, style.header)}>
                <div className={grid.col_6}>
                    <H1>Мои правила импорта</H1>
                </div>
                <div className={grid.col_4}>
                    <Link
                        href={`https://www.moedelo.org/manual/professionalnaja-uchjotnaja-sistema/rabota-v-servise/dengi-prof/pravila-po-raspoznavaniu-vypiski`}
                        target={`_blank`}
                        text={`Как это работает?`}
                    />
                </div>
                <div className={grid.col_11} />
                <div className={grid.col_3}>
                    <Button
                        className={`qa-paymentImportRulesAddButton`}
                        onClick={() => {
                            navigateHelper.push(`add`);
                            sendMetric(metrics.import_settings_rule_add_button_click);
                        }}
                    >
                        {svgIconHelper.getJsx({ name: `plus` })}
                        Правило
                    </Button>
                </div>
            </div>
        </header>
        <TablePaymentImportRules />
    </section>;
};

ListOfRulesPage.propTypes = {};

export default ListOfRulesPage;
