import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/**
 *  В проекте маркетинг есть валидация email, password и etc.
 */

/* eslint-disable */
/// Выключить стандартную валидацию целых чисел - она не пропускает пробелы
$.validator.attributeRulesOld = $.validator.attributeRules;
$.validator.setDefaults({ ignore: '[data-val-ignore], :hidden, :disabled' });
$.validator.attributeRules = function(element) {
    var rules = $.validator.attributeRulesOld(element);
    delete rules['number'];
    delete rules['min'];
    return rules;
}

/// Форматный контроль ИНН
$.validator.addMethod('inn', function(valueItem) {
    var value = valueItem.replace(/\s/g, '');
    switch (value.length) {
        case 10:
            var controlSumm10 = ((2 * value.charAt(0) + 4 * value.charAt(1) + 10 * value.charAt(2) + 3 * value.charAt(3)
                + 5 * value.charAt(4) + 9 * value.charAt(5) + 4 * value.charAt(6) + 6 * value.charAt(7) + 8 * value.charAt(8)) % 11) % 10;
            if (value[9] != controlSumm10) {
                return false;
            }
            return true;
        case 12:
            var controlSumm11 = ((7 * value.charAt(0) + 2 * value.charAt(1) + 4 * value.charAt(2) + 10 * value.charAt(3) + 3 * value.charAt(4)
                + 5 * value.charAt(5) + 9 * value.charAt(6) + 4 * value.charAt(7) + 6 * value.charAt(8) + 8 * value.charAt(9)) % 11) % 10;
            var controlSumm12 = ((3 * value.charAt(0) + 7 * value.charAt(1) + 2 * value.charAt(2)
                + 4 * value.charAt(3) + 10 * value.charAt(4) + 3 * value.charAt(5) + 5 * value.charAt(6)
                + 9 * value.charAt(7) + 4 * value.charAt(8) + 6 * value.charAt(9) + 8 * value.charAt(10)) % 11) % 10;
            if ((value[10] != controlSumm11) || (value[11] != controlSumm12)) {
                return false;
            }
            return true;
        case 8:
            //Когда-то здесь будет форматный контроль. Может.
            return true;
        case 0:
            return true;
        default:
            return false;

    }
});

/// Длина ИНН
$.validator.addMethod('inn-length', function(valueItem) {
    if (!valueItem || !valueItem.length) {
        return true;
    }

    if (valueItem.length == 8 || valueItem.length == 10 || valueItem.length == 12) {
        return true;
    }

    return false;
});

/// Форматный контроль ИНН
$.validator.addMethod('productDeclarationNumber', function(valueItem) {
    var value = valueItem.replace(/\s/g, '');

    if (!value.length) {
        return true;
    }

    if (/^(\d){21}$/.test(value) || /^(\d){8}\/(\d){6}\/(\d){7}$/.test(value)) {
        return true;
    }

    return false;
});

/** Ограничение денежной суммы основного средства */
$.validator.addMethod('fixedAssetsRestriction', function(value) {
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }

    var sum = Converter.toFloat(value);
    if (sum !== false && sum < 40000) {
        return false;
    }
    return true;
});

/// Максимальная сумма для денег
$.validator.addMethod('maxMoney', function(value, element) {
    if (_.isUndefined(value) || value.length === 0) {
        return true;
    }

    var maxValue = Converter.toFloat($(element).data('val-maxmoney-value')) || 1000000000;

    var sum = Converter.toFloat(value);
    if (sum !== false && sum > maxValue) {
        return false;
    }
    return true;
});

/**
 * Максимально возможное число;
 * @see в атрибуте 'data-val-maxCount-customValue' можно указывать кастомное максимальное значение
 */
$.validator.addMethod('maxCountValue', function(value, element) {
    var defaultValue = $(element).attr('data-val-maxCount-customValue') || 1000000000000;

    if (_.isUndefined(value) || value.length === 0) {
        return true;
    }

    var currentValue = Converter.toFloat(value);

    if (currentValue > defaultValue) {
        return false;
    }

    return true;
});

/// Положительное целое число
$.validator.addMethod('positiveInteger', function(value) {
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }

    var sum = Converter.toInteger(value);
    if (sum === false || sum < 0) {
        return false;
    }
    return true;
});

