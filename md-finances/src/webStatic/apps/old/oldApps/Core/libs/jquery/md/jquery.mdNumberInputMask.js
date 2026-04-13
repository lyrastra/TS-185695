(function ($) {

    $.fn.mdNumberInputMask = function (settings) {
        if (window && window.TestScope) {
            window.TestScope = {
                NumberInput: NumberInput,
                InputEvent: inputEvent,
                Utils: utils
            };
        }

        return this.each(function () {
            var $input = $(this),
                currentObj = $input.data('MdNumberInputMask'),
                options = $.extend({
                    thousandSeparator: ' ',
                    decimalSeparator: ',',
                    numberOfDecimals: 2,
                    allowedNegative: false,
                    numberType: 'float',
                    symbolsLimitation: false,
                    needAddition: false,
                    maskSetEvent: 'keypress',
                    maxLength: 17
                }, settings),
                pluginObj;

            if (!currentObj) {
                pluginObj = new NumberInput($input, options);
                $input.data('MdNumberInputMask', pluginObj);
            } else {
                currentObj.refreshOptions(settings);
            }
        });
    };

    var NumberInput = function ($input, options) {
            var value = $input.val(),
                pluginOptions = options;

            if ($input.attr('data-number')) {
                pluginOptions.numberType = $input.attr('data-number');
            }

            if ($input.attr('data-maxlength')) {
                pluginOptions.maxLength = parseInt($input.attr('data-maxlength'), 10);
            } else if ($input.attr('maxlength')) {
              pluginOptions.maxLength = parseInt($input.attr('maxlength'), 10);
            }

            if (value !== '') {
                $input.val(utils.setMask($input[0].value, pluginOptions));
            }

            initEvents();

            this.refreshOptions = function (newOptions) {
                pluginOptions = $.extend(pluginOptions, newOptions);
            };

            function initEvents() {
                if(options.maskSetEvent !== 'change'){
                    $input.on('keyup.mdNumberInputMask', function (e) {
                        inputEvent.keyUpEvent(e, pluginOptions);
                    });
                    $input.on('keypress.mdNumberInputMask', function (e) {
                        inputEvent.keyPressEvent(e, pluginOptions);
                    });
                }
                $input.on('paste.mdNumberInputMask', function (e) {
                    inputEvent.pasteText(e, pluginOptions);
                });
                $input.on('focusout.mdNumberInputMask', function (e) {
                    inputEvent.onFocusOut(e, pluginOptions);
                });
                $input.on('change.mdNumberInputMask', function (e) {
                    inputEvent.changeText(e, pluginOptions);
                });
            }

            return this;
        },
        inputEvent = {
            constKey: {
                BorderControlKeys: 32,
                CurrentCultureSeparator: 44,
                DefaultsSeparator: 46,
                NegativCharacter: 45,
                Backspace: 8
            },
            keyPressEvent: function (e, options) {
                e = e || window.event;
                var key = e.keyCode || e.charCode || e.which;

                if (key === undefined) {
                    return false;
                }

                if (options.symbolsLimitation && e.target.value.length >= options.symbolsLimitation) {
                    e.preventDefault();
                    return false;
                }

                if (e.ctrlKey || e.altKey || e.metaKey || key < this.constKey.BorderControlKeys) {
                    return true;
                }

                var maskOptions = $.extend({}, options, { needAddition: false });
                return this.setChar(e, maskOptions, key);
            },
            keyUpEvent: function (e, options) {
                e = e || window.event;
                var key = e.keyCode || e.charCode || e.which;

                if (key === this.constKey.Backspace) {
                    utils.deleteSymbol(e.target, options);
                }

                return true;
            },
            onFocusOut: function (e, options) {
                var value = e.target.value;
                if(value !== '') {
                    e.target.value = utils.setMask(value, options);
                    $(e.target).trigger('change');
                }
            },
            pasteText: function (e, options) {
                e.target.value = e.originalEvent.clipboardData.getData('Text');
                this.changeText(e, options);
                this.preventDefault(e);
            },
            changeText: function (e, options) {
                e.target.value = utils.setMask(e.target.value, options);
            },
            preventDefault: function (e) {
                if (e.preventDefault) {
                    e.preventDefault();
                } else {
                    e.returnValue = false;
                }
            },
            setChar: function (e, options, key) {
                var result = utils.setCharInInput(key, e.target, options);

                inputEvent.preventDefault(e);
                return result;
            }
        },
        utils = {
            dependingOnInputType: {
                'int': function (wholePart, fractionPart, isWhole) {
                    return isWhole ? wholePart : '';
                },
                'float': function (wholePart, fractionPart, isWhole, isFraction) {
                    var result = wholePart + fractionPart;

                    if (!isWhole) {
                        return isFraction ? result : '';
                    }

                    return result;
                }
            },
            getSelection: function ($input) {
                var startPos, endPos, range;

                if (typeof $input.selectionStart === 'number' && typeof $input.selectionEnd === 'number') {
                    startPos = $input.selectionStart;
                    endPos = $input.selectionEnd;
                } else {
                    range = utils.createRange($input);
                    startPos = range.start;
                    endPos = range.end;
                }

                return {
                    start: startPos,
                    end: endPos,
                    status: startPos === endPos
                };
            },
            createRange: function ($input) {
                var startPos = 0, endPos = 0,
                    range = document.selection.createRange();

                if (range && range.parentElement() == $input) {
                    var vl = $input.value,
                        len = vl.length,
                        normalizedValue = vl.replace(/\r\n/g, '\n'),
                        textInputRange = $input.createTextRange(),
                        endRange = textInputRange;

                    textInputRange.moveToBookmark(range.getBookmark());
                    endRange.collapse(false);

                    if (textInputRange.compareEndPoints('StartToEnd', endRange) > -1) {
                        startPos = endPos = len;
                    } else {
                        startPos = -textInputRange.moveStart('character', -len);
                        startPos += normalizedValue.slice(0, startPos).split('\n').length - 1;

                        if (textInputRange.compareEndPoints('EndToEnd', endRange) > -1) {
                            endPos = len;
                        } else {
                            endPos = -textInputRange.moveEnd('character', -len);
                            endPos += normalizedValue.slice(0, endPos).split('\n').length - 1;
                        }
                    }
                }

                return { start: startPos, end: endPos };
            },
            setCharInInput: function (key, input, options) {
                var selection = utils.getSelection(input),
                    keyValue = String.fromCharCode(key),
                    value = input.value, beforeLen = input.value.length,
                    pos = 0;

                if (!isNaN(keyValue) || key === inputEvent.constKey.CurrentCultureSeparator || key === inputEvent.constKey.DefaultsSeparator ||
                    (key === inputEvent.constKey.NegativCharacter && options.allowedNegative)) {

                    var maskOptions = $.extend({}, options, { parse: false }),
                        inputValue = value.substring(0, selection.start) + keyValue + value.substring(selection.end);

                    input.value = utils.setMask(inputValue, maskOptions);

                    pos = selection.start + (input.value.length - beforeLen);

                    if (pos <= 0) {
                        pos = selection.start + 1;
                    }

                    utils.setCursorPosition(input, pos);

                    return true;
                }

                return false;
            },
            deleteSymbol: function (input, options) {
                var selection = utils.getSelection(input),
                    pos;

                input.value = utils.setMask(input.value, options);
                pos = selection.start - (selection.end - selection.start);
                utils.setCursorPosition(input, pos);
            },
            setMask: function (inputValue, options) {
                return mask(inputValue, options).getValueWithMask(options.needAddition);
            },
            setCursorPosition: function (elem, pos) {
                if (elem.setSelectionRange) {
                    elem.focus();
                    elem.setSelectionRange(pos, pos);
                } else if (elem.createTextRange) {
                    var range = elem.createTextRange();
                    range.collapse(true);
                    range.moveEnd('character', pos);
                    range.moveStart('character', pos);
                    range.select();
                }
            }
        };

    function mask(value, options) {

        function getWholeAndFraction() {
            value = value.replace(/\s/g, '').replace('.', options.decimalSeparator);
            return value.split(options.decimalSeparator);
        }

        function maskOffDefaultFractionPart(needAddition) {
            isFraction = false;
            if (needAddition && options.numberOfDecimals > 0) {
                return options.decimalSeparator + additionFractionPart(needAddition);
            }

            return '';
        }

        function additionFractionPart(needAddition) {
            var str = fractionPart.length ? fractionPart : '',
                i;

            if (needAddition) {
                for (i = 0; i < options.numberOfDecimals - fractionPart.length; i++) {
                    str += '0';
                }
            }

            if (str === '') {
                isFraction = false;
            }

            return str;
        }

        function checkFractionPart(needAddition) {
            if (fractionPart.length > options.numberOfDecimals) {
                return fractionPart.substring(0, options.numberOfDecimals);
            }

            return additionFractionPart(needAddition);
        }

        function maskOffWholePart() {
            wholePart = wholeAndFraction[0];

            if (!wholePart.length || (!options.allowedNegative && isNaN(wholePart))) {
                isWhole = false;
                return '0';
            }

            if (options.parse !== false) {
                if (wholePart !== '-'){
                    wholePart = parseInt(wholePart).toString();
                }
            }

            if (!options.allowedNegative) {
                wholePart = wholePart.replace('-', '');
            }

            if (wholePart.length >= options.maxLength) {
                wholePart = wholePart.substring(0, options.maxLength);
            }

            if (options.allowedNegative && wholePart.indexOf('-') !== -1) {
                wholePart = wholePart.replace(/-/g, '');
                wholePart = '-' + wholePart;
            }

            for (var p = wholePart.length; (p -= 3) >= 1;) {
                wholePart = wholePart.substring(0, p) + options.thousandSeparator + wholePart.substring(p);
            }

            return wholePart;
        }

        function maskOffFractionPart(needAddition) {
            var str;

            if (wholeAndFraction.length === 1) {
                return maskOffDefaultFractionPart(needAddition);
            }

            fractionPart = wholeAndFraction[1];

            if (isNaN(fractionPart)) {
                fractionPart = '';
            }

            str = checkFractionPart(needAddition);

            return options.decimalSeparator + str;
        }

        var wholePart = '', fractionPart = '',
            isWhole = true, isFraction = true,
            wholeAndFraction = getWholeAndFraction(),
            maskOff = function (needAddition) {
                var wholeStr = maskOffWholePart(),
                    fractionStr = maskOffFractionPart(needAddition),
                    resultFunc = utils.dependingOnInputType[options.numberType];

                if (resultFunc) {
                    var number = resultFunc(wholeStr, fractionStr, isWhole, isFraction);
                    if( parseFloat(value.replace(',', '.')) < 0 &&
                        parseFloat(number.replace(',', '.')) >= 0 &&
                        options.allowedNegative)
                    {
                        number = '-' + number;
                    }

                    return number;
                }

                return '';
            };

        return {
            getValueWithMask: maskOff
        };
    }

})(jQuery);