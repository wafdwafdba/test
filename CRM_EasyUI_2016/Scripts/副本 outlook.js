var _menus = {
    "menus": [
         {
             "menuid": "8", "icon": "icon icon-home", "menuname": "客户管理",
             "menus": [{ "menuname": "客户信息管理", "icon": "icon icon-home", "url": "/CustomerManager/Customer/Index" },
//                      { "menuname": "客户通讯", "icon": "icon-nav", "url": "/CustomerManager/Customer/Index" },
                      { "menuname": "简单客户列表", "icon": "icon-nav", "url": "/CustomerManager/Customer/Index1"}
             ]
         }, {
             "menuid": "28", "icon": "icon icon-user-minus", "menuname": "权限管理",
             "menus": [
                      { "menuname": "部门列表", "icon": "icon-nav", "url": "/CRM_Dept/Index" },
                      { "menuname": "角色列表", "icon": "icon-nav", "url": "/CRM_Role/Index" },
                      { "menuname": "角色权限设置", "icon": "icon-nav", "url": "/CRM_Right/Index" },
                      { "menuname": "用户管理", "icon": "icon-add", "url": "/CRM_User/Index" },
                      { "menuname": "菜单管理", "icon": "icon-nav", "url": "/CRM_Module/Index" }
             ]
         }, {
             "menuid": "39", "icon": "icon icon-heart", "menuname": "基本信息管理",
             "menus": [{ "menuname": "类别管理", "icon": "icon-nav", "url": "/Systems/Type/Index" },
                     { "menuname": "商品列表", "icon": "icon-nav", "url": "/shop/product.aspx" },
                     { "menuname": "商品订单", "icon": "icon-nav", "url": "/shop/orders.aspx" }
             ]
         }, {
             "menuid": "40", "icon": "icon icon-html5", "menuname": "系统日志",
             "menus": [             ]
         }
    ]
};
$(function () {
    InitLeftMenu();
    tabClose();
    tabCloseEven();
    clockon();

});
//初始化左侧
function InitLeftMenu() {
    $("#nav").accordion({ animate: true });
    $.each(_menus.menus, function (i, n) {
        var menulist = '';
        menulist += '<ul>';
        $.each(n.menus, function (j, o) {
            if (o != undefined)
                menulist += '<li><div class="daccordion"><a ref="' + o.menuid + '" href="javascript:void(0)" rel="' + o.url + '" ><span class="' + o.icon + '" >&nbsp;</span><span class="nav">' + o.menuname + '</span></a></div></li> ';
        })
        menulist += '</ul>';
        $('#nav').accordion('add', {
            title: n.menuname,
            content: menulist,
            iconCls: n.icon,
            animate: false
        });
    });
    $('.easyui-accordion li a').click(function () {
        var tabTitle = $(this).children('.nav').text();
        var url = $(this).attr("rel");
        var menuid = $(this).attr("ref");
        var s = $(this).children('span');
        var icon = s.attr('class');
        addTab(tabTitle, url, icon);
        $('.easyui-accordion li div').removeClass("selected");
        $(this).parent().addClass("selected");
    }).hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });
    //选中第一个
    var panels = $('#nav').accordion('panels');
    var t = panels[0].panel('options').title;
    $('#nav').accordion('select', t);
}
//不用的原因是，每次关闭都要查询
//function reloaGrid() {
//    $('#tabs').tabs({
//        border: false,
//        onSelect: function (title, index) {
//            frameReturnByReload(title);

//        }
//    })
//}
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
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });
        var subtitle = $(this).children(".tabs-closable").text();
        $('#mm').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
}
//绑定右键菜单事件
function tabCloseEven() {
    //刷新
    $('#mm-tabupdate').click(function () {
        var currTab = $('#tabs').tabs('getSelected');
        var url = $(currTab.panel('options').content).attr('src');
        $('#tabs').tabs('update', {
            tab: currTab,
            options: {
                content: createFrame(url)
            }
        })
    })
    //关闭当前
    $('#mm-tabclose').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
    })
    //全部关闭
    $('#mm-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            $('#tabs').tabs('close', t);
        });
    });
    //关闭除当前之外的TAB
    $('#mm-tabcloseother').click(function () {
        $('#mm-tabcloseright').click();
        $('#mm-tabcloseleft').click();
    });
    //关闭当前右侧的TAB
    $('#mm-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            //msgShow('系统提示','后边没有啦~~','error');
            alert('后边没有啦~~');
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#mm-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            alert('到头了，前边没有啦~~');
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });

    //退出
    $("#mm-exit").click(function () {
        $('#mm').menu('hide');
    })
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

       
