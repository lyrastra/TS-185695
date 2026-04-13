(function(common) {
    common.Mixin.PostingAndTaxListenMixin = {
        
        listenFileds(fields) {
            let events = '',
                objEvents = 'add remove reset';
            _.each(fields, function(fieldName) {
                const field = this.sourceDocument.get(fieldName);

                if (_.isObject(field) && _.isFunction(field.on)) {
                    if (fieldName != 'Operations') {
                        objEvents += ' change';
                        field.on(objEvents, this.loadData, this);
                    } else {
                        field.on(objEvents, this.loadData, this);
                    }
                } else {
                    events += String.format('change:{0} ', fieldName);
                }
            }, this);
            
            if (events) {
                this.sourceDocument.on(events, this.loadData, this);
            }
        },

        listenPostingsFileds(fields) {
            let events = '',
                objEvents = 'add remove reset';
            _.each(fields, function(fieldName) {
                const field = this.sourceDocument.get(fieldName);

                if (_.isObject(field) && _.isFunction(field.on)) {
                    if (fieldName != 'Operations') {
                        objEvents += ' change';
                        field.on(objEvents, this.generatePostings, this);
                    } else {
                        field.on(objEvents, this.generatePostings, this);
                    }
                } else {
                    events += String.format('change:{0} ', fieldName);
                }
            }, this);

            if (events) {
                this.sourceDocument.on(events, this.generatePostings, this);
            }
        }
    };
}(Common));
