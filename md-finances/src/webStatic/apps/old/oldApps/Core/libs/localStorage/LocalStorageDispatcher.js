window.Store = function(name) {
    this.name = name;
    const store = localStorage.getItem(this.name);
    this.data = (store && JSON.parse(store)) || { };
};

_.extend(window.Store.prototype, {
    save() {
        localStorage.setItem(this.name, JSON.stringify(this.data));
    },

    create(model) {
        this.data[model.id] = model;
        this.save();
        return model;
    },

    update(model) {
        this.data[model.id] = model;
        this.save();
        return model;
    },

    find(model) {
        return this.data[model.id];
    },

    findAll() {
        return _.values(this.data);
    },

    destroy(model) {
        delete this.data[model.id];
        this.save();
        return model;
    }
});
