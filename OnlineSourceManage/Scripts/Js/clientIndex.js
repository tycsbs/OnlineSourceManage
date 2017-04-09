/**
 * Created by DELL on 2017/4/6.
 */
$(function () {
    CheckLogin();
    //nav 加载
    LoadNavData();
    //加载默认卡片内容
    LoadCartData("/ClientIndex/GetDefaultCart", {});
    //获取不同类型的课程
    $("#nav-container").on('click', 'li', function () {
        var keys = $(this).children('a').data("key");
        LoadCartData("/ClientIndex/GetCartByTypes", { types: keys });
    });

    //登录操作

    $("#login").click(function () {
        var content = '<div style="padding:10px;"><span>用户名：</span><input type="text" id="uName" class="form-control input-inline">'
            + '<span>密码：</span><input type="text" id="pwd" class="form-control input-inline"><a class="btn w-success btn-block" style="margin-top:10px" id="Logins">登录</a></div>';

        LoginAlert("用户登录", content, 370, 220);

        $("#Logins").on('click', function () {
            var name = $("#uName").val(), pwd = $("#pwd").val();
            $.ajax({
                url: "/ClientIndex/UserLogin",
                data: { name: name, pwd: pwd },
                success: function (data) {
                    var count = data.total;
                    if (count > 0) {
                        layer.closeAll();
                        $("#login-box").empty().append('<a id="user"><i class="fa fa-user"></i> ' + name + '</a> | <a id="user"> <i class="fa fa-sign-out"></i>退出</a>');
                    } else {
                        layer.msg("用户名或密码错误！", { icon: 3 });
                    }
                }
            });
        });
    });
    //用户注册

    $("#register").click(function () {
        FrameAlert("新用户注册", "/ClientIndex/RegisterPage", 370, 380);
    });

});



//加载导航条数据
function LoadNavData() {
    var iconMap = ["fa-desktop", "fa-codepen", "fa-picture-o", "fa-database", "fa-tablet", "fa-cloud-upload", "fa-cogs"];
    $.ajax({
        url: "/ClientIndex/GetCourseToNav",
        success: function (d) {
            var intlist = d.numlist, strlist = d.types, len = strlist.length;
            $("#nav-container ul").empty();
            for (var i = 0; i < len; i++) {
                var s = '<li> <span class="icon"><i class="fa ' + iconMap[i] + '"></i></span>' +
                    '<a href="javascript:;" data-key="' + strlist[i] + '" ><p class="nav-title">' + strlist[i] +
                    '</p><i class="course-num">' + intlist[i] + ' 门课程</i></a></li>';
                $("#nav-container ul").append(s);
            }

        }
    });

}
/**
 * 加载卡片数据
 * @param {} url 
 * @param {} parm 
 * @returns {} none 
 */
function LoadCartData(url, parm) {
    $.ajax({
        url: url,
        data: parm,
        success: function (d) {
            var levelMap = ["初级", "中级", "高级"];
            var count = d.total, data = d.rows;
            var html = "";
            $.each(data, function (i, val) {
                html += '<div class="col-md-3"><div class="course-wrapper"><a target="_blank" class="course-card" href="/ClientCourse/ClientChapters?cid=' + val.cId + '&cname=' + val.cName + '&types=' + val.types + '&level=' + val.levelNum + '&time=' + val.startTime + '">'
                   + '<div class="course-card-top"><i class="fa fa-play-circle"></i><span>'
                   + val.types + '</span></div><div class="course-card-content"><h3 class="course-card-name">'
                   + val.cName + '</h3><p>' + val.mark + '</p><div class="clearfix course-card-bottom"><div class="course-card-info">'
                   + levelMap[val.levelNum] + ' <span>·</span> ' + val.startTime.substr(0, val.startTime.length - 7) + '</div></div></div></a></div></div>';
            });
            $("#container").empty().append(html);

            //加载卡片界面的渐变背景样式
            LoadCardCss();
        }
    });
}


/**
 * 动态加载card 头部颜色样式
 * @constructor none
 */
function LoadCardCss() {
    var classMap = ["purple", "orange", "green", "blue"];
    $(".course-card-top").each(function (i, v) {
        $(v).addClass(classMap[i % classMap.length]);
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
        shade: 0.01,
        area: [w + "px", h + "px"],
        zIndex: layer.zIndex
    });
}

function LoginAlert(title, content, w, h) {
    layer.open({
        title: title,
        type: 1,
        skin: "layui-layer-molv",
        content: content,
        btns: false,
        shade: 0.01,
        area: [w + "px", h + "px"],
        zIndex: layer.zIndex
    });
}

function CheckLogin() {

    $.ajax({
        url: "/ClientCourse/CheckLogin",
        success: function (d) {
            var name = d.name;
            if (name.length > 0) {
                $("#login-box").empty().append('<a id="user"><i class="fa fa-user"></i> ' + name + '</a> | <a id="user"> <i class="fa fa-sign-out"></i>退出</a>');
            }
        }
    });
}