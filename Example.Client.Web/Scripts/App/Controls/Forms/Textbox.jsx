 var Textbox = React.createClass({

     getInitialState: function () {
         return {
             value: this.props.value,
             inError: false
         };
     },

     componentWillReceiveProps: function (newProps) {
         this.setState({ value: newProps.value });
     },

     changeText: function (event) {
         var value = $.trim(event.target.value);
         this.setState({
             value: value,
             inError: value === "" && this.props.mandatory ? true : false
         });

         if (typeof this.props.newValue !== "undefined") {
             this.props.newValue(value, this.props.stateProp);
         }
     },

     render: function () {
         return (
               <div className="row">
                    <div className="col-xs-12 form-group">
                        <label htmlFor="{this.props.ref}" className="mr10">{this.props.label}</label><span className={this.props.mandatory ? "error" : "hidden"}>*</span>
                        <input maxLength="100" id="{this.props.ref}" className={this.state.inError ? "form-control error " : "form-control"} type="text" placeholder={"Enter a value for " + this.props.label.toLowerCase()} value={this.state.value || ""} onChange={this.changeText} onBlur={this.changeText} />
                        {this.state.inError ? <span className="error">{"Please enter a value for " + this.props.label.toLowerCase()}</span> : null}
                    </div>
                </div>
        );
     }
 });