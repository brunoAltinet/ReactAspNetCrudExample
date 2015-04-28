/** @jsx React.DOM*/

var React = require('react');
var Reflux = require("reflux");
var connect= Reflux.connect;
var Router = require('react-router');
var InvoiceListStore = require('client/stores/InvoiceListStore');
var $ = require('jquery');
var LoadingIndicator = require('client/components/common/LoadingIndicator');
var InvoiceTableRow = require('./InvoiceTableRow');
var ConfirmationModal = require('client/components/common/ConfirmationModal');
var componentTransitionMixin = require("client/mixins/componentTransition");
var restApiActions = require("client/actions/resourceActions");
var Link=Router.Link;

var InvoiceListPage = React.createClass({

    mixins: [connect(InvoiceListStore),Router.NavigatableMixin],

	 componentDidMount: function () {
        restApiActions.loadResource("invoices");
      },

	getInitialState:function(){
        return {};
    },

    OnButtonClick:function()
    {
        this.setState({firstRed:(!this.state.firstRed)});
    },

    render: function () {

	var btnStyle ={
            float:'right',
            "padding-top":10
        };


        var data = function(component){
            if (component.state.loading) {
				return <tr><td colspan="4">Loading</td></tr>;
            }

            if (component.state.data && !component.state.data.length) {
                return <tr><td colspan="4">There are no invoices in the system</td></tr>;
            }

            return component.state.data.map(function (invoice) {
                return (
                    <InvoiceTableRow key={invoice.Id}  firstRed={this.state.firstRed} invoice={invoice} onDelete={this.handleDelete} />
                    );
    }.bind(component));
}(this);



return <div>
            <h4>Invoices</h4> <Link to="invoiceNew">Add New</Link>
            <div className="ui divider"></div>
<div>
<input type="text" name="testxt" /> <input type="button" value="First red" onClick={this.OnButtonClick}/>
    <table className="ui small table segment">
        <thead>
            <tr>
            <th>
            Ordinal
                </th>
                <th>
                    Partner
                </th>
                <th></th>
                <th></th>
            </tr>
    </thead>
    <tbody>
        {data}
    </tbody>
    </table>
      <div style={btnStyle} data-omit="Member">
    </div>
</div>
</div>;
}
});


module.exports = InvoiceListPage;
