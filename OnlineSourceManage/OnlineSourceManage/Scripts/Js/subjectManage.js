/**
 * Created by DELL on 2017/2/10.
 */
/**
 * 初始化数据列表
 * @param id 容器id
 */
$(function () {
    initDataGridHeight("#dataList", 156)
    initDataGrid("#dataList")
    initBtnEffect(".form-wrapper")
})


function initBtnEffect(id){
    $(id).on('click','.item',function(){
        $(this).addClass('current').siblings().removeClass('current')
    })
}

var gridConfig = {
    striped: true,
    fitColumns: true,
    rownumbers: true,
    border: true,
    //autoHeight: true,
    editable: false,
    singleSelect: true,
    loadMsg: '玩命加载中...',
    frozenColumns: [[]],
    resizable: true,
    pagination: false,
    fit: false
};
var homeMap = ["前端开发","后台开发","移动端开发","数据库","图形图像"];
function initDataGrid(id) {
    /*初始化datagrid*/
    var optionSet = {
        data: (function () {
            var arr = [];
            for (var i = 10; i < 30; i++) {
                var item = {
                    "id": "022101213" + i,
                    "name": "课程" + i,
                    "role": Math.round((Math.random() * 11)) > 3 ? "管理员" : "普通用户",
                    "starttime": "2017-03-" + i,
                    "ctype": homeMap[Math.round((Math.random() * 4))],
                    "mark": ""
                };
                arr.push(item)
            }
            return arr
        })(),
        columns: [[
            {field: 'id', title: '编号', width: 100},
            {field: 'name', title: '课程名称', width: 100},
            {field: 'ctype', title: '课程类型', width: 100},
            {field: 'starttime', title: '上传时间', width: 100},
            {field: 'mark', title: '备注', width: 100},
            {
                field: 'operate', title: '操作', width: 100,
                formatter: function (value, row) {
                    return '<a href="javascript:;" class="delUser text-danger" data-name="'+row.name+'" data-id="' + row.id + '">删除</a> /' + ' <a href="javascript:;" class="editUser" data-name="'+row.name+'" data-id="' + row.devName + '">编辑</a>'
                }
            }
        ]]
    };
    var option = $.extend({}, gridConfig, optionSet);
    $(id).datagrid(option)

}


function initDataGridHeight(id, titleH) {
    var winH = $(window).outerHeight();
    $(id).height(winH - titleH)
}