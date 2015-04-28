var baseStore = require("client/stores/BaseResStore").getProps();
var Reflux = require("reflux");
var $ = require("jquery");
baseStore.resourceDef = {
    type: "invoices",
    id: true,
    childrenType: null
};

var resActions = require("client/actions/ResourceActions");

baseStore.itemsToDelete = [];

baseStore.onMarkToDelete = function (type, id) {
    if (type !== "invoiceItems") return;

    this.itemsToDelete.push(id);

}

baseStore.onSaveResource = function (type, id) {
    if (type != "invoices") return;
    for (var i = 0; i < this.itemsToDelete.length; i++)
        resActions.deleteResource("invoiceItems", this.itemsToDelete[i]);
}

var InvoiceStore = Reflux.createStore(baseStore);
module.exports = InvoiceStore;