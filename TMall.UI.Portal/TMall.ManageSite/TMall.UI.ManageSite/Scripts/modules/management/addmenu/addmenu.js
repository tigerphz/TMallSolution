// 所有模块都通过 define 来定义
define(function (require, exports, module) {
    var ko = require("knockout");

    var viewModel = {
        addmenuSelectedMenu: ko.observable($("#MenuAddLeve12").val())
    };

    module.exports = {
        InitKO: function () {
            ko.applyBindings(viewModel, $("#AddMenuContent").get(0));
        }
    };
});

