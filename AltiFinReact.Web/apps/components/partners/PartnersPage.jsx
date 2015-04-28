/** @jsx React.DOM*/

var React = require('react');
var Reflux = require("reflux");
var connect= Reflux.connect;
var Router = require('react-router');
var PartnerListStore = require('client/stores/PartnerListStore');
var $ = require('jquery');
var LoadingIndicator = require('client/components/common/LoadingIndicator');
var PartnerTableRow = require('./PartnerTableRow');
var ConfirmationModal = require('client/components/common/ConfirmationModal');
var componentTransitionMixin = require("client/mixins/componentTransition");
var restApiActions = require("client/actions/resourceActions");
var LoadingIndicator = require("client/components/common/LoadingIndicator");
var Link=Router.Link;

var PartnersPage = React.createClass({

    mixins: [connect(PartnerListStore),Router.NavigatableMixin],

	 componentDidMount: function () {
        restApiActions.loadResource("partners");
      },

	getInitialState:function(){
        return {};
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
                return <tr><td colspan="4">There are no partners in the system</td></tr>;
            }

            return component.state.data.map(function (partner) {
                return (
                    <PartnerTableRow key={partner.Id} partner={partner} onDelete={this.handleDelete} />                   
                    );
    }.bind(component));
}(this);



return <div>
            <h4>Partners</h4> <Link to="partnerNew">Add New</Link>
            <div className="ui divider"></div>
<div>
    <table className="ui small table segment">
        <thead>
            <tr>
            <th>
            Name
                </th>
                <th>
                    Address
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
</div> 
}
});


module.exports = PartnersPage;
