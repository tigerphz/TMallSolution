// 所有模块都通过 define 来定义
define(function (require, exports, module) {
    var eu = require("easyui");

    module.exports = {
        InitTreeGrid: function (roleId, navId, jsonData, type) {
            $treeGrid = $('#permission_' + roleId);
            $treeGrid.treegrid({
                iconCls: 'icon-save',
                width: 750,
                height: 400,
                nowrap: false,
                rownumbers: true,
                animate: false,
                collapsible: true,
                idField: 'Identifier',
                treeField: 'name',
                frozenColumns: [[
	                { title: '权限名称', field: 'name', width: 200,
	                    formatter: function (value, rowData, rowIndex) {
	                        return "<input type='checkbox' sid='" + rowData.sysno + "' id='" + rowData.Identifier + "' " + (rowData.ischecked ? 'checked' : '') + "/>" + rowData.name;
	                    }
	                }
				]],
                columns: [[
					{ field: 'controller', title: '控制器', width: 80 },
					{ field: 'action', title: '动作', width: 80 },
                    { field: 'type', title: '类型', width: 80 },
					{ field: 'status', title: '状态', width: 50 },
					{ field: 'desc', title: '描述信息', width: 200 }
				]]
            });

            $treeGrid.treegrid('loadData', jsonData);

            //去掉结点前面的文件及文件夹小图标
            $(".tree-icon,.tree-file").removeClass("tree-icon tree-file");
            $(".tree-icon,.tree-folder").removeClass("tree-icon tree-folder tree-folder-open tree-folder-closed");
            $(".tree-icon,.tree-folder,.tree-folder-open ").removeClass("tree-icon tree-folder tree-folder-open");
            $("div.pageContent div.panel").removeClass("panel");

            $("div#bindPermissionPanel a.ExpandAll").click(function () {
                var node = $treeGrid.treegrid('getSelected');
                if (node) {
                    $treeGrid.treegrid('expandAll', node.code);
                } else {
                    $treeGrid.treegrid('expandAll');
                }
            });

            $("div#bindPermissionPanel a.collapseAll").click(function () {
                var node = $treeGrid.treegrid('getSelected');
                if (node) {
                    $treeGrid.treegrid('collapseAll', node.code);
                } else {
                    $treeGrid.treegrid('collapseAll');
                }
            })

            $("div#bindPermissionPanel a.selectAll").click(function () {
                $("div.permissionTreeGrid").find("input[type='checkbox']").each(function () {
                    $(this).attr("checked", "checked");
                })
            })

            $("div#bindPermissionPanel a.cancelAll").click(function () {
                $("div.permissionTreeGrid").find("input[type='checkbox']:checked").each(function () {
                    $(this).removeAttr("checked");
                })
            })

            $("div.permissionTreeGrid").find("input[type='checkbox']").click(function () {
                var id = $(this).attr("id");
                var level = $treeGrid.treegrid('getLevel', id);
                if (level != 1 && $(this).attr("checked") == "checked") {
                    var parent = $treeGrid.treegrid('getParent', id);
                    var Identifier = parent.Identifier;
                    $("div.permissionTreeGrid").find("input[type='checkbox'][id='" + Identifier + "']")
                    .attr("checked", "checked");
                }
                else if ($(this).attr("checked") != "checked") {
                    var childs = $treeGrid.treegrid('getChildren', id);
                    var Identifier;
                    for (var i = 0; i < childs.length; i++) {
                        Identifier = childs[i].Identifier;
                        $("div.permissionTreeGrid").find("input[type='checkbox'][id='" + Identifier + "']")
                         .removeAttr("checked");
                    }
                }
            })

            $("div#bindPermissionPanel button[type='submit']").click(function () {
                var sysnoList = [];
                $("div.permissionTreeGrid").find("input[type='checkbox']:checked").each(function () {
                    var sysno = $(this).attr("sid");
                    sysnoList.push(sysno);
                })

                var strList = sysnoList.join(',');

                $.ajax({
                    type: "POST",
                    url: "/Management/Role/BindPermission/",
                    data: { "id": roleId, "navId": navId, "sysnoList": strList },
                    dataType: "json",
                    beforeSend: function (XMLHttpRequest) {

                    },
                    success: function (data, textStatus) {
                        if (type == "simpleview")
                            $.pdialog.closeCurrent();

                        if (data.statusCode == "200")
                            alertMsg.correct(data.message);
                        else {
                            alertMsg.error(data.message);
                        }
                    },
                    complete: function (XMLHttpRequest, textStatus) {

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alertMsg.correct('权限绑定失败！')
                    }
                });
            })
        }
    };
});

