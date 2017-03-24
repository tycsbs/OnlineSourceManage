/**
 * Created by DELL on 2017/2/9.
 */
$(function () {
    // 按钮active效果切换
    SwicthBtnEffect("#btn-wrapper", "w-success");
    // 初始化tab
    initTab("#btn-wrapper");
    // 初始化iframe区域的宽高度
    initFrameHeight("#content-filed", 135);
});

/**
 * 按钮active效果切换
 * @param parentId 按钮组容器选择器
 * @param className 切换后的按钮效果的类名
 */
function SwicthBtnEffect(parentId, className) {
    var btnLen = $(parentId).find('.btn').length;
    if (btnLen > 0) {
        $(parentId).on("click", '.btn', function () {
            $(this).removeClass('btn-default').addClass(className).siblings().removeClass(className);
        });
    }
}

/**
 *
 * @param id 容器
 * @param titleH 非容器区域高度
 */
function initFrameHeight(id, titleH) {
    var winH = $(window).outerHeight();
    $(id).height(winH - titleH);
}

/**
 * init tab
 * @param container container
 */
function initTab(container) {
    $(container).on('click', '.btn', function () {
        if (!$(this).hasClass('clicked')) {
            var $flag = $(this).data("src");
            $("#Frames").attr("src", $flag);
        }
        $(this).addClass('clicked').siblings().removeClass('clicked');
    });
}