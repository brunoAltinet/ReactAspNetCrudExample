// router.js
var React = require('react');
var reactRouter = require('react-router');

module.exports = {
    router: null,
    routes: null,
    Link: reactRouter.Link,
    RouteHandler: reactRouter.RouteHandler,
    Route: reactRouter.Route,
    DefaultRoute: reactRouter.DefaultRoute,
    renderToDom: function (domNodeId) {
        // Create a router
        var router = reactRouter.create({
            routes: this.routes
        });

        // Run the app
        router.run(function (handler, state) {
            React.render(React.createElement(handler), document.getElementById(domNodeId));
        });
        this.router = router;

        return router;
    },
    renderToString: function (path) {
        var content;
        reactRouter.run(routes, path, function (Handler) {
            content = React.renderToString(React.createElement(Handler));
        });
        return content;
    }
};
