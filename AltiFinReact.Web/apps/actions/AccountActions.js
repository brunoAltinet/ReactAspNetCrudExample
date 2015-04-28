﻿var Reflux = require("reflux");
var dataInterface = require("client/core/dataInterface");

// Create actions
var actions = Reflux.createActions([
  // Login
  "login",
  "loginFail",
  "loginSuccess",
  "loginError",

  // Register
  "register",
  "registerFail",
  "registerSuccess",

  // Session
  "sessionRetrieve",
  "sessionUpdate",

  // Logout
  "logout",
  "logoutSuccess",
  "logoutError",

  // Set session info
  "setLoginReturnPath"
]);
module.exports = actions;

// Action handlers
actions.login.listen(function (username, password) {
    dataInterface.post("/auth/login", { username: username, password: password })
      .then(function (data) {
          if (data.success) {
              actions.loginSuccess(data.user);
          }
          else {
              actions.loginFail(data.message);
          }
      })
      .catch(function (jqXHR, textStatus, errorThrown) {
          actions.loginError(textStatus, errorThrown);
      });
});

actions.logout.listen(function (username, password) {
    dataInterface.get("/auth/logout")
      .then(function (data) {
          actions.logoutSuccess();
      })
      .catch(function (jqXHR, textStatus, errorThrown) {
          actions.logoutError(textStatus, errorThrown);
      });
});