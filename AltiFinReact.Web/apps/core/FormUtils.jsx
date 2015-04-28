var blje=
{
	partial: function (fn) {
		var args = Array.prototype.slice.call(arguments, 1);
		return function () {
			return fn.apply(this, args.concat(Array.prototype.slice.call(arguments)));
		};
	},

    /**
     * Returns true if all the items in a given list are truthy, false otherwise.
     */
     all: function (list) {
     	for (var i = 0, l = list.length; i < l; i++) {
     		if (!list[i]) {
     			return false;
     		}
     	}
     	return true;
     },
     addOnother: function (formset, e) {
     	/* jshint validthis: true */
     	e.preventDefault();
     	formset.addAnother();
     },
     extend:function(dest) {
     	for (var i = 1, l = arguments.length, src; i < l; i++) {
     		src = arguments[i];
     		if (src) {
     			for (var prop in src) {
     				if (blje.hasOwn(src, prop)) {
     					dest[prop] = src[prop];
     				}
     			}
     		}
     	}
     	return dest;
     },
     hasOwn:(function() {
     	var hasOwnProperty = Object.prototype.hasOwnProperty;
     	return function(obj, prop) { return hasOwnProperty.call(obj, prop); };
     })(),

     field:function(bf, cssClass, options,addToDiv) {
     	options = blje.extend({label: true}, options);
     	var errors = bf.errors().messages().map(message => <div className="help-block">{message}</div>);
     	var errorClass = errors.length > 0 ? ' has-error' : '';
     	return <div key={bf.htmlName} className={cssClass + errorClass}>
     	<div className="form-group">
     	{options.label && bf.labelTag()}
     	{bf.asWidget({attrs: {className: 'form-control'}})}
     	{errors}
     	{addToDiv}
     	</div>
     	</div>;
     },


     listenDataChange:function()
     {

     	var tfu=true;

     	if(!this.inDataChange && this.formHandlers && this.formHandlers.length>0)
     	{
     		for(var ifh=0;ifh<this.formHandlers.length;ifh++){
     			ch=this.formHandlers[ifh];
     			var forms=ch.form;
     			if(ch.form.changedData)
     				forms=[forms];
     			else //we'll say it's a formSet
     			{
     				forms=ch.form.forms();
     			}
     			for(var ifrm=0;ifrm<forms.length;ifrm++)
     			{
     				form=forms[ifrm];
     				var onHandled=[];
     				var cProps=form.changedData();
     				if(cProps.length>0)
     				{
     					var data=form._deprefixData(form.data);
     					var cleaned=form.cleanedData;
     					var upd={};
     					for(var i=0;i<ch.handlers.length;i++)
     					{
     						hndlr=ch.handlers[i];
     						var props=Array.isArray(hndlr.props)?hndlr.props:[hndlr.props];
     						for(var iprop=0;iprop<props.length;iprop++)
     						{
     							var prop=props[iprop];
     							if((cProps.indexOf(prop)>-1 && !cleaned[prop]) || (cleaned[prop] && cleaned[prop]!=data[prop])) //if it's changed but not cleaned (first change) or cleaned and different
     							{
     								upd=hndlr.fn(data,upd);
     								if(hndlr.onHandled)
     									onHandled.push(hndlr.onHandled);
     								tfu=false;
     								break;
     							}
     						}
     					}

     					if(!tfu)
     					{
     						this.inDataChange=true;
     						form.updateData(upd);
     						onHandled.forEach(x=>x());
     						this.inDataChange=false;

     					}
     				}
     			}
     		}
     	}

     	if(tfu)
     		this.forceUpdate();
     },

     execHandlers:function(container,form,newData)
     {
     	if(!container || !container.formHandlers) return data;
     	var inputData={};
     	blje.extend(inputData,form.data,newData);
     	for(var i=0;i<container.formHandlers.length;i++)
     	{
     		var  fHandler=container.formHandlers[i];
     		if(!fHandler.form || fHandler.form!=form) continue;
     		for(var ih=0;ih<fHandler.handlers.length;ih++)
     		{
     			var hndlr=fHandler.handlers[ih];
     			if(Object.keys(newData).some(x=>hndlr.props.indexOf(x)>-1))
     			{
     				newData=hndlr.fn(inputData,newData);
     			}
     		}
     	}
     	return newData;
     },

     widget:function (bf, cssClass,addToDiv) {
     	return blje.field(bf, cssClass, {label: false});
     },
 };

 module.exports = blje;