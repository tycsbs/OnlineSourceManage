/**
 * Created by DELL on 2017/4/6.
 */
$(function(){
    //加载卡片界面的渐变背景样式
    LoadCardCss();
});




/**
 * 动态加载card 头部颜色样式
 * @constructor none
 */
function LoadCardCss() {
    var classMap = ["purple","orange","green","blue"];
    $(".course-card-top").each(function(i,v){
        $(v).addClass(classMap[i % classMap.length]);
    });
}