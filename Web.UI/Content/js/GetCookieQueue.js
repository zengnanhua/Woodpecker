//最近浏览
function GetLook() {
    var proList = CookieQueue.Gets();
    var html = "";
    for (var i = 0; i < proList.length; i++) {
        var img = "";
        if (proList[i].FImgPath == null || proList[i].FImgPath == undefined) {
            continue;
        } else {
            img = "<img src=" + proList[i].FImgPath + ">";
        }
        html += '<div class="spxq-content-left-content-zjll-content">';
        html += '<a href="/ProductDetail/ProductDetailsIndex?ProductId=' + proList[i].FProId + '">' + img + '</a>';
        html += '<p>' + proList[i].FProName + '</p>';
        html += '<p>¥' + proList[i].FPrice + '</p>';
        html += '</div>';
    }
    $(".Look").append(html);
}