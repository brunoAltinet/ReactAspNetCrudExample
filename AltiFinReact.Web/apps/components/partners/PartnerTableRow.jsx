/** @jsx React.DOM */

var React = require("react");
var Router =require("react-router");
var Link=Router.Link;

var PartnerTableRow = React.createClass({
    handleDelete:function(){
        var id= this.props.partner.Id;
        this.props.onDelete(id);
    },
    render: function () {

        var iconStyle ={
            width:'4%'
        };

        var partner = this.props.partner;
        return (<tr><td>{partner.Name}</td><td>{partner.Address}</td><td style={iconStyle}><Link to="partnerEdit" params={{id: partner.Id}}>Edit</Link></td></tr>);
        }
});


module.exports = PartnerTableRow;