var resourceActions = require("client/actions/resourceActions");

var BaseResStore = {
    getProps: function () {
        return {
            listenables: resourceActions,
            state: { loading: true, responseError: null, data: null },
            getInitialState: function () {
                this.state.loading = true;
                this.state.responseError = null;
                this.state.data = null;
                return this.state;
            },
            resourceDef: {
                type: null,
                id: false,
                childrenType: null
            },
            // Helpers
            forThisStore: function (type, id, childrenType) {
                id = (this.resourceDef.id) ? !!id : !id;
                return type == this.resourceDef.type && id && childrenType == this.resourceDef.childrenType;
            },
            // Action handlers
            onLoadResource: function (type, id, childrenType) {
                if (this.forThisStore.apply(null, arguments)) {
                    this.state.loading = true;
                    //this.trigger(this.state);
                }
            },
            onLoadResourceSuccess: function (type, id, childrenType, data) {
                if (this.forThisStore.apply(null, arguments)) {
                    this.state.data = data;
                    this.state.loading = false;
                    this.trigger(this.state);
                }
            },
            onLoadResourceFail: function (type, id, childrenType) {
                if (this.forThisStore.apply(null, arguments)) {
                    console.log(" loading failed!");
                }
            }
        };
    }
};
module.exports = BaseResStore;