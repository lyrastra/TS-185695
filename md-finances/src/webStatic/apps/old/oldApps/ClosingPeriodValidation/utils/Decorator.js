import ClosedPeriodUtils from '@moedelo/frontend-common/helpers/ClosedPeriodUtils';

(function (closingPeriod) {
    closingPeriod.Utils.Decorator = function (view) {
        var decorator = this;

        var defaultOptions = {
            replaceButtons: true,
            createLabel: true
        };

        this.decorateDocument = function (options) {
            if (!view.isClosed) {
                throw new Error("Method 'isClosed' not implemented in document view.");
            }

            if (_.result(view, 'isClosed')) {
                options = _.extend({}, defaultOptions, options);

                var isLabelExist = view.$('.js-documentForm .closedPeriodLabel').length > 0;
                if (options.createLabel && !isLabelExist) {
                    view.$('.js-documentForm').prepend(createClosedPeriodLabel());
                }

                if (options.replaceButtons && !isBalanceDocument()) {
                    view.$('.buttons').append(createSaveButtonsReplacement());
                }

                return true;
            }
            return false;
        };

        function createClosedPeriodLabel(){
                var closedPeriodLabel = $('<span class="closedPeriodLabel closedPeriodLabel-withIcon">').text("В закрытом периоде");
            if (isBalanceDocument()) {
                closedPeriodLabel.addClass('disabled');
            } else {
                closedPeriodLabel.on('click', showOpenPeriodDialog);
            }
            return closedPeriodLabel;
        }

        function createSaveButtonsReplacement() {
            var link = $("<span />").addClass("link openPeriod").text("откройте период");
            link.click(showOpenPeriodDialog);

            return $("<span />").addClass("closedPeriodHint").html("Чтобы сохранить, ").append(link);
        }

        var removeDecoration = function () {
            if (view.openPeriod) {
                view.$('.closedPeriodLabel').remove();
                view.$('.closedPeriodHint').remove();

                view.openPeriod();
            } else {
                window.location.reload();
            }
        };

        var showOpenPeriodDialog = function () {
            if (view.openPeriodDialog) {
                view.openPeriodDialog.show();
            } else {
                var link = view.$('.closedPeriodLabel');
                if (link.hasClass('disabled')) {
                    return;
                }

                link.addClass('disabled');
                scrollTo(link, function() {
                    ClosedPeriodUtils.showOpenPeriodDialog({
                        date: view.model.get('Date'),
                        $target: link
                    }).then(
                        () => removeDecoration(),
                        () => link.removeClass('disabled')
                    );
                });
            }
        };

        function scrollTo(el, cb) {
            var topPadding = 50,
                duration = 300;

            var properties = {
                scrollTop: el.offset().top - topPadding
            };

            $('body').animate(properties, {
                duration: duration,
                complete: cb
            });
        }

        function isBalanceDocument(){
            var date = view.model.get('Date'),
                requisites = new Common.FirmRequisites();

            return Converter.toDate(date) <= Converter.toDate(requisites.get('BalanceDate'));
        }

        return decorator;
    };
    
})(ClosingPeriodValidation);