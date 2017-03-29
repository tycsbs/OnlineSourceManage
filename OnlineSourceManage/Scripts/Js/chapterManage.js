/**
 * Created by DELL on 2017/2/10.
 */




$(function () {

    chart("line");
    chart("bar");
    //初始化数据列表容器高度
    initDataGridHeight(".dataList", 360);
    //初始化数据列表
    initDataGrid("#dataList");
   
});


/**
 * 删除指定章节
 * @param {} that 
 * @returns {} 
 */
function DeleteChapter(that) {
    var chId = $(that).data("id");
    layer.confirm("确定删除这一章节？",
        function () {
            $.ajax({
                url: "/Chapter/DeleteChapter",
                data: { chId: chId },
                success: function (msg) {
                    if (msg == "ok") {
                        layer.msg("删除成功！", { icon: 1 });
                    } else {
                        layer.msg("删除失败！", { icon: 3 });
                    }
                    $("#dataList").datagrid("reload");
                }
            });
        });

}

/**
 * 初始化Echart 获取数据
 * @param {} id 
 * @returns {} 
 */
function chart(id) {
    $.ajax({
        url: "/Chapter/GetChapterForChart",
        success: function (d) {
            var seriesData = d.num;
            var xAxisData = d.name;
            initEchart(id, seriesData, xAxisData);
        }
    });
}

/**
 * 初始化图表
 * @param id 容器id
 */
function initEchart(id, seriesData, xAxisData) {
    var container = echarts.init(document.getElementById(id));
    var option = {
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['课程章节统计'],
            top: 8
        },
        grid: {
            top: 40
        },
        toolbox: {
            show: true,
            right: 40,
            feature: {
                magicType: { show: true, type: ['line', 'bar'] }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                boundaryGap: true,
                data: xAxisData
            }
        ],
        yAxis: [
            {
                type: 'value'
            }
        ],
        label: {
            normal: {
                show: true,
                formatter: '{b} 共 {c} 节',
                position: 'top'
            }
        },
        series: [
            {
                name: "课程章节统计",
                type: 'bar',
                data: seriesData,
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

/**
 * 初始化数据列表
 * @param id 容器id
 */
function initDataGrid(id) {
    /*初始化datagrid*/
    var optionSet = {
        striped: true,
        fitColumns: true,
        rownumbers: true,
        border: true,
        editable: false,
        singleSelect: true,
        loadMsg: '玩命加载中...',
        pagination: true,
        pageSize: 5,
        pageList: [5, 10, 15, 25],
        url: "/Chapter/GetChapter",
        fit: true,
        columns: [[
            { field: 'cName', title: '课程名称', width: 100 },
            { field: 'chName', title: '章节名称', width: 100 },
            { field: 'mark', title: '章节描述', width: 100 },
            { field: 'types', title: '课程方向', width: 100 },
            { field: 'starttime', title: '创建时间', width: 100 },
            {
                field: 'manage', title: '更多操作', width: 100, formatter: function (index, row) {
                    return '<a href="javascript:;" class="delUser text-danger" data-id="' + row.chId + '" data-cId="' + row.cid + '" onclick="DeleteChapter(this)">删除</a> /'
                        + ' <a href="javascript:;" class="editUser" data-name="' + row.name + '" data-chIs="' + row.chId + '">编辑</a>';
                }
            }

        ]]
    };
    $(id).datagrid(optionSet);
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