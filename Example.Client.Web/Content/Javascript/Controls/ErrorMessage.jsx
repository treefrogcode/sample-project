var ErrorMessage = React.createClass({

    render: function () {

        return (
        <div className="col-xs-12">
            <div className="error-message mt20">{this.props.message}</div>
        </div>
        );
    }
});