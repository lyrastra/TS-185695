(function ($) {
    $.fn.decimalMask = function (args) {
        var defaultSettings = {
            decimalSeparator: ',',
            thousandsSeparator: ' ',
            fractionalPart: 2,
            negative: false
        };

        if(arguments.length === 3 && arguments[0] === 'option'){
            var settingName = arguments[1],
                settingValue = arguments[0];

            return this.each(function () {
                var input = $(this);
                var inputOptions = input.data('decimalMask');
                inputOptions[settingName] = settingValue;
                input.data('decimalMask', inputOptions);
            });

        }
        var settings = $.extend({}, defaultSettings, args);
        
        if (settings.fractionalPart == 0) {
            settings.decimalSeparator = '';
        }

        return this.each(function () {
            var input = $(this);

            if (input.data('decimalMask')) {
                return;
            }
            
            input.data('decimalMask', settings);
            
            var prevValue = input.val();
            var fractionPoint = false;

            setMaskOnPrevValue();

            function getSettings(){
                return input.data('decimalMask') || {};
            }

            function setMaskOnPrevValue() {
                var current = input.get(0);
                setMask(current);
            }

            function setFractionPoint() {
                fractionPoint = true;
            }

            function clearFractionPoint() {
                fractionPoint = false;
            }

            function keyPressEvent(e) {
                e = e || window.event;
                var key = e.charCode || e.keyCode || e.which;
                if (key == undefined) return false;
                if (input.attr("readonly") && (key != 13 && key != 9)) return false;

                if (key < 48 || key > 57) {
                    if (key == 45) {
                        if (!getSettings().negative == false) {
                            preventDefault(e);
                            var k = String.fromCharCode(key);
                            setSymbol(k);
                        }
                        return false;
                    } else if (key == 43) {
                        return false;
                    } else if (key == 13 || key == 9) {
                        return true;
                    } else if (key == 37 || key == 39) {
                        if (e.shiftKey == true)
                            return false;
                        return true;
                    } else if (key == 44) {
                        if (fractionPoint == false && !checkFractial()) {
                            setSymbol(String.fromCharCode(key));
                            setFractionPoint();
                            return false;
                        }
                        clearFractionPoint();
                        return false;
                    } else if (key == 118) {
                        if (e.ctrlKey == true)
                            return true;
                        else return false;
                    } else if (key == 46) {
                        if (fractionPoint == false && !checkFractial()) {
                            setSymbol(String.fromCharCode(key));
                            setFractionPoint();
                            return false;
                        }
                        clearFractionPoint();
                        return false;
                    } else {
                        preventDefault(e);
                        return true;
                    }
                } else {
                    preventDefault(e);

                    var k = String.fromCharCode(key);
                    setSymbol(k);
                    return false;
                }
            }

            function keyDownEvent(e) {
                e = e || window.event;
                var key = e.charCode || e.keyCode || e.which;
                if (key == undefined) return false;
                if (input.attr("readonly") && (key != 13 && key != 9)) return false;

                var currentInput = input.get(0);
                var selection = input.getInputSelection(currentInput);
                var startPos = selection.start;
                var endPos = selection.end;

                var maxLengthFill = input.attr("maxlength") && parseInt(input.attr("maxlength")) <= input.val().length,
                    digitKeys = [13, 9, 8, 46, 63272];
                if (maxLengthFill && $.inArray(key, digitKeys) == -1 && (selection.start - selection.end) == 0) return false;

                if (key == 8) { // backspace key
                    preventDefault(e);

                    if (startPos == endPos) {
                        currentInput.value = currentInput.value.substring(0, startPos - 1) + currentInput.value.substring(endPos, currentInput.value.length);
                        startPos = startPos - 1;
                    } else {
                        currentInput.value = currentInput.value.substring(0, startPos - 1) + currentInput.value.substring(endPos, currentInput.value.length);
                    }
                    input.change();
                    setMask(currentInput, startPos);
                    return false;
                } else if (key == 9) { // tab key 
                    return true;
                } else if (key == 46 || key == 63272) { // delete key (63272 - special case for safari)
                    preventDefault(e);

                    if (currentInput.selectionStart == currentInput.selectionEnd) {
                        currentInput.value = currentInput.value.substring(0, startPos) + currentInput.value.substring(endPos + 1, currentInput.value.length);
                    } else {
                        currentInput.value = currentInput.value.substring(0, startPos) + currentInput.value.substring(endPos, currentInput.value.length);
                    }
                    input.change();
                    setMask(currentInput, startPos);
                    
                    return false;
                } else {
                    return true;
                }
            }

            function inputText(e) {
                var current = input.get(0);
                var selection = input.getInputSelection(current);

                setMask(current, selection.start + 1);
            }

            function setSymbol(symbol) {
                var currentInput = input.get(0);
                var selection = input.getInputSelection(currentInput);
                var startPos = selection.start;
                var endPos = selection.end;
                currentInput.value = currentInput.value.substring(0, startPos) + symbol + currentInput.value.substring(endPos, currentInput.value.length);
                setMask(currentInput, startPos + 1);
            }

            function checkFractial() {
                var number = input.get(0).value;
                var result = number.replace(".", ",").indexOf(",") != -1;
                return result;
            }

            function preventDefault(e) {
                if (e.preventDefault) {
                    e.preventDefault();
                } else {
                    e.returnValue = false;
                }
            }

            function setMask(selectInput, startPos) {
                if (!checkFractial()) clearFractionPoint();
                var befoLen = input.val().length;
                input.val(installMask(selectInput.value));
                var afterLen = input.val().length;
                startPos = startPos - (befoLen - afterLen);
                
                if (startPos) {
                    input.setCursorPosition(startPos);
                }
            }

            function changeInput() {
                if (input.val() != prevValue) {
                    input.change();
                    prevValue = input.val();
                }
            }

            function onFocusOut() {
                changeInput();
            }

            function installMask(v) {
                var wholePart, fractionPart, wholeStr = '', fractionStr = '', negativePath = '';

                var checkStr = "0123456789";

                var number = v.replace(".", ",");
                var wholeAndFraction = number.split(",");

                fractionPart = wholeAndFraction.length > 1 ? wholeAndFraction[1] : "";
                wholePart = wholeAndFraction[0];

                if (wholePart.length == 0) return (fractionPart.length > 0) ? "0" + getSettings().decimalSeparator + fractionPart : "";

                if (getSettings().negative && wholePart.length > 0 && wholePart.charAt(0) == '-') negativePath += "-";

                for (var i = 0; i < wholePart.length; i++) {
                    if (checkStr.indexOf(wholePart.charAt(i)) != -1) wholeStr += wholePart.charAt(i);
                }

                for (i = 0; i < fractionPart.length; i++) {
                    if (checkStr.indexOf(fractionPart.charAt(i)) != -1) fractionStr += fractionPart.charAt(i);
                }

                for (var p = wholeStr.length; (p -= 3) >= 1; ) {
                    wholeStr = wholeStr.substring(0, p) + getSettings().thousandsSeparator + wholeStr.substring(p);
                }

                fractionPart = (fractionStr.length > 0 && fractionStr.length > getSettings().fractionalPart) ? fractionStr.substring(0, getSettings().fractionalPart) : fractionStr;
                return (fractionPoint == false && !checkFractial()) ? negativePath + wholeStr : negativePath + wholeStr + getSettings().decimalSeparator + fractionPart;
            }
            
            input.bind("keypress.decimalMask", keyPressEvent);
            input.bind("keydown.decimalMask", keyDownEvent);
            input.bind("focusin", inputText);
            input.bind("focusout", onFocusOut);
        });
    };

    $.fn.getInputSelection = function (element) {
        var startPos = 0, endPos = 0;

        if (typeof element.selectionStart == "number" && typeof element.selectionEnd == "number") {
            startPos = element.selectionStart;
            endPos = element.selectionEnd;
        } else {
            var range = document.selection.createRange();

            if (range && range.parentElement() == element) {
                var len = element.value.length;
                var normalizedValue = element.value.replace(/\r\n/g, "\n");

                // Create a working TextRange that lives only in the input
                var textInputRange = element.createTextRange();
                textInputRange.moveToBookmark(range.getBookmark());

                // Check if the start and end of the selection are at the very end
                // of the input, since moveStart/moveEnd doesn't return what we want
                // in those cases
                var endRange = element.createTextRange();
                endRange.collapse(false);

                if (textInputRange.compareEndPoints("StartToEnd", endRange) > -1) {
                    startPos = endPos = len;
                } else {
                    startPos = -textInputRange.moveStart("character", -len);
                    startPos += normalizedValue.slice(0, startPos).split("\n").length - 1;

                    if (textInputRange.compareEndPoints("EndToEnd", endRange) > -1) {
                        endPos = len;
                    } else {
                        endPos = -textInputRange.moveEnd("character", -len);
                        endPos += normalizedValue.slice(0, endPos).split("\n").length - 1;
                    }
                }
            }
        }

        return {
            start: startPos,
            end: endPos
        };
    };

    $.fn.setCursorPosition = function (pos) {
        this.each(function (index, elem) {
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
        });
    };
})(jQuery);
