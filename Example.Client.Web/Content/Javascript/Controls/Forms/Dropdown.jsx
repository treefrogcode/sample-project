 class Dropdown extends React.Component {

     constructor(props) {
         super(props);

         this.state = {
             value: this.props.value,
             inError: false
         };

         this.onChange = this.onChange.bind(this);
     }

     componentWillReceiveProps(newProps) { // inbuilt method
         this.setState({ value: newProps.value });
     }

     onChange(event) {
         var value = $.trim(event.target.value);
         this.setState({
             value: value,
             inError: value === "" && this.props.mandatory ? true : false
         });

         if (typeof this.props.newValue === "function") {
             this.props.newValue(value, this.props.stateProp);
         }
     }

     render() {
         const options = this.props.data.map((opt) => {
             return <option key={opt.EntityId} value={opt.EntityId}>{opt.EntityName}</option>
         });

         return (
               <div className="row">
                    <div className="col-xs-12 form-group">
                        <label htmlFor="{this.props.ref}" className="mr10">{this.props.label}</label><span className={this.props.mandatory ? "error" : "hidden"}>*</span>
                        <select id="{this.props.ref}" className={this.state.inError ? "form-control error " : "form-control"} value={this.state.value || ""} onChange={this.onChange} onBlur={this.onChange}>
                            {!this.props.mandatory  ? <option className="grey" value="">{"-- Select a value for " + this.props.label.toLowerCase() + " --"}</option>  : null}
                            {options}
                        </select>
                        {this.state.inError ? <span className="error">{"Please select a value for " + this.props.label.toLowerCase()}</span> : null}
                    </div>
                </div>
        );
     }
 };