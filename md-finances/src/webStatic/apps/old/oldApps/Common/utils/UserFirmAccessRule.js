const main = window;
const data = {
    AccessRule: {},

    AccessType: {
        Denied: 0,
        Read: 1,
        Edit: 2
    },

    HaseRules(ruleList) {
        var result = false;

        for (var item in ruleList) {
            result = this.HasRule(ruleList[item]);
        }

        return result;
    },

    HasRule(rule) {
        return rule && this.AccessRule[rule];
    }
};

if (main.UserAccessManager) {
    main.UserAccessManager = { ...data, ...main.UserAccessManager };
} else {
    main.UserAccessManager = data;
}
