import React from 'react';
import { HashRouter as Router, Switch, Route } from 'react-router-dom';
import ListOfRulesPage from './components/ListOfRulesPage';
import OneRulePage from './components/OneRulePage';
import style from './style.m.less';

const PaymentImportRules = () => {
    return <div className={style.wrapper}>
        <Router basename="/Finances/PaymentImportRules" hashType="noslash">
            <Switch>
                <Route path="/add">
                    <OneRulePage />
                </Route>
                <Route path="/edit/:ruleId?">
                    {
                        ({ match }) => <OneRulePage id={match?.params?.ruleId} />
                    }
                </Route>
                <Route path="/">
                    <ListOfRulesPage />
                </Route>
            </Switch>
        </Router>
    </div>;
};

export default PaymentImportRules;
