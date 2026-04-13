(function(module) {

    module.BootProcessMessage = {
        Type: {
            Global: 0,
            Inner: 1,
            HalfGlobal: 2,
            Contract: 3,
            Bills: 4,
            InnerBills: 5,
            Region: 6,
            Other: 7
        },

        Options: {
            ForImage: {
                Global: { segments: 11, steps: 3, opacity: 0.3, width: 5, space: 3, length: 10, color: '#0b0b0b', speed: 1.5 },
                Table: { segments: 11, steps: 3, opacity: 0.3, width: 4, space: 0, length: 5, color: '#0b0b0b', speed: 1.5 },
                Document: { segments: 11, steps: 3, opacity: 0.3, width: 5, space: 0, length: 10, color: '#0b0b0b', speed: 1.5 }
            }
        },

        Image: {
            busy: function (type, parent, addClass, userOptions) {
                userOptions = userOptions || {};

                var $el = $("<div id='busyShowing' ><span></span></div>"),
                    $parent = parent ? parent : $('#page_content'),
                    additionalClass = addClass ? addClass : 'busyShowing',
                    options = null,
                    $this = module.BootProcessMessage,
                    methodAdd = 'html';

                if (type == $this.Type.Global) {
                    additionalClass += ' globalFiller';
                } else if (type == $this.Type.Inner) {
                    $parent = parent ? parent.find('.mdTableBody') : $('.mdTable .mdTableBody');
                    options = $this.Options.ForImage.Table;
                    additionalClass += ' innerTableFiller';
                } else if (type == $this.Type.HalfGlobal) {
                    additionalClass = 'busyShowingHG globalFiller';
                    methodAdd = 'append';
                } else {
                    additionalClass = 'busyShowing tableFiller';
                }

                if (userOptions && userOptions.imageType) {
                    var imageType = userOptions.imageType;
                    if (imageType === 'small') {
                        options = $this.Options.ForImage.Table;
                    }
                }

                $el.addClass(additionalClass);
                $parent[methodAdd]($el);

                showActivityElement($el, options);

                return $el;
            },
            createActivity: function (parent, options, additionalClass) {
                var $el = $("<div id='busyShowing' class=''><span></span></div>");

                $el.addClass(additionalClass);
                $(parent).html($el);
                
                showActivityElement($el, options);
            }
        },

        Text: {
            
        }
    };

    function showActivityElement($el, options) {
        if (options == null) {
            options = { segments: 11, steps: 3, opacity: 0.3, width: 5, space: 3, length: 10, color: '#0b0b0b', speed: 1.5 };
        }

        $el.activity({
            segments: options.segments,
            steps: options.step,
            opacity: options.opacity,
            width: options.width,
            space: options.space,
            length: options.length,
            color: options.color,
            speed: options.speed
        });
    }


    module.FieldsWarning = {
       
    };

})(window);

