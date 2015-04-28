/** @jsx React.DOM*/

var React = require('react');
var Router = require('react-router');
var InvoiceForm = require("./InvoiceForm");
var LoadingIndicator = require("client/components/common/LoadingIndicator");
var InvoiceStore = require('client/stores/InvoiceStore');
var restApiActions = require("client/actions/resourceActions");
var connect= require("reflux").connect;
var listenTo=require("reflux").listenTo;
var forms= require("newforms");
var BootstrapForm=require("newforms-bootstrap");
var PartnerListStore=require("client/stores/PartnerListStore");
var restApiActions = require("client/actions/resourceActions");
var InvoiceItem = require("./InvoiceItem");
var formUtils=require("client/core/FormUtils");
var field=formUtils.field;


var InvoiceItemFormSet = forms.FormSet.extend({form:InvoiceItem});

var InvoiceEditPage = React.createClass({


	mixins: [listenTo(PartnerListStore,"onLoadPartners"),listenTo(InvoiceStore,"onLoadInvoice"),Router.Navigation],

	getInitialState:function(){
		return {invoice:{data:null,loading:true},partners:{data:null,loading:true}};
	},

	componentDidMount: function () {

		this.invoiceForm= new InvoiceForm({controlled:true,onChange: formUtils.listenDataChange.bind(this)});
		this.itemForms= new InvoiceItemFormSet({
			prefix: 'items',
			onChange: formUtils.listenDataChange.bind(this),
			controlled:true,
			canDelete:true,
			extra:0
		});

		this.formHandlers=[
		{
			form:this.invoiceForm,
			handlers:[{props:["NettoValue","TaxValue"],fn:this.onTaxNettoChange}]
		},
		{
			form:this.itemForms,
			handlers:[{props:["UnitPrice","Qty","DELETED"],fn:this.onPriceQtyChange,onHandled:this.onItemChanged}]
		}
		];

		var id= this.context.router.getCurrentParams().id;
		if(id)
			restApiActions.loadResource("invoices",id);
		else
			this.state.invoice.loading=false;
		restApiActions.loadResource("partners");
	},

	onPriceQtyChange:function(data,item)
	{
		var up=parseFloat(data.UnitPrice);
		var qty=parseFloat(data.Qty);
		item.Price=(up*qty).toFixed(2);
		return item;
	},

	onItemChanged:function()
	{
		data={};
		this.itemForms.validate();
		var itms=this.getValidItems();
		itms=itms.map(x=>parseFloat(x.Price));
		data.NettoValue=itms.reduce((s,c)=>s+c).toFixed(2);
		data=formUtils.execHandlers(this,this.invoiceForm,data); //fire events for netto;

		this.invoiceForm.updateData(data);
	},

	onTaxNettoChange:function(data,inv)
	{
		var nettoValue=parseFloat(data.NettoValue);
		var taxValue=parseFloat(data.TaxValue);
		var brutto=(nettoValue+taxValue).toFixed(2);
		inv.BruttoValue=brutto;
		return inv;
	},

	getValidItems:function()
	{
		var itms=this.itemForms.cleanedData();
		for(var i=itms.length-1;i>=0;i--){
			if(itms[i].DELETE)
				itms.splice(i,1);
		}
		return itms;
	},

	submitForm:function(e){
		e.preventDefault();
		var form = this.refs.InvoiceForm.getForm();
		var detailsValid=this.itemForms.validate();
		var isValid = form.validate(this.refs.invoiceForm);
		if (isValid && detailsValid) {
			var cData=form.cleanedData;
			cData.InvoiceItems=this.getValidItems();
			this.state.Invoice=cData;
			restApiActions.saveResource("invoices",this.state.Invoice.Id,this.state.Invoice,true);
		}
	},

	deleteItem: function(id)
	{
		var entId=this.itemForms.forms()[id].initial.Id;
		//restApiActions.markToDelete("invoiceItems",this.itemForms.forms()[id].initial.Id);
		if(entId)
			this.itemForms.forms()[id].cleanedData.DELETE=true;
		else
			this.itemForms.removeForm(id);
		this.forceUpdate();
	},

	addLineItem:function(e)
	{
		e.preventDefault();
		this.itemForms.addAnother();
	},

	onLoadInvoice:function(data)
	{
		this.state.invoice=data;
		if(!data || !data.data) return;
		this.itemForms.initial=data.data.InvoiceItems;
		this.invoiceForm.reset(data.data);
	},

	onLoadPartners:function(data)
	{
		var partners=data.data;
		this.setState({partners:data});
	},

	renderItems:function()
	{
		return cmp.itemForms.forms().map((form, i) => {
			var label = (i === 0 ? {label:true} : {label:false});
			var bfo = form.boundFieldsObj();
			var dataId=form.initial?form.initial.Id:null;
			return <div className="row">
			{field(bfo.Ordinal, 'col-sm-2',label)}
			{field(bfo.Name, 'col-sm-2',label)}
			{field(bfo.UnitPrice, 'col-sm-2',label)}
			{field(bfo.Qty, 'col-sm-2',label)}
			{field(bfo.Price, 'col-sm-2',label)}
			{field(bfo.DELETE,'col-sm-2',label)}
			</div>;
		});
	},

	render: function () {

		cmp=this;
		if (cmp.state.invoice.loading) {
			return <LoadingIndicator />;
		}

		if (cmp.state.responseError) {
			var errorMessage = <div className="error-message">{cmp.state.responseError.Message}</div>;
		}

		var dButton=function(id){ 
			return <input type="button" onClick={cmp.deleteItem.bind(cmp,id)} value="Delete" />;
		};

		var partners=this.state.partners.data;
		if(partners){
			cmp.invoiceForm.fields.PartnerId.setChoices(forms.util.makeChoices(partners,'Id','Name'));
			cmp.invoiceForm.fields.PartnerId.widget.isHidden=false;
		}
		else
			cmp.invoiceForm.fields.PartnerId.widget.isHidden=true;


		return(
			<div>
			<h4>Create Invoice</h4>
			<form onSubmit={cmp.submitForm} ref="invoiceForm">
			<div className="row">
			<div className="col-md-6">
			<forms.RenderForm form={cmp.invoiceForm} ref="InvoiceForm">
			<BootstrapForm/>
			</forms.RenderForm>
			</div>
			</div>
			<h4 style={{margin:"30px 0px 10px 0px"}}>Items:</h4>
			{this.renderItems()}
			<p><a href="#" onClick={cmp.addLineItem}>+ add another line item</a></p>
			<button type="submit" className="btn btn-primary">
			Submit
			</button>
			</form>
			</div>
			);
	}
});


module.exports = InvoiceEditPage;