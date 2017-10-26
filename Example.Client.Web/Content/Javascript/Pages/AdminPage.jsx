class AdminPage extends React.Component {

    constructor(props) {
        super(props);

        var initialDataObj = JSON.parse(this.props.initialData);
        this.state = {
            list: initialDataObj.Results,
            page: initialDataObj.Paging.Page,
            pageSize: initialDataObj.Paging.PageSize,
            totalRecords: initialDataObj.Paging.TotalRecords,
            errorMessage: '',
            search: ''
        };

        this.deleteClick = this.deleteClick.bind(this);
        this.saveClick = this.saveClick.bind(this);
        this.onSearch = this.onSearch.bind(this);
        this.gotoPage = this.gotoPage.bind(this);
    }

    deleteClick(data, callback) {
        VDS.Utils.Ajax.post('/api/' + this.props.type + '/delete', data,
        {
            onOK: (result) => {
                this.onSearch(this.state.search, this.state.page);
                callback();
            },
            onInUse: (result) => {
                this.setState({
                    errorMessage: '\'' + data.EntityName + '\' cannot be deleted as it is currently in use.'
                });
            }
        });
    }

    saveClick(data, callback) {
        var add = data.EntityId === 0;
        var url = add ? '/api/' + this.props.type + '/create' : '/api/' + this.props.type + '/update';

        VDS.Utils.Ajax.post(url, data,
        {
            onOK: (result) => {
                if (add) {
                    this.onSearch(this.state.search, this.state.page);
                }
                else {
                    VDS.Utils.Array.updateEntityItem(this.state.list, result, 'EntityId');
                }

                this.setState({
                    list: this.state.list
                });

                callback();
            },
            onInvalid: (result) => {
                this.setState({
                    errorMessage: 'You must enter a value for name.'
                });
            },
            onDuplicate: (result) => {
                this.setState({
                    errorMessage: 'The name you have entered already exists. Please change the name to make it unique.'
                });
            }
        });
    }

    onSearch(search, page) {
        VDS.Utils.Ajax.post('/api/' + this.props.type + '/search?search=' + search + '&page=' + page, null,
        {
            onOK: (response) => {
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
        this.setState({
            errorMessage: ''
        });
    }
    
    render() {
        return (
            <div className="row">
                <SearchBar title={this.props.type + " search"} onSearch={this.onSearch} />
                <AdminEntityList errorMessage={this.state.errorMessage} type={this.props.type} title={this.props.type + " list"} data={this.state.list} page={this.state.page} pageSize={this.state.pageSize} totalRecords={this.state.totalRecords} gotoPage={this.gotoPage} saveClick={this.saveClick} deleteClick={this.deleteClick} />
            </div>
        );
    }
};
