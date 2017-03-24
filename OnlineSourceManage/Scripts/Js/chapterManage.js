/**
 * Created by DELL on 2017/2/10.
 */

var setting = {
    view: {
        showLine: true
    },
    check: {
        enable: true
    },
    callback: {
        onCheck: zTreeOnClick
    }
}, zNodes = [
    {
        name: "前端开发",
        id: 1,
        open: true,
        children: [
            {name: "课程1", id: 1},
            {name: "课程2"},
            {name: "课程3"},
            {name: "课程4"},
            {name: "课程5"}
        ]
    },
    {
        name: "后端开发",
        open: true,
        children: [
            {name: "课程1"},
            {name: "课程2"},
            {name: "课程3"},
            {name: "课程4"},
            {name: "课程5"}
        ]
    },
    {
        name: "数据库开发",
        open: true,
        children: [
            {name: "课程1"},
            {name: "课程2"},
            {name: "课程3"},
            {name: "课程4"},
            {name: "课程5"}
        ]
    },
    {
        name: "移动app开发",
        open: true,
        children: [
            {name: "课程1"},
            {name: "课程2"},
            {name: "课程3"},
            {name: "课程4"},
            {name: "课程5"}
        ]
    }
]
function zTreeOnClick(event, treeId, treeNode) {
    if (treeNode.checked) {
        console.log(treeId + "==>" + treeNode)
        parent.layer.alert(treeNode.id + ", " + treeNode.name);
    }
};
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
    pagination: true,
    fit: false
}

$(function () {

    //初始化树
    $.fn.zTree.init($("#tree"), setting, zNodes);
    initEchart("echart")
    //初始化数据列表容器高度
    initDataGridHeight("#dataList", 356)
    //初始化数据列表
    initDataGrid("#dataList")
    //按钮切换
    SwicthBtnEffect(".btn-group", "w-success", 'bg-white')
    $("#outport").click(function () {
        $("#dataList").datagrid('loadData', (function () {
            var arr = []
            for (var i = 1; i < 20; i++) {
                var item = {
                    "devName": "水表1-" + i,
                    "t1": (Math.random() * 50).toFixed(0),
                    "t2": (Math.random() * 50).toFixed(0),
                    "t3": (Math.random() * 50).toFixed(0),
                    "t4": (Math.random() * 50).toFixed(0),
                    "t5": (Math.random() * 50).toFixed(0)

                }
                arr.push(item)
            }
            return arr
        })())
    })
})


/**
 * 初始化图表
 * @param id 容器id
 */
function initEchart(id) {
    var container = echarts.init(document.getElementById(id));
    var option = {
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['当日流量'],
            top: 8
        },
        grid: {
            top: 40
        },
        toolbox: {
            show: true,
            right: 40,
            feature: {
                magicType: {show: true, type: ['line', 'bar']}
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                boundaryGap: false,
                data: (function () {
                    var arr = []
                    for (var i = 0; i < 14; i++) {
                        arr.push(subMap[i % 7])
                    }
                    return arr
                })()
            }
        ],
        yAxis: [
            {
                type: 'value'
            }
        ],
        dataZoom: [{
            type: 'inside',
            start: 0,
            end: 100
        }],
        series: [
            {
                name: "当日流量",
                type: 'line',
                data: (function () {
                    var arr = []
                    for (var i = 0; i < 14; i++) {
                        arr.push((Math.random() * 20).toFixed(2))
                    }
                    return arr
                })(),
                areaStyle: {
                    normal: {
                        opacity: 0.6
                    }
                },
                lineStyle: {
                    normal: {
                        color: "#01babf",
                        width: '3'
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

var subMap = ["Jquery实战", "HTML5精髓", "Nodejs", "VUE", "CSS3动画效果", "SQL-server", "mongodb"];
/**
 * 初始化数据列表
 * @param id 容器id
 */
function initDataGrid(id) {
    /*初始化datagrid*/
    var optionSet = {
        data: (function () {
            var arr = []
            for (var i = 1; i < 20; i++) {
                var item = {
                    "id": "EDULINE201700" + i,
                    "subname": subMap[Math.floor((Math.random() * 50)) % 6],
                    "name": "第" + i + "章节",
                    "desc": "",
                    "starttime": new Date().getSeconds(),
                    "endtime": new Date().getSeconds()

                }
                arr.push(item)
            }
            return arr
        })(),
        columns: [[
            {field: 'id', title: '编号', width: 100},
            {field: 'subname', title: '课程名称', width: 100},
            {field: 'name', title: '章节名称', width: 100},
            {field: 'desc', title: '章节描述', width: 100},
            {field: 'starttime', title: '上传时间', width: 100},
            {field: 'endtime', title: '更新时间', width: 100},
            {
                field: 'oporate', title: '更多操作', width: 100, formatter: function (index, row) {
                return '<a href="javascript:;" class="delUser text-danger" data-name="' + row.name + '" data-id="' + row.id + '">删除</a> /' + ' <a href="javascript:;" class="editUser" data-name="' + row.name + '" data-id="' + row.devName + '">编辑</a>'
            }
            }

        ]]
    }
    var option = $.extend({}, gridConfig, optionSet)
    $(id).datagrid(option)

}

/**
 * 初始化容器高度
 * @param id
 * @param titleH
 */
function initDataGridHeight(id, titleH) {
    var winH = $(window).outerHeight()
    $(id).height(winH - titleH)
}

/**
 * 按钮active效果切换
 * @param parentId 按钮组容器选择器
 * @param className 切换后的按钮效果的类名
 */
function SwicthBtnEffect(parentId, className1, className2) {
    var btnLen = $(parentId).find('.btn').length
    if (btnLen > 0) {
        $(parentId).on("click", '.btn', function () {
            $(this).addClass(className1).removeClass(className2).siblings().removeClass(className1).addClass(className2)
        });
    }
}