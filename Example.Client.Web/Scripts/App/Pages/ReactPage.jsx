ReactPage = React.createClass({

    getInitialState: function() {
        return {
            list: JSON.parse(this.props.initialData),
            adding: false,
            stuff: this.emptyStuff()
        };
    },
    emptyStuff: function () {
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
            stuff: this.emptyStuff()
        });
    },
    editClick: function(event) {
        this.setState({
            adding: true,
            stuff: event
        });
    },
    deleteClick: function(stuff) {
        $.ajax({
            url: '/api/stuff/delete',
            data: stuff,
            type: "post",
            success: (result) => {
                VDS.Utils.ArrayUtils.removeEntityItem(this.state.list, result, "StuffId");
                this.setState({
                    adding: false,
                    list: this.state.list
                });
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => {
                console.log(XMLHttpRequest.responseText);
                alert("Whoops: " + errorThrown);
            }
        });
    },
    cancelClick: function() {
        this.setState({
            adding: false,
            stuff: this.emptyStuff()
        });
    },
    saveClick: function(stuff) {
        var add = stuff.StuffId === 0;
        $.ajax({
            url: add ? '/api/stuff/create' : '/api/stuff/update', 
            data: stuff,
            type: "post",
            success: (result) => {
                if (add) {
                    this.state.list.push(result);
                }
                else {
                    VDS.Utils.ArrayUtils.updateEntityItem(this.state.list, result, "StuffId");
                }
                this.setState({
                    adding: false,
                    list: this.state.list
                });
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => {
                console.log(XMLHttpRequest.responseText);
                alert("Whoops: " + errorThrown);
            }
        });
    },
    onSearch: function (search) {
        $.ajax({
            url: '/api/stuff/search?search=' + search,
            type: "post",
            success: (result) => {
                this.setState({
                    list: result
                });
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => {
                console.log(XMLHttpRequest.responseText);
                alert("Whoops: " + errorThrown);
            }
        });
    },
    render: function() {
        return (
            <div className="row">
                <SearchBar onSearch={this.onSearch} />
                <ListOfStuff stuffList={this.state.list} editClick={this.editClick} deleteClick={this.deleteClick} />
                <div className="col-xs-12 pb20">
                    <a className="btn btn-primary mr20" onClick={this.reverseClick}>Reverse list</a>
                    <a className="btn btn-warning" onClick={this.addClick}>Add New</a>
                </div>
                {this.state.adding ? <AddStuff stuff={this.state.stuff} saveClick={this.saveClick} cancelClick={this.cancelClick} /> : null }
            </div>
        );
    }
});
