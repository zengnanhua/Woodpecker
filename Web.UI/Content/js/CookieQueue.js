/**
 * Created by Administrator on 2016/1/14.
 */
function  CookieQueue()
{
}
CookieQueue.key="queue";
CookieQueue.Set=function(obj)
{
    if(typeof obj!="object")
    {
        alert("不是为对象");return;
    }
    var arrString="[";
    var first=JSON.stringify(obj);
    var queue = CookieQueue.Gets();
    for (var i = 0; i < queue.length; i++) {
        var temp = JSON.stringify(queue[i]);
        if (first == temp)
        {
            return;
        }
    }
    if(queue.length<5) {
        for (var i = 0; i < queue.length; i++) {
            arrString+=JSON.stringify(queue[i])+",";
        }
    }
    else {
        for (var i = 1; i < queue.length; i++) {
            arrString+=JSON.stringify(queue[i])+",";
        }
    }
    arrString += first + "]";   
    $.cookie(CookieQueue.key,arrString,{ expires: 7, path: '/' });
}
CookieQueue.Gets=function()
{

    var queue= $.cookie(CookieQueue.key);
    if(queue==""||queue==null)
    {
        return "";
    }
    return JSON.parse(queue);
}