/// валидация кбк
$.validator.addMethod('kbk', function(value) {
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }

    value = value.replace(new RegExp(' ', 'g'), '');
    var sum = Converter.toInteger(value);
    if (sum === false || sum < 0 || $.trim(value).length != 20) {
        return false;
    }
    return true;
});

/// валидация формата кбк
$.validator.addMethod('kbkFormat', function(value) {
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }

    var firstThreeDigits = value.substring(0, 3);
    if (firstThreeDigits == '000') {
        return false;
    }
    return true;
});

/// валидация формата октмо
$.validator.addMethod('oktmoZeroFormat', function(value) {
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }

    if (value == '00000000' || value == '00000000000') {
        return false;
    }
    return true;
});

/// валидация формата УИН
$.validator.addMethod('uinZeroFormat', function(value) {
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }

    if (value == '00000000000000000000' || value == '0000000000000000000000000') {
        return false;
    }
    return true;
});

/// Положительное число
$.validator.addMethod('positiveFloat', function(value) {
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }

    var sum = Converter.toFloat(value);
    if (sum === false || _.isNaN(sum) || sum < 0) {
        return false;
    }
    return true;
});

$.validator.addMethod('positiveFloatNotZero', function(value, element) {
    var notZero = $(element).attr('data-val-positiveFloatNotZero-notZero');
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }

    var sum = Converter.toFloat(value);

    if (notZero == 'true' && (sum === false || _.isNaN(sum) || sum == 0)) {
        return false;
    } else {
        if (sum === false || _.isNaN(sum) || sum < 0) {
            return false;
        }
    }
    return true;
});

/// аналогично positiveFloat, но можно вводить отрицательное число
$.validator.addMethod('float', function(value) {
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }

    var sum = Converter.toFloat(value);
    if (sum === false) {
        return false;
    }
    return true;
});

$.validator.addMethod('notNull', function(value) {
    var sum = Converter.toFloat(value);
    if (sum === 0) {
        return false;
    }
    return true;
});

$.validator.addMethod('agencyProfitSum', function(value) {
    var profitSum = Converter.toFloat(value);

    if (profitSum === false) {
        return true;
    }

    var sumVal = $('#Sum').val()
    var sum = Converter.toFloat(sumVal);

    if (sum === false) {
        return true;
    }

    if (profitSum > sum) {
        return false;
    }

    return true;
});

/**
 * Максимально возможное колличество знаков после запятой;
 * @see в атрибуте 'data-val-fractionalPart-custom' можно указывать кастомное значение
 */
$.validator.addMethod('fractionalPart', function(value, element) {
    if (_.isUndefined(value) || value.length === 0) {
        return true;
    }

    var sum = Converter.toFloat(value),
        customValue = $(element).attr('data-val-fractionalPart-custom'),
        defaultMinCount = customValue || 2,
        regExp = new RegExp('^-?([\\d ]?)*([.,]\\d{1,' + defaultMinCount + '})?$');

    if (sum === false) {
        return true;
    }

    if (regExp.test($.trim(value))) {
        return true;
    }

    return false;
});

/**
 * Максимально возможное колличество знаков после запятой;
 * @see в атрибуте 'data-val-maxlength-custom' можно указывать кастомное значение
 * @see в атрибуте 'data-val-maxlength-resetvalue' можно указывать кастомное значение
 */
$.validator.addMethod('totalmaxlength', function(value, element) {
    if (_.isUndefined(value) || value.length === 0) {
        return true;
    }
    var count = value.length,
        customValue = $(element).attr('data-val-maxlength-custom'),
        resetvalue = $(element).attr('data-val-maxlength-resetvalue'),
        defaultMaxCount = customValue || 20;
    if (value == resetvalue) {
        return true;
    } else if (count == defaultMaxCount) {
        return true;
    }

    return false;
});

/// Количество знаков после запятой должно быть не больше 3
$.validator.addMethod('fractionalPartForCount', function(value) {
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }

    var sum = Converter.toFloat(value);
    if (sum === false) {
        return true;
    }

    if (/^-?([\d ]?)*([.,]\d{1,3})?$/.test($.trim(value))) {
        return true;
    }

    return false;
});

