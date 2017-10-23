class AddStuff extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            data: props.data,
            inError: this.checkIfError(props.data)
        };

        this.newValue = this.newValue.bind(this);
        this.saveClick = this.saveClick.bind(this);
        this.cancelClick = this.cancelClick.bind(this);
    }

    componentWillReceiveProps(newProps) {
        this.setState({
            data: newProps.data,
            inError: this.checkIfError(newProps.data)
        });
    }

    checkIfError(data) {
        return data.One === "" || data.Two === ""
    }

    newValue(value, ref) {
        this.state.data[ref] = value;
        this.setState({ data: this.state.data, inError: this.checkIfError(this.state.data) });
    }

    saveClick(event) {
        event.preventDefault();
        this.props.saveClick(this.state.data);
    }

    cancelClick() {
        this.props.cancelClick();
    }

    render() {
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
};