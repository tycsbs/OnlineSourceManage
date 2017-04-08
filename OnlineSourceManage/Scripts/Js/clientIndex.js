/**
 * Created by DELL on 2017/4/6.
 */
$(function () {
    //nav 加载
    LoadNavData();
    //加载默认卡片内容
    LoadCartData("/ClientIndex/GetDefaultCart", {});
    //获取不同类型的课程
    $("#nav-container").on('click', 'li', function () {
        var keys = $(this).children('a').data("key");
        LoadCartData("/ClientIndex/GetCartByTypes", { types: keys });
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
                    '<a href="javascript:;" data-key="' + strlist[i] +'" ><p class="nav-title">' + strlist[i] +
                    '</p><i class="course-num">' +intlist[i] +' 门课程</i></a></li>';
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
                html += '<div class="col-md-3"><div class="course-wrapper"><a target="_blank" class="course-card" href="/ClientCourse/ClientChapters?cid='+val.cId+'&cname='+val.cName+'&types='+val.types+'&level='+val.levelNum+'&time='+val.startTime+'">'
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