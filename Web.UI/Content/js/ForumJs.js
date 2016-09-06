var ForumWindow = {}
ForumWindow.LoginFrom = {
    ///显示登录框
    ShowWindow: function () {
        layer.open({
            title: false,
            type: 2,
            closeBtn: false,
            area: ['546px', '303px'],
            skin: 'layui-layer-demo', //样式类名
            content: ['/qForum/Login', 'no']
        });
    }
}

ForumWindow.TopicWindow = {
    SendTopicWindow: function (classId, userid) {
        if (classId == null || classId == "") {
            alert("请填写板块id");
            return;
        }
        if (userid == null || userid == "") {
            alert("请先登录");
            ForumWindow.LoginFrom.ShowWindow();  //调用登录框
        }
        layer.open({
            moveType: 0,
            title: ["发表新帖", "background:#182852;color:#fff"],
            type: 2,
            closeBtn: 2,
            area: ['628px', '510px'],
            skin: 'layui-layer-demo', //样式类名
            content: ['/qForum/SendTopic?classId=' + classId + "&userid="+userid, 'no']
        });
    }
}
ForumWindow.PostWindow = {
    SendPostWindow: function (topId, userId) {
        if (topId == null || topId == "") {
            alert("请填写主题id");
            return;
        }
        layer.open({
            title: false,
            moveType: 0,
            title: ["帖子回复", "background:#182852;color:#fff"],
            type: 2,
            closeBtn: 2,
            area: ['628px', '530px'],
            skin: 'layui-layer-demo', //样式类名
            content: ['/qForum/SendPost?topId=' + topId + "&userId="+userId, 'no']
        });
    }
}
