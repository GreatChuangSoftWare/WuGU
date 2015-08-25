/**
* @Description TreeMenu
* @author V.Yao  
* @date 2014-11-17
* @version 1.1.0.2
**/

; (function () {
    $.fn.myTree = function (options) {

        $.fn.myTree.defaults = {
            obj: this
			, subClass: 'sub'
        };
        var o = $.extend({}, $.fn.myTree.defaults, options);
        var $obj = o.obj;
        var $openDOM = $('<i class="glyphicon glyphicon-folder-open"></i>');
        var $closeDOM = $('<i class="glyphicon glyphicon-folder-close"></i>');

        $obj.find('ul>li').first().addClass('');

        //$obj.find('ul.' + o.subClass).first().prev('a').prepend($openDOM);
        //$obj.find('ul.' + o.subClass).first().find('ul.sub').hide().prev('a').each(function (idx, n) {
        //    $(n).text(function (i, s) {
        //        return $.trim(s);
        //    })
        //}).prepend($closeDOM);


        //为展开图标绑定事件
        $obj.find('ul>li>a>i').on('click', function (event) {
            if ($(this).parents('li:eq(0)').attr('class') == 'active') {
                //如果子节点打开则隐藏
                $(this).parent().next('ul.sub').hide('fast', function () {
                    $(this).parents('li:eq(0)').removeClass('active').children('a').children('i.glyphicon-folder-open').removeClass('glyphicon-folder-open').addClass('glyphicon-folder-close');
                })
            } else {
                //如果子节点隐藏则打开 
                $(this).parent().next('ul.sub').show('fast', function () {

                    $(this).parents('li:eq(0)').addClass('active').children('a').children('i.glyphicon-folder-close').removeClass('glyphicon-folder-close').addClass('glyphicon-folder-open');
                })
            }
            event.stopPropagation();
        })

        //为超链接添加事件
        $obj.find('ul>li>a').on('click', function () {
            $('a.selected').removeClass('selected');
            $(this).addClass('selected')
        })
    }
})(jQuery)