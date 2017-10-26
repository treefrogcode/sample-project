class CategoryModal extends React.Component {

    constructor(props) { 
        super(props);
    }

    render() {

        const cats = this.props.categories.map(function (cat) {
            return <span className="modal-list" key={cat.EntityId }>{cat.EntityName}</span>
        });

        return (
            <div id="categoryModal" className="modal fade" role="dialog">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <button type="button" className="close" data-dismiss="modal">&times;</button>
                            <h4 className="modal-title">Category List</h4>
                        </div>
                        <div className="modal-body">
                            {cats}
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}