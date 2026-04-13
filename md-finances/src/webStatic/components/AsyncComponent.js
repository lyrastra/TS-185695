import React from 'react';
import PropTypes from 'prop-types';

class AsyncComponent extends React.Component {
    constructor(props) {
        super(props);
        this.Component = null;
        this.state = { Component: AsyncComponent.Component };
    }

    // eslint-disable-next-line camelcase
    UNSAFE_componentWillMount() {
        if (!this.state.Component) {
            this.props.loader().then(() => {
                this.setState({ Component: this.props.component });
            });
        }
    }

    // eslint-disable-next-line camelcase
    UNSAFE_componentWillReceiveProps(nextProps) {
        if (this.state.Component) {
            this.setState({ Component: nextProps.component });
        }
    }

    render() {
        const { stubComponent } = this.props;

        if (this.state.Component) {
            return this.state.Component;
        }

        return stubComponent;
    }
}

AsyncComponent.propTypes = {
    stubComponent: PropTypes.element,
    loader: PropTypes.func,
    component: PropTypes.element
};

export default AsyncComponent;
