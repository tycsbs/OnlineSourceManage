$(function () {
    //监测是否登录
    CheckLogin();

    //默认隐藏文件区域的video视图
    //$("#video").css("display", "none");
    //根据cid查询课程章节
    LoadChapterNav();

    //根据章节Id查找相应文件

    //点击章节显示对应的文件列表
    $("#listwrapper").on('click', 'li', function () {
        $("#file-wrapper").empty();
        $(this).addClass('active').siblings().removeClass('active');
        var chapterId = $(this).data("id");
        GetChapterFile("/ClientCourse/GetFileByChapter", chapterId);
    });

    // } else { window.location.herf = '/ClientIndex/Index' }
});

function CheckLogin() {
    $.ajax({
        url: "/ClientCourse/CheckLogin",
        success: function (d) {
            var name = d.name;
            if (name != "-1") {
                $("#login-box").empty().append('<a id="user" data-u="' + name + '"><i class="fa fa-user"></i> ' + name + '</a> | <a id="logout"  onclick="LogOut()"> <i class="fa fa-sign-out"></i>退出</a>');
            } else {
                layer.msg("请先登录", { shade: [0.8, '#fff'] });
                setTimeout(function () {
                    window.location.href = '/ClientIndex/Index';
                }, 1500);
            }



        }
    });
}

function LoadChapterNav() {
    var parmObj = toQueryParams(window.location.href);

    var cid = parmObj.cid;
    //加载课程名称
    LoadHeadInfo(parmObj);
    //加载章节名称
    LoadChapters("/ClientCourse/GetChaptersById", { cId: cid });
}
//加载头部区域的内容
function LoadHeadInfo(obj) {
    var container = $("#chaptersList"), headNav = $("#headNav"), headNavWrapper = $("#headNavWrapper"), header = "", lis = "";
    container.append('<p class="modal-title">' + obj.cname + '课程章节列表</p>');
    $.each(obj, function (i, v) {
        if (i == "level") {
            var levelMap = ["初级", "中级", "高级"];
            lis += ' <li><span>难度级别<p>' + levelMap[v] + '</p></span></li>';
        }
        if (i == "types") {
            lis += ' <li><span>课程方向<p>' + v + '</p></span></li>';
        }
        if (i == "time") {
            lis += '<li><span>上传时间<p>' + v + '</p></span></li>';
        }
        if (i == "cname") {
            header += '<h2>' + v + '</h2>';
        }

    });

    headNavWrapper.append(lis);
    headNav.empty().append(header).append(headNavWrapper);

}

function GetChapterFile(url, chapterId) {
    //清空右侧显示区域内容，video默认不显示

    $.ajax({
        url: url,
        data: { chId: chapterId },
        success: function (d) {
            var count = d.total, data = d.rows, fileHtml = "" ,imgHtml = "";
            if (count == 0) {
                //$("#video").css("display", "none");
                $("#file-wrapper").empty().append('<div class="alert alert-danger text-center" style="font-size:12px;margin-top:10px;">数据暂无！</div>');
            } else {
                $.each(data, function (i, v) {
                    if (v.srcType == "PNG" || v.srcType == "JPG" || v.srcType == "GIF") {
                        imgHtml += '<a href="/' + v.srcUrl + '"><img src="/' + v.srcUrl + '"></a>';
                    }
                    else {
                        fileHtml += '<li><a href="/' + v.srcUrl + '"><i class="fa fa-file"></i>' + v.fileDesc + '</a></li>';
                    }
                });
                $("#img-wrapper").empty().append(imgHtml);
                $("#file-wrapper").empty().append(fileHtml);
            }
        }
    });
}

//加载课程的所有章节名称
function LoadChapters(url, parm) {
    $.ajax({
        url: url,
        data: parm,
        success: function (d) {
            var data = d.rows, count = d.total;
            var container = $("#chaptersList"), listwrapper = $("#listwrapper"), chapter = "";
            if (count == 0) {
                //如果暂无章节，则显示提示信息！
                listwrapper.append('<div class="alert alert-danger text-center" style="font-size:12px;margin-top:10px;">数据暂无！</div>');
                container.append(listwrapper);
                $("#file-wrapper").empty();

            } else {
                $.each(data, function (i, val) {
                    chapter += '<li data-id="' + val.chId + '"><i class="fa fa-arrow-circle-right"></i> ' + val.chName + '</li>';
                });
                listwrapper.empty().append(chapter);
                container.append(listwrapper);
                listwrapper.children().eq(0).addClass('active');

                //默认加载第一章节的相关文件
                var id = $("#listwrapper").children('li').eq(0).data("id");
                GetChapterFile("/ClientCourse/GetFileByChapter", id);
            }


        }
    });
}


function LogOut() {
    $.ajax({
        url: "/ClientCourse/LogOut",
        success: function (d) {
            if (d == "ok") {
                layer.alert("用户已经登出！", function () {
                    window.location.href = '/ClientIndex/Index';
                });
            }
        }
    });
}
//操作url参数
function toQueryParams(url) {
    var search = url.replace(/^\s+/, '').replace(/\s+$/, '').match(/([^?#]*)(#.*)?$/);
    if (!search) {
        return {};
    }
    var searchStr = search[1];
    var searchHash = searchStr.split("&");

    var ret = {};
    searchHash.forEach(function (pair) {
        var temp = "";
        if (temp = (pair.split("=", 1))[0]) {
            var key = decodeURIComponent(temp);
            var value = pair.substring(key.length + 1);
            if (value != undefined) {
                value = decodeURIComponent(value);
            }
            if (key in ret) {
                if (ret[key].constructor != Array) {
                    ret[key] = [ret[key]];
                }
                ret[key].push(value);
            } else {
                ret[key] = value;
            }
        }
    });
    return ret;
}
