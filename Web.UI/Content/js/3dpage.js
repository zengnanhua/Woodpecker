$(document).ready(function () {
    $("#example1-1,#example1-2").imgbox({'alignment': 'center'});
    $("#example1-3,#example1-4").imgbox({'alignment': 'center'});
    $("#example1-5,#example1-6").imgbox({'alignment': 'center'});
    $("#example1-7,#example1-8").imgbox({'alignment': 'center'});
    $("#example1-9,#example1-10").imgbox({'alignment': 'center'});
    $("#example1-11,#example1-12").imgbox({'alignment': 'center'});
    $("#example1-13,#example1-14").imgbox({'alignment': 'center'});
    $("#example1-15,#example1-16").imgbox({'alignment': 'center'});
    $("#example1-17,#example1-18").imgbox({'alignment': 'center'});
    $("#example1-19,#example1-20").imgbox({'alignment': 'center'});
    $("#example1-21,#example1-22").imgbox({'alignment': 'center'});
    $("#example1-23,#example1-24").imgbox({'alignment': 'center'});
    $("#example1-25,#example1-26").imgbox({'alignment': 'center'});
    $("#example1-27,#example1-28").imgbox({'alignment': 'center'});
    
    $("#l1").mouseover(function () { 
        $(this).addClass("circle"); 
    }).mouseout(function () {
        $(this).removeClass("circle") 
    });
    
    $("#Li1").mouseover(function () { 
        $(this).addClass("circle"); 
    }).mouseout(function () { 
        $(this).removeClass("circle") 
    });

    $("#222").mouseover(function () { 
        $(this).addClass("circle"); 
    }).mouseout(function () { 
        $(this).removeClass("circle") 
    });  

    $("h2").append('<em></em>')

    $(".thumbs a").click(function () {
        var largePath = $(this).attr("href");
        var largeAlt = $(this).attr("title");
        $("#largeImg").attr({ src: largePath, alt: largeAlt });
        $("h2 em").html(" (" + largeAlt + ")"); return false;
    });
    
    //hide message_body after the first one
    $(".message_list .message_body:last-child").show();
    //toggle message_body
    $(".message_head").click(function () {
        $(this).next(".message_body").slideToggle(500)
        return false;
    });

    //collapse all messages
    $(".collpase_all_message").click(function () {
        $(".message_body").slideUp(500)
        return false;
    });

    //show all messages
    $(".show_all_message").click(function () {
        $(this).hide()
        $(".show_recent_only").show()
        $(".message_list li:gt(4)").slideDown()
        return false;
    });

    //show recent messages only
    $(".show_recent_only").click(function () {
        $(this).hide()
        $(".show_all_message").show()
        $(".message_list li").slideUp()
        return false;
    });

    $("#imag img").click(function () {
        if ($(this).attr("name") == "aaa") {
            $("#imag p img[name='aaa']").removeClass();
            $(this).toggleClass("searchfont_on")
        }
        var spanNodes = $("#lar  img[name='ka']");
        var Nodes = $("#imag p img[name='aaa']");
        for (var i = 0; i < spanNodes.length; i++) {
            var spanNode = spanNodes[i];
            if ($(spanNode).hasClass("searchfont_onz")){//判断是否选中
                var key = $(spanNode).attr("key");
                if (key == "1") {
                    for (var i = 0; i < Nodes.length; i++) {
                        var spNode = Nodes[i];
                        if ($(spNode).hasClass("searchfont_on")){//判断是否选中
                            var key = $(spNode).attr("key");
                            if (key == "1") {
                                $("#largeImg").removeAttr("src");
                                $("#largeImg").attr("src", "../image/1.png");
                            }
                        }
                    }
                } if (key == "2") {
                    alert("aaa");
                }
                if (key == "3") {
                    alert("bbb");
                }
                if (key == "4") {
                    alert("ccc");
                }
            }
        }
    });
    $("#ima img").click(function () {
        var spanNodes = $("#imag p img[name='aaa']");
        for (var i = 0; i < spanNodes.length; i++) {
            var spanNode = spanNodes[i];
            if ($(spanNode).hasClass("searchfont_on"))//判断是否选中
            {
                var key = $(spanNode).attr("key");
                if (key == "1") {
                    $("#largeImg").removeAttr("src");
                    $("#largeImg").attr("src", "../image/3x.jpg");
                }
            }
        }
        if ($(this).attr("name") == "zzz") {
            $("#ima img[name='zzz']").removeClass();
            $(this).toggleClass("searchfont_on")
        }
    });
    
    $("#lar img").click(function () {
        if($(this).attr("key")=="5"){
            document.location.href ="1.html";
        }
        $(".sds").css('background-image', "url(" + $(this).attr("src") + ")");

    });

    $("#Button1").click(function () {
        if (document.cookie.length > 0) {
            var strCookie = document.cookie;
            var arrCookie = strCookie.split("; ");
            var userId;
            for (var i = 0; i < arrCookie.length; i++) {

                var arr = arrCookie[i].split("=");

                document.location.href = "diamonds1.aspx?image=" + $("#largeImg").attr("src") + "&username=" + arr[2];


            }
        }
        document.location.href = "diamonds1.aspx?image=" + $("#largeImg").attr("src") + "&username=" + 0;
    });

    
        $.fn.Drag = function (options) {
        
            var defaults = {
                limit: window,
                drop: false,
                handle: false,
                finish: function () { }
            };
            var options = $.extend(defaults, options);
            this.X = 0;
            this.Y = 0;
            this.dx = 0;
            this.dy = 0;
            var This = this;
            var ThisO = $(this);
            var thatO;
            if (options.drop) {
                var ThatO = $(options.drop);
                ThisO.find('div').css({ cursor: 'move' });
                var tempBox = $('<div id="tempBox" class="grid"></div>');
            } else {
                options.handle ? ThisO.find(options.handle).css({ cursor: 'move', '-moz-user-select': 'none' }) : ThisO.css({ cursor: 'move', '-moz-user-select': 'none' });
            }
            //拖动开始
            this.dragStart = function (e) {
            
                var cX = e.pageX;
                var cY = e.pageY;
                if (options.drop) {
                    ThisO = $(this);
                    if (ThisO.find('div').length != 1) { return }
                    This.X = ThisO.find('div').offset().left;
                    This.Y = ThisO.find('div').offset().top;
                    tempBox.html(ThisO.html());
                    ThisO.html('');
                    $('body').append(tempBox);
                    tempBox.css({ left: This.X, top: This.Y });
                } else {
                    This.X = ThisO.offset().left;
                    This.Y = ThisO.offset().top;
                    ThisO.css({ margin: 0 })
                }
                This.dx = cX - This.X;
                This.dy = cY - This.Y;
                if (!options.drop) { ThisO.css({ position: 'absolute', left: This.X, top: This.Y }) }
                $(document).mousemove(This.dragMove);
                $(document).mouseup(This.dragStop);
                if ($.browser.msie) { ThisO[0].setCapture(); }
            }

            this.dragMove = function (e) {
               
                var cX = e.pageX;
                var cY = e.pageY;
             
                    if (options.drop) {
                        tempBox.css({ left: cX - This.dx, top: cY - This.dy })
                    } else {
                        ThisO.css({ left: cX - This.dx, top: cY - This.dy });
                    }
                
            }

            this.dragStop = function (e) {
                if (options.drop) {

                    var flag = false;
                    var cX = e.pageX;
                    var cY = e.pageY;
                    var oLf = ThisO.offset().left;
                    var oRt = oLf + ThisO.width();
                    var oTp = ThisO.offset().top;
                    var oBt = oTp + ThisO.height();
                    if (!(cX > oLf && cX < oRt && cY > oTp && cY < oBt)) {
                        for (var i = 0; i < ThatO.length; i++) {
                            var XL = $(ThatO[i]).offset().left;
                            var XR = XL + $(ThatO[i]).width();
                            var YL = $(ThatO[i]).offset().top;
                            var YR = YL + $(ThatO[i]).height();
                            if (XL < cX && cX < XR && YL < cY && cY < YR) {
                                var newElm = $(ThatO[i]).html();
                                $(ThatO[i]).html(tempBox.html());
                                ThisO.html(newElm);
                                thatO = $(ThatO[i]);
                                sa(thatO);
                                tempBox.remove();
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (!flag) {
                        tempBox.css({ left: This.X, top: This.Y });
                        ThisO.html(tempBox.html());
                        tempBox.remove();
                    }
                }
                $(document).unbind('mousemove');
                $(document).unbind('mouseup');
                options.finish(e, ThisO, thatO);
                if ($.browser.msie) { ThisO[0].releaseCapture(); }
            }

            options.handle ? ThisO.find(options.handle).mousedown(This.dragStart) : ThisO.mousedown(This.dragStart);
        }

        $('.drag li').Drag({ drop: '.drop li', finish: change });

        $('.drop li').Drag({ drop: '.drop li, .drag li', finish: change });
        $('#test').Drag({ handle: 'h2', finish: change });





        var change = function (e, oldElm, newElm) {

        }



        $.fn.Drag2 = function (options) {
            var defaults = {
                limit: window,
                drop: false,
                handle: false,
                finish: function () { }
            };
            var options = $.extend(defaults, options);
            this.X = 0;
            this.Y = 0;
            this.dx = 0;
            this.dy = 0;
            var This = this;
            var ThisO = $(this);
            var thatO;
            if (options.drop) {
                var ThatO = $(options.drop);
                ThisO.find('div').css({ cursor: 'move' });
                var tempBox = $('<div id="tempBox" class="grid2"></div>');
            } else {
                options.handle ? ThisO.find(options.handle).css({ cursor: 'move', '-moz-user-select': 'none' }) : ThisO.css({ cursor: 'move', '-moz-user-select': 'none' });
            }

            this.dragStart = function (e) {
                var cX = e.pageX;
                var cY = e.pageY;
                if (options.drop) {
                    ThisO = $(this);
                    if (ThisO.find('div').length != 1) { return }
                    This.X = ThisO.find('div').offset().left;
                    This.Y = ThisO.find('div').offset().top;
                    tempBox.html(ThisO.html());
                    ThisO.html('');
                    $('body').append(tempBox);
                    tempBox.css({ left: This.X, top: This.Y });
                } else {
                    This.X = ThisO.offset().left;
                    This.Y = ThisO.offset().top;
                    ThisO.css({ margin: 0 })
                }
                This.dx = cX - This.X;
                This.dy = cY - This.Y;
                if (!options.drop) { ThisO.css({ position: 'absolute', left: This.X, top: This.Y }) }
                $(document).mousemove(This.dragMove);
                $(document).mouseup(This.dragStop);
                if ($.browser.msie) { ThisO[0].setCapture(); } //IE,鼠标移到窗口外面也能释放
            }

            this.dragMove = function (e) {
                var cX = e.pageX;
                var cY = e.pageY;
              
                    if (options.drop) {
                        tempBox.css({ left: cX - This.dx, top: cY - This.dy })
                    } else {
                        ThisO.css({ left: cX - This.dx, top: cY - This.dy });
                    }
               
            }

            this.dragStop = function (e) {
                if (options.drop) {
                    var flag = false;
                    var cX = e.pageX;
                    var cY = e.pageY;
                    var oLf = ThisO.offset().left;
                    var oRt = oLf + ThisO.width();
                    var oTp = ThisO.offset().top;
                    var oBt = oTp + ThisO.height();
                    if (!(cX > oLf && cX < oRt && cY > oTp && cY < oBt)) {
                        for (var i = 0; i < ThatO.length; i++) {
                            var XL = $(ThatO[i]).offset().left;
                            var XR = XL + $(ThatO[i]).width();
                            var YL = $(ThatO[i]).offset().top;
                            var YR = YL + $(ThatO[i]).height();
                            if (XL < cX && cX < XR && YL < cY && cY < YR) {
                                var newElm = $(ThatO[i]).html();
                                $(ThatO[i]).html(tempBox.html());
                                ThisO.html(newElm);
                                thatO = $(ThatO[i]);
                                sb(thatO);
                                tempBox.remove();
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (!flag) {
                        tempBox.css({ left: This.X, top: This.Y });
                        ThisO.html(tempBox.html());
                        tempBox.remove();
                    }
                }
                $(document).unbind('mousemove');
                $(document).unbind('mouseup');
                options.finish(e, ThisO, thatO);
                if ($.browser.msie) { ThisO[0].releaseCapture(); }
            }

            options.handle ? ThisO.find(options.handle).mousedown(This.dragStart) : ThisO.mousedown(This.dragStart);

            //document.body.onselectstart=function(){return false;}
        }
        $('#test').Drag2({ handle: 'h2', finish: change });




        $('.drop2 li').Drag2({ drop: '.drop2 li, .drag2 li', finish: change });
        $('.drag2 li').Drag2({ drop: '.drop2 li', finish: change });












        function sa(ts) {

                if (ts.attr("id") == "Li1") {
                    var ds = ts.find("img").attr("key");

                    ts.find("img").attr("id", "rotImg");




                    var param = {


                        right: document.getElementById("rotRight"),
                        left: document.getElementById("rotLeft"),
                        img: document.getElementById("rotImg"),
                        rot: 0
                    };
                    var fun = {
                        right: function () {
                            param.rot += 1;
                            param.img.className = "rot" + param.rot;
                            if (param.rot === 3) {
                                param.rot = -1;
                            }
                        },
                        left: function () {
                            param.rot -= 1;
                            if (param.rot === -1) {
                                param.rot = 3;
                            }
                            param.img.className = "rot" + param.rot;
                        }
                    };
                    param.right.onclick = function () {
                        fun.right();
                        return false;
                    };
                    param.left.onclick = function () {
                        fun.left();
                        return false;
                    };



                } else if (ts.attr("id") == "l1") {
                    ts.find("img").attr("id", "rotImg3");


                    var param = {

                        right: document.getElementById("rotRight"),
                        left: document.getElementById("rotLeft"),
                        img: document.getElementById("rotImg3"),
                        rot: 0
                    };
                    var fun = {
                        right: function () {
                            param.rot += 1;
                            param.img.className = "rot" + param.rot;
                            if (param.rot === 3) {
                                param.rot = -1;
                            }
                        },
                        left: function () {
                            param.rot -= 1;
                            if (param.rot === -1) {
                                param.rot = 3;
                            }
                            param.img.className = "rot" + param.rot;
                        }
                    };
                    param.right.onclick = function () {
                        fun.right();
                        return false;
                    };
                    param.left.onclick = function () {
                        fun.left();
                        return false;
                    };


                } else {
                    ts.find("img").attr("id", " ");



                }
            }

        




        function sb(ts) {
            ts.find("img").attr("id", "rotImg1");
            var ds = ts.find("img").attr("key");
            if (ds == 1) {
               $("#l1").css("margin-left", "230px");
$("#Li1").css("margin-left", "110px");

            } if (ds == 2) {
                           $("#l1").css("margin-left", "230px");
$("#Li1").css("margin-left", "110px");
            }
            if (ds == 3) {
                          $("#l1").css("margin-left", "230px");
$("#Li1").css("margin-left", "110px");
            }
            if (ds == 4) {
                         $("#l1").css("margin-left", "230px");
$("#Li1").css("margin-left", "110px");
       }
            if (ds == 5) {
                       $("#l1").css("margin-left", "230px");
$("#Li1").css("margin-left", "110px");          }
            if (ds == 6) {
                         $("#l1").css("margin-left", "230px");
$("#Li1").css("margin-left", "110px");   }
            if (ds == 7) {
           $("#l1").css("margin-left", "230px");
$("#Li1").css("margin-left", "110px");
            }
            if (ds == 8) {
                          $("#l1").css("margin-left", "230px");
$("#Li1").css("margin-left", "110px");     }

            if (ds == 9) {
                          $("#l1").css("margin-left", "230px");
$("#Li1").css("margin-left", "110px");       }

            if (ds == 10) {
                          $("#l1").css("margin-left", "230px");
$("#Li1").css("margin-left", "110px");      }


            var param = {

                right: document.getElementById("rotRight"),
                left: document.getElementById("rotLeft"),
                img: document.getElementById("rotImg1"),
                rot: 0
            };
            var fun = {
                right: function () {
                    param.rot += 1;
                    param.img.className = "rot" + param.rot;
                    if (param.rot === 3) {
                        param.rot = -1;
                    }
                },
                left: function () {
                    param.rot -= 1;
                    if (param.rot === -1) {
                        param.rot = 3;
                    }
                    param.img.className = "rot" + param.rot;
                }
            };
            param.right.onclick = function () {
                fun.right();
                return false;
            };
            param.left.onclick = function () {
                fun.left();
                return false;
            };




        }
});

