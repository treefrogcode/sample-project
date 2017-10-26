class SearchBar extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            search: ""
        };

        this.changeText = this.changeText.bind(this);
        this.onSearch = this.onSearch.bind(this);
    }

     changeText(event) {
         var value = $.trim(event.target.value);
         this.setState({
             search: value
         });
     }

     onSearch(event) {
         event.preventDefault();
         var search = this.state.search;
         this.props.onSearch(search);
         this.setState({
             search : ""
         });
     }

     render() {
         return (
            <div className="col-xs-12">
                <form onSubmit={this.onSearch}>
                    <div className="row">
                        <div className="col-xs-12">
                            <h3>{this.props.title}</h3>
                        </div>
                    </div>
                    <div className="row form-inline">
                        <div className="col-xs-12 form-group">
                            <input type="text" value={this.state.search} maxLength="100" className="form-control mr10" onChange={this.changeText} />
                            <Button type="submit" label="Search" icon="search" title="Search" class="secondary" />
                        </div>
                    </div>
                </form>
            </div>
        );
     }
 };