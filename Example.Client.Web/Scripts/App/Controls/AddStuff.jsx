var AddStuff = React.createClass({

    getInitialState: function() {
        return {
            stuff: this.props.stuff,
            inError: this.checkIfError(this.props.stuff)
        };
    },

    componentWillReceiveProps: function (newProps) {
        this.setState({
            stuff: newProps.stuff,
            inError: this.checkIfError(newProps.stuff)
        });
    },

    checkIfError: function (stuff) {
        return stuff.One === "" || stuff.Two === "" || stuff.Three === ""
    },

    newValue: function (value, ref) {
        this.state.stuff[ref] = value;
        this.setState({ stuff: this.state.stuff, inError: value === "" });
    },

    saveClick: function(event) {
        event.preventDefault();
        this.props.saveClick(this.state.stuff);
    },

    cancelClick: function() {
        this.props.cancelClick();
    },

    render: function() {
        return (
            <form onSubmit={this.saveClick}>
                <div className="col-xs-12 mt20">
                    <Textbox label="One" stateProp="One" value={this.state.stuff.One} newValue={this.newValue} mandatory="true" />
                    <Textbox label="Two" stateProp="Two" value={this.state.stuff.Two} newValue={this.newValue} mandatory="true" />
                    <Textbox label="Three" stateProp="Three" value={this.state.stuff.Three} newValue={this.newValue} />
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