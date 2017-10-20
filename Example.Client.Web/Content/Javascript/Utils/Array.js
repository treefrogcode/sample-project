var VDS = VDS || {};
VDS.Utils = VDS.Utils || {};

VDS.Utils.Array = function () {

    var updateEntityItem = function (list, entity, property) {
        for (var i = 0; i < list.length; i++) {
            if (list[i][property] === entity[property]) {
                list[i] = entity;
                break;
            }
        }
    };

    var removeEntityItem = function (list, entity, property) {
        var index = -1;
        for (var i = 0; i < list.length; i++) {
            if (list[i][property] === entity[property]) {
                index = i;
                break;
            }
        }
        if (index > -1) {
            list.splice(index, 1);
        }
    };

    return {
        updateEntityItem: updateEntityItem,
        removeEntityItem: removeEntityItem
    };

}();