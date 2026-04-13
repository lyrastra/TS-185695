import React, { Fragment } from 'react';
import NotificationPanel from '@moedelo/frontend-core-react/components/NotificationPanel';
import Link from '@moedelo/frontend-core-react/components/Link';
import VoteForm from '@moedelo/frontend-common-v2/apps/marketing/components/VoteForm';

class NewFeature extends React.Component {
    constructor() {
        super();

        this.state = {
            dialogShow: false,
            panelShow: false
        };
    }

    onClose = () => {
        this.setState({ dialogShow: false, panelShow: true });
    };

    onDone = () => {
        this.setState({ dialogShow: false, panelShow: false });
    };

    onClosePanel = () => {
        this.setState({ panelShow: false });
    };

    onClickLink = () => {
        this.setState({ dialogShow: true });
    };

    render() {
        const { panelShow, dialogShow } = this.state;

        return <Fragment>
            {panelShow && <NotificationPanel type="info" canClose onClose={this.onClosePanel}>
            Вы можете оценить новый интерфейс операций по р/сч по&nbsp;
                <Link text={`ссылке`} onClick={this.onClickLink} />
            </NotificationPanel>}

            <VoteForm
                headerText={`Новый дизайн операций по р/сч!`}
                subHeaderText={`Оценить новый функционал можно путём нажатий на иконки "Похлопать" и "Кинуть", а так же вы можете оставить пожелание по данной доработке.`}
                open={dialogShow}
                onClose={this.onClose}
                onDone={this.onDone}
            />
        </Fragment>;
    }
}

export default NewFeature;
