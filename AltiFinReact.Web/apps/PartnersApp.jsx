var routing = require('client/core/routing'); // or var Router = ReactRouter; in browsers
var React = require('react');

var PartnersPage=require('client/components/partners/PartnersPage');
var PartnerEditPage=require('client/components/partners/PartnerEditPage');
var InvoicesPage=require('client/components/invoices/InvoiceListPage');
var InvoiceEditPage=require('client/components/invoices/InvoiceEditPage');
var Link = routing.Link;
var Route = routing.Route;
var RouteHandler = routing.RouteHandler;


var App = React.createClass({
    render: function () {
        return (
         <div>
		  <div className="col-sm-2">
            <div className="sidebar-wrapper">
              <ul className="nav nav-sidebar">
                <li><Link to="app">Dashboard</Link></li>
                <li><Link to="partnerList">Partners</Link></li>
                <li><Link to="invoiceList">Invoices</Link></li>
              </ul>
      </div>
	  </div>
	  <div className="col-sm-10">
        {/* this is the important part */}
        <RouteHandler/>
		</div>
      </div>
      );
    }
});


var routes = (
  <Route name="app" path="/" handler={App}>
      <Route name="partnerNew" path="/partners/new" handler={PartnerEditPage} ></Route>
	<Route name="partnerEdit" path="/partners/:id" handler={PartnerEditPage}></Route>
    <Route name="partnerList" path="/partners/" handler={PartnersPage} ></Route>
	<Route name="invoiceNew" path="/invoices/new" handler={InvoiceEditPage} ></Route>
	<Route name="invoiceEdit" path="/invoices/:id" handler={InvoiceEditPage}></Route>
    <Route name="invoiceList" path="/invoices/" handler={InvoicesPage} ></Route>
  </Route>
);

routing.routes=routes;



module.exports = routing;