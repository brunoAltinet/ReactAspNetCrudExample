/** @jsx React.DOM */

var forms=require("newforms");

var Item = forms.Form.extend({
 Id: forms.IntegerField({widget:forms.HiddenInput,required:false}),
 Ordinal    : forms.CharField({label:"Nr.",required: false, maxLength: 50}),
 Name    : forms.CharField({required: true, maxLength: 50}),
 UnitPrice    : forms.DecimalField({label:"U.P.",required: false, maxLength: 50}),
 Qty    : forms.DecimalField({required: false, maxLength: 50}),
 Price    : forms.DecimalField({required: false, maxLength: 50}), 
 InvoiceId: forms.IntegerField({widget:forms.HiddenInput,required:false}),
});


module.exports = Item;