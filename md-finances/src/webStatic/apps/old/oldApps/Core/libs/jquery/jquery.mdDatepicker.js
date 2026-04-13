/* global $ */

import ReactDOM from 'react-dom';
import React from 'react';
import Days from '@moedelo/frontend-core-react/components/calendar/Days';
import zIndexHelper from '@moedelo/frontend-core-v2/helpers/zIndexHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import onClickOutside from 'react-onclickoutside';

/* eslint-disable */
function datepicker(options) {
    const input = $(this);

    if (input.hasClass(`hasDatepicker`)) {
        return;
    }

    let visible = false;
    const clickOutsideConfig = {
        handleClickOutside() {
            return (evt) => {
                if (!$(evt.target).closest(`.datepickerWrapper`).length) {
                    hideCalendar();
                }
            };
        }
    };
    const Calendar = onClickOutside(Days, clickOutsideConfig);

    initInput();

    const wrapper = initCalendar();

    function initCalendar() {
        const container = $(`<div class="js-datepicker datePicker__calendarContainer" style="z-index: ${zIndexHelper.getMax()}"></div>`);
        container.hide();
        input.closest(`.datepickerWrapper`).append(container);
        return container;
    }

    function showCalendar() {
        ReactDOM.render(
            <Calendar
                onChangeMonth={({ value }) => onChangeMonth(value)}
                onChangeYear={({ value }) => onChangeYear(value)}
                onChange={({ value }) => onChange(value)}
                date={dateHelper(input.val(), `DD.MM.YYYY`, true).isValid() ? input.val() : null}
                minDate={options.minDate ? dateHelper(options.minDate).format(`YYYY-MM-DD`) : null}
            />,
            wrapper[0]
        );
        wrapper.show();
        visible = true;
    }

    function hideCalendar() {
        if (!visible) {
            return;
        }
        visible = false;
        ReactDOM.unmountComponentAtNode(wrapper[0]);
        wrapper.hide();
        input.trigger(`change`);
    }

    function isDisabled() {
        return this.attr(`disabled`);
    }

    function initInput() {
        if ($.fn.mdDatepickerMask && input.attr(`data-mask`) !== `false`) {
            input.mdDatepickerMask();
        }
        input.addClass(`hasDatepicker`);
        if (!input.closest(`.datepickerWrapper`).length) {
            if (input.attr(`id`)) {
                input.wrap(`<div class='datepickerWrapper' data-wrapper-for=${input.attr(`id`)}/>`);
            } else {
                input.wrap(`<div class='datepickerWrapper'/>`);
            }
        }

        /* костыль чтобы не отображать иконку у collapsible ссылки */
        if (!input.hasClass(`notDatepickerIcon`)) {
            if (!input.next().hasClass(`datepickerIcon`)) {
                const $span = $(`<span />`).addClass(`datepickerIcon`);
                input.after($span);
            }
            input.next()
                .unbind(`click`)
                .bind(`click`, () => {
                    if (isDisabled.call(input)) {
                        return;
                    }
                    if (visible) {
                        hideCalendar();
                    } else {
                        showCalendar();
                    }
                });
        }

        input.on(`focus`, () => {
            showCalendar();
        });
        input.on(`input`, (event) => {
            if (event.key === `Enter` || event.key === `Tab`) {
                hideCalendar();
            } else {
                showCalendar();
            }
        });
        input.on(`blur`, (e) => {
            if (e.isTrigger) {
                hideCalendar();
            }
        });
        input.on(`change`, () => {
            return !visible;
        });
    }

    function onChangeMonth(value) {
        input.val(value);
        showCalendar();
    }

    function onChangeYear(value) {
        input.val(value);
        showCalendar();
    }

    function onChange(value) {
        input.val(value);
        hideCalendar();
    }
}

// eslint-disable-next-line
$.fn.mdDatepicker = function(customOptions = {}) {
    return MoedeloDatepicker(this, customOptions);
};

function MoedeloDatepicker(elem, options) {
    return $(elem).each(function() {
        datepicker.call(this, options);
    });
}
