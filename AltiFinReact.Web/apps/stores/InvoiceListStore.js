var Reflux = require("reflux");
var baseStore = require("client/stores/BaseResStore").getProps();

baseStore.resourceDef = {
    type: "invoices",
    id: false,
    childrenType: null
}
var InvoiceListStore = Reflux.createStore(baseStore);
module.exports = InvoiceListStore;