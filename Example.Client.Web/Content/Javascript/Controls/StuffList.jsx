var List = React.createClass({

    editClick: function (event) {
        this.props.editClick(event);
    },

    deleteClick: function (event) {
        this.props.deleteClick(event);
    },

    render: function () {
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
            </div>
        );
    }
});