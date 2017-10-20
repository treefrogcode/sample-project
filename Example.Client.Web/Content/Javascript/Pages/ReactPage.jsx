ReactPage = React.createClass({

    getInitialState: function() {
        return {
            list: JSON.parse(this.props.initialData),
            adding: false,
            data: this.emptyData(),
            errorMessage: ""
        };
    },
    emptyData: function () {
        var empty = {
            StuffId: 0,
            One: '',
            Two: '',
            Three: '',
            EntityId: 0,
        };

        return empty;
    },
    reverseClick: function() {
        this.setState({
            list: this.state.list.reverse()
        });
    },
    addClick: function () {
        this.setState({
            adding: true,
            data: this.emptyData()
        });
    },
    editClick: function(eventData) {
        this.setState({
            adding: true,
            data: eventData
        });
    },
    deleteClick: function (data) {
        VDS.Utils.Ajax.post('/api/stuff/delete', data,
        {
            onOK: (result) => {
                VDS.Utils.Array.removeEntityItem(this.state.list, result, "StuffId");
                this.setState({
                    adding: false,
                    list: this.state.list
                });
            }
        });
    },
    cancelClick: function() {
        this.setState({
            adding: false,
            data: this.emptyData()
        });
    },
    saveClick: function(data) {
        var add = data.StuffId === 0;
        var url =  add ? '/api/stuff/create' : '/api/stuff/update';
        VDS.Utils.Ajax.post(url, data,
        {
            onOK: (result) => {
                if (add) {
                    this.state.list.push(result);
                }
                else {
                    VDS.Utils.Array.updateEntityItem(this.state.list, result, "StuffId");
                }
                this.setState({
                    adding: false,
                    list: this.state.list
                });
            },
            onInvalid: (result) => {
                this.setState({
                    errorMessage: "You have not entered data for all required fields"
                });
            }
        });
    },
    onSearch: function (search) {
        VDS.Utils.Ajax.post('/api/stuff/search?search=' + search, null,
        {
            onOK: (result) => {
                this.setState({
                    list: result
                });
            }
        });
    },
    render: function() {
        return (
            <div className="row">
                <SearchBar onSearch={this.onSearch} />
                <List child="Stuff" title="List of Stuff" data={this.state.list} editClick={this.editClick} deleteClick={this.deleteClick} />
                <div className="col-xs-12 pb20">
                    <a className="btn btn-primary mr20" onClick={this.reverseClick}>Reverse list</a>
                    <a className="btn btn-warning" onClick={this.addClick}>Add New</a>
                </div>
                {this.state.errorMessage.length > 0 ? <ErrorMessage message={this.state.errorMessage}></ErrorMessage> : null}
                {this.state.adding ? <AddStuff data={this.state.data} saveClick={this.saveClick} cancelClick={this.cancelClick} /> : null }
            </div>
        );
    }
});
