/**
 * Created by DELL on 2017/2/10.
 */
/**
 * 初始化数据列表
 * @param id 容器id
 */
$(function () {
    //初始化datagrid高度
    initDataGridHeight("#dataList", 356);
    //初始化Echarts图表
    initEchart("linechart", "home");
    initEchartPie("piechart-role", "role");
    initEchartPie("piechart-sex", "sex");
    //初始化datagrid
    initDataGrid("#dataList");

    $("#searchUser").click(function (e) {
        e.preventDefault();
        var result = $("#searchVal").val();
        if ("管理员".indexOf(result) > -1) result = 1;
        if ("普通用户".indexOf(result) > -1) result = 0;
        $.ajax({
            url: "/UserInfo/GetUserByKey",
            data: { keys: result },
            success: function (data) {
               $("#dataList").datagrid("loadData", data);
            }
        });

    });
    $("#addUser").click(function() {
        FrameAlert("新增用户", "/UserLayer/UserLayer", 740, 310);
    });

    $("#viewExcel").click(function() {
        FrameAlert("报表预览","/Handson/index",1260,660);
    });

});

var gridHeight;
/**
 * 初始化用户列表
 * @param {} id 
 * @returns {} 
 */
function initDataGrid(id) {
    /*初始化datagrid*/
    var option = {
        url: "/UserInfo/GetAllUsers",
        striped: true,
        fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        loadMsg: "玩命加载中...",
        resizable: true,
        pagination: true,
        pageSize: 10,
        pageList: [5, 10, 15,25],
        fit: false,
        columns: [[
            { field: "uName", title: "用户名", width: 100 },
            { field: "home", title: "所在地", width: 100 },
            {
                field: "sex", title: "性别", width: 100, formatter: function (value) {
                    return value == 0 ? "<span class=' text-success'><i class='fa fa-female'></i> 萌妹子</span>" : "<span class='text-success'><i class='fa fa-user'></i> 帅小伙</span>";
                }
            },
            {
                field: "role", title: "角色", width: 100, formatter: function (value) {
                    return value == 0 ? "普通用户" : "管理员";
                }
            },
            { field: "pwd", title: "密码", width: 100 },
            {
                field: "useable", title: "状态", width: 100, formatter: function (value) {
                    var s1 = "<span class='text-success'> <i class='fa fa-check'></i> 正常</span>";
                    var s2 = "<span class='text-danger'><i class='fa fa-close'></i> 禁用中...</span>";
                    return value == 0 ? s1 : s2;
                }
            },

            { field: "regTime", title: "注册时间", width: 100 },

            {
                field: "operate", title: "操作", width: 160,
                formatter: function (value, row) {
                    var s = row.useable == 0 ? " 禁用权限" : ' 启用权限';
                    var r = row.role == 0 ? " 设为管理员" : ' 取消管理员';

                    return '<a onclick="DeleteUser(this)" class="text-danger set-btn" data-id="' + row.uId + '">删除</a> /'
                        + ' <a onclick="EditUser(this)" class="text-success set-btn" data-id="' + row.uId + '">编辑</a> /'

                        + ' <a onclick="limitUser(this)" class="text-danger set-btn" data-id="'
                        + row.uId + '" data-useable= "' + row.useable + '">' + s + '</a> /'

                        + '<a onclick="setAdmin(this)" data-id="'
                        + row.uId + '" data-role= "' + row.role + '" class="text-success set-btn">' + r + '</a>';

                }
            }
        ]]
    };
    $(id).datagrid(option);
    $(id).datagrid("reload", { height: $(window).height() });
}

function initDataGridHeight(id, titleH) {
    var winH = $(window).height();
    var gridHeight = winH - titleH;
    $(id).height(gridHeight);
    //将datagrid的高度值缓存起来
    //$("#gridHeight").val(gridH);
}

/**
 * 删除指定用户
 * @param {} that 
 * @returns {} 
 */
function DeleteUser(that) {
    layer.confirm("确定删除该用户？",
        { icon: 3, shade: 0.01 },
        function () {
            var id = $(that).data("id");
            $.ajax({
                url: "/UserInfo/DeleteUser",
                data: { uId: id },
                success: function (data) {
                    if (data == "ok") {
                        layer.msg("删除成功！", { icon: 1, time: 800 });
                        $("#dataList").datagrid("reload");
                        initDataGrid("#dataList");
                        initEchart("linechart", "home");
                        initEchartPie("piechart-role", "role");
                        initEchartPie("piechart-sex", "sex");
                    } else {
                        layer.msg("删除失败", { icon: 2, time: 800 });
                    }
                }
            });
        });

}

/**
 * 开启和禁用用户权限
 * @param {} that 
 * @returns {} 
 */
