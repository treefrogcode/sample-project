 class Button extends React.Component {

     constructor(props) {
         super(props);

         var buttonType = this.props.type == 'submit' ? 'submit' : 'button';
     }

     render() {
         return (
            <button disabled={this.props.disabled} type={this.buttonType} title={this.props.title} className={"btn btn-" + this.props.class} onClick={this.props.onClick }>
                <i className={"fa fa-" + this.props.icon}></i>
                <span>{this.props.label}</span>
            </button>

         )
     };
}