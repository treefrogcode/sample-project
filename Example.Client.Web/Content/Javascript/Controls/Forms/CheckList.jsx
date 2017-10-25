﻿class CheckList extends React.Component {

    constructor(props) {
        super(props);

        this.cleanLabel = this.props.label.replace(" ", "");

        this.state = {
            value: VDS.Utils.Array.turnIntoFlatStringArray(props.value, "EntityId"),
            data: props.data
        };

        this.onChange = this.onChange.bind(this);
    }

    componentWillReceiveProps(newProps) {
        this.setState({
            value: VDS.Utils.Array.turnIntoFlatStringArray(newProps.value, "EntityId"),
            data: newProps.data
        });
    }

    onChange(event) {
        var value = $.trim(event.target.value);
        var checked = event.target.checked;
        var current = VDS.Utils.Array.getEntityItem(this.state.value, value, "EntityId");
        VDS.Utils.Array.removeEntityItem(this.state.value, value);
        if (checked) this.state.value.push(value);

        this.setState({
            value: this.state.value
        });

        if (typeof this.props.newValue === "function") {
            this.props.newValue(this.state.value, this.props.stateProp);
        }
    }

    render() {

        const options = this.state.data.map((opt) => {
            var checked = VDS.Utils.Array.getEntityItem(this.state.value, opt.EntityId) !== null;
            return (
                <div key={opt.EntityId} className="checkbox">
                    <label>
                        <input type="checkbox" checked={checked} value={opt.EntityId} onChange={this.onChange } />
                        <span>{opt.Name}</span>
                    </label>
                </div>
            );
        });

        return (
               <div className="row">
                    <div className="col-xs-12 form-group">
                        <label className="mr10">{this.props.label}</label>
                        <div>
                            {options}
                        </div>
                    </div>
               </div>
        );
    }
};