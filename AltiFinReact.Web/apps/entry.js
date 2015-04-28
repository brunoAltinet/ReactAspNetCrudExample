/*
 * React.js Starter Kit
 * Copyright (c) 2014 Konstantin Tarkus (@koistya), KriaSoft LLC.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.txt file in the root directory of this source tree.
 */

/*jslint node: true */
"use strict";
var React = require('react');
// Export React so the dev tools can find it
if(typeof window !== 'undefined')
    (window !== window.top ? window.top : window).React = React;
else {
    global.React = React;
}

/**
 * Check if Page component has a layout property; and if yes, wrap the page
 * into the specified layout, then mount to document.body.
 */
function render(page) {
  var layout = null, child = null, props = {};
  //while ((layout = page.type.layout || (page.defaultProps && page.defaultProps.layout))) {
  //  child = React.createElement(page, props, child);
  //  page = layout;
  //}
  React.render(React.createElement(page, props, child), document.getElementById("page-content"));
}

module.exports =
{
    //HomePage: require('./components/pages/HomePage'),
    PartnersPage: require('./components/partners/PartnersPage'),
    PartnerEditPage: require('./components/partners/PartnerEditPage'),
    //RegistrationPage: require('./components/pages/RegistrationPage'),
    //ResourcesPage: require('./components/pages/ResourcesPage'),
    PartnersApp: require('./PartnersApp'),
    render: render,
}