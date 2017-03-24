/**
 * Created by DELL on 2016/10/27.
 */

/*datagrid配置参数默认值*/
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
    pagePosition: "bottom",
    fit: false
};

var chartConfig = {
    title: {
        text:'',
        subtext:'',
        x:'center'
    },

    tooltip : {
        trigger: 'axis'/*item*/
    },
    grid: {
        x: 40,
        y: 50,
        x2: 40,
        y2: 40

    },
    calculable : true,
    yAxis : [
        {
            type : 'value'
        }
    ]
};


//测试数据--静态属性
var staticAttr = [
    {
        "enname": "areaCentiare",
        "chname": "面积",
        "unit": "平方米",
        "desc":"详情"
    },
    {
        "enname": "name",
        "chname": "名称",
        "unit": "",
        "desc":"详情"
    }
]
//测试数据--实时属性
var RealTimeAttr = [
    {
        "enname": "inPressure",
        "chname": "进口压力",
        "unit": "Mpa"
    }
]

/**
 * iframe弹出层
 * @param title 弹出层显示的名称
 * @param url 显示的iframe页路径
 * @param w 弹出层宽度
 * @param h 弹出层高度
 * @param cb yes回调
 */
function FramesAlert(title, url, w, h, cb) {
    top.layer.open({
        title: title,
        type: 2,
        skin: "layui-layer-molv",
        content: url,
        maxmin: true,
        btn: ['确定', '取消'],
        shade: false,
        area: [w, h],
        //点击确定按钮后执行的回调
        yes: function(index, dom) {
            //执行别的dom操作
            if (cb && typeof(cb) === "function") {
                cb(index,dom)
            }
            top.layer.close(index); //如果设定了yes回调，需进行手工关闭
        },
        zIndex: layer.zIndex,
        success: function(layero) {
            //layer.setTop(layero);
        }
    });
}