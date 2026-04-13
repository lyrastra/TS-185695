(function(common) {

    var qtipStyle = { classes: "qtip-yellow ui-tooltip-rounded mdHint", width: 280 },
        qtipPosition = { my: "left bottom", at: "right top" },
        qtipDelay = 1000;
       
    common.Utils.HintHelper = {
        setAdoptionNdsDeductionHint: function(element, isSales) {
            /// <summary>Подсказка для поля "Принять НДС к вычету" </summary>
            var hintText = this.getHintTextForNdsDeduction(isSales);

            var style = _.clone(qtipStyle);
            style.width = 500;

            var options = {
                    style: style,
                content: { text: hintText },
                hide: {
                    fixed: true,
                    delay: qtipDelay / 3
                }
            };

            setHintWithOptions(element, options);
        },

        setAdvanceNameForInvoiceHint: function(element) {
            var hintText = 'Укажите описание графы "Наименование" авансового счета-фактуры. Например, "Предоплата по договору № 254 от 12 ноября 2014 г."';

            var style = _.clone(qtipStyle);
            style.width = 415;

            var options = {
                style: style,
                content: { text: hintText }
            };

            setHintWithOptions(element, options);
        },

        getHintTextForNdsDeduction: function (isSales) {
            
            return isSales ? 

                'Налогоплательщик на дату отгрузки, в счет которой ранее был получен аванс, ' +
                '<a href="https://www.moedelo.org/Pro/View/Questions/111-17640">вправе принять НДС, рассчитанный с аванса, к вычету</a>' :

                "По <a href='/Pro/View/Questions/111-12919'>общему правилу</a> НДС можно принять к вычету, если одновременно соблюдаются 3 условия: " +
                "имущество (работы, услуги, имущественные права) приобретено для операций, признаваемых объектами обложения НДС, или для перепродажи; " +
                "получен правильно оформленный счет-фактура; имущество (работы, услуги, имущественные права) принято на учет, что подтверждено документально. " +
                "Подтверждение: п. 2 ст. 171, п. 1 ст. 172 Налогового кодекса РФ. " +
                "В некоторых <a href='/Pro/View/Questions/111-12919'>особых случаях</a> общие условия для вычета НДС могут не применяться или могут быть дополнены специальными условиями.";
        },

        setKudirHint: function(element, hintText, withDelay) {
            if (!withDelay) {
                this.setHint(element, hintText);
            } else {
                this.setHintWithDelay(element, hintText);
            }
        },
        
        setHint: function(element, text, classname, options) {
            options = _.extend({
                content: { text: text }
            }, options);
            setHintWithOptions(element, options, classname);
        },
        
        setHintWithDelay: function(element, text, classname) {
            var options = {
                    content: { text: text },
                    hide: {
                    delay: qtipDelay
                }
            };
            setHintWithOptions(element, options, classname);
                    },
        
        setHintWithTextFromData: function(element, classname) {
            element.each(function() {
                var $item = $(this),
                    options = {
                        content: { text: $item.data("text") }
                    };
                
                setHintWithOptions($item, options, classname);
            });
        }
    };

    function setHintWithOptions(element, specialOptions, specialClass) {
        var options = getExtendedDefaultOptions(specialOptions, specialClass);
        element.qtip(options);
    }

    function getExtendedDefaultOptions(specialOptions, specialClass) {
        var options = getDefaultOptions();

        if (specialOptions) {
            _.each(specialOptions, function(option, name) {
                if (_.isObject(option)) {
                    options[name] = _.extend(options[name] || {}, option);
                } else {
                    options[name] = option;
                }
            });
        }

        if (specialClass) {
            options.style.classes += " " + specialClass;
                }

        return options;
        }

    function getDefaultOptions() {
        return {
            style: _.clone(qtipStyle),
            position: _.clone(qtipPosition)
    };
    }

})(Common);