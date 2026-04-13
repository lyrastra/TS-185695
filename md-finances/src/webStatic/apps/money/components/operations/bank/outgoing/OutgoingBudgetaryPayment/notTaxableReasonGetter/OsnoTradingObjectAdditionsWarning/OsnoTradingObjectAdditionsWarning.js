import React from 'react';
import P from '@moedelo/frontend-core-react/components/P';
import Link, { Type as LinkType } from '@moedelo/frontend-core-react/components/Link';
import CollapsibleContent from '@moedelo/frontend-core-react/components/CollapsiblePanel/components/CollapsibleContent';

const OsnoTradingObjectAdditionsWarning = () => {
    const [expanded, setExpanded] = React.useState(false);

    const toggleAdditionalText = () => setExpanded(currentExpandedState => !currentExpandedState);

    return <React.Fragment>
        <P>
            Сумма торгового сбора будет учтена при расчете НДФЛ при выполнении
            {` `}
            <Link type={LinkType.modal} onClick={toggleAdditionalText}>следующих условий</Link>:
        </P>
        <CollapsibleContent opened={expanded}>
            <P>
                – Торговый сбор уплачен в бюджет того же региона, в который зачисляется НДФЛ. Например, ИП,
                который проживает в Московской области, но ведет торговую деятельность только в г. Москве,
                не вправе уменьшить НДФЛ на сумму торгового сбора. Это связано с тем,
                что торговый сбор зачисляется в бюджет г. Москвы,
                а НДФЛ — в бюджет Московской области по месту жительства ИП.
            </P>
            <P>
                – Было подано уведомление о постановке на учет в качестве плательщика торгового сбора.
                Вычет нельзя применить, если налогоплательщик не представил в отношении объекта,
                по которому уплачен торговый сбор, уведомление о постановке на учет в качестве плательщика торгового сбора.
                Несвоевременное представление уведомления не лишит права на уменьшение суммы налога.
            </P>
        </CollapsibleContent>
    </React.Fragment>;
};

export default OsnoTradingObjectAdditionsWarning;
