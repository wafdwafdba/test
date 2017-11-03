var _menus = "";

function initMenuData()
{
 $.ajax({
   url:'/Login/initMenuData',
   type:"Post",
   dataType:"json",
   success:function(data)
   {
     if (data != "") {
        _menus = data;
         InitLeftMenu();
      }
   }
 });
}

$(function () {
    initMenuData();
    tabClose();
    tabCloseEven();
    clockon();

});
//初始化左侧
function InitLeftMenu() {
    $("#nav").accordion({ animate: true });
    $.each(_menus, function (i, n) {
        var menulist = ''; 
        menulist += '<ul>';
        $.each(n.menus, function (j, o) {
            if (o != undefined)
                menulist += '<li><div class="daccordion"><a onclick="clickaction(this)" ref="' + o.menuid + '" href="javascript:void(0)" rel="'
                 + o.url + '" ><span class="' + o.icon + '" >&nbsp;</span><span class="nav">'
                  + o.menuname + '</span></a></div></li> ';

            $.each(o.children, function (e, h) {
                menulist +='<li><div class="daccordion"><ul>'
                   if (h != undefined)
                     {
                        menulist += '<li style="padding-left:50px"><a onclick="clickaction(this)" ref="' + h.menuid + '" href="javascript:void(0)" rel="'
                         + h.url + '" ><span class="' + h.icon + '" >&nbsp;</span><span class="nav">'
                          + h.menuname + '</span></a></li>'; }
                         menulist +='</div></li></ul>'  
                    });
        })
        menulist += '</ul>';
        $('#nav').accordion('add', {
            title: n.menuname,
            content: menulist,
            iconCls: n.icon,
            animate: false
        });
    });

//    .hover(function () {
//        $(this).parent().addClass("hover");
//    }, function () {
//        $(this).parent().removeClass("hover");
//    });

    //选中第一个
    var panels = $('#nav').accordion('panels');
    var t = panels[0].panel('options').title;
    $('#nav').accordion('select', t);
}


    function frameReturnByReload(tabsName) {
        if ($('#tabs').tabs('exists', tabsName)) {
            try {
                var iframe_ = $('#tabs').tabs('getTab', tabsName).find('iframe')[0];
                if (iframe_ != undefined) {
                    iframe_.contentWindow.frameReturnByReload();
                }
            } catch (e) {
            }
        }
    }
    //点击菜单的a标签，在右边显示列表
    function clickaction(obj){
            var tabTitle = $(obj).children('.nav').text();
            var url = $(obj).attr("rel");
            var menuid = $(obj).attr("ref");
            var s = $(obj).children('span');
            var icon = s.attr('class');
            addTab(tabTitle, url, icon);
            $('.easyui-accordion li div').removeClass("selected");
            $(obj).parent().addClass("selected");
    }


//消息提示
function frameSingal() {
 
}
function addTab(subtitle, url, icon) {
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            icon: icon
        });
    } else {
        $('#tabs').tabs('select', subtitle);
        $('#mm-tabupdate').click();
    }

    //刷新标签页
		var tab = $('#tabs').tabs('getSelected');  
		$('#tabs').tabs('update', {
			tab: tab,
			options: {
				title: subtitle,
				content: createFrame(url),
			},});
    tabClose();
}
function createFrame(url) {
    var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:99.5%;"></iframe>';
    return s;
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    })
    /*为选项卡绑定右键*/
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#rcmenu').menu('show', {
            left: e.pageX,
            top: e.pageY
        });
        var subtitle = $(this).children(".tabs-closable").text();
        $('#rcmenu').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
}
//绑定右键菜单事件
function tabCloseEven() {
   $(".tabs-inner").bind('contextmenu', function (e) {
                 e.preventDefault();
                 $('#rcmenu').menu('show', {
                     left: e.pageX,
                     top: e.pageY
                 });
             });
    //关闭当前标签页
    $("#closecur").bind("click", function () {
        var tab = $('#tabs').tabs('getSelected');
        var index = $('#tabs').tabs('getTabIndex', tab);

        var tab = $('#tabs').tabs("getTab", index);
        if (tab.panel('options').title == "欢迎页") return;
        $('#tabs').tabs('close', index);
    });
    //关闭所有标签页
    $("#closeall").bind("click", function () {
        var tablist = $('#tabs').tabs('tabs');
        for (var i = tablist.length - 1; i >= 0; i--) {

            var tab = $('#tabs').tabs("getTab", i);
            if (tab.panel('options').title != "欢迎页")
                $('#tabs').tabs('close', i);
        }
    });
    //关闭非当前标签页（先关闭右侧，再关闭左侧）
    $("#closeother").bind("click", function () {
        var tablist = $('#tabs').tabs('tabs');
        var tab = $('#tabs').tabs('getSelected');
        var index = $('#tabs').tabs('getTabIndex', tab);
        for (var i = tablist.length - 1; i > index; i--) {
            var tab = $('#tabs').tabs("getTab", i);
            if (tab.panel('options').title != "欢迎页")
                $('#tabs').tabs('close', i);
        }
        var num = index - 1;
        for (var i = num; i >= 0; i--) {
            var tab = $('#tabs').tabs("getTab", i);
            if (tab.panel('options').title != "欢迎页")
                $('#tabs').tabs('close', i);
        }
    });
    //关闭当前标签页右侧标签页
    $("#closeright").bind("click", function () {
        var tablist = $('#tabs').tabs('tabs');
        var tab = $('#tabs').tabs('getSelected');
        var index = $('#tabs').tabs('getTabIndex', tab);
        for (var i = tablist.length - 1; i > index; i--) {
            var tab = $('#tabs').tabs("getTab", i);
            if (tab.panel('options').title != "欢迎页")
                $('#tabs').tabs('close', i);
        }
    });
    //关闭当前标签页左侧标签页
    $("#closeleft").bind("click", function () {
        var tab = $('#tabs').tabs('getSelected');
        var index = $('#tabs').tabs('getTabIndex', tab);
        var num = index - 1;
        for (var i = num; i >= 0; i--) {
            var tab = $('#tabs').tabs("getTab", i);
            if (tab.panel('options').title != "欢迎页")
                $('#tabs').tabs('close', i);
        }
    });
}

function closeTab(closeTab, openTab, isReload,url) {
    $('#tabs').tabs('close', closeTab);
    if(openTab!="")addTab(openTab, url, "");
    if (isReload)
        frameReturnByReload(openTab);
}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
    $.messager.alert(title, msgString, msgType);
}
function clockon() {
    var now = new Date();
    var year = now.getFullYear(); //getFullYear getYear
    var month = now.getMonth();
    var date = now.getDate();
    var day = now.getDay();
    var hour = now.getHours();
    var minu = now.getMinutes();
    var sec = now.getSeconds();
    var week;
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    week = arr_week[day];
    var time = "";
    time = year + "年" + month + "月" + date + "日" + " " + hour + ":" + minu + ":" + sec + " " + week;
    $("#bgclock").html(time);
    var timer = setTimeout("clockon()", 200);
}

//生成唯一的GUID
function GetGuid() {
    var s4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };
    return s4() + s4() + s4() + "-" + s4();
}

       
