﻿class Stuff extends React.Component {

    constructor(props) {
        super(props);
    }

    editClick(event) {
        this.props.editClick(event);
    }

    deleteClick(event) {
        this.props.deleteClick(event);
    }

    showModal(event) {
        this.props.showModal(event);
    }

    render() {

        var style = {
            backgroundColor: this.props.data.Colour == null ? "" : this.props.data.Colour.EntityName,
            border: "solid 1px #a0a0a0"
        };

        return (
            <div className="col-xs-4 col-md-3 col-lg-2">
                <div className="row">
                    <div className="stuff-card">
                        <div className="col-xs-12">
                            <div className="row">
                                <div className="col-xs-9">{this.props.data.One}</div>
                                <div className="col-xs-3 pull-right">
                                    {this.props.data.Categories.length == 0 ?
                                        <span title="No categories" className="badge">{this.props.data.Categories.length}</span> :
                                        <a title="Click to view a list of categories" onClick={this.showModal.bind(this, this.props.data.Categories)} className="badge">{this.props.data.Categories.length}</a> 
                                    }
                                </div>
                            </div>
                        </div>
                        <div className="col-xs-12">{this.props.data.Two}</div>
                        <div className="col-xs-12">{this.props.data.Three}</div>
                        <div className="col-xs-12">
                            <div className="row mt20">
                                <div className="col-xs-12" style={style}>&nbsp;</div>
                            </div>
                            <div className="row mt20">
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