$.validator.addMethod('budgetaryUsnSum', function(value) {
    var currentSettlement = parseInt($('#Settlement').val(), 10);
    var isCurrencySettlements = Md.Applications.Money.Data.Settlements.isCurrencySettlement(currentSettlement);

    clearValidation('UsnSum');

    if (isCurrencySettlements) {
        return true;
    }

    if ($('#EnvdSum').length && $('#EnvdSum').is(':visible') && $('#EnvdSum').val() > 0) {
        $('#EnvdSum').valid();
        return true;
    }

    var usnSum = Converter.toFloat(value);

    if (usnSum === false) {
        usnSum = 0;
    }

    var sum = Converter.toFloat($('#Sum').val());

    if (sum === false) {
        sum = 0;
    }

    if (sum < usnSum) {
        return false;
    }

    return true;
});

$.validator.addMethod('budgetaryEnvdSum', function(value) {
    clearValidation('UsnSum');
    clearValidation('EnvdSum');
    var currentSettlement = parseInt($('#Settlement').val(), 10);
    var isCurrencySettlements = Md.Applications.Money.Data.Settlements.isCurrencySettlement(currentSettlement);

    if (isCurrencySettlements) {
        return true;
    }

    var envdSum = Converter.toFloat(value);
    var resultSum;

    if (envdSum === false) {
        return true;
    }

    var sum = Converter.toFloat($('#Sum').val());

    if (sum === false) {
        sum = 0;
    }

    var usnSum = 0;
    if ($('#UsnSum').length && $('#UsnSum').is(':visible')) {
        usnSum = Converter.toFloat($('#UsnSum').val());
    }

    if (usnSum === false) {
        usnSum = 0;
    }

    resultSum = +(usnSum + envdSum).toFixed(2);

    if (sum < resultSum) {
        return false;
    }

    return true;
});

function clearValidation(elementId) {
    $('#' + elementId).removeClass('input-validation-error');
    $('[data-valmsg-for=' + elementId + ']').text('');
}

/// Корректный формат даты
$.validator.addMethod('date', function(value) {
    if (_.isUndefined(value) || value.length == 0) {
        return true;
    }
    var date = null;
    try {
        date = Converter.toDate(value);
    } catch(e) {
        return false;
    }
    if (_.isNull(date)) {
        return false;
    }
    return true;
});

$.validator.addMethod('differenceinmonth', function(value, element) {
    var date = ValueCrusher.reformatDate(value),
        siblings = $(element).siblings('input'),
        siblingsVal = ValueCrusher.reformatDate($(element).siblings('input').val()),
        elementName = $(element).attr('name'),
        anothSpan = $(element).siblings('.field-validation-valid:not([data-valmsg-for=' + elementName + '])')
    difference = Math.abs(siblingsVal - date);

    if (difference > 2592000000) {
        anothSpan.hide();
        siblings.addClass('input-validation-error');
        return false;
    }

    siblings.removeClass('input-validation-error');
    return true;
});

/// Минимальная дата
$.validator.addMethod('checkMinDate', function(value, element) {
    if (_.isUndefined(value) || value.length === 0) {
        return true;
    }
    var date = null;
    try {
        date = Converter.toDate(value);
    } catch(e) {
        return true;
    }

    if (_.isNull(date)) {
        return true;
    } else {
        var minDate = Converter.toDate($(element)[0].dataset.mindate);
        if (_.isDate(minDate)) {
            if (date < minDate) {
                return false;
            }
        }
        return true;
    }
});

$.validator.addMethod('minDate', function(value, element) {
    if (_.isUndefined(value) || value.length === 0) {
        return true;
    }
    var date = null;
    try {
        date = Converter.toDate(value);
    } catch(e) {
        return true;
    }

    if (_.isNull(date)) {
        return true;
    } else {
        var minDate = Converter.toDate($(element).attr('MinDateValue'));
        if (_.isDate(minDate)) {
            if (date < minDate) {
                return false;
            }
        }
        return true;
    }
});

$.validator.addMethod('offValidate', function(value, element) {
    var offValidAttr = $(element).hasClass('input-validation-custom');

    if (offValidAttr) {
        return false;
    }

    return true;
});

