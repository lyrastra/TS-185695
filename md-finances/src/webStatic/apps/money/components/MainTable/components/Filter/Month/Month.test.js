/* global it, describe */
import React from 'react';
import Adapter from 'enzyme-adapter-react-16';
import { shallow, configure } from 'enzyme';
import Month from './Month';

configure({
    adapter: new Adapter()
});

describe(`Month - part of Date Filter`, () => {
    it(`props.value undefined - current year selected, month non selected`, () => {
        const component = shallow(<Month minDate={new Date(2015, 0)} maxDate={new Date(2017, 2)} onChange={() => {}} />);

        expect(component.find(`.qa-selectedYear`).length).toEqual(1, `year must be selected`);
        expect(component.find(`.qa-selectedYear`).text()).toEqual(new Date().getFullYear().toString());

        expect(component.find(`.qa-selectedMonth`).length).toEqual(0, `month must not be selected`);
    });

    it(`props.value={month: 2, year: 2016} - 2016 year selected, "фев" month selected`, () => {
        const component = shallow(<Month value={{ month: 2, year: 2016 }} minDate={new Date(2015, 0)} maxDate={new Date(2017, 2)} onChange={() => {}} />);

        expect(component.find(`.qa-selectedYear`).length).toEqual(1, `year must be selected`);
        expect(component.find(`.qa-selectedYear`).text()).toEqual(`2016`);

        expect(component.find(`.qa-selectedMonth`).length).toEqual(1, `month must be selected`);
        expect(component.find(`.qa-selectedMonth`).text()).toEqual(`фев`);
    });

    it(`props.value={quarter: 2, year: 2016} - current year selected, "апр май июн" month selected`, () => {
        const component = shallow(<Month value={{ quarter: 2, year: 2016 }} minDate={new Date(2015, 0)} maxDate={new Date(2017, 2)} onChange={() => {}} />);

        expect(component.find(`.qa-selectedYear`).length).toEqual(1, `year must be selected`);
        expect(component.find(`.qa-selectedYear`).text()).toEqual(`2016`);

        expect(component.find(`.qa-selectedMonth`).length).toEqual(3, `3 month must be selected`);
        expect(component.find(`.qa-selectedMonth`).at(0).text()).toEqual(`апр`);
        expect(component.find(`.qa-selectedMonth`).at(1).text()).toEqual(`май`);
        expect(component.find(`.qa-selectedMonth`).at(2).text()).toEqual(`июн`);
    });


    /** until jest will come :) */
    // it(`props.value=undefined; click on month "фев" - call onChange({year: currentYear, month: 2})`, () => {
    //     const component = mount(<TestWrapper value={{ quarter: 2, year: 2016 }} minDate={new Date(2015, 0)} maxDate={new Date(2017, 2)} onChange={() => {}} />);
    //     const february = component.find(Month).find(`.qa-month`).filterWhere(m => m.text() === `фев`);
    //
    //     expect(february.length).toEqual(1, `february must exist`);
    //
    //     february.simulate(`click`);
    //
    //     const state = component.state().value;
    //
    //     expect(state.month).toEqual(2);
    // });
});

// class TestWrapper extends React.Component {
//     render() {
//         return <Month {...this.props} onChange={value => this.setState({ value })} />;
//     }
// }
