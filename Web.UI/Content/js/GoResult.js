function minusNum() {
    var Num = $("#Num").text();
    var Price = $("#Price").text();
    $("#Num").text(parseInt(Num) - 1);
    if (Num<=1) {
        layer.msg("购买数量不能小于1件！");
        $("#Num").text(1);
    }
    $("#AllPrice").text(parseInt($("#Num").text()) * parseInt(Price));

}

function addNum() {
    var Num = $("#Num").text();
    var Price = $("#Price").text();
    $("#Num").text(parseInt(Num) + 1);
    $("#AllPrice").text(parseInt($("#Num").text()) * parseInt(Price));
}

function CanCollection() {
    layer.msg("已经收藏过了", { icon: 7, time: 1000 });
}