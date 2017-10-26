class AdminEntityList extends React.Component {

    constructor(props) {
        super(props);

        this.editClick = this.editClick.bind(this);
        this.deleteClick = this.deleteClick.bind(this);
        this.addClick = this.addClick.bind(this);
        this.saveClick = this.saveClick.bind(this);
        this.cancelClick = this.cancelClick.bind(this);
        this.onChange = this.onChange.bind(this);
        this.onKeyDown = this.onKeyDown.bind(this);
        this.gotoPage = this.gotoPage.bind(this);

        this.state = {
            totalRecords: this.props.totalRecords,
            editId: -1,
            editValue: '',
            errorMessage: this.props.errorMessage
        };
    }

    componentWillReceiveProps(newProps) { // inbuilt method
        this.setState({
            totalRecords: newProps.totalRecords,
            errorMessage: newProps.errorMessage
        });
    }

    addClick(event) {
        this.reset(true);
    }

    editClick(event) {
        this.setState({
            editId: event.EntityId,
            editValue: event.EntityName,
            errorMessage: ''
        });
    }

    deleteClick(event) {
        this.props.deleteClick(event, () => {
            this.reset();
        });
    }

    saveClick(event) {
        var entity = event !== null ? event : { EntityId: 0 };
        entity.EntityName = this.state.editValue;
        this.props.saveClick(entity, () => {
            this.reset();
        });
    }

    cancelClick(event) {
        this.reset();
    }

    onChange(event) {
        this.setState({
            editValue: event.target.value
        });
    }

    gotoPage(page) {
        this.props.gotoPage(page);
        this.reset();
    }

    reset(addNew) {
        this.setState({
            editId: addNew ? 0 : -1,
            editValue: '',
            errorMessage: ''
        });
    }

    onKeyDown(entity, event) {
        if (event.key == 'Enter') {
            this.saveClick(entity);
        }
    }

    render() {
        var nodes = this.props.data.map(function(item, index) {
            return (
                <tr key={index}>
                    <td width="10%">{item.EntityId}</td>
                    <td>
                        {
                        this.state.editId === item.EntityId ?
                        <input type="text" value={this.state.editValue} onChange={this.onChange} onKeyDown={(event) => this.onKeyDown(item, event)} />
                        :
                        <span>{item.EntityName}</span>
                        }
                    </td>
                    <td width="30%" className="text-right">
                        {
                            this.state.editId === item.EntityId ?
                                <span>
                                    <a className="btn btn-sm btn-success mr20" onClick={this.saveClick.bind(this, item)}>Save</a>
                                    <a className="btn btn-sm btn-danger" onClick={this.cancelClick}>Cancel</a>
                                </span>
                            :
                                <span>
                                    <a className="btn btn-sm btn-primary mr20" onClick={this.editClick.bind(this, item)}>Edit</a>
                                    <a className="btn btn-sm btn-danger" onClick={this.deleteClick.bind(this, item)}>Delete</a>
                                </span>
                        }
                    </td>
                </tr>
                );
        }.bind(this));

        return (
            <div className="col-xs-12">
                <h3>{this.props.title}</h3>
                {this.state.errorMessage.length > 0 ? <ErrorMessage message={this.state.errorMessage}></ErrorMessage> : null}
                <table className="table table-striped">
                    <thead className="thead-dark">
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th className="text-right">
                                <button className="btn btn-success" onClick={this.addClick}>Add New</button>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                        this.state.editId === 0 ? 
                        <tr className="add-row">
                            <td width="10%">-</td>
                            <td>
                                <input type="text" value={this.state.editValue} onChange={this.onChange} onKeyDown={(event) => this.onKeyDown(null, event)} />
                            </td>
                            <td width="30%" className="text-right">
                                <span>
                                    <a className="btn btn-sm btn-success mr20" onClick={this.saveClick.bind(this, null)}>Save</a>
                                    <a className="btn btn-sm btn-danger" onClick={this.cancelClick}>Cancel</a>
                                </span>
                            </td>
                        </tr>
                        : null
                        }

                        {nodes}
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colSpan="3">
                                <Paging page={this.props.page} pageSize={this.props.pageSize} totalRecords={this.state.totalRecords} gotoPage={this.gotoPage}></Paging>
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        );
    }
};