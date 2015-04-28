var Reflux = require("reflux");
var baseStore = require("client/stores/BaseResStore").getProps();


baseStore.resourceDef= {
    type: "partners",
    id: true,
    childrenType: null
}


var PartnerStore = Reflux.createStore(baseStore);
module.exports = PartnerStore;