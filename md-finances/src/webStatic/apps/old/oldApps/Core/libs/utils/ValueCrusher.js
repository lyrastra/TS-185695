import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* eslint-disable */
window.ValueCrusher = {
    parseStr: function (str) {
        if (str || str === 0) {

            str = (+str).toFixed(2);
            var str_temp = this.createStr(str);
            return str_temp;
        }

        return null;
    },

    parseStrHowIsNotRounded: function (str) {
        if (str || str === 0) {

            var str_temp = this.createStr(str);
            return str_temp;
        }

        return null;
    },

    createStr: function (str) {
        str = new String(str);
        var narr = str.split('.');
        var arr = narr[0].split('');
        var str_temp = '';

        for (var i = arr.length - 1, j = 1; i >= 0; i--, j++) {

            str_temp = arr[i] + str_temp;
            if (j % 3 == 0) {
                str_temp = ' ' + str_temp;
            }
        }

        if (narr[1]) {

            str_temp = str_temp + "," + narr[1];

        } else if (str_temp) {

            str_temp = str_temp + ",00";
        }

        return str_temp;
    },

        //ValueCrusher.reformatDate()
    reformatDate: function (str, type) {
        str = new String(str);
        var narr = str.split('.');

        if (type == "half") {
            if (narr[2] && narr[1]) {
                var reFormatedDate = narr[2] + "-" + narr[1] + "-" + narr[0];
            }
            else {
                if (narr[0] != "") {
                    var reFormatedDate = null;
                }
                else {
                    var reFormatedDate = narr[0];
                }
            }

        }
        else if(type == "object") {
            var quarter, month = narr[1];

            if (month <= 3) {
                quarter = 1;
            }
            else if (month > 3 && month <= 6) {
                quarter = 2;
            }
            else if (month > 6 && month <= 9) {
                quarter = 3;
            }
            else if (month > 9) {
                quarter = 4;
            }

            var reFormatedDate = {
                day: parseInt(narr[0], 10),
                month: parseInt(month, 10),
                year: parseInt(narr[2], 10),
                quarter: quarter
            };

            return reFormatedDate;
        }
        else {
            var reFormatedDate = Date.parse(narr[2] + "-" + narr[1] + "-" + narr[0]);
        }
        return reFormatedDate;
    },

	formatDate: function(date) {
		var day = date.getDate(),
            month = date.getMonth() + 1,
            year = date.getFullYear();

		if (month < 10) {
			month = "0" + month;
		}
		if (day < 10) {
			day = "0" + day;
		}


		return day + "." + month + "." + year;
	},

    convertToDate: function (str) {
        if (!_.isString(str)) {
            return null;
        }
        //        var patternFromServer = "/\/Date\((-?\d+)\)\//";
        if (/\/Date\((-?\d+)\)\//.test(str)) {
            var newDate = parseInt(str.replace(/[^0-9]/g, ""));
            return new Date(newDate);
        } else {
            var date = dateHelper(str, 'DD.MM.YYYY').toDate();
            return date;
        }
    },

    normalizeDate: function (badDate, formatType) {
        //        var newDate = parseInt(badDate.replace(/[^0-9]/g, ""));

        var monthes = new Array();
        monthes[0] = "января";
        monthes[1] = "февраля";
        monthes[2] = "марта";
        monthes[3] = "апреля";
        monthes[4] = "мая";
        monthes[5] = "июня";
        monthes[6] = "июля";
        monthes[7] = "августа";
        monthes[8] = "сентября";
        monthes[9] = "октября";
        monthes[10] = "ноября";
        monthes[11] = "декабря";

        /*var smallMonth = new Array(12);
        smallMonth[0] = "янв";
        smallMonth[1] = "фев";
        smallMonth[2] = "мар";
        smallMonth[3] = "апр";
        smallMonth[4] = "май";
        smallMonth[5] = "июн";
        smallMonth[6] = "июл";
        smallMonth[7] = "авг";
        smallMonth[8] = "сен";
        smallMonth[9] = "окт";
        smallMonth[10] = "ноя";
        smallMonth[11] = "дек";*/

        var currentDate = new Date(),
            currentYear = currentDate.getFullYear(),
            newDate = this.convertToDate(badDate),
            day = newDate.getDate(),
            month = newDate.getMonth(),
            monthInt = newDate.getMonth() + 1,
            year = newDate.getFullYear();


        if (monthInt < 10) {
            monthInt = "0" + monthInt;
        }
        if (day < 10) {
            day = "0" + day;
        }

        if (formatType == "year") {
            return year;
        }
        else if (formatType == "month") {
            return day + " " + monthes[month];
        } else if (formatType == "full") {
            return day + " " + monthes[month] + " " + year;
        } else {
            return day + "." + monthInt + "." + year;
        }

    },

    //ValueCrusher.rounding()
    rounding: function (value) {
        return value.toFixed(2);
    },

    trimmer: function (value) {
        value = $.trim(value.replace(/—/g, ""));
        return value;
    },

    //ValueCrusher.spaceKiller();
    spaceKiller: function (value) {
        value = $.trim(value.replace(/ /g, ""));
        return value;
    },

    //ValueCrusher.parseGetParams();
    parseGetParams: function () {
        var $_GET = {};
        var __GET = window.location.search.substring(1).split("&");
        for (var i = 0; i < __GET.length; i++) {
            var getVar = __GET[i].split("=");
            $_GET[getVar[0]] = typeof (getVar[1]) == "undefined" ? "" : getVar[1];
        }
        return $_GET;
    },
    //ValueCrusher.getCaretPos();
    getCaretPos: function (obj) {
        obj.focus();
        if (obj.selectionStart) return obj.selectionStart; //Gecko
        else if (document.selection) { // ie
            var sel = document.selection.createRange();
            var clone = sel.duplicate();
            sel.collapse(true);
            clone.moveToElementText(obj);
            clone.setEndPoint('EndToEnd', sel);
            return clone.text.length;
        }
        return 0;
    },

    //ValueCrusher.calculateDates();
    calculateDates: function (firmRegistrationDate) {
        var valueCrusher = this,
            today = new Date(),
            todayMonth = today.getMonth() + 1,
            startupCreating = 2002,
            registrationDate,
            todayDate = {
                month: todayMonth,
                year: today.getFullYear(),
                quarter: valueCrusher.quarterCalculation(todayMonth),
                halfYear: valueCrusher.quarterCalculation(todayMonth) > 2 ? 2 : 1
            },
            timeScope = {
                top: {
                    month: 12,
                    year: today.getFullYear(),
                    quarter: 4,
                    halfYear: 2
                },

                today: todayDate,

                bottom: {}
            };


        if (firmRegistrationDate) {
            var registrationDateArray = firmRegistrationDate.split(".");

            registrationDate = {
                month: parseInt(registrationDateArray[1], 10),
                year: parseInt(registrationDateArray[2])
            };
            //
            if (parseInt(registrationDateArray[2], 10) < startupCreating) {
                timeScope.bottom.year = startupCreating;
                timeScope.bottom.month = 1;
                timeScope.bottom.quarter = 1;
            }
            else if (registrationDate.year > startupCreating) {
                timeScope.bottom.year = registrationDate.year;
                timeScope.bottom.month = registrationDate.month;
                timeScope.bottom.quarter = valueCrusher.quarterCalculation(registrationDate.month);
            }
            else if (parseInt(registrationDateArray[2]) == startupCreating) {
                timeScope.bottom.year = startupCreating;
                if (registrationDate.month > 1) {
                    var bottomMonth = registrationDate.month;
                }
                else {
                    var bottomMonth = 1;
                }
                timeScope.bottom.month = bottomMonth;
                timeScope.bottom.quarter = valueCrusher.quarterCalculation(bottomMonth);
            }

            if (timeScope.bottom.quarter > 2) {
                timeScope.bottom.halfYear = 2;
            } else {
                timeScope.bottom.halfYear = 1;
            }
        }
        else {
            timeScope.bottom.year = startupCreating;
            timeScope.bottom.month = 1;
            timeScope.bottom.quarter = 1;
            timeScope.bottom.halfYear = 1;
        }

        return timeScope;
    },

    quarterCalculation: function (month) {
        var quarter;

        if (month <= 3) {
            quarter = 1;
        }
        else if (month > 3 && month <= 6) {
            quarter = 2;
        }
        else if (month > 6 && month <= 9) {
            quarter = 3;
        }
        else if (month > 9) {
            quarter = 4;
        }

        return quarter;
    },

    //ValueCrusher.makingDigitWithZero();
    makingDigitWithZero: function (digit) {
        var val = digit.toString();
        if (val.length == 1) {
            val = "0" + val;
        }
        return val;

    },

    //ValueCrusher.insertTextCursor();
    insertTextCursor: function (obj, _text, position)
    // _obj_name - name объекта - как правило, textarea, но при желании можно сделать любой
    // указываем именно NAME, так как согласно стандартам DOCTYPE HTML 4.01 strict и выше
    // свойство ID у объектов ввода является не приемлемым и требуется обращаться только name
    // _text - текст, который требуется вставить в том место, где сейчас находится курсор
    {
        // берем объект
        var area = obj;

        // Mozilla и другие НОРМАЛЬНЫЕ браузеры
        //        if ((area.selectionStart) || (area.selectionStart == '0')) { // определяем, где начало выделения, если оно существует
        //            var p_start = area.selectionStart;
        //
        //            // определяем, где заканчивается выделение, если оно существует
        //            var p_end = area.selectionEnd;
        //
        //            // вставляем соответствующий текст в указанное место
        //            area.value     = area.value.substring(0, p_start) + _text + area.value.substring(p_end, area.value.length);
        //        }
        area.value = area.value.substring(0, position) + _text + area.value.substring(position, area.value.length);
        // Исправляем очередной геморрой с Internet Explorer
        // единственный НЕ человеческий браузер
        //        if (document.selection)// если объект может иметь выделения
        //        { // передаем фокус ввода на нужный нам объект
        //            area.focus();
        //
        //            // узнаем выделение, если оно существует
        //            sel = document.selection.createRange();
        //
        //            // вставляет текст в указанное место
        //            sel.text = _text;
        //        }
    } // end function
};



function trim(str, charlist) {
    charlist = !charlist ? ' \s\xA0' : charlist.replace(/([\[\]\(\)\.\?\/\*\{\}\+\$\^\:])/g, '\$1');
    var re = new RegExp('^[' + charlist + ']+|[' + charlist + ']+$', 'g');
    return str.replace(re, '');
}
