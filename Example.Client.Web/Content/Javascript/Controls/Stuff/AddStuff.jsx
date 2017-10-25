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

    componentWillReceiveProps(newProps) { // inbuilt method
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

        if (ref === "ColourId") {
            this.state.data.Colour = VDS.Utils.Array.getEntityItem(this.props.colours, value, "EntityId");
        }
        else if (ref === "Categories") {
            this.state.data.Categories = VDS.Utils.Array.getAllEntityItems(this.props.categories, value, "EntityId");
        }

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
                    <Dropdown label="Colour" data={this.props.colours} stateProp="ColourId" value={this.state.data.ColourId} newValue={this.newValue} />
                    <Textbox label="One" stateProp="One" value={this.state.data.One} newValue={this.newValue} mandatory="true" />
                    <Textbox label="Two" stateProp="Two" value={this.state.data.Two} newValue={this.newValue} mandatory="true" />
                    <Textbox label="Three" stateProp="Three" value={this.state.data.Three} newValue={this.newValue} />
                    <CheckList label="Categories" data={this.props.categories} stateProp="Categories" value={this.state.data.Categories} newValue={this.newValue} />
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