function limitUser(that) {
    var id = $(that).data("id");
    var useflag = $(that).data("useable");
    $.ajax({
        url: "/UserInfo/UserRightsManage",
        data: { uId: id, useable: useflag },
        success: function (data) {
            if (data == "ok") {
                layer.msg("操作成功！", { icon: 1, time: 800 });
                $("#dataList").datagrid("reload");
            } else {
                layer.msg("操作失败", { icon: 2, time: 800 });
            }
        }
    });
}
/**
 * 编辑用户信息
 * @param {} that 
 * @returns {} 
 */
function EditUser(that) {
    var id = $(that).data("id");
    FrameAlert("编辑用户", "/UserLayer/UserLayer?id=" + id, 730, 310);
}

/**
 * 管理员角色管理
 * @param {} that 
 * @returns {} 
 */
function setAdmin(that) {
    layer.confirm("确定对该用户进行管理员相关权限操作？",
        { icon: 3 },
        function () {
            var id = $(that).data("id");
            var roleflag = $(that).data("role");
            $.ajax({
                url: "/UserInfo/SetAdmin",
                data: { uId: id, role: roleflag },
                success: function (data) {
                    if (data == "ok") {
                        layer.msg("操作成功！", { icon: 1, time: 800 });
                        $("#dataList").datagrid("reload");
                        initEchartPie("piechart-role", "role");
                    } else {
                        layer.msg("操作失败", { icon: 2, time: 800 });
                    }
                }
            });
        });
}

/**
 * iframe弹出层
 * @param title 弹出层显示的名称
 * @param url 显示的iframe页路径
 * @param w 弹出层宽度
 * @param h 弹出层高度
 */
function FrameAlert(title, url, w, h) {
    layer.open({
        title: title,
        type: 2,
        skin: "layui-layer-molv",
        content: url,
        maxmin: true,
        shade: 0.01,
        area: [w + "px", h + "px"],
        zIndex: layer.zIndex
    });
}

/**
 * 初始化图表
 * @param id 容器id
 */
function initEchart(id, t) {
    var container = echarts.init(document.getElementById(id));
    var strName = [], strNum = [];
    $.ajax({
        url: "/UserInfo/GetUsersInType",
        data: { type: t },
        async: true,
        success: function (data) {
            if (data != null) {
                strName = data.name;
                strNum = data.num;
            }

            var option = {
                tooltip: {
                    trigger: "axis"
                },
                legend: {
                    data: strName,
                    top: 8
                },
                grid: {
                    top: 40
                },
                toolbox: {
                    show: true,
                    right: 40,
                    feature: {
                        magicType: { show: true, type: ["line", "bar"] }
                    }
                },
                calculable: true,
                xAxis: [
                    {
                        type: "category",
                        boundaryGap: false,
                        data: strName
                    }
                ],
                yAxis: [
                    {
                        type: "value"
                    }
                ],
                dataZoom: [{
                    type: "inside",
                    start: 0,
                    end: 100
                }],
                series: [
                    {
                        name: "用户地区分布",
                        type: "line",
                        data: strNum,
                        areaStyle: {
                            normal: {
                                opacity: 0.6
                            }
                        },
                        lineStyle: {
                            normal: {
                                color: "#01babf",
                                width: "3"
                            }
                        },
                        itemStyle: {
                            normal: {
                                color: "#01babf"
                            }
                        },
                        smooth: true
                    }
                ]
            };
            container.setOption(option);
        }
    });



}

/**
 * 初始化图表
 * @param id 容器id
 */
function initEchartPie(id, t) {
    var container = echarts.init(document.getElementById(id));
    var strName = [], strNum = [], pieDatas = [], piedata;

    $.ajax({
        url: "/UserInfo/GetUsersInType",
        data: { type: t },
        async: true,
        success: function (data) {
            if (data != null) {
                strName = data.name;
                strNum = data.num;

                if (t == "sex") {
                    for (var j = 0; j < strName.length; j++) {
                        if (j == 0) {
                            strName[j] = "萌妹子";
                        } else {
                            strName[j] = "帅小伙";
                        }
                    }
                }

                if (t == "role") {
                    for (var j = 0; j < strName.length; j++) {
                        if (j == 0) {
                            strName[j] = "普通用户";
                        } else {
                            strName[j] = "管理员";
                        }
                    }
                }
            }
            for (var i = 0; i < strName.length; i++) {
                piedata = {}
                piedata.name = strName[i];
                piedata.value = strNum[i];
                pieDatas.push(piedata);

            }

            var option = {
                tooltip: {
                    trigger: "item",
                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                },
                legend: {
                    orient: "vertical",
                    x: "left",
                    data: strName
                },
                color: ["#22c3aa", "#d0648a", "#f58db2", "#f2b3c9", "#4ea397", "#7bd9a5"],
                series: [
                    {
                        name: "人数占比",
                        type: "pie",
                        radius: ["30%", "60%"],
                        label: {
                            normal: {
                                formatter: "{b} : {c}人"
                            }
                        },
                        data: pieDatas
                    }
                ]
            };
            container.setOption(option);
        }
    });

}
