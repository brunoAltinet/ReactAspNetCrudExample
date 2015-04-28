"use strict";

var Reflux = require("reflux");
var dataInterface = require("client/core/dataInterface");


// Create actions
var actions = Reflux.createActions([
  // Get
  "loadResource",
  "loadResourceSuccess",
  "loadResourceFail",

  // Create
  "createResource",
  "createResourceSuccess",
  "createResourceFail",

  // Update
  "updateResource",
  "updateResourceSuccess",
  "updateResourceFail",
  // Save
  "saveResource",

  //delete
    "markToDelete",
  "deleteResource",
  "deleteResourceSuccess",
  "deleteResourceFail",

  // Error
  "resourceNotFound"
]);

// Action handlers
actions.loadResource.listen(function (type, id, childrenType,querystring) {

    dataInterface.get("/api/" + [type, id, childrenType,querystring].filter(function (e) { return e; }).join("/"))
      .then(function (data) {
          actions.loadResourceSuccess(type, id, childrenType, data);
      })
      .catch(function (jqXhr, textStatus, errorThrown) {
          actions.loadResourceFail(type, id, childrenType, textStatus, errorThrown);
      });
});

actions.createResource.listen(function (type, data, navigateTo) {
    dataInterface.post("/api/" + [type].filter(function (e) { return e; }).join("/"), data)
      .then(function (data) {
          actions.createResourceSuccess(type, data);

          // Navigate to resource
          if (navigateTo) {
              var router = require("client/core/routing").router;
              router.transitionTo("/" + type + "/");
          }
      })
      .catch(function (jqXHR, textStatus, errorThrown) {
          actions.createResourceFail(type, id, childrenType, textStatus, errorThrown);
      });
});

actions.saveResource.listen(function(type, id, data, navigateTo) {
    if (!id || id == 0)
        actions.createResource(type, data, navigateTo);
    else 
        actions.updateResource(type, id, data, navigateTo);    
});

actions.updateResource.listen(function (type,id, data, navigateTo) {
    dataInterface.put("/api/" + [type,id].filter(function (e) { return e; }).join("/"), data)
      .then(function (data) {
          actions.updateResourceSuccess(type, data);
          if (navigateTo) {
                  var router = require("client/core/routing").router;
                  router.transitionTo("/"+type+"/");
          }
      })
      .catch(function (jqXHR, textStatus, errorThrown) {
          actions.createResourceFail(type, id, childrenType, textStatus, errorThrown);
      });
});

actions.deleteResource.listen(function(type, id,navigateTo) {
    dataInterface.delete("/api/" + [type, id].filter(function (e) { return e; }).join("/"), id)
          .then(function () {
              actions.deleteResourceSuccess(type, id);
              if (navigateTo) {
                  var router = require("client/core/routing").router;
                  router.transitionTo("/" + type + "/");
              }
          })
      .catch(function (jqXHR, textStatus, errorThrown) {
          actions.deleteResourceFail(type, id, childrenType, textStatus, errorThrown);
      });
    }
);

module.exports = actions;