var VDS = VDS || {};
VDS.Utils = VDS.Utils || {};

VDS.Utils.Ajax = function () {

    //supports methods { onOK, onInvalid, onError, onOther }

    var post = function (url, data, methods) {
        $.ajax({
            url: url,
            data: data,
            type: "post",
            success: (response) => {
                if (typeof methods !== "undefined") {
                    switch (response.Message) {
                        case "OK":
                            if (typeof methods.onOK !== "undefined") {
                                methods.onOK(response.Result);
                            }
                            break;
                        case "Invalid":
                            if (typeof methods.onInvalid !== "undefined") {
                                methods.onInvalid(response.Result);
                            }
                            break;
                        default:
                            if (typeof methods.onOther !== "undefined") {
                                methods.onOther(response);
                            }
                            else {
                                // do nothing
                            }
                    }
                }
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => {
                if (typeof methods !== "undefined" && typeof methods.onError !== "undefined") {
                    methods.onError(XMLHttpRequest, textStatus, errorThrown);
                }
                else {
                    console.log(XMLHttpRequest.responseText);
                    alert("An unknown error occurred: " + errorThrown);
                }
            }
        });
    };

    return {
        post: post
    };

}();