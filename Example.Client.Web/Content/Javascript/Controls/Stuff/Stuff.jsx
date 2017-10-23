class Stuff extends React.Component {

    constructor(props) {
        super(props);
    }

    editClick(event) {
        this.props.editClick(event);
    }

    deleteClick(event) {
        this.props.deleteClick(event);
    }

    render() {
        return (
            <div className="col-xs-4 col-md-3 col-lg-2">
                <div className="row">
                    <div className="stuff-card">
                        <div className="col-xs-12">{this.props.data.One}</div>
                        <div className="col-xs-12">{this.props.data.Two}</div>
                        <div className="col-xs-12">{this.props.data.Three}</div>
                        <div className="col-xs-12">
                            <div className="row">
                                <div className="col-xs-6">[<a onClick={this.editClick.bind(this, this.props.data)} href="#">edit</a>]</div>
                                <div className="col-xs-6">[<a onClick={this.deleteClick.bind(this, this.props.data)} href="#">delete</a>]</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
};