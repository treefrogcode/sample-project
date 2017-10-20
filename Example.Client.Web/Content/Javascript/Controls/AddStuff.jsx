var AddStuff = React.createClass({

    getInitialState: function() {
        return {
            data: this.props.data,
            inError: this.checkIfError(this.props.data)
        };
    },

    componentWillReceiveProps: function (newProps) {
        this.setState({
            data: newProps.data,
            inError: this.checkIfError(newProps.data)
        });
    },

    checkIfError: function (data) {
        return data.One === "" || data.Two === ""
    },

    newValue: function (value, ref) {
        this.state.data[ref] = value;
        this.setState({ data: this.state.data, inError: this.checkIfError(this.state.data) });
    },

    saveClick: function(event) {
        event.preventDefault();
        this.props.saveClick(this.state.data);
    },

    cancelClick: function() {
        this.props.cancelClick();
    },

    render: function() {
        return (
            <form onSubmit={this.saveClick}>
                <div className="col-xs-12 mt20">
                    <Textbox label="One" stateProp="One" value={this.state.data.One} newValue={this.newValue} mandatory="true" />
                    <Textbox label="Two" stateProp="Two" value={this.state.data.Two} newValue={this.newValue} mandatory="true" />
                    <Textbox label="Three" stateProp="Three" value={this.state.data.Three} newValue={this.newValue} />
                    <div className="row">
                        <div className="col-xs-12 form-group">
                            <button type="submit" disabled={this.state.inError} className="btn btn-success mr20">Save</button>
                            <button type="button" className="btn btn-danger" onClick={this.cancelClick}>Cancel</button>
                        </div>
                    </div>
                </div>
            </form>
        );
    }
});