window.ToolTip = {
    /// count - можно поставить 1, если не важно количество
    /// true  - желтое сообщение, false - красное
    /// message - текст сообщения
    /// endless - сообщение будет висеть бесконечно
	/// timeout - время, по истечении которого сообщение будет скрыто
    globalMessage: function(count, result, message, endless, timeout) {
        if (!timeout) {
            timeout = 5000;
        }

        $('.status-panel-wrapper').remove();
        if (result == true) {
            var messageText;
            if (!message) {
                if (count > 1) {
                    messageText = "Операции удалены";
                } else {
                    messageText = "Операция удалена";
                }
            } else {
                messageText = message;
            }

            $("<div/>", {
                "class": "status-panel-wrapper",
                html: $("<div/>", {
                    "class": "status_panel status_warning money-note",
                    html: messageText
                })
            }).prependTo("body")
                .show();

        } else if (result == false) {
            if (!message) {
                messageText = "Ошибка при удалении. Позвоните нам по телефону 8 800 200 77 27 или <a href='mailto:support@moedelo.org'>напишите</a>, и мы исправим ее в максимально короткие сроки.";
            } else {
                messageText = message;
            }
            $("<div class='status-panel-wrapper'><div class='status_panel status_warning money-note error'>" + messageText + "</div></div>")
                .prependTo("body")
                .show();
        }

        if (!endless) {
            setTimeout('$(".money-note").fadeOut("normal").queue(function() {$(this).remove()})', timeout);
        }

    },

    globalMessageClose: function() {
        $(".money-note").fadeOut("fast", function() {
            $(this).remove();
        });
    },

    popupMessage: function() {
        var message,
            anchor,
            position,
            wrapperClass = 'infoWindowWrapper';

        if (arguments[0] instanceof Object) {
            message = arguments[0].message;
            anchor = arguments[0].anchor;
            position = arguments[0].position;
            var autoWidth = arguments[0].autoWidth,
                specificPosition = arguments[0].specificPosition;
        } else {
            message = arguments[0];
            anchor = arguments[1];
            position = arguments[2];
        }

        if (anchor.find('.' + wrapperClass).length) {
            return;
        }

        removingElement($('.' + wrapperClass));

        anchor.addClass("no-transition");
        var domElement = $("<div class='" + wrapperClass + "'><div class='infoWindow'>" + message + "</div><span class='close_cross'>&times;</span></div>")
            .appendTo(anchor)
            .fadeIn("fast");

        // Ширина блока устанавливается в зависимости от ширины контента
        if (autoWidth) {
            domElement.addClass("autoWidth");
        }

        if (position) {
            domElement.css("left", position.left - (domElement.outerWidth() / 3));
        } else if (specificPosition) {
            domElement.css(specificPosition);
        }

        function removingElement(elem) {
            if (!elem) {
                elem = domElement;
            }

            elem.fadeOut("normal").queue(function() {

                if (position) {
                    elem.closest(".no-transition").removeClass('no-transition');
                } else {
                    elem.parents().removeClass('no-transition');
                }

                if (anchor.hasClass('envelope')) {
                    anchor.children("a").unwrap();
                }

                $(this).remove();
            });
        }

        setTimeout(removingElement, 5000);
        domElement.delegate(".close_cross, .infoWindow", "click", function(e) {
            e.stopPropagation();
            removingElement(domElement);

        });

        return domElement;
    },

    popupMessageClose: function(anchor) {
        anchor.find(".infoWindowWrapper").fadeOut("fast", function() {
            $(this).remove();
        });
    },

    //ToolTip.busyShowing();
    busyShowing: function(position, className) {
        var busyShowing = $("<div id='busyShowing'></div>");

        if (position && position.toString() != '[object String]') {
            busyShowing.addClass(className);
            $(position).append(busyShowing);
            busyShowing.addClass("large");
            busyShowing.activity({ segments: 11, steps: 3, opacity: 0.3, width: 5, space: 3, length: 10, color: '#0b0b0b', speed: 1.5 });
            return busyShowing;
        }

        if (position == "inner") {
            $("#transferTable tbody").html("<tr><td colspan='8' class='empty'></td></tr>").find(".empty").append(busyShowing);
            $("#paginator").empty();
            busyShowing.activity({ segments: 11, steps: 3, opacity: 0.3, width: 4, space: 0, length: 5, color: '#0b0b0b', speed: 1.5 });
        } else if (position == "contract") {
            $("#transferPage").html(busyShowing);
            busyShowing.activity({ segments: 11, steps: 3, opacity: 0.3, width: 4, space: 0, length: 5, color: '#0b0b0b', speed: 1.5 });
        } else if (position == "bills") {
            $("#moneyIncomingTable").html(busyShowing);
            busyShowing.activity({ segments: 11, steps: 3, opacity: 0.3, width: 4, space: 0, length: 5, color: '#0b0b0b', speed: 1.5 });
        } else if (position == "innerbills") {
            $("#transferTable tbody").html("<tr><td colspan='8' class='empty'></td></tr>").find(".empty").append(busyShowing);
            ;
            busyShowing.addClass("small-table");
            $("#paginator").empty();
            busyShowing.activity({ segments: 11, steps: 3, opacity: 0.3, width: 4, space: 0, length: 5, color: '#0b0b0b', speed: 1.5 });
        } else {
            $("#mainMoneyContent").html("<tr><td colspan='8' class='empty'></td></tr>").find(".empty").append(busyShowing);
            busyShowing.addClass("large");
            busyShowing.activity({ segments: 11, steps: 3, opacity: 0.3, width: 5, space: 3, length: 10, color: '#0b0b0b', speed: 1.5 });
        }
    },

    serverErrorMessage: "Произошла непредвиденная ошибка. Просьба сообщить об этом в техническую поддержку онлайн-сервиса «Моё дело», написав на почту <a href='mailto:support@moedelo.org'>support@moedelo.org</a> или позвонив по телефону 8&nbsp;800&nbsp;200&nbsp;77&nbsp;27.<br/><br/>Мы устраним причины её появления в максимально короткие сроки. Сейчас вы можете перейти на любую из предыдущих страниц или на главную страницу сайта. Приносим свои извинения за доставленные неудобства!<br/><br/>Команда  сервиса «Моё дело».",

    //ToolTip.Sales.busyShowing();
    Sales: {
        busyShowing: function(params) {
            var type = BootProcessMessage.Type.Other;
            var position;
            var parentElement;
            var addClass;
            var options = {};

            if (_.isObject(params)) {
                position = params.position;
                parentElement = params.parentElement;
                addClass = params.addClass;
                options = params;
            } else {
                position = arguments[0];
                parentElement = arguments[1];
                addClass = arguments[2];
            }

            if (position == 'global') {
                type = BootProcessMessage.Type.Global;
            } else if (position == 'inner') {
                type = BootProcessMessage.Type.Inner;
            } else if (position == 'halfGlobal') {
                type = BootProcessMessage.Type.HalfGlobal;
            }

            return BootProcessMessage.Image.busy(type, parentElement, addClass, options);
        },

        Documents: {
            loading: function(selector, element_class) {
                BootProcessMessage.Image.createActivity(selector, BootProcessMessage.Options.ForImage.Document, 'busyShowing ' + element_class);
            }
        }
    },

    Masters: {
        moneyBalance: function(selector) {
            BootProcessMessage.Image.createActivity(selector, BootProcessMessage.Options.ForImage.Global, 'large');
        }
    }
};