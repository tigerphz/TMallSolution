// 所有模块都通过 define 来定义
define(function (require, exports) {    
    var am = require("./updatemenu");

    exports.Init = function () {
        am.InitKO();
    }
});

