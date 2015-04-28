/** @jsx React.DOM*/

var React = require('react');
var Router = require('react-router');
var PartnerForm = require("./PartnerForm");
var LoadingIndicator = require("client/components/common/LoadingIndicator");
var PartnerStore = require('client/stores/PartnerStore');
var RestApiActions = require("client/actions/resourceActions");
var connect= require("reflux").connect;
var forms= require("newforms")
var BootstrapForm=require("newforms-bootstrap");

var PartnerEditPage = React.createClass({

    mixins: [connect(PartnerStore,"partner"),Router.Navigation,Router.State],

	componentDidMount: function() {
	         var id= this.getParams().id;
		if(id)
			RestApiActions.loadResource("partners",id);
		else
			this.state.partner.loading=false;
	},
    submitForm:function(e){
		e.preventDefault();
		var form = this.refs.partnerForm.getForm();
		var isValid = form.validate();
		if (isValid) {
			var cData=form.cleanedData;
			RestApiActions.saveResource("partners",cData.Id,cData,true);
		}
    },
    render: function () {

        if (this.state.loading) {
            return <LoadingIndicator />;
        }

        if (this.state.responseError) {
            var errorMessage = <div className="error-message">{this.state.responseError.Message}</div>;
        }
		var ent=this.state.partner.data;
		entName=ent!=null?ent.Name:null;
        return(
           <div className="col-md-6">
		   <h3>{entName}</h3>
        <form onSubmit={this.submitForm}>
          <forms.RenderForm form={PartnerForm} data={ent} ref="partnerForm">
            <BootstrapForm/>
          </forms.RenderForm>
          <button type="submit" className="btn btn-primary">
            Submit
          </button>
        </form>
      </div>
            );
    }
});


module.exports = PartnerEditPage;