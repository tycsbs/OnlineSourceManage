/**
 * Created by DELL on 2017/2/10.
 */


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
};
$(function () {

    initEchart("echart");
    //初始化数据列表容器高度
    initDataGridHeight("#dataList", 356);
    //初始化数据列表
    initDataGrid("#dataList");
});


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
                    var arr = [];
                    for (var i = 0; i < 14; i++) {
                        arr.push(subMap[i % 7]);
                    }
                    return arr;
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
                    var arr = [];
                    for (var i = 0; i < 14; i++) {
                        arr.push((Math.random() * 20).toFixed(2));
                    }
                    return arr;
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
        url:"/Chapter/GetChapter",
        columns: [[
            {field: 'cName', title: '课程名称', width: 100},
            {field: 'chName', title: '章节名称', width: 100},
            {field: 'mark', title: '章节描述', width: 100},
            {field: 'starttime', title: '上传时间', width: 100},
            {
                field: 'manage', title: '更多操作', width: 100, formatter: function (index, row) {
                return '<a href="javascript:;" class="delUser text-danger" data-name="' + row.name + '" data-id="' + row.id + '">删除</a> /' + ' <a href="javascript:;" class="editUser" data-name="' + row.name + '" data-id="' + row.devName + '">编辑</a>';
                }
            }

        ]]
    };
    var option = $.extend({}, gridConfig, optionSet);
    $(id).datagrid(option);
}

/**
 * 初始化容器高度
 * @param id
 * @param titleH
 */
function initDataGridHeight(id, titleH) {
    var winH = $(window).outerHeight();
    $(id).height(winH - titleH);
}

/**
 * 按钮active效果切换
 * @param parentId 按钮组容器选择器
 * @param className 切换后的按钮效果的类名
 */
function SwicthBtnEffect(parentId, className1, className2) {
    var btnLen = $(parentId).find('.btn').length;
    if (btnLen > 0) {
        $(parentId).on("click", '.btn', function () {
            $(this).addClass(className1).removeClass(className2).siblings().removeClass(className1).addClass(className2);
        });
    }
}