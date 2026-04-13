(function (common) {

    common.Models.PostingsAndTaxOperationModel = Backbone.Model.extend({
        toJSON: function () {
            var jsonModel = _.clone(this.attributes);
        
            jsonModel.MainPostings = this.get('MainPostings').toJSON();
            jsonModel.ManualPostings = this.get('ManualPostings').toJSON();
        
            return jsonModel;
        },
        
        getPostingByCid: function (cid) {
            var manualPostings = this.get('ManualPostings');
            var mainPostings = this.get('MainPostings');

            if (manualPostings.getByCid) {
                return manualPostings.getByCid(cid) || mainPostings.getByCid(cid);
            } else {
                return manualPostings.get(cid) || mainPostings.get(cid);
            }
        }
    });

})(Common);