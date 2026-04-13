(function(common) {

    common.Mixin.DirectivesMixin = {
        DocumentDate: {
            DateSpan: {
                text: function(target) {
                    var date = this.Date,
                        textDate;

                    textDate = common.Utils.Converter.getDateForDocument(date);
                    
                    if (target.element.tagName.toLowerCase() === 'input') {
                        $(target.element).val(textDate);
                    } else {
                        $(target.element).text(textDate);
                    }
                }
            }
        }
    };

})(Common);