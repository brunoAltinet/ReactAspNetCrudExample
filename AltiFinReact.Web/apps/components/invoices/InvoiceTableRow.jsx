/** @jsx React.DOM */

var React = require("react");
var Router =require("react-router");
var Link=Router.Link;

var InvoiceTableRow = React.createClass({
    handleDelete:function(){
        var id= this.props.invoice.Id;
        this.props.onDelete(id);
    },
    render: function () {

        var iconStyle ={
            width:'4%'
        };

        var invoice = this.props.invoice;
        
        var trStyle={};
        if(this.props.firstRed)
        {
            trStyle={background:"red"};
        }
        return (<tr style={trStyle}><td>{invoice.Ordinal}</td><td>{invoice.PartnerName}</td><td style={iconStyle}><Link to="invoiceEdit" params={{id: invoice.Id}}>Edit</Link></td></tr>);
        }
});


module.exports = InvoiceTableRow;