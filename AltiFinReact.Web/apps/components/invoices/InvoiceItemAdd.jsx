/**@jsx React.DOM */

var React = require("react");

var ItemAdd = React.createClass({

    render: function() {
        return  <div className="budget-item-add">
                    <div className="row budget-item">
                        <div className="col-xs-4 col-sm-4 col-md-4 col-lg-6 desc">
                            <input type="text" id="item_desc" placeholder="description" />
                        </div>
                        <div className="col-xs-4 col-sm-4 col-md-4 col-lg-3">
                            <input type="text" id="item_budget" placeholder="budget"/>
                        </div>
                        <div className="col-xs-4 col-sm-4 col-md-4 col-lg-3">
                            <input type="text" id="item_actual" placeholder="actual"/>
                        </div>
                    </div>
                    <div className="row budget-item ctrls">
                        <div className="col-xs-4 col-sm-4 col-md-4 col-lg-6 desc">
                            <select id="item_type">
                                <option value="expense">Expense</option>
                                <option value="income">Income</option>
                            </select>
                        </div>
                        <div className="col-xs-8 col-sm-8 col-md-8 col-lg-6">
                            <button name="addItem" className="btn btn-default" onClick={this.addItem}>Add</button>
                        </div>
                    </div>
                </div>;
    }
});

module.exports=ItemAdd;