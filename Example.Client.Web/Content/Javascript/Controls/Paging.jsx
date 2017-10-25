class Paging extends React.Component {

    constructor(props) {
        super(props);

        this.state = this.getPagingState(props);
    }

    componentWillReceiveProps(newProps) { // inbuilt method
        this.setState(this.getPagingState(newProps));
    }

    getPagingState(props) {
        let totalPages = Math.ceil(props.totalRecords / props.pageSize);
        let prevPage = props.page === 1 ? null : props.page - 1;
        let nextPage = props.page === totalPages ? null : props.page + 1;
        let showStart = props.totalRecords === 0 ? 0 : ((props.page - 1) * props.pageSize) + 1;
        let showEnd = showStart + props.pageSize > props.totalRecords ? props.totalRecords : showStart + props.pageSize - 1;
        return {
            page: props.page,
            totalPages: totalPages,
            nextPage: nextPage,
            prevPage: prevPage,
            nextDisabled: props.page === totalPages,
            prevDisabled: props.page === 1,
            showStart: showStart,
            showEnd: showEnd
        };
    }

    gotoPage(page) {
        this.props.gotoPage(page);
    }

    render() {

        var pages = [];
        for (var i = 1; i <= this.state.totalPages; i++) {
            if (i === this.state.page) {
                pages.push(<a className="btn btn-primary mr10" key={i} disabled="disabled">{i}</a>);
            }
            else {
                pages.push(<a className="btn btn-primary mr10" key={i} onClick={this.gotoPage.bind(this, i) }>{i}</a>);
            }
        }

        return (
            <div className="col-xs-12 pagination">
                <div className="row">
                    <div className="col-xs-6 hidden-xs hidden-sm">Showing records {this.state.showStart} to {this.state.showEnd} of {this.props.totalRecords}</div>
                    <div className="col-xs-12 col-md-6 pull-right text-right">
                        <a disabled={this.state.prevDisabled} className="btn btn-primary mr10" onClick={this.gotoPage.bind(this, this.state.prevPage) }>&laquo;</a>
                        {pages}
                        <a disabled={this.state.nextDisabled} className="btn btn-primary mr10" onClick={this.gotoPage.bind(this, this.state.nextPage) }>&raquo;</a>
                    </div>
                </div>
            </div>
        );
    }
};