class StuffPage extends React.Component {

    constructor(props) {
        super(props);

        var initialDataObj = JSON.parse(this.props.initialData);

        this.colours = initialDataObj.Colours;
        this.categories = initialDataObj.Categories;

        this.state = {
            list: initialDataObj.PagedResults.Results,
            page: initialDataObj.PagedResults.Paging.Page,
            pageSize: initialDataObj.PagedResults.Paging.PageSize,
            totalRecords: initialDataObj.PagedResults.Paging.TotalRecords,
            adding: false,
            data: this.emptyData(),
            errorMessage: "",
            search: "",
            reverse: false
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
            EntityName : ''
        };

        return empty;
    }

    reverseClick() {
        this.setState({
            reverse: !this.state.reverse
        }, () => {
            this.onSearch(this.state.search, 1);
        });
    }

    addClick() {
        this.setState({
            adding: true,
            data: this.emptyData()
        }, () => {
            $('html, body').animate({
                scrollTop: $("#stuff-form").offset().top
            }, 800);
        });
    }

    editClick(eventData) {
        this.setState({
            adding: true,
            data: eventData
        }, () => {
            $('html, body').animate({
                scrollTop: $("#stuff-form").offset().top
            }, 800);
        });
    }

    deleteClick(data) {
        Example.Utils.Ajax.post('/api/stuff/delete', data,
        {
            onOK: (result) => {
                this.onSearch(this.state.search, this.state.page);
                this.setState({
                    adding: false,
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
        var url = add ? '/api/stuff/create' : '/api/stuff/update';
        data.EntityName = data.One; // needed as Stuff implements INamedEntity but we used property One in the jsx pages
        Example.Utils.Ajax.post(url, data,
        {
            onOK: (result) => {
                if (add) {
                    this.onSearch(this.state.search, this.state.page);
                }
                else {
                    Example.Utils.Array.updateEntityItem(this.state.list, result, "StuffId");
                }
                this.setState({
                    adding: false,
                    list: this.state.list
                });
            },
            onInvalid: (result) => {
                this.setState({
                    errorMessage: "You have not entered valid data for all the fields"
                });
            }
        });
    }

    onSearch(search, page) {

        var url = this.state.reverse ? 'reverse-search' : 'search';

        Example.Utils.Ajax.post('/api/stuff/' + url + '?search=' + search + '&page=' + page, null,
        {
            onOK: (response) => {
                this.setState({
                    search: search,
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
                <SearchBar title="Stuff search" onSearch={this.onSearch} />
                <StuffList title="Stuff List" data={this.state.list} page={this.state.page} pageSize={this.state.pageSize} totalRecords={this.state.totalRecords} gotoPage={this.gotoPage} editClick={this.editClick} deleteClick={this.deleteClick} />
                <div className="col-xs-12 pb20">
                    <a className="btn btn-primary mr20" onClick={this.reverseClick }>{this.state.reverse ? "Sort A-Z" : "Sort Z-A"}</a>
                    <a className="btn btn-warning" onClick={this.addClick}>Add New</a>
                </div>
                {this.state.errorMessage.length > 0 ? <ErrorMessage message={this.state.errorMessage}></ErrorMessage> : null}
                {this.state.adding ? <AddStuff data={this.state.data} colours={this.colours} categories={this.categories} saveClick={this.saveClick} cancelClick={this.cancelClick} /> : null }
            </div>
        );
    }
};
