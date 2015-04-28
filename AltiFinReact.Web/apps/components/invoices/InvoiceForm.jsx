/**@jsx React.DOM */

var React = require('react');
var Router = require('react-router');
var forms=require('newforms');
var BsForm=require('newforms-bootstrap');

var InvoiceForm = forms.Form.extend({
  Id: forms.IntegerField({widget:forms.NumberInput,required:false}),
  Ordinal: forms.IntegerField({widget:forms.NumberInput,required:false}),
  OrderNumber: forms.CharField({label: 'Order number',helpText: 'Invoice name',required:true}),
  PartnerId: forms.ChoiceField({label: 'Partner',required:true}),
  DateModified:forms.DateField({widget:forms.DateInput({format:"%d.%m.%Y"}),inputFormats:["%d.%m.%Y"],label:"Date modified",required:true}),
  NettoValue:forms.DecimalField({maxDigits: 7, decimalPlaces: 2,required:false}),
  TaxValue:forms.DecimalField({widget:forms.NumberInput,required:false}),
  BruttoValue:forms.DecimalField({widget:forms.NumberInput,required:false})
});

module.exports = InvoiceForm;
