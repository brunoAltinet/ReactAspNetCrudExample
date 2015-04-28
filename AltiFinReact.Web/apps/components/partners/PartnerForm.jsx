/**@jsx React.DOM */

var React = require('react');
var Router = require('react-router');
var forms=require('newforms');
var BsForm=require('newforms-bootstrap');

var PartnerForm = forms.Form.extend({
  Id: forms.IntegerField({widget:forms.HiddenInput,required:false}),
  Name: forms.CharField({label: 'Name',
                         helpText: 'Partner name',required:true}),
  Address: forms.CharField({label: 'Address',required:false}),
  
  DateModified:forms.DateField({widget:forms.DateInput({format:"%d.%m.%Y"}),inputFormats:["%d.%m.%Y"],label:"Date modified",required:true}),

  clean() {
    var colour = this.cleanedData.colour
    if (/arthur/i.test(this.cleanedData.name) && colour && colour != 'Bisque') {
      throw forms.ValidationError("Imposter! King Arthur's favourite colour is Bisque! WHOOSH!")
    }
  }

});

module.exports = PartnerForm;
