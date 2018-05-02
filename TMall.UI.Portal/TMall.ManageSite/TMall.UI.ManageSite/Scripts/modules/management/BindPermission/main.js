// 所有模块都通过 define 来定义
define(function (require, exports) {
    require("/Content/easyui/easyui.css");
    var am = require("./bindpermission");

    exports.Init = function (roleId, navId, jsonData, type) {
        am.InitTreeGrid(roleId, navId, jsonData, type);
    }
});

