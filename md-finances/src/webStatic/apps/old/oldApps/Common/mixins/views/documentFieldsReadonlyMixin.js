// Миксин для делания определенных полей readonly || disabled по массиву readOnlyFields,
// который содержится в модели

(function(common) {
    'use strict';

    common.Mixin.DocumentFieldsReadonlyMixin = {
        setFieldsToReadOnly: setFieldsToReadOnly
    };
    
    /** @access public */
    function setFieldsToReadOnly() {
        var readOnlyFields = this.model.readOnlyFields;
        if(_.contains(readOnlyFields, 'KontragentName')){
            setReadOnlyKontragentField.call(this);
        }
    }
    
    /** @access private */ 
    function setReadOnlyKontragentField() {
        var $field = this.$('#KontragentName, [data-bind=KontragentName]');
        var $clearLink = $field.siblings('.mdCloseLink');

        $field.attr('disabled', 'disabled');
        $clearLink.remove();
    }
    
})(Common);