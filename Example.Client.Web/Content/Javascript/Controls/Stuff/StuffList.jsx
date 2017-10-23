class StuffList extends React.Component {

    constructor(props) {
        super(props);

        this.editClick = this.editClick.bind(this);
        this.deleteClick = this.deleteClick.bind(this);
        this.gotoPage = this.gotoPage.bind(this);
    }

    editClick(event) {
        this.props.editClick(event);
    }

    deleteClick(event) {
        this.props.deleteClick(event);
    }

    gotoPage(page) {
        this.props.gotoPage(page);
    }

    render() {
        var nodes = this.props.data.map(function(item, index) {
            return (
                <Stuff data={item} key={index} editClick={this.editClick} deleteClick={this.deleteClick} />
                );
            }.bind(this));

        return (
            <div className="col-xs-12">
                <h3>{this.props.title}</h3>
                <div className="row">
                    {nodes}
                </div>
                <Paging page={this.props.page} pageSize={this.props.pageSize} totalRecords={this.props.totalRecords} gotoPage={this.gotoPage}></Paging>
            </div>
        );
    }
};