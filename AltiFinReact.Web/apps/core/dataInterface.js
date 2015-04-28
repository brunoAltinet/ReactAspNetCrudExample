var Q = require("q");
var inDOMEnvironment = typeof window !== 'undefined';
var dataProvider = require("./syncDataProvider");
var serverAddress = window.location.host;

serverAddress = "altifinreact.azurewebsites.net";
var DataInterface = (function() {
  // "private" variables
  var _inDom = inDOMEnvironment;
  var _response = {};
  var _error = false;
  var _profiling = false;
  var _profile = {
    get: [],
    post: []
  };

  var reDate = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}/;
  function jsonDate(obj) {
      var type = typeof (obj);
      if (type == 'object') {
          for (var p in obj)
              if (obj.hasOwnProperty(p))
                  obj[p] = jsonDate(obj[p]);
          return obj;
      } else if (type == 'string' && reDate.test(obj)) {
          return new Date(obj);
      }
      return obj;
  }

  // constructor
  function DataInterface(){}

  // Rest methods
  DataInterface.prototype.get = function(path) {
    // Check for hydrated data
    var hydratedData = dataProvider.getDataByPath(path);
    if(Object.getOwnPropertyNames(hydratedData).length !== 0){
      _response = hydratedData;
      return this;
    }
    else {
      if (_inDom) {
        var $ = require("jquery");
        return Q.when($.get("http://" + serverAddress + path)).then(function (result) {
          //console.log("di(dom) get (" + path + "): ", result);
          return jsonDate(result);
        });
      }
      else {
        //console.log("di(node) get: ", path);
        _response = dataProvider.getDataByPath(path);

        // Profiling
        if (_profiling) {
          _profile.get.push(path);
        }
        return this;
      }
    }
  };

  DataInterface.prototype.post = function(path, data) {
    if (_inDom) {
      var $ = require("jquery");
      return Q.when(
              $.ajax({
                  type: 'POST',
                  url: "http://" + serverAddress + path,
                  data: JSON.stringify(data),
                  contentType: "application/json",
                  dataType: "json"
              })).then(function (result) {
        //console.log("di(dom) post (" + path + "): ", result);
        return result;
      });
    }
    else {
      //console.log("di(node) post: ", path, data);
      var dataProvider = require("client/core/syncDataProvider");
      _response = dataProvider.getDataByPath(path);

      // Profiling
      if (_profiling) {
        _profile.post.push(path);
      }

      return this;
    }
  };

  DataInterface.prototype.delete = function (path) {
      if (_inDom) {
          var $ = require("jquery");
          return Q.when(
                  $.ajax({
                      type: 'DELETE',
                      url: "http://" + serverAddress + path,
                      contentType: "application/json",
                      dataType: "json"
                  })).then(function (result) {
                      //console.log("di(dom) post (" + path + "): ", result);
                      return result;
                  });
      }
      else {
          //console.log("di(node) post: ", path, data);
          var dataProvider = require("client/core/syncDataProvider");
          _response = dataProvider.getDataByPath(path);

          // Profiling
          if (_profiling) {
              _profile.post.push(path);
          }

          return this;
      }
  };

  DataInterface.prototype.put = function (path, data) {
      if (_inDom) {
          var $ = require("jquery");
          return Q.when(
              $.ajax({
                  type:'PUT',
                  url: "http://" + serverAddress + path,
                  data: JSON.stringify(data),
                  contentType: "application/json",
                  dataType: "json"
              })).then(function (result) {
              //console.log("di(dom) post (" + path + "): ", result);
              return result;
          });
      }
      else {
          //console.log("di(node) post: ", path, data);
          var dataProvider = require("client/core/syncDataProvider");
          _response = dataProvider.getDataByPath(path);

          // Profiling
          if (_profiling) {
              _profile.put.push(path);
          }

          return this;
      }
  };

  // Callbakcs
  DataInterface.prototype.then = function(callback) {
    if (!_error) {
      callback(_response);
    }
    return this;
  };

  DataInterface.prototype.catch = function(callback) {
    if (_error) {
      callback(null, "", "");
    }
    return this;
  };

  // Helper methods
  DataInterface.prototype.enableProfiling = function(enable) {
    _profiling = true;
  };

  DataInterface.prototype.disableProfiling = function(enable) {
    _profiling = false;
  };

  DataInterface.prototype.getProfile = function() {
    return _profile;
  };

  return DataInterface;
})();


module.exports = new DataInterface();