$.validator.addMethod('checkCashClosedGroup', function(value, element, useCash) {
    var useCash = $(element).attr('data-val-checkCashClosedGroup-useCash');
    var validator = this;

    if (validator.optional(element)) {
        return 'dependency-mismatch';
    }

    var previous = validator.previousValue(element);
    if (!validator.settings.messages[element.name]) {
        validator.settings.messages[element.name] = {};
    }

    if (useCash == false || useCash == 'false') {
        previous.old = '';
        return true;
    }

    previous.originalMessage = validator.settings.messages[element.name].checkCashClosedGroup;

    if (validator.pending[element.name]) {
        return 'pending';
    }
    if (previous.old === value) {
        return previous.valid;
    }

    previous.old = value;
    validator.startRequest(element);
    var data = {
        date: value
    };

    $.ajax({
        url: WebApp.Cash.CheckClosedGroupForDate,
        mode: 'abort',
        port: 'validate' + element.name,
        dataType: 'json',
        data: data,
        success: function(response) {
            validator.settings.messages[element.name].checkCashClosedGroup = previous.originalMessage;
            var valid = !response || response.Closed === false;
            if (valid) {
                var submitted = validator.formSubmitted;
                validator.prepareElement(element);
                validator.formSubmitted = submitted;
                validator.successList.push(element);
                validator.showErrors();
            } else {
                var errors = {};
                var message = validator.defaultMessage(element, 'checkCashClosedGroup');
                errors[element.name] = previous.message = $.isFunction(message) ? message(value) : message;
                validator.showErrors(errors);
            }
            previous.valid = valid;
            validator.stopRequest(element, valid);
        }
    });

    return 'pending';
});

$.validator.addMethod('lessThanMoney', function(value, element, field) {
    if (_.isUndefined(value) || value.length === 0) {
        return true;
    }

    var sum = Converter.toFloat(value);
    if (sum === false || sum < 0) {
        return true;
    }

    var otherSumVal = $('#' + field).val();

    if (_.isUndefined(otherSumVal) || otherSumVal.replace(/\s/g, '').length == 0) {
        return true;
    }

    var otherSum = Converter.toFloat(otherSumVal);
    if (otherSum === false || otherSum >= sum) {
        return true;
    }

    return false;
});

$.validator.addMethod('kbkRequired', function(value, element) {
    var isPatent = $(element).attr('data-val-kbkRequired-isPatent');

    if (isPatent == 'true') {
        if (_.isUndefined(value) || $.trim(value).length == 0) {
            return false;
        }
    }

    return true;
});

$.validator.addMethod('positiveFloatByUsnType', function(value, element) {
    if (_.isUndefined(value) || $.trim(value).length === 0) {
        return true;
    }

    var isUsn15 = $(element).attr('data-val-positiveFloatByUsnType-isUsn15');
    var sum = Converter.toFloat(value);

    if (sum === false || _.isNaN(sum) || sum < 0) {
        return false;
    }

    return true;
});

// В прочем списании для безденежного типа можно указыать отрицательные значения
$.validator.addMethod('positiveFloatForOtherType', function(value) {
    if (_.isUndefined(value) || $.trim(value).length === 0) {
        return true;
    }

    var moneyBayType = $('[name=MoneyBayType]:checked').val();
    var isWithoutMoney = moneyBayType && parseInt(moneyBayType, 10) === Enums.MoneyBayTypes.WithoutMoney;
    var sum = Converter.toFloat(value);

    if (sum === false || _.isNaN(sum) || (sum < 0 && !isWithoutMoney)) {
        return false;
    }

    return true;
});

// Нельзя учесть больше чем оплачено и подтверждено документом.
// См. https://pm.moedelo.org/browse/BUG-2558
$.validator.addMethod('usnLessThanSumm', function(value) {
    var sum = Converter.toFloat($('#Sum').val() || $('#SumOfLoan').val());
    var percent = Converter.toFloat($('#SumOfPercent').val());
    var usn = Converter.toFloat(value);
    var currentSettlement = parseInt($('#Settlement').val(), 10);
    var isCurrencySettlements = Md.Applications.Money.Data.Settlements.isCurrencySettlement(currentSettlement);

    //при невалидных числах действуют другие валидаторы
    if (!usn || isCurrencySettlements) {
        return true;
    }

    // В списаниях "Погашение займов учредителю" и "Погашение займов" значение, которое заносится в строку "Сумма расхода,
    // учитываемая в УСН" должно быть НЕ БОЛЬШЕ значения в строке "сумма процентов". В остальных списаниях учитывается сумма займа.
    if (percent) {
        return usn <= percent;
    } else {
        if (!sum) {
            return true;
        }
        return usn <= sum;
    }
});

