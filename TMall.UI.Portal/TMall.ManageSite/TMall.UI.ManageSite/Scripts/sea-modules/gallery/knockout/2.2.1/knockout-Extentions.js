//ko validation扩展
define('gallery/knockout/2.2.1/knockout-Extentions', [], function (require, exports, module) {
    var ko = require('knockout');    
    ko.validation = require('knockoutValidation');
    (function () {
        //region 扩展ko validation的规则
        ko.validation.rules['mustEqual'] = {
            validator: function (val, mustEqualVal) {
                return val == mustEqualVal;
            },
            message: '输入值必须等于{0}'
        };
        //注册新的验证扩展
        ko.validation.registerExtenders();
        //#endregion

        //region bindingHandler扩展
        ko.bindingHandlers.asterisk = {
            update: function (element, valueAccessor) {
                var val = valueAccessor();
                if (!val.isModified()) {
                    return;
                }

                if (!val.isValid()) {
                    ko.bindingHandlers.text.update(element, function () { return "*"; });
                }
            }
        };
        // Custom Bindings
        ko.bindingHandlers.addOnEnter = {
            init: function (element, valueAccessor) {
                var value = valueAccessor();
                var a = require('jquery');
                a(element).keypress(function (event) {
                    var keyCode = (event.which ? event.which : event.keyCode);
                    if (keyCode === 13) {
                        value.call(this);
                        return false;
                    }
                    return true;
                });
            },
            update: function (element, valueAccesor) {

            }
        };

        //endregion   

        ko.extenders.logChange = function (target, option) {
            target.subscribe(function (newValue) {
                console.log(option + ": " + newValue);
            });
            return target;
        };

        ko.extenders.numeric = function (target, precision) {
            //create a writeable computed observable to intercept writes to our observable
            var result = ko.computed({
                read: target,  //always return the original observables value
                write: function (newValue) {
                    var current = target(),
                roundingMultiplier = Math.pow(10, precision),
                newValueAsNum = isNaN(newValue) ? 0 : parseFloat(+newValue),
                valueToWrite = Math.round(newValueAsNum * roundingMultiplier) / roundingMultiplier;

                    //only write if it changed
                    if (valueToWrite !== current) {
                        target(valueToWrite);
                    } else {
                        //if the rounded value is the same, but a different value was written, force a notification for the current field
                        if (newValue !== current) {
                            target.notifySubscribers(valueToWrite);
                        }
                    }
                }
            });

            //initialize with current value to make sure it is rounded appropriately
            result(target());

            //return the new computed observable
            return result;
        };

        ko.computed.fn.test = function (newValue) {
            return ko.computed(function () {
                var desc = this();
                alert(newValue);
            }, this);
        };

        ko.observable.fn.raisePropertyChanged = function (propertyName, value) {
            var expression;
            if (typeof (value) == "string")
                expression = propertyName + "('" + value + "');"
            else
                expression = propertyName + "(" + value + ");"
            return ko.computed(function () {
                eval(expression);
            }, this);
        };

        //初始化验证选项，提示信息样式
        var validationOptions = {
            insertMessages: true,
            decorateElement: false,
            errorsAsTitleOnModified: true,
            errorMessageClass: "error",
            //errorElementClass: "validation-advice",
            messageTemplate: "tips-template",
            grouping: { deep: true, observable: true }
        };
        ko.validation.init(validationOptions);
    })();
});
