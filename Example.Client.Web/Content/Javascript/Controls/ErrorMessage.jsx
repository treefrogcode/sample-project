﻿class ErrorMessage extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {

        return (
            <div className="row">
                <div className="col-xs-12">
                    <div className="error-message mt20">{this.props.message}</div>
                </div>
            </div>
        );
    }
};