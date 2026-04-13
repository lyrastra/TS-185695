import 'babel-polyfill';
import React from 'react';
import ReactDOM from 'react-dom';
import { Header, Nav } from '@moedelo/frontend-header/components';
import { setPublicPathForDomain } from '@moedelo/frontend-core-v2/helpers/importHelper';
import '@moedelo/frontend-header/components/Body';

setPublicPathForDomain();

ReactDOM.render(<Header />, document.getElementById(`headContent`));
ReactDOM.render(<Nav selectedMenuItem={window.selectedMenuItem} />, document.getElementById(`navContent`));
