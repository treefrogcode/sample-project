var Example = Example || {};
Example.Utils = Example.Utils || {};

Example.Utils.Array = function () {

    var getEntityItem = function (list, value, property) {
        var withProperty = typeof property !== "undefined" && property !== null && property !== "";
        if (list.length) {
            for (var i = 0; i < list.length; i++) {
                if ((!withProperty && list[i] == value) || (withProperty && list[i][property] == value)) {
                    return list[i];
                }
            }
        }

        return null;
    };

    var getAllEntityItems = function (mainlist, sublist, property) {
        var result = [];
        if (sublist.length) {
            for (var i = 0; i < sublist.length; i++) {
                result.push(getEntityItem(mainlist, sublist[i], property));
            }
        }

        return result;
    };

    var updateEntityItem = function (list, entity, property) {
        var withProperty = typeof property !== "undefined" && property !== null && property !== "";
        for (var i = 0; i < list.length; i++) {
            if ((!withProperty && list[i] === entity) || (withProperty && list[i][property] === entity[property])) {
                list[i] = entity;
                break;
            }
        }
    };

    var removeEntityItem = function (list, entity, property) {
        var index = -1;
        var withProperty = typeof property !== "undefined" && property !== null && property !== "";
        for (var i = 0; i < list.length; i++) {
            if ((!withProperty && list[i] === entity) || (withProperty && list[i][property] === entity[property])) {
                index = i;
                break;
            }
        }
        if (index > -1) {
            list.splice(index, 1);
        }
    };

    var turnIntoFlatStringArray = function (list, property) {
        var withProperty = typeof property !== "undefined" && property !== null && property !== "";
        var newArray = [];
        if (typeof list === "undefined") return newArray;
        for (var i = 0; i < list.length; i++) {
            newArray.push(withProperty ? list[i][property].toString() : list[i].toString());
        }
        return newArray;
    };

    return {
        updateEntityItem: updateEntityItem,
        removeEntityItem: removeEntityItem,
        getEntityItem: getEntityItem,
        getAllEntityItems: getAllEntityItems,
        turnIntoFlatStringArray: turnIntoFlatStringArray,
    };

}();