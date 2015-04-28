var Reflux = require("reflux");
var baseStore = require("client/stores/BaseResStore").getProps();

baseStore.resourceDef = {
    type: "partners",
    id: false,
    childrenType: null
}
var PartnerListStore = Reflux.createStore(baseStore);
module.exports = PartnerListStore;