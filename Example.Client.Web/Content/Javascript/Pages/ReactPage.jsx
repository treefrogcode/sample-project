class ReactPage extends React.Component {

    constructor(props) {
        super(props);

        var initialDataObj = JSON.parse(this.props.initialData);
        this.state = {
            list: initialDataObj.Results,
            page: initialDataObj.Paging.Page,
            pageSize: initialDataObj.Paging.PageSize,
            totalRecords: initialDataObj.Paging.TotalRecords,
            adding: false,
            data: this.emptyData(),
            errorMessage: "",
            search: ""
        };

        this.reverseClick = this.reverseClick.bind(this);
        this.addClick = this.addClick.bind(this);
        this.editClick = this.editClick.bind(this);
        this.deleteClick = this.deleteClick.bind(this);
        this.cancelClick = this.cancelClick.bind(this);
        this.saveClick = this.saveClick.bind(this);
        this.onSearch = this.onSearch.bind(this);
        this.gotoPage = this.gotoPage.bind(this);
    }

    emptyData() {
        var empty = {
            StuffId: 0,
            One: '',
            Two: '',
            Three: '',
            EntityId: 0,
        };

        return empty;
    }

    reverseClick() {
        this.setState({
            list: this.state.list.reverse()
        });
    }

    addClick() {
        this.setState({
            adding: true,
            data: this.emptyData()
        });
    }

    editClick(eventData) {
        this.setState({
            adding: true,
            data: eventData
        });
    }

    deleteClick(data) {
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
    }

    cancelClick() {
        this.setState({
            adding: false,
            data: this.emptyData()
        });
    }

    saveClick(data) {
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
    }

    onSearch(search, page) {
        VDS.Utils.Ajax.post('/api/stuff/search?search=' + search + '&page=' + page, null,
        {
            onOK: (response) => {
                console.log(response);
                this.setState({
                    list: response.Results,
                    page: response.Paging.Page,
                    pageSize: response.Paging.PageSize,
                    totalRecords: response.Paging.TotalRecords
                });
            }
        });
    }
    
    gotoPage(page) {
        this.onSearch(this.state.search, page);
    }
    
    render() {
        return (
            <div className="row">
                <SearchBar onSearch={this.onSearch} />
                <StuffList child="Stuff" title="List of Stuff" data={this.state.list} page={this.state.page} pageSize={this.state.pageSize} totalRecords={this.state.totalRecords} gotoPage={this.gotoPage} editClick={this.editClick} deleteClick={this.deleteClick} />
                <div className="col-xs-12 pb20">
                    <a className="btn btn-primary mr20" onClick={this.reverseClick}>Reverse list</a>
                    <a className="btn btn-warning" onClick={this.addClick}>Add New</a>
                </div>
                {this.state.errorMessage.length > 0 ? <ErrorMessage message={this.state.errorMessage}></ErrorMessage> : null}
                {this.state.adding ? <AddStuff data={this.state.data} saveClick={this.saveClick} cancelClick={this.cancelClick} /> : null }
            </div>
        );
    }
};
