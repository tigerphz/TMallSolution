// 所有模块都通过 define 来定义
define(function (require, exports, module) {
    var ko = require("knockout");

    var viewModel = {
        updatemenuSelectedMenu: ko.observable()
    };

    module.exports = {
        InitKO: function () {

            viewModel.updatemenuSelectedMenu($("#MenuUpdateLeve12").val());

            ko.applyBindings(viewModel, $("#UpdateMenuContent").get(0));
        }
    };
});