$.validator.addMethod('requiredAccount', function(value, element, a) {
    var $el = $(element);

    if ($el.attr('offbalance')) {
        return true;
    }

    return value != '';
});

$.validator.addMethod('modelValidation', function(value, element) {
    var $el = $(element);
    var view = $el.closest('[data-id]');
    if (view.length === 0) {
        return true;
    }

    var cid = view.attr('data-id');
    var model = window.Validation.models[cid];
    if (!model) {
        return true;
    }

    var actionName = $el.attr('data-bind') || $el.attr('name');

    var result = model.validateAttr.call(model, actionName, value);
    if (result !== true) {
        var validator = $el.closest('form').data().validator;
        validator.settings.messages[$el.attr('name')].modelValidation = result;
        $el.closest('form').data('validator', validator);

        var unobtrusive = $el.closest('form').data().unobtrusiveValidation;
        unobtrusive.options.messages[$el.attr('name')].modelValidation = result;
        $el.closest('form').data('unobtrusiveValidation', unobtrusive);

        return false;
    }
    return true;
});

$.validator.addMethod('dateDifferenceOrEqual', function(value, element, a) {
    var siblingsSelector = $(element).attr('data-val-dateDifferenceOrEqual-target');
    var direction = $(element).attr('data-val-dateDifferenceOrEqual-direction');
    var $siblingsEl = $(siblingsSelector);
    var curValue = dateHelper(value, 'DD.MM.YYYY').toDate();
    var siblingsVal = dateHelper($siblingsEl.val(), 'DD.MM.YYYY').toDate();

    if (!curValue || !siblingsVal) {
        return true;
    } else {
        if (direction === 'more') {
            return curValue >= siblingsVal;
        } else {
            return curValue <= siblingsVal;
        }
    }

});

$.validator.unobtrusive.adapters.addBool('inn');
$.validator.unobtrusive.adapters.addBool('inn-length');
$.validator.unobtrusive.adapters.addBool('productDeclarationNumber');
$.validator.unobtrusive.adapters.addBool('kbk');
$.validator.unobtrusive.adapters.addBool('kbkFormat');
$.validator.unobtrusive.adapters.addBool('oktmoZeroFormat');
$.validator.unobtrusive.adapters.addBool('uinZeroFormat');
$.validator.unobtrusive.adapters.addBool('fixedAssetsRestriction');
$.validator.unobtrusive.adapters.addBool('maxMoney');
$.validator.unobtrusive.adapters.addBool('checkMinDate');
$.validator.unobtrusive.adapters.addBool('date');
$.validator.unobtrusive.adapters.addBool('offValidate');
$.validator.unobtrusive.adapters.addBool('differenceinmonth');
$.validator.unobtrusive.adapters.addBool('positiveInteger');
$.validator.unobtrusive.adapters.addBool('positiveFloat');
$.validator.unobtrusive.adapters.addBool('float');
$.validator.unobtrusive.adapters.addBool('notNull');
$.validator.unobtrusive.adapters.addBool('fractionalPart');
$.validator.unobtrusive.adapters.addBool('fractionalPartForCount');
$.validator.unobtrusive.adapters.addBool('usnLessThanSumm');
$.validator.unobtrusive.adapters.addBool('requiredAccount');
$.validator.unobtrusive.adapters.addBool('budgetaryUsnSum');
$.validator.unobtrusive.adapters.addBool('budgetaryEnvdSum');
$.validator.unobtrusive.adapters.addBool('agencyProfitSum');
$.validator.unobtrusive.adapters.addBool('minDate');
$.validator.unobtrusive.adapters.addBool('totalmaxlength');
$.validator.unobtrusive.adapters.addBool('modelValidation');
$.validator.unobtrusive.adapters.addBool('maxCountValue');
$.validator.unobtrusive.adapters.addBool('positiveFloatForOtherType');
$.validator.unobtrusive.adapters.addBool('dateDifferenceOrEqual');
$.validator.unobtrusive.adapters.addSingleVal('checkCashClosedGroup', 'useCash');
$.validator.unobtrusive.adapters.addSingleVal('kbkRequired', 'isPatent');
$.validator.unobtrusive.adapters.addSingleVal('positiveFloatByUsnType', 'isUsn15');
$.validator.unobtrusive.adapters.addSingleVal('positiveFloatNotZero', 'notZero');
$.validator.unobtrusive.adapters.addSingleVal('lessThanMoney', 'field');

