// JavaScript Document

$(function () {
    $("input").tooltip(); 
	 
    
	 
	
	 
	
    //阻止菜单事件冒泡
    $("#mymenu>li").children('.submenu').click(function (event) {
        event.stopPropagation();
    })
    //meun
    $("#mymenu>li").click(function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass("active").children('.submenu').slideUp("fast", function () {
                setRightHeigh();
                setLeftHeight();
            });
            
        } else {
            $(this).addClass("active").siblings().removeClass("active");
            $(this).children(".submenu").slideDown("fast", function () {
                setRightHeigh();
                setLeftHeight();
            }).parent().siblings().children(".submenu").slideUp("fast");
        }
        
    }).has(".submenu").children("a").append("<b class='icon-angle-down'></b>");//对有子菜单的menu添加下拉图标
    
  
   
    //左侧菜单收缩展开
    $("#menu-collapse").on('click', function (event, isauto) {
        if (isauto != null) {
            $(this).css("right", "-20px").children().removeClass().addClass("icon-double-angle-right");
            $(this).parents(".main-left").animate({ left: "-202px" }, 300, function () {
                menuCallBack();
            });
            $(this).data("open", "close");
            $("#main-right").css("margin-left", "0px")
        } else {
            if ($(this).data("open") == "open" ) {//收缩
			
                $(this).css("right","-20px").children().removeClass().addClass("icon-double-angle-right");
                $(this).parents(".main-left").animate({left:"-202px"}, 300, function(){
                    menuCallBack();
                });
                $(this).data("open","close");
                $("#main-right").css("margin-left", "0px")

            } else if ($(this).data("open") == "close") {//展开
                $(this).parents(".main-left").animate({left:"0px"}, 300, function(){
                    menuCallBack();
                });
                $(this).css("right","0px").children().removeClass().addClass("icon-double-angle-left");
                $(this).data("open","open");
                $("#main-right").css("margin-left","202px")
            }
        }
    });
    if ($(window).width() < 768) {
        $("#menu-collapse").trigger('click', ['yes'])
    }

    $(window).resize(function(){
        menuCallBack();
    });

    
	
    //
    $('.set-height').height($('#main-right').height());
    
    setRightHeigh();
    setLeftHeight();

    //页面滚动处理
    $(window).scroll(function () {
        if ($(window).scrollTop() >= 60) {
            if ($(window).height() > $("#main_left").height()) {
                $('#main_left').css({ position: 'fixed', top: '0' });
            } 
        } else {
                $('#main_left').css({ position: 'absolute', top: 'auto' });
        }
            
    })
    //高级查询
    if ($('#advSearch').length > 0) {
        //$('#SearchArea').css({ position: 'static' });
        //var form_id = $('#advSearch').attr('data-module');
        //$('#' + form_id).css({
        //    //top: '105px',
        //    //right: '30px',
        //    position: 'absolute',
        //    //display:'none'
        //})
        //$('#advSearch').on('click', function () {
        //    var form_id = $(this).attr('data-module');
        //    $('#' + form_id).css({
        //        position: 'absolute',
        //        display: 'inline-block'
        //    }).find('a#module_close').on('click', function () {
        //        $('#' + form_id).css({ display: 'none' })
        //    })
        //});

        $(document).delegate('#advSearch', 'click', function (e) {
            var form_id = $(this).attr('data-module');
            $('#' + form_id).css({
                position: 'absolute',
                display: 'inline-block'
            }).find('a#module_close').on('click', function () {
                $('#' + form_id).css({ display: 'none' })
            })
        });


        // $('#advSearch').floatModule();
	}
	
	if ($('.main-right').height()>$(window).height()) {
	    $('.form-footer').css({
	        'background': '#ddd'
            , 'position': 'fixed'
	    })
	}

	function menuCallBack() {  //浏览器窗口大小改变时重新计算表格宽度

	    window.kgridResize && window.kgridResize();

	}

	function setRightHeigh() {
	    if (($("#mymenu").height() + 50) > $("#main-right").height()) {
	        $("#main-right").height($("#mymenu").height() + 50)

	    } else {
	        $("#main-right").height('auto')
	    }

	}
	function setLeftHeight() {
	   /* if (($(window).height() - 60) > $("#main-right").height()) {
	        $("#main_left").height("auto")
	    } else {
	        $("#main_left").height($("#main-right").height() + 20);
	    }*/
	}
	if ($('#total_count').length > 0) {
	    setTotalCount();
	}
	

	function setTotalCount() {
	    $('#total_count').width(function () {
	        return $(this).next().outerWidth();
	    })
	    $('.scrollBar').scroll(function () {
	        var _top = $(this).scrollTop();
	        $('#total_count').css({
	            top: _top
	        })
	    })
	}

	$('.lockhead').each(function (idx, el) {
	    $(el).find('.sort-icon').css({
	        top: function () {
	                return ($(this).parent().outerHeight() - 17) / 2;
	             }
	    })
	})
    
	
});