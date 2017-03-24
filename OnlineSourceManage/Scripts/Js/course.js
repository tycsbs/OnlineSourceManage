/**
 * Created by DELL on 2017/2/10.
 */
/**
 * 初始化数据列表
 * @param id 容器id
 */
$(function () {
    initDataGridHeight("#dataList", 366);
    initEchart("linechart", "types");
    initEchartPie("piechart", "levelNum");
    initDataGrid("#dataList");

    //模糊查询
    $("#searchBtn").click(function (e) {
        e.preventDefault();
        var result = $("#searchVal").val();
        if (result != null) {
            $.ajax({
                url: "/Course/GetCourseBySearch",
                data: { keys: result },
                success: function (data) {
                    $("#dataList").datagrid("loadData", data);
                }
            });
        }
    });
    //点击新增按钮，弹出表单
    $("#addCourse").click(function () {
        //显示提交新增课程按钮，隐藏提交编辑课程按钮
        $("#EditCourseBtn").css("display", "none");
        $("#addCourseBtn").css("display", "block");
        AlertCourseForm("新增用户");
    });
    //提交新增课程的表单 添加课程
    $("#addCourseBtn").click(function () {
        var parm = $("#courseForm").serialize();
        ExcuteAjax("/Course/AddCourse", parm);
    });
    $("#EditCourseBtn").click(function () {
        var parm = $("#courseForm").serialize();
        ExcuteAjax("/Course/EditCourse", parm);
    });

    //编辑用户信息

});

//根据不同的接口进行课程的新增、修改操作
function ExcuteAjax(url, parm) {
    $.ajax({
        url: url+"?" + parm,
        success: function (data) {
            if (data == "ok") {
                layer.msg("操作成功！", { icon: 1, time: 800 });
                $("#dataList").datagrid("reload");
                initEchart("linechart", "types");
                initEchartPie("piechart", "levelNum");
            } else {
                layer.msg("操作失败", { icon: 2, time: 800 });
            }
        }
    });
}


//点击编辑按钮，弹出表单
function EditUser(that) {
    $("#addCourseBtn").css("display", "none");
    $("#EditCourseBtn").css("display", "block");
    var id = $(that).data("id");
    AlertCourseForm("编辑用户");
    if (id != null) {
        $.ajax({
            url: "/Course/GetCourseById",
            data: { id: id },
            success: function (data) {
                var course = data.rows;
                $("#cName").val(course.cName);
                $("#types").val(course.types);
                $("#mark").val(course.mark);
                $("#levelNum").val(course.levelNum);
                $("#cId").val(id);
            }
        });
    }
}


function initDataGrid(id) {
    /*初始化datagrid*/
    var optionSet = {
        //url: "/Course/GetAllCourse",
        striped: true,
        fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        loadMsg: "玩命加载中...",
        resizable: true,
        pagination: true,
        pageSize: 5,
        pageList: [5, 10, 15, 25],
        fit: false,
        url: "/Course/GetAllCourse?isdesc=true",
        //data:{isdesc:true},
        columns: [[

            { field: "cName", title: "课程名称", width: 100 },
            { field: "types", title: "课程类型", width: 100 },
            {
                field: "levelNum", title: "难度", width: 100, formatter: function (value) {
                    if (value == 0) return "初级";
                    if (value == 1) return "中级";
                    if (value == 2) return "高级";
                }
            },
            { field: "startTime", title: "上传时间", width: 100 },
            { field: "mark", title: "备注", width: 100 },
            {
                field: "operate", title: "操作", width: 100,
                formatter: function (value, row) {
                    return '<a href="javascript:;" class="delUser text-danger" data-id="' + row.cId + '" onclick="DeleteUser(this)">删除</a> /' + ' <a href="javascript:;" class="editUser" data-id="' + row.cId + '" onclick="EditUser(this)">编辑</a>';
                }
            }
        ]]
    };
    $(id).datagrid(optionSet);

}


function initDataGridHeight(id, titleH) {
    var winH = $(window).outerHeight();
    $(id).height(winH - titleH);
}

/**
 * 初始化图表
 * @param id 容器id
 */
function initEchart(id, t) {
    var container = echarts.init(document.getElementById(id));
    var strName = [], strNum = [];
    $.ajax({
        url: "/Course/GetCourseForChart",
        data: { keys: t },
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
        url: "/Course/GetCourseForChart",
        data: { keys: t },
        success: function (data) {

            strName = data.name;
            strNum = data.num;

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
                        name: "课程占比",
                        type: "pie",
                        radius: ["30%", "60%"],
                        label: {
                            normal: {
                                formatter: "{b} : {c}门"
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


/**
 * 删除指定课程
 * @param {} that 
 * @returns {} 
 */
function DeleteUser(that) {
    layer.confirm("确定删除该课程？",
        { icon: 3, shade: 0.01 },
        function () {
            var id = $(that).data("id");
            $.ajax({
                url: "/Course/DeleteCourse",
                data: { id: id },
                success: function (data) {
                    if (data == "ok") {
                        layer.msg("删除成功！", { icon: 1, time: 800 });
                        $("#dataList").datagrid("reload");
                        initEchart("linechart", "types");
                        initEchartPie("piechart", "levelNum");
                    } else {
                        layer.msg("删除失败", { icon: 2, time: 800 });
                    }
                }
            });
        });

}


function AlertCourseForm(title) {
    layer.open({
        type: 1,
        shade: false,
        title: title,
        area: ["400px", "300px"],
        content: $("#addUser-wrapper")
    });
}
