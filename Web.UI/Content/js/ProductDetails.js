function minusNum() {
    var Num = $("#Num").text();
    var Price = $("#Price").text();
    var SPrice = $("#SPrice").text();
    $("#Num").text(parseInt(Num) - 1);
    if (Num < 10) {
        $("#AllPrice").text(parseInt($("#Num").text()) * parseInt(Price));
    } else {
        $("#AllPrice").text(parseInt($("#Num").text()) * parseInt(SPrice));
    }
}

function addNum() {
    var Num = $("#Num").text();
    var Price = $("#Price").text();
    var SPrice = $("#SPrice").text();
    $("#Num").text(parseInt(Num) + 1);
    if (Num < 10) {
        $("#AllPrice").text(parseInt($("#Num").text()) * parseInt(Price));
    } else {
        $("#AllPrice").text(parseInt($("#Num").text()) * parseInt(SPrice));
    }
}

function canCollection() {
    layer.msg("已经收藏过了", { icon: 7, time: 1000 });
}

