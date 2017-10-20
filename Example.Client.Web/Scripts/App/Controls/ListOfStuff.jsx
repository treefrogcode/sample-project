var ListOfStuff = React.createClass({

    editClick: function (event) {
        this.props.editClick(event);
    },

    deleteClick: function (event) {
        this.props.deleteClick(event);
    },

    render: function() {
        var nodes = this.props.stuffList.map(function(item, index) {
            return (
                <Stuff data={item} key={item.StuffId} editClick={this.editClick} deleteClick={this.deleteClick} />
                );
            }.bind(this));

        return (
            <div className="col-xs-12">
                <h3>List of stuff</h3>
                <div className="row">
                    {nodes}
                </div>
            </div>
        );
    }
});