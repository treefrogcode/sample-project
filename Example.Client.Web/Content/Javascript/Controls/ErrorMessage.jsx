class ErrorMessage extends React.Compoment{

    constructor(props) {
        super(props);
    }

    render() {

        return (
            <div className="col-xs-12">
                <div className="error-message mt20">{this.props.message}</div>
            </div>
        );
    }
};