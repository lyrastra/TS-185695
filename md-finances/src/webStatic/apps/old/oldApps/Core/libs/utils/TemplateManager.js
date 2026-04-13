window.TemplateManager = {
    templates: {},

    useExpireCacheParam: true,
    prefix: "",
    debug: false,

    get: function(id, callback, url, prefix) {
        if (typeof url !== `string`) {
            let tmpl = url(`./${id}.html`);

            tmpl = this.setCompanyId(tmpl);
            setTimeout(() => callback(tmpl), 0);

            return;
        }

        var temlateId = url + id;

        var template = this.templates[temlateId];

        if (template) {
            callback(template);
            return;
        }

        if (!(url && url.length)) {
            this.trace("Templates url not found!", "error");
        }

        if(url || url === ''){
            this.rootUrl = url;
        }

        var expireCache = this.getExpireCachePostfix();
        prefix = prefix !== undefined ? prefix : _.isFunction(this.prefix) ? this.prefix(this.rootUrl + id) : this.prefix;
        var templateUrl = prefix + this.rootUrl + id + ".html" + expireCache;

        this.trace("Load template. Url - " + templateUrl);

        var jqxhr =
            $.ajax({
                url: templateUrl,
                success: function (tmpl) {
                    tmpl = this.setCompanyId(tmpl);
                    if (_.isString(tmpl)) {
                        this.trace("Template loaded.");
                        this.templates[temlateId] = tmpl;
                        callback(tmpl);
                    }
                }.bind(this),
                dataType: "html",
                cache: true
            });

        if (this.debug === true) {
            jqxhr.fail(function () {
                var errorMessage = "Can't load template. Url - " + templateUrl;
                this.trace(errorMessage, "error");
            }.bind(this));
        }
    },

    setCompanyId: function(tmpl) {
        var regexp = /href=['|"]((?!#)((?!_companyId).)*?)['|"]/g;

        return tmpl.replace(regexp, function(match, url){
            var fixedUrl = Md.Core.Engines.CompanyId.getLinkWithParams(url);
            return match.replace(url, fixedUrl);
        });
    },

    getExpireCachePostfix: function() {
        if (!this.expireCache) {
            this.expireCache = "";
            var input = $("#expireCache");
            this.expireCache += "?";
            this.expireCache += input.val() || new Date().getTime();
        }
        return this.useExpireCacheParam ? this.expireCache : "";
    },

    trace: function(message, mode) {
        if (this.debug === true) {
            if (mode === "error") {
                console.error(message);
            } else {
                console.log(message);
            }
        }
    },

    getSync: function(id, callback, url) {
        var manager = this;
        var template = this.templates[id];

        if (url && url.length) {
            this.rootUrl = url;
        } else {
            console.error("Templates url not found!");
        }

        if (template) {
            callback(template);
        } else {
            var expireCache = "";
            if (this.useExpireCacheParam) {
                expireCache += "?";
                expireCache += $("#expireCache").val() || new Date().getTime();
            }

            var prefix = _.isFunction(this.prefix) ? this.prefix(this.rootUrl + id) : this.prefix;

            $.ajax({
                url: prefix + this.rootUrl + id + ".html" + expireCache,

                success: function(tmpl) {
                    {
                        if (this.debug === true) {
                            console.log("Template loaded.");
                        }
                        callback(tmpl);
                    }
                },

                error: function() {
                    if (this.debug === true) {
                        console.log("Can't load template. Url - " + prefix + manager.rootUrl + id + ".html" + expireCache);
                    }
                },

                async: false,
                cache: true
            });
        }
    },

    getFromPage: function(templId) {
        var tpl = $("#" + templId);
        if (tpl.length == 0) {
            throw "Template '" + templId + "' is not found!";
        }

        return tpl.html();
    },

    isTemplateExist: function(id) {
        var el = document.getElementById(id);
        return el && el.tagName.toLowerCase() === 'script';
    },

    getBussinessTemplate: function(template, callback, context){
        getAppTemplate('business', template, callback, context);
    },

    getMoneyTemplate: function(template, callback, context){
        getAppTemplate('money', template, callback, context);
    }
};

function getAppTemplate(app, template, callback, context){
    var tm = window.TemplateManager,
        url = '/app/templates/' + app + '/' + template;

    tm.isTemplateExist(template)
        ? callback.call(context, tm.getFromPage(template))
        : tm.get(url, _.bind(callback, context), '','');
}